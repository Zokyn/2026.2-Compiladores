namespace Sintatico.Nodes
{
    public class Declaracoes(int? numero = null) : Node
    {
        private static int _count;
        private int _numeroId { get; set; } = numero ?? _count++;
        public List<Declaracao> Declaracao { get; } = [];
        public override string Descricao => $"(Declaracoes de Dados {_numeroId})";
        public override IEnumerable<Node> Filhos => Declaracao;

    }
    // <declaracao> ::= "dados" IDENTIFICADOR <dados>
    public class Declaracao(string identificador) : Node
    {
        public string Identificador { get; set; } = identificador;
        public Node Definicao { get; set; } = new Invalido();
        public override string Descricao => $"(Dados) {Identificador}";
        public override IEnumerable<Node> Filhos => [Definicao];
    }
    // <dados> ::= <conjunto_dados> | <construtor> | ATRIBUIDOR <valor>

    // <itens_dados> ::= <item_dado> <mais_itens>
    // <mais_itens> ::= SEPARADOR <item_dado> <mais_itens> | <vazio>
    // <item_dado> ::= IDENTIFICADOR <resto_item_dado> | <literal>
    // <resto_item_dado> ::= ATRIBUIDOR <valor> | <construtor> | <mais_acesso>

    // <conjunto_dados> ::= ABRE_TUPLA <itens_dados> FECHA_TUPLA
    public class ConjuntoDados(int? numero = null) : Node
    {
        private static int _count;
        private int _numeroId = numero ?? _count++;
        public List<ListaDados> ListaDados { get; set; } = [];
        public override string Descricao => $"(Conjunto de Dados {_numeroId})";
    }

    public class ListaDados : Node
    {
        public List<ItemDado> Itens { get; } = new();
        public override string Descricao => "(Lista) [ ]";
        public override IEnumerable<Node> Filhos => Itens;
    }

    public class ItemDado(string identificador = "") : Node
    {
        public Node Resto { get; set; } = new Invalido(); // Atribuicao | Construtor | MaisAcesso
        public string Identificador { get; set; } = identificador;
        public override string Descricao => $"(Dado) {Identificador}";
    }

    // IDENTIFICADOR <bloco_construtor>, ex.: produto(nome: "Caneca")
    public class Instancia : Node
    {
        public string Nome { get; set; } = "";
        public Construtor Construtor { get; set; } = new();
        public override string Descricao => $"(Instância) {Nome}";
        public override IEnumerable<Node> Filhos => Construtor.Argumentos;
    }
}
