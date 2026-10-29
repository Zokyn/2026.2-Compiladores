namespace Sintatico.Nodes
{
    // NUMERO_INTEIRO | NUMERO_REAL
    public class Numero : Node
    {
        public string Valor { get; set; } = "";
        public bool Real { get; set; }
        public override string Descricao => Real ? $"(Número Real) {Valor}f" : $"(Número Inteiro) {Valor}";
    }

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
