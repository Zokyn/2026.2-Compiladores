using Lexico;
namespace Sintatico
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _posicao;
        private readonly ResultadoParser _resultado = new();
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
            // TODO: Insira Regra de Produção 
            // do símbolo de entrada da linguagem
            _resultado.Arvore = null;
            return _resultado;
        }


        private Token EspiarToken(int n = 1) =>
            _posicao + n < _tokens.Count ? _tokens[_posicao + n] : _tokens[^1];

        private Token AvancarToken()
        {
            var atual = TokenAtual;
            if (!FimDeTokens) _posicao++;
            return atual;
        }

        private bool Verificar(TokenType tipo) => !FimDeTokens && TokenAtual.Tipo == tipo;

        private Token Consumir(TokenType esperado, string? contexto = null)
        {
            if (Verificar(esperado)) return AvancarToken();

            var e = new ErroSintatico(
                $"Esperado '{esperado}' mas encontrado " +
                $"{TokenAtual.Tipo} ('{TokenAtual.Valor}')" +
                (contexto != null ? $"no contexto {contexto ?? "desconhecido"}" : ""),
                TokenAtual.Posicao.Item1, TokenAtual.Posicao.Item2);
            _resultado.Erros.Add(e);
            return TokenAtual;
        }

        private void AvancarAte(params TokenType[] pontos)
        {
            while (!FimDeTokens && !pontos.Contains(TokenAtual.Tipo))
            {
                AvancarToken();
            }
        }

    }
}
