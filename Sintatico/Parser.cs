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

        #region Documento
        // <documento>   ::= <declaracoes> <componente> FIM_DE_ARQUIVO
        private Documento Documento()
        {
            var documento = Posicionar(new Documento(), TokenAtual);

            documento.Corpo.Add(Declaracoes());
            if (_emPanico) AvancarAte(TokenType.Dados, TokenType.Componente);

            documento.Corpo.Add(Componente());

            if (!Verificar(TokenType.FimDeArquivo))
                RegistrarErro($"Esperado fim de arquivo após o componente raiz, mas encontrado {Descrever(TokenAtual)}");

            return documento;
        }

        #region Dados
        // <declaracoes> ::= <declaracao> <declaracoes> | ε 
        // <declaracao>     ::= DADOS IDENTIFICADOR <dados> | ε
        // <dados>          ::= <conjunto_dados> | <construtor> | ATRIBUIDOR <valor>
        private Node Declaracoes()
        {
            var declaracoes = Posicionar(new Declaracoes(), Consumir(TokenType.Dados, "declaração de dados"));

            if (!Verificar(TokenType.Identificador)) return new Invalido();

            declaracoes.Declaracao.Add(Declaracao());

            return declaracoes;
        }

        private Declaracao Declaracao()
        {
            var declaracao = new Declaracao(Consumir(TokenType.Identificador, "identificador de dados").Valor);

            if (Verificar(TokenType.AbreTupla))
            {
                declaracao.Definicao = ConjuntoDados();

            }
            if (Verificar(TokenType.AbreConstrutor))
            {
                declaracao.Definicao = Construtor();
            }
            if (Verificar(TokenType.Atribuidor))
            {
                declaracao.Definicao = new Atribuicao { Nome = declaracao.Identificador, Valor = Literal() };
            }
            else
            {
                RegistrarErro("Definição de Dados Incompleta", "Esperado bloco de dados, construtor ou atribuição, mas encontrado " +
                    Descrever(TokenAtual));
            }

            return declaracao;
        }

        /*
         * <conjunto_dados>            ::= ABRE_TUPLA <itens_dados> FECHA_TUPLA
         * <itens_dados>               ::= <item_dado> <mais_itens>
         * <mais_itens>                ::= SEPARADOR <item_dado> <mais_itens> | ε
         * <item_dado>                 ::= IDENTIFICADOR <resto_item_dado> | <literal>
         */
        private ConjuntoDados ConjuntoDados()
        {
            var conjunto = Posicionar(new ConjuntoDados(), Consumir(TokenType.AbreTupla, "bloco de dados"));

            while (!Verificar(TokenType.FechaTupla) && !Verificar(TokenType.FimDeArquivo))
            {
                int inicio = _posicao;

                if (Verificar(TokenType.Identificador))
                    conjunto.Itens.Add(ItemDado());
                else
                    RegistrarErro("Esperado item de dados", $"Esperado item de dados (identificador), mas encontrado {Descrever(TokenAtual)}");

                if (Verificar(TokenType.Separador)) AvancarToken();
                if (_posicao == inicio) Descartar();
            }

            Consumir(TokenType.FechaTupla, "bloco de dados");
            return conjunto;
        }

        // <item_dado>                  ::= IDENTIFICADOR <resto_item_dado> | <literal>
        // <resto_item_dados>           ::= ATRIBUIDOR <valor> | <construtor> | <mais_acesso>
        private ItemDado ItemDado()
        {
            var token = Consumir(TokenType.Identificador, "item de dados");
            var identificador = token.Valor;
            var item = new ItemDado(identificador);

            if (Verificar(TokenType.Atribuidor))
            {
                AvancarToken();
                item.Resto = Posicionar(new Atribuicao { Nome = identificador, Valor = Valor() }, token);
            }
            else if (Verificar(TokenType.AbreConstrutor))
            {
                item.Resto = Posicionar(Construtor(), token);
            }
            
            return item;
        }
        #endregion
        #region Construtor
        // <bloco_construtor> ::= ABRE_CONSTRUTOR <argumentos> FECHA_CONSTRUTOR
        // <argumentos>       ::= <argumento> <mais_argumentos> | ε
        // <mais_argumentos>  ::= SEPARADOR <argumento> <mais_argumentos> | ε
        private Construtor Construtor()
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

        #region Argumento
        // <argumento>       ::= IDENTIFICADOR <resto_argumento> | <literal>
        // <resto_argumento> ::= ATRIBUIDOR <valor> | <construtor> | <mais_acesso>
        private Node Argumento()
        {
            if (!Verificar(TokenType.Identificador)) return Literal();
            else
            {
                var token = AvancarToken();

                if (Verificar(TokenType.Atribuidor))
                {
                    AvancarToken();
                    return Posicionar(new Atribuicao { Nome = token.Valor, Valor = Valor() }, token);
                }
                if (Verificar(TokenType.AbreConstrutor))
                {
                    return Posicionar(Construtor(), token);
                }
                if (Verificar(TokenType.Acesso))
                {
                    return Acesso();
                }
            }

            RegistrarErro("Resto de argumento inválido.", $"Resto de argumento inválido. Esperado Atribuição, " +
                $"Construtor ou Acesso, mas encontrado {Descrever(TokenAtual)}");
            return new Invalido();
        }
        #endregion

        #endregion
        #region Componente
        // <componente>                ::= COMPONENTE <nome_componente> <bloco_construtor> <bloco_componente_opcional>
        // <nome_componente>           ::= IDENTIFICADOR | ε
        // <bloco_componente_opcional> ::= <bloco_componente> | ε
        private Componente Componente()
        {
            var tipo = Consumir(TokenType.Componente, "componente");
            var componente = Posicionar(new Componente { Tipo = tipo.Valor }, tipo);

            if (Verificar(TokenType.Identificador))
                componente.Nome = AvancarToken().Valor;

            componente.Construtor = Construtor();

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

            RegistrarErro("Esperado componente ou controle.", $"Esperado componente, 'para', 'enquanto' ou 'se', " +
                $"mas encontrado {Descrever(TokenAtual)}");
            return null;
        }
        #endregion

        #region Controle
        #region Iteracao
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
            Verificar(TokenType.AbreTupla) ? ConjuntoDados() : Acesso();

        // <passo>       ::= IDENTIFICADOR <resto_passo>
        // <resto_passo> ::= ATRIBUIDOR <valor> | INCREMENTO | DECREMENTO
        private Node Passo()
        {
            var token = Consumir(TokenType.Identificador, "passo do 'para'");

            if (Verificar(TokenType.Atribuidor))
            {
                AvancarToken();
                return Posicionar(new Atribuicao { Nome = token.Valor, Valor = Valor() }, token);
            }
            if (Verificar(TokenType.Incremento) || Verificar(TokenType.Decremento))
                return Posicionar(new Incremento { Variavel = token.Valor, Operador = AvancarToken().Valor }, token);

            RegistrarErro($"Esperado ':', '++' ou '--' no passo do 'para', mas encontrado {Descrever(TokenAtual)}");
            return Posicionar(new Invalido(), TokenAtual);
        }
        #endregion

        #region Condicional
        // <condicional> ::= "se" <condicao> <bloco_componente>
        private Condicional Condicional()
        {
            var condicional = Posicionar(new Condicional(), ConsumirControle("se"));
            condicional.Condicao = Condicao();
            BlocoComponente(condicional.Corpo);
            return condicional;
        }
        #region Condicao
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
        #endregion
        #endregion

        #region Comparacao
        // <comparacao>       ::= <valor> <resto_comparacao>
        // <resto_comparacao> ::= OPERADOR_RELACIONAL <valor> | ε
        private Node Comparacao()
        {
            Node esquerda = Valor();

            if (!Verificar(TokenType.OperadorRelacional)) return esquerda;

            Token operador = AvancarToken();
            return Posicionar(new OperacaoBinaria { Operador = operador.Valor, Esquerda = esquerda, Direita = Valor() }, operador);
        }
        #endregion
        #endregion

        #region Valor
        // <valor> ::= <literal> | <acesso> 
        private Node Valor() => Verificar(TokenType.Identificador) ? Acesso() : Literal();
        #region Acesso
        // <acesso> ::= IDENTIFICADOR <resto_acesso>
        private Acesso Acesso()
        {
            // 1. Lê identificador base, considerado "nome" do acesso.
            // Ex.: carro.cor, esse identificador inicial seria "carro"
            Token token = Consumir(TokenType.Identificador, "acesso");

            Acesso nodeAtual = Posicionar(new Acesso { Identificador = token.Valor }, token);

            bool fimDaArvore = false; 
            while (!fimDaArvore)
            {
                if (Verificar(TokenType.Acesso)) // carro.cor 
                {
                    // Acesso a identificadores membro de um objeto, como atributos ou proprieadades
                    AvancarToken(); // Pula o símbolo terminal "." 

                    var membro = Consumir(TokenType.Identificador, "membro do objeto");

                    nodeAtual = new AcessoMembro
                    {
                        Origem = nodeAtual,
                        Membro = membro.Valor
                    };
                }
                else if (Verificar(TokenType.AbreTupla)) // carro.rodas[0]
                {
                    // Acesso a um número inteiro cercado por delimitadores de tuplas, acessando um
                    // item da colecao
                    AvancarToken(); // Pula o símbolo terminal "[" 

                    var indice = Consumir(TokenType.NumeroInteiro, "índice");

                    ConsumirFechamento(TokenType.FechaTupla, "índice");

                    nodeAtual = new AcessoIndice
                    {
                        Colecao = nodeAtual,
                        Indice = int.Parse(indice.Valor)
                    };
                }
                else
                {
                    fimDaArvore = true;
                }
            }
            return nodeAtual;
        }
        #endregion
        #region Literal
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
        #endregion
        #endregion Valor
        #endregion
        // ---------------------------------------------------------------- Auxiliares
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
        /// <summary>Verifica se o TokenAtual é to TokenType passado como parâmetro.</summary>
        private bool Verificar(TokenType tipo) => !FimDeTokens && TokenAtual.Tipo == tipo;
        /// <summary>Verifica se o TokenAtual é o controle passado como string palavra.</summary>
        private bool VerificarControle(string palavra) =>
            Verificar(TokenType.Controle) && TokenAtual.Valor == palavra;
        /// <summary>
        /// Consome o TokenAtual, comparando, com o método <see cref="Verificar(TokenType)"/>,
        /// com o TokenType esperado e se for verdade, avança o Token, caso contrário, 
        /// armazena o erro por meio do <see cref="RegistrarErro(string)"/>.
        /// </summary>
        /// <returns><see cref="TokenType"/> TokenAtual do Parser.</returns>
        private Token Consumir(TokenType esperado, string? contexto = null)
        {
            if (Verificar(esperado)) return AvancarToken();

            RegistrarErro($"Esperado {Nome(esperado)}" +
                (contexto != null ? $" em {contexto}" : "") +
                $", mas encontrado {Descrever(TokenAtual)}");
            return TokenAtual;
        }
        /// <summary>
        /// Método que chama o <see cref="VerificarControle(string)"/> e registra 
        /// o erro caso não seja validado nenhum controle.
        /// </summary>
        /// <returns><see cref="TokenType"/> TokenAtual do Parser.</returns>
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

        private void RegistrarErro(string mensagem) => RegistrarErro("", mensagem);
        private void RegistrarErro(string titulo, string mensagem) => RegistrarErro(titulo, mensagem, TokenAtual);
        /// <summary>
        /// Registra erro, armazenando a mensagem e um Token de entrada (por padrão, TokenAtual).
        /// Caso já esteja em modo Pânico, ele retorna, ignorando esse erro. Caso contrário, ativa
        /// o modo pânico, adiciona o erro a lista do ResultParser para ser listado na lista de erros
        /// posteriormente.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro gerado pela regra de produção do Parser.</param>
        /// <param name="token">Token lido que levantou o erro, com informações de linha e coluna.</param>
        private void RegistrarErro(string titulo, string mensagem, Token token)
        {
            if (_emPanico) return;
            token ??= TokenAtual; // if (token == null) token = TokenAtual; 

            _emPanico = true;
            
            _resultado.Erros.Add(new ErroSintatico(titulo, mensagem, token.Linha, token.Coluna));
        }
        /// <summary>Atribui, a partir do Node passado como parâmetro, a um Node, ou qualquer filho dele 
        /// a linha e coluna do token encontrado. </summary>
        /// <typeparam name="T">Tipo herdado de node.</typeparam>
        /// <param name="node">Node que terá campos atulizados.</param>
        /// <param name="token">Token com linha e coluna que foi encontrado.</param>
        /// <returns>Node ou classe derivada de Node com coluna de linha atualizados.</returns>
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
        /// <summary>Método que transforma o Token recebido em texto, iniciado por seu tipo e seguido do lexema.</summary>
        /// <param name="token">Token que será descrito</param>
        /// <returns>Contéudo de texto do tipo do Token e seu lexema.</returns>
        private static string Descrever(Token token) =>
            token.Tipo == TokenType.FimDeArquivo ? "fim de arquivo" : $"{Nome(token.Tipo)} '{token.Lexema}'";
    }
}
