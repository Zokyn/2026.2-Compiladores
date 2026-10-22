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
            string filename = args.Length > 0 ? args[0] : Environment.GetEnvironmentVariable("FILEPATH") ?? "";
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Arquivo não encontrado: '{filename}'. Passe o caminho como argumento ou defina FILEPATH no .env.");
                return;
            }

            List<Token> tokens;
            try
            {
                tokens = new Lexer(File.ReadAllText(filename)).Tokenizar();
            }
            catch (ErroLexico e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            ResultadoParser r = new Parser(tokens).Parse();

            if (r.Arvore != null) Imprimir(r.Arvore, "");

            Console.WriteLine();
            Console.WriteLine(r.Sucesso ? "Parse bem-sucedido!" : $"Parse com {r.Erros.Count} erro(s):");
            foreach (var erro in r.Erros) Console.WriteLine($"  {erro}");
        }

        static void Imprimir(Node node, string recuo)
        {
            Console.WriteLine($"{recuo}{node.Descricao}  [{node.Linha},{node.Coluna}]");
            foreach (var filho in node.Filhos) Imprimir(filho, recuo + "  ");
        }
    }
}
