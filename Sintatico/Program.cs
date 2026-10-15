using DotNetEnv;
using Lexico;
using Sintatico.Nodes;

namespace Sintatico
{

    public class ResultadoParse
    {
        public NodeDocumento Arvore { get; set; }
        public List<ErroSintatico> Erros { get; } = new();
        public bool Sucesso => Erros.Count == 0;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Env.Load();
            string filename = Environment.GetEnvironmentVariable("FILEPATH") ?? "";
            string codigo = File.ReadAllText(filename);
            Lexer l = new Lexer(codigo);
            Parser p = new Parser(l.Tokenizar());
            ResultadoParse r = p.Parse();

            Console.WriteLine(r.Sucesso ? "Parse bem-sucedido!" : "Parse com erros!" +
                $"{r.Erros}" );
        }
    }
}