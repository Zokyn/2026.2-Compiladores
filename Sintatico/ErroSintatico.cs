namespace Sintatico
{
    public class ErroSintatico : Exception
    {
        public string Mensagem { get; }
        public int Linha { get; }
        public int Coluna { get; }
        public ErroSintatico(string mensagem, int linha, int coluna)
            : base($"Erro sintático [{linha};{coluna}]: {mensagem}")
        {
            Mensagem = mensagem;
            Linha = linha;
            Coluna = coluna;
        }
        public override string ToString() => $"Erro sintático [{Linha};{Coluna}]: {Mensagem}";
    }
}
