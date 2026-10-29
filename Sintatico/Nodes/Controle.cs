namespace Sintatico.Nodes
{
    // <iteracao> e <condicional> sempre terminam em <bloco_componente>
    public abstract class Controle : Node
    {
        public List<Node> Corpo { get; } = new();
    }

    // "para" IDENTIFICADOR "em" <colecao> <bloco_componente>
    public class ParaEm : Controle
    {
        public string Variavel { get; set; } = "";
        public Node Colecao { get; set; } = new Invalido();
        public override string Descricao => $"(Para) {Variavel} em";
        public override IEnumerable<Node> Filhos => Corpo.Prepend(Colecao);
    }

    // "para" IDENTIFICADOR "enquanto" <condicao> "sendo" <passo> <bloco_componente>
    public class ParaEnquanto : Controle
    {
        public string Variavel { get; set; } = "";
        public Node Condicao { get; set; } = new Invalido();
        public Node Passo { get; set; } = new Invalido();
        public override string Descricao => $"(Para) {Variavel} enquanto";
        public override IEnumerable<Node> Filhos => new[] { Condicao, Passo }.Concat(Corpo);
    }

    // "enquanto" <condicao> <bloco_componente>
    public class Enquanto : Controle
    {
        public Node Condicao { get; set; } = new Invalido();
        public override string Descricao => "(Enquanto)";
        public override IEnumerable<Node> Filhos => Corpo.Prepend(Condicao);
    }

    // <condicional> ::= "se" <condicao> <bloco_componente>
    public class Condicional : Controle
    {
        public Node Condicao { get; set; } = new Invalido();
        public override string Descricao => "(Se)";
        public override IEnumerable<Node> Filhos => Corpo.Prepend(Condicao);
    }

    // <passo> ::= IDENTIFICADOR INCREMENTO | IDENTIFICADOR DECREMENTO
    public class Incremento : Node
    {
        public string Variavel { get; set; } = "";
        public string Operador { get; set; } = "";
        public override string Descricao => $"(Passo) {Variavel}{Operador}";
    }
}
