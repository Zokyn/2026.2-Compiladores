using Lexico;

namespace Sintatico.Nodes
{
    public class Literal(string valor, TokenType tipo) : Node
    {
        public TokenType Tipo { get; } = tipo | TokenType.TextoLiteral | TokenType.NumeroInteiro | TokenType.NumeroReal | TokenType.Booleano;
        public bool IsTexto => Tipo == TokenType.TextoLiteral;
        public bool IsInteiro => Tipo == TokenType.NumeroInteiro;
        public bool IsReal => Tipo == TokenType.NumeroReal;
        public bool IsBooleano => Tipo == TokenType.Booleano;
        public string Valor { get; set; } = valor;
        public override string Descricao => $"<literal [{Utils.GetNomeByTokenType(tipo)}]> {Valor}";
    }
}
