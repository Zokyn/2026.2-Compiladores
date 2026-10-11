namespace Lexico
{
    public static class Program
    {
        public static void Main()
        {
            string? entradaDeTexto = File.ReadAllText(Environment.GetEnvironmentVariable("FILEPATH");
            string entrada = entradaDeTexto ?? @"
dados produtos {
  produto(nome: ""Camiseta"", descricao: ""Algodão"", promocao: verdadeiro)
  produto(nome: ""Caneca"", descricao: ""Cerâmica"", promocao: falso)
}

pagina(pag-loja, ""Loja Declara"") {
  secao(sec-produtos, ""Produtos em destaque"") {
    para item em produtos {
      cartao(titulo: item.nome, texto: item.descricao) {
        se item.promocao {
          selo(""OFERTA"")
        }
        botao(btn-comprar, ""Comprar"", acao: adicionarCarrinho)
      }
    }
  }
}
";

            try
            {
                var lexer = new AnalisadorLexico(entrada);
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

