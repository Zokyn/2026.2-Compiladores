namespace Sintatico.Nodes
{
    // <componente> ::= COMPONENTE <nome_componente> <bloco_construtor> <bloco_componente_opcional>
    public class Componente : Node
    {
        public string Tipo { get; set; } = "";
        public string? Nome { get; set; }
        public Construtor Construtor { get; set; } = new();
        public List<Node> Corpo { get; } = new();
        public override string Descricao => $"<componente> ({Tipo})" + $" {Nome}" ?? "";
        public override IEnumerable<Node> Filhos => Corpo.Prepend(Construtor);
    }
}
