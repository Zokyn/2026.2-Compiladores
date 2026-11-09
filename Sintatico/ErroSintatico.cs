namespace Sintatico
{
    public class ErroSintatico : Exception
    {
        public string Titulo { get; }
        public string Mensagem { get; }
        public int Linha { get; }
        public int Coluna { get; }
        public int Posicao { get; }
        public ErroSintatico(string titulo, string mensagem, int linha, int coluna, int posicao)
            : base($"Erro sintático [{linha};{coluna}]: {titulo}" + (!string.IsNullOrEmpty(mensagem) ? $"\n\t{mensagem}": ""))
        {
            Titulo = titulo;
            Mensagem = mensagem;
            Linha = linha;
            Coluna = coluna;
            Posicao = posicao;
        }
        public override string ToString() => "Erro sintático" 
            + (string.IsNullOrEmpty(Titulo) ? $":{Titulo}" : "") 
            + $"\n[{Linha};{Coluna}]: {Mensagem}";
    }
}
