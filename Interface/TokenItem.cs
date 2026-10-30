using Lexico;

namespace Interface
{
    public sealed class TokenItem
    {
        public int Id { get; init; }
        public string Tipo { get; init; } = "";
        public string Lexema { get; init; } = "";
        public int Inicio { get; init; } = 0;
        public int Fim { get; init; } = 0;
        public int Linha { get; init; }
        public int Coluna { get; init; }
        public int Comprimento { get; init; }
        public string Categoria { get; init; } = "";

        public TokenType TipoEnum { get; init; }
    }
}
