using Lexico;
using Sintatico;
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
                    dados titulo: ""Teste""

                    pagina(pag-teste, titulo) {
                      secao(sec-botao <<<secção sem título>>>) {
                        > constroi uma página com apenas um botão no meio.
                        botao(btn-comprar, ""Clicar"", acao: abrirPopUp)
                      }
                    }
                        ";
                var lexer = new Lexico.Lexer(entrada);

                var tokens = lexer.Tokenizar();

                var parser = new Sintatico.Parser(tokens);
                ResultadoParser resultado = parser.Parse();
                resultado.Arvore = resultado.Arvore ?? throw new ErroArvoreSintaticaNula();

                var sintatico = new Semantico.TypeChecker();
                sintatico.Analisar(resultado.Arvore);
            }
            catch (ErroLexico erroLexico)
            {
                Console.WriteLine(erroLexico.Message);
            }
            catch (ErroSintatico erroSintatico)
            {
                Console.WriteLine(erroSintatico.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro inesperado: {e.Message}");
            }
        }
    }
}