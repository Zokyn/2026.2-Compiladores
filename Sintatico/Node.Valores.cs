namespace Sintatico.Nodes
{
    // NUMERO_INTEIRO | NUMERO_REAL
    public class Numero : Node
    {
        public string Valor { get; set; } = "";
        public bool Real { get; set; }
        public override string Descricao => Real ? $"(Real) {Valor}" : $"(Inteiro) {Valor}";
    }

    // <acesso> ::= IDENTIFICADOR <resto_acesso>, ex.: turma[0].nome
    public class Acesso : Node
    {
        public string Nome { get; set; } = "";
        public List<string> Caminho { get; } = new();
        public override string Descricao => $"(Acesso) {Nome}{string.Concat(Caminho)}";
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

    // Ocupa o lugar de um nó que não pôde ser reconhecido por erro sintático
    public class Invalido : Node
    {
        public override string Descricao => "(Inválido)";
    }
}
