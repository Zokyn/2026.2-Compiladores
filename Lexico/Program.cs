using System; 
using System.Text.RegularExpressions;

namespace Declara
{
    public enum TokenType
    {
        Componente,         // Ex.: pagina, secao, cartao, botao
        Controle,           // Ex.: para, em, se 
        Identificador,      // nome de variáveis, de atributos e membros
        Texto,              // literal de texto delimitado por aspas duplas Ex.:"Hello World"
        Atribuidor,         // :
        AcessoMembro,       // .
        AbreBloco,          // {
        FechaBloco,         // } 
        Separador,          // ; <- Subsituindo o quebra de linha
        FimDeArquivo
    }
    public class Token
    {
        public TokenType Tipo { get; }
        /// <summary>Cadeia de caracteres retirada do código-fonte (entrada)</summary
        public string Lexema { get; }

        /// <summary>Valor processado do token, quando aplicável, podendo ser 
        /// o valor de texto sem as aspas delimitadoras, número inteiros ou só
        /// o próprio lexema</summary>
        public string Valor { get; }

        /// <summary>
        /// Par-ordenado da posição (linha e coluna) da declaração do token
        /// </summary>
        public (int, int) Posicao { get; }
        
        public Token(TokenType tipo, string lexema, string valor, (int, int) posicao)
        {
            Tipo = tipo;
            Lexema = lexema;
            Valor = valor;
            Posicao = posicao;
        }

        public override string ToString()
        {
            string valor = Valor;
            return $"({Tipo}, '{valor}') - linha {Posicao.Item1}, coluna {Posicao.Item2}";
        }
    }

    public class Lexer
    {
        private static readonly List<string> Componentes = new List<string>
        {
            "pagina", 
            "secao", 
            "cartao", 
            "botao"
        };
        private static readonly List<string> Controles = new List<string>
        {
            "para", 
            "em", 
            "se"
        };

        private readonly string _entrada;
        private int _posicao;
        private int _linha = 1;
        private int _coluna = 1;

        public Lexer(string entrada)
        {
            _entrada = entrada ?? throw new ArgumentNullException(nameof(entrada));
        }

        public List<Token> Tokenizar()
        {
            List<Token> tokens = new List<Token>();

            while (!FimDoCodigo())
            {
                char c = CaractereAtual();

                if (c == ' ' || c == '\t' || c == '\r')
                {
                    ProximoCaractere();
                    continue;
                }

                switch (c)
                {
                    case ' ':
                    case '\n':
                    case '\t':
                    case '\r':
                        ProximoCaractere();
                        continue;
                    case '{':
                        tokens.Add(CriarToken(TokenType.AbreBloco, "{"));
                        continue;
                    case '}':
                        tokens.Add(CriarToken(TokenType.FechaBloco, "}"));
                        continue;
                    case ':':
                        tokens.Add(CriarToken(TokenType.Atribuidor, ":"));
                        continue;
                    case '.':
                        tokens.Add(CriarToken(TokenType.AcessoMembro, "."));
                        continue;
                    case ';':
                        tokens.Add(CriarToken(TokenType.Separador, ";"));
                        continue;
                    case '"':
                        tokens.Add(LerLiteral());
                        continue;
                    default:
                        if (char.IsLetter(c) || c == '_')
                        {
                            tokens.Add(LerIdentificadorOuPalavraChave());
                            continue;
                        }

                        throw new Exception($"Caractere inesperado '{c}' na linha {_linha}, coluna {_coluna}.");

                }
            }

            tokens.Add(CriaTokenFimDeArquivo());
            return tokens;
        }
        private Token LerLiteral()
        {
            int linha = _linha;
            int coluna = _coluna;
            ProximoCaractere(); // Para consumir as aspas de abertura

            string conteudo = "";

            while (true)
            {
                if (FimDoCodigo())
                {
                    throw new Exception($"Literal de texto não fechado na linha {linha}, coluna {coluna}.");
                }

                char c = CaractereAtual();

                if (c == '"')
                {
                    ProximoCaractere(); // Para consumir as aspas de fechamento
                    break;
                }
                else if (c == '\n')
                {
                    throw new Exception($"Literal de texto não pode conter quebras de linha na linha {linha}, coluna {coluna}.");
                }
                //TO-DO: adicionar suporte para escapes \ e \\ 
                //TO-DO: adicionar suporte a quebras de linha com \n dentro do literal de texto, e quebra de linha sem quebrar o texto

                conteudo += c;
                ProximoCaractere();
            }


            return new Token(TokenType.Texto, $"\"{conteudo}\"", conteudo.ToString(), (linha, coluna));
        }

        private Token CriaTokenFimDeArquivo() => new Token(TokenType.FimDeArquivo, string.Empty, "FimDoArquivo", (_linha, _coluna));

        private Token LerIdentificadorOuPalavraChave()
        {
            int linha = _linha;
            int coluna = _coluna;
            int inicio = _posicao;

            while (!FimDoCodigo() &&
                (char.IsLetterOrDigit(CaractereAtual()) || CaractereAtual() == '_'))
            {
                ProximoCaractere();
            }

            string lexema = _entrada.Substring(inicio, _posicao - inicio);

            if (Componentes.Contains(lexema))
            {
                return new Token(TokenType.Componente, lexema, lexema.ToLowerInvariant(), (linha, coluna));
            }
            else if (Controles.Contains(lexema))
            {
                return new Token(TokenType.Controle, lexema, lexema.ToLowerInvariant(), (linha, coluna));
            }

            return new Token(TokenType.Identificador, lexema, lexema, (linha, coluna));
        }
        private Token CriarToken(TokenType tipo, string lexema)
        {
            var posicao = (_linha, _coluna);
            ProximoCaractere();
            return new Token(tipo, lexema, lexema, posicao);
        }

        // Retorna o caractere atual do código-fonte de entrada, baseado na posição atual.
        private char CaractereAtual() => _entrada[_posicao];

        // Retorna true se a posição atual mais o valor de deslocamento for maior que o tamanho do código-fonte.
        private bool FimDoCodigo(int deslocamento = 0) => _posicao + deslocamento >= _entrada.Length;

        /// <summary>
        /// Avança para o próximo caractere do código-fonte de entrada, atualizando reiniciando a coluna e
        /// incrementando o valor da linha, tenha ocorrido uma quebra de linha. Caso contrário apenas a coluna
        /// é incrementada. A posição no código-fonte é incrementada sempre, independente do caractere atual.
        /// </summary>
        private void ProximoCaractere()
        {
            if (_entrada[_posicao] == '\n')
            {
                _linha++;
                _coluna = 1;
            }
            else
            {
                _coluna++;
            }
            _posicao++;
        }
    }
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

