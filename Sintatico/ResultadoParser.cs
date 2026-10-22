namespace Sintatico
{
    public class ResultadoParser
    {
        public Node.Documento Arvore { get; set; }
        public List<ErroSintatico> Erros { get; } = new();
        public bool Sucesso => Erros.Count == 0;
    }
}
