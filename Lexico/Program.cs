using System; 
using System.Text.RegularExpressions;

namespace Lexico
{
    public static class Program
    {
        public static void Main()
        {
            string entrada = @"
pagina ""Loja Declara"" {
    secao ""Produtos em destaque"" {
    para item em produtos {
        cartao {
        titulo: item.nome;
        texto: item.descricao;
        se item.promocao {
            texto: ""OFERTA"";
        }
        botao {
            texto: ""Comprar"";
            acao: ""adicionarCarrinho"";
        }
        }
    }
    }
}
";

            try
            {
                var lexer = new Lexer(entrada);
                var tokens = lexer.Tokenizar();

                foreach (var token in tokens)
                {
                    Console.WriteLine(token);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

