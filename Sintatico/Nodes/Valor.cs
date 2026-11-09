namespace Sintatico.Nodes
{

    public class Valor(string identificador, Literal? literal = null) : Node
    {
        public Literal? ValorLiteral { get; set; } = literal ?? null;
        public string Identificador { get; set; } = identificador;
        public Acesso Acesso { get; set; } = new();
        public Construtor? Construtor { get; set; } = null;
        public bool IsLiteral => literal != null;

        public override string Descricao => IsLiteral ? $"<valor literal> {literal.Valor}" :
            ((Construtor != null ? $"<construtor>" : "") +
            ((Acesso != null) ? $"<acesso> {Identificador}" : "")) +
             $"<valor> {Identificador}:";
    }
}