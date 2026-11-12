using DotNetEnv;
namespace Lexico
{
    public static class Program
    {
        public static void Main()
        {
            Env.Load();
            string filename = Environment.GetEnvironmentVariable("FILEPATH") ?? "";
            string? entradaDeTexto = File.ReadAllText(filename);

            string codigoEntrada = entradaDeTexto ?? @"
                    > Arquivo teste

                    pagina(pag-teste, ""Teste"") {
                      secao(sec-botao <<<secção sem título>>>) {
                        > constroi uma página com apenas um botão no meio.
                        botao(btn-comprar, ""Clicar"", acao: abrirPopUp)
                      }
                    }
            ";

            try
            {
                var lexer = new Lexer(codigoEntrada);
                var tokens = lexer.Tokenizar();

                foreach (var token in tokens)
                {
                    Console.WriteLine(token);
                }
            } catch (ErroLexico erroLexico)
            {
                Console.WriteLine(erroLexico.Message);
            }
        }
    }
}

