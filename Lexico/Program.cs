
string entrada = """"
    pagina "Loja Declara" {
      secao "Produtos em destaque" {
        para item em produtos {
          cartao {
            titulo: item.nome
            texto: item.descricao
            se item.promocao {
              texto: "OFERTA"
            }
            botao {
              texto: "Comprar"
              acao: "adicionarCarrinho"
            }
          }
        }
      }
    }
    """";

List<string> tokens = new List<string>();

for(int i = 0; i < entrada.Length; i++) {
    char c = entrada[i];

    // Ignorar espaços em branco
    if (char.IsWhiteSpace(c)) continue;

    // Percorre cada caractere alfanumerico, underscore ou aspas e adiciona como token
    if (char.IsLetterOrDigit(c) || c == '_' || c == '"') {
        string token = "";
        while(i < entrada.Length && (char.IsLetterOrDigit(entrada[i]) || entrada[i] == '_' || entrada[i] == '"')) {
            token += entrada[i];
            i++;
        }
        tokens.Add(token);
        i--;
    } else {
        tokens.Add(c.ToString());
    }
}


Console.WriteLine($"Código-fonte:\n{entrada}\n");
Console.WriteLine($"Tokens:\n{string.Join(", ", tokens)}");