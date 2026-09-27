namespace Lexico
{
    /// <summary>
    /// Analisador Léxico (Lexer) para o compilador. 
    /// Responsável por ler o código-fonte de entrada e gerar uma lista de tokens.
    /// </summary>
    public class Lexer(string entrada)
    {
        /// <summary>
        /// Lista de Palavras Chaves de Componentes
        /// </summary>
        public static readonly List<string> Componentes = new List<string>
        {
            "pagina",
            "secao",
            "cartao",
            "botao"
        };
        /// <summary>
        /// Lista de Palavras Chaves de Controles
        /// </summary>
        public static readonly List<string> Controles = new List<string>
        {
            "para",
            "em",
            "se"
        };

        /// <summary>String de entrada; código-fonte do compilador.</summary>
        private readonly string _entrada = entrada ?? throw new ArgumentNullException(nameof(entrada));
        /// <summary>Posição do caractere atual no leitor do analisador léxico</summary>
        private int _posicao;
        /// <summary>Posição, em linha, do caracter atual no leitor.</summary>
        private int _linha = 1;
        /// <summary>Posição, em coluna, do caracter atual no leitor</summary>
        private int _coluna = 1;
        /// <summary>Propriedade calcula e retorna a posição em par-ordenado de linha e coluna</summary>
        public (int, int) LinhaColuna => (_linha, _coluna);
        /// <summary>Propriedade calcula e retorna posição em inteiro do leitor no código-fonte de entrada.</summary>
        public int Posicao => _posicao;

        /// <summary>
        /// Retorna o caractere atual do código-fonte de entrada, baseado na posição atual.
        /// </summary>
        public char GetCaractereAtual() => _entrada[_posicao];
        /// <summary>
        /// Retorna verdadeiro se a posição atual mais o valor de deslocamento for maior que o tamanho do código-fonte.
        /// </summary>
        /// <param name="deslocamento">Valor inteiro do deslocamento que somado a posição do leitor, é verificado se 
        /// é o final do código de entrada.</param>
        public bool IsFimDoCodigo(int deslocamento = 0) => _posicao + deslocamento >= _entrada.Length;
        /// <summary>
        /// Avança para o próximo caractere do código-fonte de entrada, atualizando reiniciando a coluna e
        /// incrementando o valor da linha, tenha ocorrido uma quebra de linha. Caso contrário apenas a coluna
        /// é incrementada. A posição no código-fonte é incrementada sempre, independente do caractere atual.
        /// </summary>
        public void ProximoCaractere()
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
                        tokens.Add(TokenFactory.GeraToken(this, TokenType.AbreBloco, "{", LinhaColuna));
                        continue;
                    case '}':
                        tokens.Add(TokenFactory.GeraToken(this, TokenType.FechaBloco, "}", LinhaColuna));
                        continue;
                    case ':':
                        tokens.Add(TokenFactory.GeraToken(this, TokenType.Atribuidor, ":", LinhaColuna));
                        continue;
                    case '.':
                        tokens.Add(TokenFactory.GeraToken(this, TokenType.AcessoMembro, ".", LinhaColuna));
                        continue;
                    case ';':
                        tokens.Add(TokenFactory.GeraToken(this, TokenType.Separador, ";", LinhaColuna));
                        continue;
                    case '"':
                        tokens.Add(TokenFactory.GeraTokenLiteral(this));
                        continue;
                    default:
                        if (char.IsLetter(c) || c == '_')
                        {
                            tokens.Add(TokenFactory.GeraTokenIdentificadorOuPalavraChave(this, _entrada));
                            continue;
                        }

                        throw new Exception($"Caractere inesperado '{c}' na linha {_linha}, coluna {_coluna}.");

                }
            }

            tokens.Add(TokenFactory.GeraTokenFimDeArquivo(this));
            return tokens;
        }
    }
}

