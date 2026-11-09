
namespace Sintatico.Nodes
{
    // <bloco_construtor> ::= ABRE_CONSTRUTOR <argumentos> FECHA_CONSTRUTOR
    public class Construtor(): Node
    {
        public List<Argumento> Argumentos { get; set; } = new();
        public override IEnumerable<Node> Filhos => Argumentos;
        public override string Descricao => $"<construtor> ( ... )";
    }
}
