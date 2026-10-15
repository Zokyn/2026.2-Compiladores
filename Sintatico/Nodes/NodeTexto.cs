namespace Sintatico.Nodes
{
    public class NodeTexto : Node
    {
        public string Valor { get; set; }
        public override string Descricao => $"(Valor) \"{Valor}\"";
    }

    public class NodeExpressao : Node
    {
        public string Valor { get; set; }
        public override string Descricao => $"(Expressão) {Valor}";
    }
}
