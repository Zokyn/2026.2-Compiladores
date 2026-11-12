using Lexico;

namespace Sintatico.Nodes
{

    public class Valor(string? conteudo, TokenType tipo) : Node
    {
        public Literal? Conteudo { get; set; } = new Literal(conteudo ?? "", tipo) ?? null;
        public Acesso Acesso { get; set; } = new();
        public Construtor? Construtor { get; set; } = null;
        public override string Descricao => Conteudo != null ? $"<valor> {Conteudo.Descricao}" :
            ((Construtor != null ? $"{Construtor.Descricao}" : "") +
            ((Acesso != null) ? $"{Acesso.Descricao}" : "")) +
             $"<valor> {Conteudo}:";
    }
}