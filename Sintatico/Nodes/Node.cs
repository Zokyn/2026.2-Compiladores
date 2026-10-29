namespace Sintatico.Nodes
{
    public abstract class Node
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public abstract string Descricao { get; }
        public override string ToString() => Descricao;
        public virtual IEnumerable<Node> Filhos => Enumerable.Empty<Node>();
    }
}
