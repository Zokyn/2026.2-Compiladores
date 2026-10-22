using Lexico;

namespace Sintatico.Node
{
    //<declaracao> ::= "dados" IDENTIFICADOR <bloco_dados>
    public class Declaracao : Node
    {
        Token identificador { get; set; }
        List<Node> dados { get; set; } = new List<Node>();

    }
    
}
