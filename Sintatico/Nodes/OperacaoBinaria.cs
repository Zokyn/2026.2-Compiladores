namespace Sintatico.Nodes
{
    // <comparacao> com OPERADOR_RELACIONAL e <condicao> com OPERADOR_LOGICO
    public class OperacaoBinaria : Node
    {
        public string Operador { get; set; } = "";
        public Node Esquerda { get; set; } = new Invalido();
        public Node Direita { get; set; } = new Invalido();
        public override string Descricao => $"(Operação) {Operador}";
        public override IEnumerable<Node> Filhos => [Esquerda, Direita];
    }
}
