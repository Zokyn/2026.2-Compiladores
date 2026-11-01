using Lexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintatico.Nodes
{
    public class Literal(string valor, TokenType tipo) : Node
    {
        public TokenType Tipo { get; } = tipo | TokenType.TextoLiteral | TokenType.NumeroInteiro | TokenType.NumeroReal;
        public string Valor { get; set; } = valor;
        public override string Descricao => $"(Literal {tipo}) {Valor}";
    }
}
