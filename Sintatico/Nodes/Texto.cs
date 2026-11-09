using Lexico;

namespace Sintatico.Nodes
{
    public class Texto(string valor) : Node
    {
        public Literal Literal { get; } = new Literal(valor, TokenType.TextoLiteral);
        public override string Descricao => $"(Texto) \"{Literal.Valor}\"";
    }
}
