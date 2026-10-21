using Sintatico.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintatico
{
    public class ResultadoParser
    {
        public NodeDocumento Arvore { get; set; }
        public List<ErroSintatico> Erros { get; } = new();
        public bool Sucesso => Erros.Count == 0;
    }
}
