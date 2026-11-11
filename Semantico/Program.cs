using Lexico;
namespace Semantico
{
    public class Program
    {
        public static void Main()
        {

            try
            {
                //Env.Load();
                string filename = Environment.GetEnvironmentVariable("FILEPATH") ?? "";
                string? entradaDeTexto = null;
                if (!string.IsNullOrEmpty(filename))
                    entradaDeTexto = File.ReadAllText(filename);

                string entrada = entradaDeTexto ?? @"
                    > Arquivo teste

                    pagina(pag-teste, ""Teste"") {
                      secao(sec-botao <<<secção sem título>>>) {
                        > constroi uma página com apenas um botão no meio.
                        botao(btn-comprar, ""Clicar"", acao: abrirPopUp)
                      }
                    }
                        ";
                var lexer = new Lexico.Lexer(entrada);
                var tokens = lexer.Tokenizar();

                var parser = new Sintatico.Parser(tokens);
            }
            catch (ErroLexico erroLexico)
            {
                Console.WriteLine(erroLexico.Message);
            }
        }
    }
}