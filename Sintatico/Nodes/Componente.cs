namespace Sintatico.Nodes
{
    // <componente> ::= COMPONENTE <nome_componente> <bloco_construtor> <bloco_componente_opcional>
    public class Componente : Node
    {
        public string Tipo { get; set; } = "";
        public string? Nome { get; set; }
        public Construtor Construtor { get; set; } = new();
        public List<Node> Corpo { get; } = new();
        public override string Descricao => Nome is null ? $"(Componente) {Tipo}" : $"(Componente) {Tipo} {Nome}";
        public override IEnumerable<Node> Filhos => Corpo.Prepend(Construtor);
    }

    // <bloco_construtor> ::= ABRE_CONSTRUTOR <argumentos> FECHA_CONSTRUTOR
    public class Construtor : Node
    {
        public List<Node> Argumentos { get; } = new();
        public override string Descricao => "(Construtor) ( )";
        public override IEnumerable<Node> Filhos => Argumentos;
    }
}
