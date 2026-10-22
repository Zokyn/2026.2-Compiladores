namespace Sintatico.Nodes
{
    // <declaracao> ::= DADOS IDENTIFICADOR <definicao_dados>
    public class Declaracao : Node
    {
        public string Nome { get; set; } = "";
        public Node Definicao { get; set; } = new Invalido();
        public override string Descricao => $"(Dados) {Nome}";
        public override IEnumerable<Node> Filhos => [Definicao];
    }

    // <bloco_dados> ::= ABRE_BLOCO <itens_dados> FECHA_BLOCO
    public class BlocoDados : Node
    {
        public List<Node> Itens { get; } = new();
        public override string Descricao => "(Bloco) { }";
        public override IEnumerable<Node> Filhos => Itens;
    }

    // <lista_dados> ::= ABRE_TUPLA <itens_lista> FECHA_TUPLA
    public class ListaDados : Node
    {
        public List<Node> Itens { get; } = new();
        public override string Descricao => "(Lista) [ ]";
        public override IEnumerable<Node> Filhos => Itens;
    }

    // IDENTIFICADOR <bloco_construtor>, ex.: produto(nome: "Caneca")
    public class Instancia : Node
    {
        public string Nome { get; set; } = "";
        public Construtor Construtor { get; set; } = new();
        public override string Descricao => $"(Instância) {Nome}";
        public override IEnumerable<Node> Filhos => Construtor.Argumentos;
    }

    // IDENTIFICADOR ATRIBUIDOR <valor>, ex.: nome: "Caneca"
    public class Atribuicao : Node
    {
        public string Nome { get; set; } = "";
        public Node Valor { get; set; } = new Invalido();
        public override string Descricao => $"(Atribuição) {Nome}:";
        public override IEnumerable<Node> Filhos => [Valor];
    }
}
