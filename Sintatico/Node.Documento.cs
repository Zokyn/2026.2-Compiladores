namespace Sintatico.Node
{
    public class Documento : Node
    {
        public List<Node> Corpo { get; } = new();
        public override string Descricao => "Documento";
        public override IEnumerable<Node> Filhos => Corpo;
    }
}
