using Lexico;

namespace Sintatico.Nodes
{
    //<argumento> ::= IDENTIFICADOR <resto_argumento> | <literal> 
    public class Argumento() : Node
    {
        public string Nome { get; set; } = "";
        public virtual List<Node> Resto { get; set; } = new();
        public override IEnumerable<Node> Filhos => Resto;
        public override string Descricao => 
            string.IsNullOrEmpty(Nome) ? "<argumento>" : $"<argumento> {Nome}";
    }
    public class ArgumentoLiteral(Literal literal) : Argumento()
    {
        public override List<Node> Resto { get; set; } = [literal];
        public override string Descricao => $"<literal> {literal.Valor}";
    }
    // <resto_argumento> ::= ATRIBUIDOR <valor> | <construtor> | <mais_acesso>
    public class ArgumentoValor(string identificador, Valor valor) : Node
    {
        public Valor Valor { get; } = valor;
        public override string Descricao => $"<valor> {Valor.Descricao}";
    }
    
}
