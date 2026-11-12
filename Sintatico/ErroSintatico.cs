using Lexico;

namespace Sintatico
{
    public class ErroSintatico : Exception
    {
        public string Titulo { get; }
        public string Mensagem { get; }
        public int Linha { get; }
        public int Coluna { get; }
        public int Posicao { get; }
        public ErroSintatico(string titulo, string mensagem, int? linha, int? coluna, int? posicao)
            : base($"Erro sintático [{linha};{coluna}]: {titulo}" + (!string.IsNullOrEmpty(mensagem) ? $"\n\t{mensagem}": ""))
        {
            Titulo = titulo;
            Mensagem = mensagem;
            if (linha.HasValue)
                Linha = linha.Value;
            if (coluna.HasValue)
                Coluna = coluna.Value;
            if (posicao.HasValue)
                Posicao = posicao.Value;
        }
    }
    public class ErroTokenNaoEncontrado : ErroSintatico
    {
        public TokenType TipoEsperado { get; }
        public Token? TokenEncontrado { get; }
        public ErroTokenNaoEncontrado(TokenType tipoEsperado, Token? tokenEncontrado, int linha, int coluna, int posicao)
            : base("Token Inesperado", $"Token esperado '{tipoEsperado}', mas encontrado '{tokenEncontrado?.Tipo}'", linha, coluna, posicao)
        {
            TipoEsperado = tipoEsperado;
            TokenEncontrado = tokenEncontrado;
        }
    }
    public class ErroArvoreSintaticaNula : ErroSintatico
    {
        public ErroArvoreSintaticaNula()
            : base("Árvore Sintática Nula", "A árvore de análise sintática é nula.", -1, -1, 0) {}
    }
}
