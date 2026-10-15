namespace Sintatico.Nodes
{
    public class NodeElemento : Node
    {
        public string Nome { get; set; }
        public List<NodeAtributo> Atributos { get; } = new();
        public List<Node> Corpo { get; } = new();
        public override string Descricao => "Elemento";
        public override IEnumerable<Node> Filhos => Corpo;
    }
}
