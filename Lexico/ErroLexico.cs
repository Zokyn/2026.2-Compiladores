namespace Lexico
{
    public class ErroLexico : Exception
    {
        public int Linha { get; }
        public int Coluna { get; }
        public ErroLexico(string mensagem, int linha, int coluna)
            : base($"Erro léxico [{linha};{coluna}]: {mensagem}")
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
    public class ErroCaractereInesperado : ErroLexico
    {
        public char Caractere { get; }

        public ErroCaractereInesperado(char caractere, int linha, int coluna)
            : base($"Caractere inesperado '{caractere}'", linha, coluna)
        {
            Caractere = caractere;
        }
    }
}
