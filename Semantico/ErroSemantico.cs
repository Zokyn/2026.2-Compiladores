namespace Semantico
{
    public class ErroSemantico(string mensagem, int linha, int coluna)
    {
        public string Mensagem { get; } = mensagem;
        public int Linha { get; } = linha;
        public int Coluna { get; } = coluna;
    }
}
