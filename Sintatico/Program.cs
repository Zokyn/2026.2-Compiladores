using DotNetEnv;
using Lexico;
using Sintatico.Nodes;

namespace Sintatico
{


    internal class Program
    {
        static void Main(string[] args)
        {
            Env.Load();
            string filename = Environment.GetEnvironmentVariable("FILEPATH") ?? "";
            string codigo = File.ReadAllText(filename);
            Lexer l = new Lexer(codigo);
            Parser p = new Parser(l.Tokenizar());
            ResultadoParser r = p.Parse();

            Console.WriteLine(r.Sucesso ? "Parse bem-sucedido!" : "Parse com erros!" +
                $"{r.Erros}" );
        }
    }
}