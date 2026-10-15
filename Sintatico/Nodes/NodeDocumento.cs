namespace Sintatico.Nodes
{
    public class NodeDocumento : Node
    {
        public List<Node> Corpo { get; } = new();
        public override string Descricao => "Documento";
        public override IEnumerable<Node> Filhos => Corpo;
    }
}
