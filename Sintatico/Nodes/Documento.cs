namespace Sintatico.Nodes
{
    public class Documento : Node
    {
        public List<Node> Corpo { get; } = new();
        public override string Descricao => "(RAIZ) Documento";
        public override IEnumerable<Node> Filhos => Corpo;
    }
}
