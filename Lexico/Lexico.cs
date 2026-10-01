using System.Text.RegularExpressions;

namespace Lexico
{
    /// <summary>
    /// Analisador Léxico (Lexer) para o compilador. 
    /// Responsável por ler o código-fonte de entrada e gerar uma lista de tokens.
    /// </summary>
    public partial class Lexico(string entrada)
    {
        // LISTAS DE SIMBOLOS
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
        // PROPRIEDADES
        /// <summary>String de entrada; código-fonte do compilador.</summary>
        private readonly string _entrada = entrada ?? throw new ArgumentNullException(nameof(entrada));
        /// <summary>Posição do caractere atual no leitor do analisador léxico</summary>
        private int _posicao;
        /// <summary>Posição, em linha, do caracter atual no leitor.</summary>
        private int _linha = 1;
        /// <summary>Posição, em coluna, do caracter atual no leitor</summary>
        private int _coluna = 1;
        
        // PROPRIEDADES COMPUTADAS
        /// <summary>Propriedade computada que retorna posição em inteiro do leitor no código-fonte de entrada.</summary>
        public int Posicao => _posicao;
        /// <summary>Propriedade computada que retorna a posição em par-ordenado de linha e coluna.</summary>
        public (int, int) LinhaColuna => (_linha, _coluna);
        /// <summary>Propriedade computada que retorna o caractere atual do código-fonte de entrada, baseado na posição atual.</summary>
        public char CaractereAtual => _entrada[_posicao];
        // SUBCLASSES E OBJETOS
        public partial class TokenFactory { }

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
        public void DeslocaCaracter(int deslocamento)
        {
            int i = 0; 
            while (!IsFimDoCodigo() && i < deslocamento)
            {
                ProximoCaractere();
                i++;
            }
        }
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
    
        public Regex Expressoes = new Regex(@"
            (?<EspacosVazios>[ \t\r\n]+) 
            | (?<Identificador> [a-zA-Z_][a-zA-Z0-9_]* (?:-[a-zA-Z0-9_]+)* )
            | (?<Texto>""[^""\n]*"")
            | (?<Atribuidor>     [:] )
            | (?<AcessoMembro> [.] )
            | (?<Numero>      \d+(?:\.\d+)? )
            | (?<DelimitadorBloco> [{}] )
            | (?<DelimitadorConstrutor> [()] )
            | (?<Separador> [,;] )
            | (?<Erro>        . )
        ", RegexOptions.IgnorePatternWhitespace);
        
        public readonly Dictionary<string, TokenType?> Grupos = new Dictionary<string, TokenType?>
        {
            { "EspacosVazios", null },
            { "Identificador", TokenType.Identificador },
            { "Texto", TokenType.Texto },
            { "Atribuidor", TokenType.Atribuidor },
            { "AcessoMembro", TokenType.AcessoMembro },
            { "Numero", TokenType.Numero },
            { "DelimitadorBloco", TokenType.DelimitadorBloco },
            { "DelimitadorConstrutor", TokenType.DelimitadorConstrutor },
            { "Separador", TokenType.Separador },
            { "Erro", TokenType.Erro }
        };
        /// <summary>
        /// Dado o conjunto de caracteres de entrada, o Lexer gera uma lista de tokens.
        /// </summary>
        /// <returns>Uma lista de tokens.</returns>
        /// <exception cref="Exception"></exception>
        public List<Token> Tokenizar()
        {
            List<Token> tokens = new List<Token>();
            int linhaAtual = 1, ultimaQuebraLinha = -1;
            int colunaAtual = -1;
            foreach (Match m in Expressoes.Matches(_entrada))
            {
                string grupoNome = Grupos.Keys.First(nome => m.Groups[nome].Success);
                Grupos.TryGetValue(grupoNome, out TokenType? tipo);

                colunaAtual = m.Index - ultimaQuebraLinha;

                if (grupoNome == "Erro")
                    throw new ErroLexico($"Caractere inesperado '{m.Value}'", linhaAtual, colunaAtual);


                if (tipo == null)
                {
                    for (int i = 0; i < m.Length; i++)
                    {
                        if (m.Value[i] == '\n')
                        {
                            linhaAtual++;
                            ultimaQuebraLinha = m.Index + i;
                        }
                    }
                    continue;
                } else
                {
                    if (tipo.HasValue)
                    {
                        tokens.Add(new Token(tipo.Value, m.Value, m.Value, (linhaAtual, colunaAtual)));
                    }
                }
            }

            tokens.Add(TokenFactory.GeraTokenFimDeArquivo(linhaAtual, colunaAtual));
            return tokens;
        }
    }
    public class ErroLexico : Exception
    {
        public int Linha { get; }
        public int Coluna { get; }
        public ErroLexico(string mensagem, int linha, int coluna)
            : base($"Erro léxico [{linha};{coluna}]: {mensagem}")
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
}

