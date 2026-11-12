using Lexico;
namespace Sintatico.Nodes
{
    public class Declaracoes() : Node
    {
        public List<Declaracao> ListaDeclaracao { get; set; } = [];
        public override string Descricao => $"<declaracoes> dados";
        public override IEnumerable<Node> Filhos => ListaDeclaracao;

    }
    // <declaracao> ::= "dados" IDENTIFICADOR <dados>
    // <dados> ::= <conjunto_dados> | <construtor> | ATRIBUIDOR <valor>
    public class Declaracao(string identificador) : Node
    {
        public string Identificador { get; set; } = identificador;
        public Node Definicao { get; set; } = new Invalido();
        public override string Descricao => $"<declaracao> {Identificador}";
        public override IEnumerable<Node> Filhos => [Definicao];
    }

    // <conjunto_dados> ::= ABRE_TUPLA <itens_dados> FECHA_TUPLA
    public class ConjuntoDados(int? numero = null) : Node
    {
        private static int _count;
        private int _numeroId = numero ?? _count++;
        public List<Dado> Itens { get; set; } = [];
        public override string Descricao => $"<conjunto_dados> [ ]";
        public override IEnumerable<Node> Filhos => Itens;

    }
    // <itens_dados> ::= <item_dado> <mais_itens>
    // <mais_itens> ::= SEPARADOR <item_dado> <mais_itens> | <vazio>
    // <resto_item_dado> ::= ATRIBUIDOR <valor> | <construtor> | <mais_acesso>
    // <item_dado> ::= IDENTIFICADOR <resto_item_dado> | <literal>
    public class Dado(string identificador) : Node
    {
        public Node Resto { get; set; } = new Invalido(); // Atribuicao | Construtor | MaisAcesso
        public string Identificador { get; set; } = identificador;
        public override string Descricao => $"<item_dado> {Identificador}";
        public override IEnumerable<Node> Filhos => [Resto];

    }
}
