using Lexico;
using Sintatico.Nodes;

namespace Sintatico
{
    /// <summary>
    /// Analisador sintático descendente recursivo. Cada não-terminal de
    /// docs/regras-de-producao.bnf corresponde a um método deste parser.
    /// </summary>
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _posicao;
        private readonly ResultadoParser _resultado = new();
        /// <summary>
        /// Modo pânico: depois de um erro, os erros seguintes não são registrados
        /// até que um token esperado seja consumido, evitando erros em cascata.
        /// </summary>
        private bool _emPanico;
        private bool FimDeTokens => _posicao >= _tokens.Count;

        private Token TokenAtual =>
            FimDeTokens ? _tokens[^1] : _tokens[_posicao];
        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens.Where(t => t.Tipo != TokenType.EspacosVazios
                                    && t.Tipo != TokenType.Comentarios).ToList();
        }
        public ResultadoParser Parse()
        {
            _resultado.Arvore = Documento();
            return _resultado;
        }

        // ---------------------------------------------------------------- Documento

        // <documento>   ::= <declaracoes> <componente> FIM_DE_ARQUIVO
        // <declaracoes> ::= <declaracao> <declaracoes> | ε
        private Documento Documento()
        {
            var documento = Posicionar(new Documento(), TokenAtual);

            while (Verificar(TokenType.Dados))
            {
                documento.Corpo.Add(Declaracao());
                if (_emPanico) AvancarAte(TokenType.Dados, TokenType.Componente);
            }

            documento.Corpo.Add(Componente());

            if (!Verificar(TokenType.FimDeArquivo))
                RegistrarErro($"Esperado fim de arquivo após o componente raiz, mas encontrado {Descrever(TokenAtual)}");

            return documento;
        }

        // ---------------------------------------------------------------- Dados

        // <declaracao>      ::= DADOS IDENTIFICADOR <definicao_dados>
        // <definicao_dados> ::= <bloco_dados> | <lista_dados> | <bloco_construtor> | ATRIBUIDOR <valor>
        private Declaracao Declaracao()
        {
            var declaracao = Posicionar(new Declaracao(), Consumir(TokenType.Dados, "declaração de dados"));
            declaracao.Nome = Consumir(TokenType.Identificador, "nome da declaração de dados").Valor;

            switch (TokenAtual.Tipo)
            {
                case TokenType.AbreBloco:
                    declaracao.Definicao = BlocoDados();
                    break;
                case TokenType.AbreTupla:
                    declaracao.Definicao = ListaDados();
                    break;
                case TokenType.AbreConstrutor:
                    declaracao.Definicao = BlocoConstrutor();
                    break;
                case TokenType.Atribuidor:
                    AvancarToken();
                    declaracao.Definicao = Valor();
                    break;
                default:
                    RegistrarErro($"Esperado '{{', '[', '(' ou ':' após 'dados {declaracao.Nome}', mas encontrado {Descrever(TokenAtual)}");
                    break;
            }
            return declaracao;
        }

        // <bloco_dados> ::= ABRE_BLOCO <itens_dados> FECHA_BLOCO
        // <itens_dados> ::= <item_dados> <separador_opcional> <itens_dados> | ε
        private BlocoDados BlocoDados()
        {
            var bloco = Posicionar(new BlocoDados(), Consumir(TokenType.AbreBloco, "bloco de dados"));

            while (!Verificar(TokenType.FechaBloco) && !Verificar(TokenType.FimDeArquivo))
            {
                int inicio = _posicao;

                if (Verificar(TokenType.Identificador))
                    bloco.Itens.Add(ItemDados());
                else
                    RegistrarErro($"Esperado item de dados (identificador), mas encontrado {Descrever(TokenAtual)}");

                if (Verificar(TokenType.Separador)) AvancarToken();
                if (_posicao == inicio) Descartar();
            }

            Consumir(TokenType.FechaBloco, "bloco de dados");
            return bloco;
        }

        // <item_dados>       ::= IDENTIFICADOR <resto_item_dados>
        // <resto_item_dados> ::= <bloco_construtor> | ATRIBUIDOR <valor>
        private Node ItemDados()
        {
            var identificador = Consumir(TokenType.Identificador, "item de dados");

            if (Verificar(TokenType.Atribuidor))
            {
                AvancarToken();
                return Posicionar(new Atribuicao { Nome = identificador.Valor, Valor = Valor() }, identificador);
            }
            return Posicionar(new Instancia { Nome = identificador.Valor, Construtor = BlocoConstrutor() }, identificador);
        }

        // <lista_dados>      ::= ABRE_TUPLA <itens_lista> FECHA_TUPLA
        // <itens_lista>      ::= <item_lista> <mais_itens_lista> | ε
        // <mais_itens_lista> ::= SEPARADOR <item_lista> <mais_itens_lista> | ε
        private ListaDados ListaDados()
        {
            var lista = Posicionar(new ListaDados(), Consumir(TokenType.AbreTupla, "lista"));

            if (!Verificar(TokenType.FechaTupla))
            {
                lista.Itens.Add(ItemLista());
                while (Verificar(TokenType.Separador))
                {
                    AvancarToken();
                    lista.Itens.Add(ItemLista());
                }
            }

            ConsumirFechamento(TokenType.FechaTupla, "lista");
            return lista;
        }

        // <item_lista>       ::= IDENTIFICADOR <resto_item_lista> | <literal>
        // <resto_item_lista> ::= <bloco_construtor> | <resto_acesso>
        private Node ItemLista()
        {
            if (!Verificar(TokenType.Identificador)) return Literal();

            var identificador = AvancarToken();

            if (Verificar(TokenType.AbreConstrutor))
                return Posicionar(new Instancia { Nome = identificador.Valor, Construtor = BlocoConstrutor() }, identificador);

            return RestoAcesso(Posicionar(new Acesso { Nome = identificador.Valor }, identificador));
        }

        // ---------------------------------------------------------------- Componentes

        // <componente>                ::= COMPONENTE <nome_componente> <bloco_construtor> <bloco_componente_opcional>
        // <nome_componente>           ::= IDENTIFICADOR | ε
        // <bloco_componente_opcional> ::= <bloco_componente> | ε
        private Componente Componente()
        {
            var tipo = Consumir(TokenType.Componente, "componente");
            var componente = Posicionar(new Componente { Tipo = tipo.Valor }, tipo);

            if (Verificar(TokenType.Identificador))
                componente.Nome = AvancarToken().Valor;

            componente.Construtor = BlocoConstrutor();

            if (Verificar(TokenType.AbreBloco))
                BlocoComponente(componente.Corpo);

            return componente;
        }

        // <bloco_componente>   ::= ABRE_BLOCO <conteudo> FECHA_BLOCO
        // <conteudo>           ::= <elemento> <separador_opcional> <conteudo> | ε
        // <separador_opcional> ::= SEPARADOR | ε
        private void BlocoComponente(List<Node> corpo)
        {
            Consumir(TokenType.AbreBloco, "bloco de componente");

            while (!Verificar(TokenType.FechaBloco) && !Verificar(TokenType.FimDeArquivo))
            {
                int inicio = _posicao;

                var elemento = Elemento();
                if (elemento != null) corpo.Add(elemento);

                if (Verificar(TokenType.Separador)) AvancarToken();
                if (_posicao == inicio) Descartar();
            }

            Consumir(TokenType.FechaBloco, "bloco de componente");
        }

        // <elemento> ::= <componente> | <iteracao> | <condicional>
        private Node? Elemento()
        {
            if (Verificar(TokenType.Componente)) return Componente();
            if (VerificarControle("para") || VerificarControle("enquanto")) return Iteracao();
            if (VerificarControle("se")) return Condicional();

            RegistrarErro($"Esperado componente, 'para', 'enquanto' ou 'se', mas encontrado {Descrever(TokenAtual)}");
            return null;
        }

        // <bloco_construtor> ::= ABRE_CONSTRUTOR <argumentos> FECHA_CONSTRUTOR
        // <argumentos>       ::= <argumento> <mais_argumentos> | ε
        // <mais_argumentos>  ::= SEPARADOR <argumento> <mais_argumentos> | ε
        private Construtor BlocoConstrutor()
        {
            var construtor = Posicionar(new Construtor(), Consumir(TokenType.AbreConstrutor, "construtor"));

            if (!Verificar(TokenType.FechaConstrutor))
            {
                construtor.Argumentos.Add(Argumento());
                while (Verificar(TokenType.Separador))
                {
                    AvancarToken();
                    construtor.Argumentos.Add(Argumento());
                }
            }

            ConsumirFechamento(TokenType.FechaConstrutor, "construtor");
            return construtor;
        }

        // <argumento>       ::= IDENTIFICADOR <resto_argumento> | <literal>
        // <resto_argumento> ::= ATRIBUIDOR <valor> | <resto_acesso>
        private Node Argumento()
        {
            if (!Verificar(TokenType.Identificador)) return Literal();

            var identificador = AvancarToken();

            if (Verificar(TokenType.Atribuidor))
            {
                AvancarToken();
                return Posicionar(new Atribuicao { Nome = identificador.Valor, Valor = Valor() }, identificador);
            }
            return RestoAcesso(Posicionar(new Acesso { Nome = identificador.Valor }, identificador));
        }

        // ---------------------------------------------------------------- Controle

        // <iteracao>   ::= "para" IDENTIFICADOR <resto_para> <bloco_componente>
        //                | "enquanto" <condicao> <bloco_componente>
        // <resto_para> ::= "em" <colecao> | "enquanto" <condicao> "sendo" <passo>
        private Controle Iteracao()
        {
            if (VerificarControle("enquanto"))
            {
                var enquanto = Posicionar(new Enquanto(), AvancarToken());
                enquanto.Condicao = Condicao();
                BlocoComponente(enquanto.Corpo);
                return enquanto;
            }

            var para = ConsumirControle("para");
            var variavel = Consumir(TokenType.Identificador, "variável do 'para'").Valor;
            Controle iteracao;

            if (VerificarControle("enquanto"))
            {
                AvancarToken();
                var paraEnquanto = Posicionar(new ParaEnquanto { Variavel = variavel, Condicao = Condicao() }, para);
                ConsumirControle("sendo");
                paraEnquanto.Passo = Passo();
                iteracao = paraEnquanto;
            }
            else
            {
                ConsumirControle("em");
                iteracao = Posicionar(new ParaEm { Variavel = variavel, Colecao = Colecao() }, para);
            }

            BlocoComponente(iteracao.Corpo);
            return iteracao;
        }

        // <colecao> ::= <acesso> | <lista_dados>
        private Node Colecao() =>
            Verificar(TokenType.AbreTupla) ? ListaDados() : Acesso();

        // <passo>       ::= IDENTIFICADOR <resto_passo>
        // <resto_passo> ::= ATRIBUIDOR <valor> | INCREMENTO | DECREMENTO
        private Node Passo()
        {
            var identificador = Consumir(TokenType.Identificador, "passo do 'para'");

            if (Verificar(TokenType.Atribuidor))
            {
                AvancarToken();
                return Posicionar(new Atribuicao { Nome = identificador.Valor, Valor = Valor() }, identificador);
            }
            if (Verificar(TokenType.Incremento) || Verificar(TokenType.Decremento))
                return Posicionar(new Incremento { Variavel = identificador.Valor, Operador = AvancarToken().Valor }, identificador);

            RegistrarErro($"Esperado ':', '++' ou '--' no passo do 'para', mas encontrado {Descrever(TokenAtual)}");
            return Posicionar(new Invalido(), TokenAtual);
        }

        // <condicional> ::= "se" <condicao> <bloco_componente>
        private Condicional Condicional()
        {
            var condicional = Posicionar(new Condicional(), ConsumirControle("se"));
            condicional.Condicao = Condicao();
            BlocoComponente(condicional.Corpo);
            return condicional;
        }

        // <condicao>      ::= <comparacao> <mais_condicao>
        // <mais_condicao> ::= OPERADOR_LOGICO <comparacao> <mais_condicao> | ε
        private Node Condicao()
        {
            var esquerda = Comparacao();

            while (Verificar(TokenType.OperadorLogico))
            {
                var operador = AvancarToken();
                esquerda = Posicionar(new OperacaoBinaria { Operador = operador.Valor, Esquerda = esquerda, Direita = Comparacao() }, operador);
            }
            return esquerda;
        }

        // <comparacao>       ::= <valor> <resto_comparacao>
        // <resto_comparacao> ::= OPERADOR_RELACIONAL <valor> | ε
        private Node Comparacao()
        {
            var esquerda = Valor();

            if (!Verificar(TokenType.OperadorRelacional)) return esquerda;

            var operador = AvancarToken();
            return Posicionar(new OperacaoBinaria { Operador = operador.Valor, Esquerda = esquerda, Direita = Valor() }, operador);
        }

        // ---------------------------------------------------------------- Valores

        // <valor> ::= <acesso> | <literal>
        private Node Valor() =>
            Verificar(TokenType.Identificador) ? Acesso() : Literal();

        // <acesso> ::= IDENTIFICADOR <resto_acesso>
        private Acesso Acesso()
        {
            var identificador = Consumir(TokenType.Identificador, "acesso");
            return RestoAcesso(Posicionar(new Acesso { Nome = identificador.Valor }, identificador));
        }

        // <resto_acesso> ::= ACESSO IDENTIFICADOR <resto_acesso>
        //                  | ABRE_TUPLA NUMERO_INTEIRO FECHA_TUPLA <resto_acesso>
        //                  | ε
        private Acesso RestoAcesso(Acesso acesso)
        {
            while (true)
            {
                if (Verificar(TokenType.Acesso))
                {
                    AvancarToken();
                    acesso.Caminho.Add("." + Consumir(TokenType.Identificador, "acesso").Valor);
                }
                else if (Verificar(TokenType.AbreTupla))
                {
                    AvancarToken();
                    acesso.Caminho.Add($"[{Consumir(TokenType.NumeroInteiro, "índice").Valor}]");
                    ConsumirFechamento(TokenType.FechaTupla, "índice");
                }
                else return acesso;
            }
        }

        // <literal> ::= TEXTO_LITERAL | NUMERO_INTEIRO | NUMERO_REAL
        private Node Literal()
        {
            var token = TokenAtual;
            switch (token.Tipo)
            {
                case TokenType.TextoLiteral:
                    AvancarToken();
                    return Posicionar(new Texto { Valor = token.Valor }, token);
                case TokenType.NumeroInteiro:
                case TokenType.NumeroReal:
                    AvancarToken();
                    return Posicionar(new Numero { Valor = token.Valor, Real = token.Tipo == TokenType.NumeroReal }, token);
                default:
                    RegistrarErro($"Esperado valor (identificador, texto ou número), mas encontrado {Descrever(token)}");
                    return Posicionar(new Invalido(), token);
            }
        }

        // ---------------------------------------------------------------- Auxiliares

        private Token EspiarToken(int n = 1) =>
            _posicao + n < _tokens.Count ? _tokens[_posicao + n] : _tokens[^1];

        /// <summary>Consome o token atual, que era esperado pela gramática, e sai do modo pânico.</summary>
        private Token AvancarToken()
        {
            var atual = TokenAtual;
            if (!FimDeTokens) _posicao++;
            _emPanico = false;
            return atual;
        }

        /// <summary>Pula o token atual sem tratá-lo como reconhecido (recuperação de erro).</summary>
        private void Descartar()
        {
            if (!FimDeTokens && !Verificar(TokenType.FimDeArquivo)) _posicao++;
        }

        private bool Verificar(TokenType tipo) => !FimDeTokens && TokenAtual.Tipo == tipo;

        private bool VerificarControle(string palavra) =>
            Verificar(TokenType.Controle) && TokenAtual.Valor == palavra;

        private Token Consumir(TokenType esperado, string? contexto = null)
        {
            if (Verificar(esperado)) return AvancarToken();

            RegistrarErro($"Esperado {Nome(esperado)}" +
                (contexto != null ? $" em {contexto}" : "") +
                $", mas encontrado {Descrever(TokenAtual)}");
            return TokenAtual;
        }

        private Token ConsumirControle(string palavra)
        {
            if (VerificarControle(palavra)) return AvancarToken();

            RegistrarErro($"Esperado '{palavra}', mas encontrado {Descrever(TokenAtual)}");
            return TokenAtual;
        }

        /// <summary>
        /// Consome um delimitador de fechamento sem aninhamento, como ')' e ']'. Se não
        /// for encontrado, registra o erro e pula tokens até ele, ou até o início de outra estrutura.
        /// </summary>
        private void ConsumirFechamento(TokenType fechamento, string contexto)
        {
            if (!Verificar(fechamento))
            {
                Consumir(fechamento, contexto);
                AvancarAte(fechamento, TokenType.AbreBloco, TokenType.FechaBloco, TokenType.Componente, TokenType.Dados);
            }
            if (Verificar(fechamento)) AvancarToken();
        }

        /// <summary>Pula tokens até um dos pontos de sincronização ou o fim de arquivo.</summary>
        private void AvancarAte(params TokenType[] pontos)
        {
            while (!FimDeTokens && !Verificar(TokenType.FimDeArquivo) && !pontos.Contains(TokenAtual.Tipo))
            {
                Descartar();
            }
        }

        private void RegistrarErro(string mensagem) => RegistrarErro(mensagem, TokenAtual);

        private void RegistrarErro(string mensagem, Token token)
        {
            if (_emPanico) return;
            _emPanico = true;
            _resultado.Erros.Add(new ErroSintatico(mensagem, token.Linha, token.Coluna));
        }

        private static T Posicionar<T>(T node, Token token) where T : Node
        {
            node.Linha = token.Linha;
            node.Coluna = token.Coluna;
            return node;
        }

        /// <summary>Nome legível de um tipo de token: o próprio símbolo para delimitadores, ex.: ')'.</summary>
        private static string Nome(TokenType tipo)
        {
            string exemplo = Utils.GetExampleByType(tipo);
            return exemplo.Length <= 2 ? $"'{exemplo}'" : Utils.GetNomeByType(tipo);
        }

        private static string Descrever(Token token) =>
            token.Tipo == TokenType.FimDeArquivo ? "fim de arquivo" : $"{Nome(token.Tipo)} '{token.Lexema}'";
    }
}
