using Lexico;
using Sintatico.Nodes;
namespace Sintatico
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _posicao;
        private readonly ResultadoParser _resultado = new();

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
        private bool FimDeTokens => _posicao >= _tokens.Count;

        private Token TokenAtual =>
            FimDeTokens ? _tokens[^1] : _tokens[_posicao];

        private Token EspiarToken(int n = 1) =>
            _posicao + n < _tokens.Count ? _tokens[_posicao + n] : _tokens[^1];

        private Token AvancarToken()
        {
            var atual = TokenAtual;
            if (!FimDeTokens) _posicao++;
            return atual;
        }

        private bool Verificar(TokenType tipo) => !FimDeTokens && TokenAtual.Tipo == tipo;

        private bool Aceitar(TokenType tipo)
        {
            if (!Verificar(tipo)) return false;
            AvancarToken();
            return true;
        }

        private Token Consumir(TokenType esperado, string? contexto = null)
        {
            if (Verificar(esperado)) return AvancarToken();

            var token = TokenAtual;
            throw new ErroSintatico(
                $"Esperado '{esperado}' mas encontrado " +
                $"{token.Tipo} ('{token.Valor}')" +
                (contexto != null ? $"no contexto {contexto ?? "desconhecido"}" : ""),
                token.Posicao.Item1, token.Posicao.Item2);
        }

        private void Sincronizar(params TokenType[] pontos)
        {
            while (!FimDeTokens && !pontos.Contains(TokenAtual.Tipo))
            {
                AvancarToken();
            }
        }

        // Regras de Produção da Gramática
        // documento -> item* EOF
        private NodeDocumento Documento()
        {
            var documento = new NodeDocumento();
            while(!FimDeTokens && !Verificar(TokenType.FimDeArquivo))
            {
                try
                {
                    documento.Corpo.Add(Item());
                } catch (ErroSintatico e)
                {
                    _resultado.Erros.Add(e);
                    throw new ErroSintatico( $"Erro Sintático: {e.Message}", e.Linha, e.Coluna);
                
                    Sincronizar(TokenType.AbreBloco, TokenType.FechaBloco);
                }
            }
            return documento;
        }
        //item -> elemento | controle | componente 
        private Node Item()
        {
            return TokenAtual.Tipo switch
            {
                TokenType.AbreBloco => Elemento(),
                TokenType.Controle => Elemento(),
                TokenType.Componente => Elemento(),
                _ => throw new ErroSintatico($"Item inesperado: " +
                    $"[{(TokenAtual.Tipo != TokenType.Erro ? TokenAtual.Tipo : "DESCONHECIDO")}]'{TokenAtual.Valor}'", TokenAtual.Posicao.Item1, TokenAtual.Posicao.Item2)
            };
        }

        //elemento -> '{' NOME atributo* '}'
        private NodeElemento Elemento()
        {
            Token abertura = Consumir(TokenType.AbreBloco);
            Token nome = Consumir(TokenType.Identificador, "Nome do Elemento");
            NodeElemento elemento = new NodeElemento(){ 
                Nome = nome.Lexema, 
                Linha = abertura.Linha, 
                Coluna = abertura.Coluna 
            };

            while (Verificar(TokenType.Identificador))
                elemento.Atributos.Add(Atributo());

            Consumir(TokenType.FechaBloco);

            while (!Verificar(TokenType.AbreBloco) && !FimDeTokens)
                elemento.Corpo.Add(Item());

            Consumir(TokenType.FechaBloco);

            var nomeFecha = Consumir(TokenType.Identificador);
            if (nomeFecha.Lexema != elemento.Nome)
                throw new ErroSintatico(
                    $"Elemento '{elemento.Nome}' fechada com '{nomeFecha.Lexema}'",
                    nomeFecha.Linha, nomeFecha.Coluna);

            Consumir(TokenType.FechaBloco);
            return elemento;
        }
        //atributo -> NOME ':' valor
        private NodeAtributo Atributo()
        {
            Token nome = Consumir(TokenType.Identificador);
            Consumir(TokenType.Atribuidor, "atributo");
            return new NodeAtributo
            {
                Nome = nome.Lexema,
                Valor = Valor(),
                Linha = nome.Linha,
                Coluna = nome.Coluna
            };
        }

        private Node Valor() => Expressao();
        private NodeTexto Texto() => new NodeTexto { Valor = Consumir(TokenType.Texto).Lexema };
        private Node Expressao() => new NodeExpressao()
        {
            Valor = AvancarToken().Lexema,
        };
    }
}
