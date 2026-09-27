namespace Lexico
{
    /// <summary>
    /// Analisador Léxico (Lexer) para o compilador. 
    /// Responsável por ler o código-fonte de entrada e gerar uma lista de tokens.
    /// </summary>
    public class Lexer
    {
        /// <summary>
        /// Lista de Palavras Chaves de Componentes
        /// </summary>
        private static readonly List<string> Componentes = new List<string>
        {
            "pagina", 
            "secao", 
            "cartao", 
            "botao"
        };
        /// <summary>
        /// Lista de Palavras Chaves de Controles
        /// </summary>
        private static readonly List<string> Controles = new List<string>
        {
            "para", 
            "em", 
            "se"
        };

        /// <summary>String de entrada; código-fonte do compilador.</summary>
        private readonly string _entrada;
        /// <summary>Posição do caractere atual no leitor do analisador léxico</summary>
        private int _posicao;
        /// <summary>Posição, em linha, do caracter atual no leitor.</summary>
        private int _linha = 1;
        /// <summary>Posição, em coluna, do caracter atual no leitor</summary>
        private int _coluna = 1;

        public Lexer(string entrada)
        {
            _entrada = entrada ?? throw new ArgumentNullException(nameof(entrada));
        }

        /// <summary>
        /// Dado o conjunto de caracteres de entrada, o Lexer gera uma lista de tokens.
        /// </summary>
        /// <returns>Uma lista de tokens.</returns>
        /// <exception cref="Exception"></exception>
        public List<Token> Tokenizar()
        {
            List<Token> tokens = new List<Token>();

            while (!IsFimDoCodigo())
            {
                char c = GetCaractereAtual();

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
                        tokens.Add(GeraToken(TokenType.AbreBloco, "{"));
                        continue;
                    case '}':
                        tokens.Add(GeraToken(TokenType.FechaBloco, "}"));
                        continue;
                    case ':':
                        tokens.Add(GeraToken(TokenType.Atribuidor, ":"));
                        continue;
                    case '.':
                        tokens.Add(GeraToken(TokenType.AcessoMembro, "."));
                        continue;
                    case ';':
                        tokens.Add(GeraToken(TokenType.Separador, ";"));
                        continue;
                    case '"':
                        tokens.Add(GeraTokenLiteral());
                        continue;
                    default:
                        if (char.IsLetter(c) || c == '_')
                        {
                            tokens.Add(GeraTokenIdentificadorOuPalavraChave());
                            continue;
                        }

                        throw new Exception($"Caractere inesperado '{c}' na linha {_linha}, coluna {_coluna}.");

                }
            }

            tokens.Add(GeraTokenFimDeArquivo());
            return tokens;
        }
        /// <summary>
        /// Método interno para ler um literal de texto delimitado por aspas duplas. 
        /// O método consome os caracteres do literal até encontrar a aspa de fechamento.
        /// </summary>
        /// <returns>Token a partir do literal, caso contrário levanta exception.</returns>
        /// <exception cref="Exception"></exception>
        private Token GeraTokenLiteral()
        {
            int linha = _linha;
            int coluna = _coluna;
            ProximoCaractere(); // Para consumir as aspas de abertura

            string conteudo = "";

            while (true)
            {
                if (IsFimDoCodigo())
                {
                    throw new Exception($"Literal de texto não fechado na linha {linha}, coluna {coluna}.");
                }

                char c = GetCaractereAtual();

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
        /// <summary>
        /// Método interno para geração de token de fim de arquivo o terminar a cadeia de entrada.
        /// </summary>
        /// <returns>Token de Fim de Arquivo.</returns>
        private Token GeraTokenFimDeArquivo() => new Token(TokenType.FimDeArquivo, string.Empty, "FimDoArquivo", (_linha, _coluna));

        /// <summary>
        /// Método interno para ler identificadores ou palavras-chave. O método consome os caracteres do identificador até encontrar 
        /// um caractere que não seja letra, dígito ou sublinhado (_).
        /// </summary>
        /// <returns>Token de Identificador ou Palavra-chave</returns>
        private Token GeraTokenIdentificadorOuPalavraChave()
        {
            int linha = _linha;
            int coluna = _coluna;
            int inicio = _posicao;

            while (!IsFimDoCodigo() &&
                (char.IsLetterOrDigit(GetCaractereAtual()) || GetCaractereAtual() == '_'))
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
        /// <summary>
        /// Método interno de fabriação de Token.
        /// </summary>
        /// <param name="tipo"><see cref="TokenType"/> do Token.</param>
        /// <param name="lexema">Cadeia de caracteres que representa o token no código fonte.</param>
        /// <returns>O token gerado a partir dos parâmetros de entrada.</returns>
        private Token GeraToken(TokenType tipo, string lexema)
        {
            var posicao = (_linha, _coluna);
            ProximoCaractere();
            return new Token(tipo, lexema, lexema, posicao);
        }

        /// <summary>
        /// Retorna o caractere atual do código-fonte de entrada, baseado na posição atual.
        /// </summary>
        /// <returns></returns>
        private char GetCaractereAtual() => _entrada[_posicao];

        /// <summary>
        /// Retorna verdadeiro se a posição atual mais o valor de deslocamento for maior que o tamanho do código-fonte.
        /// </summary>
        /// <param name="deslocamento">Valor inteiro do deslocamento que somado a posição do leitor, é verificado se 
        /// é o final do código de entrada.</param>
        private bool IsFimDoCodigo(int deslocamento = 0) => _posicao + deslocamento >= _entrada.Length;

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
}

