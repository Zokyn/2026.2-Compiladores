using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintatico.Nodes
{
    public class NodeAtributo : Node
    {
        public string Nome { get; set; }
        public Node Valor { get; set; }
        public override string Descricao => 
            $"Atributo: {Nome} = {Valor.Descricao}";
        public override IEnumerable<Node> Filhos => 
            Valor != null ? new[] { Valor } : Enumerable.Empty<Node>();
    }
}
