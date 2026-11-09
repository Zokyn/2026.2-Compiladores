using Lexico;

namespace Sintatico.Nodes
{
    // NUMERO_INTEIRO | NUMERO_REAL
    public class Numero(bool isReal, string valor) : Node
    {
        public bool Real { get; set; } = isReal;
        public Literal Literal { get; } = new(valor, isReal ? TokenType.NumeroReal : TokenType.NumeroInteiro);
        public override string Descricao => Real ? $"(Número Real) {Literal.Valor}f" : $"(Número Inteiro) {Literal.Valor}";
    }
}
