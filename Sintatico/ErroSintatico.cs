namespace Sintatico
{
    public class ErroSintatico : Exception
    {
        public string Titulo { get; }
        public string Mensagem { get; }
        public int Linha { get; }
        public int Coluna { get; }
        public ErroSintatico(string titulo, string mensagem, int linha, int coluna)
            : base($"Erro sintático [{linha};{coluna}]: {mensagem}")
        {
            Titulo = titulo;
            Mensagem = mensagem;
            Linha = linha;
            Coluna = coluna;
        }
        public override string ToString() => "Erro sintático" 
            + (string.IsNullOrEmpty(Titulo) ? $":{Titulo}" : "") 
            + $"\n[{Linha};{Coluna}]: {Mensagem}";
    }
}
