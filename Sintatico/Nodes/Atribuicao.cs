namespace Sintatico.Nodes
{
    // IDENTIFICADOR ATRIBUIDOR <valor>, ex.: nome: "Caneca"
    public class Atribuicao(string identificador = "") : Node
    {
        public string Nome { get; set; } = identificador;
        public Node Valor { get; set; } = new Invalido();
        public override string Descricao => $"(Atribuição) {Nome}:";
        public override IEnumerable<Node> Filhos => [Valor];
    }
}
