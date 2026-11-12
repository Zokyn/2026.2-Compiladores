namespace Semantico
{
    public class ErroSemantico : Exception
    {
        public string Titulo { get; }
        public string Mensagem { get; }
        public int Linha { get; }
        public int Coluna { get; }
        public ErroSemantico(string titulo, string mensagem, int linha, int coluna)
            : base($"Erro semântico [{linha};{coluna}]: {mensagem}")
        {
            Titulo = titulo;
            Mensagem = mensagem;
            Linha = linha;
            Coluna = coluna;
        }
    }
}
