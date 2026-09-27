namespace Lexico
{
    public partial class Lexico
    {
        public partial class TokenFactory
        {
            /// <summary>
            /// Método interno para ler um literal de texto delimitado por aspas duplas. 
            /// O método consome os caracteres do literal até encontrar a aspa de fechamento.
            /// </summary>
            /// <returns>Token a partir do literal, caso contrário levanta exception.</returns>
            /// <exception cref="Exception"></exception>
            public static Token GeraTokenLiteral(Lexico lex)
            {
                (int linha, int coluna) = lex.LinhaColuna;
                lex.ProximoCaractere(); // Para consumir as aspas de abertura

                string conteudo = "";

                while (true)
                {
                    if (lex.IsFimDoCodigo())
                    {
                        throw new Exception($"Literal de texto não fechado na linha {linha}, coluna {coluna}.");
                    }

                    char c = lex.GetCaractereAtual();

                    if (c == '"')
                    {
                        lex.ProximoCaractere(); // Para consumir as aspas de fechamento
                        break;
                    }
                    else if (c == '\n')
                    {
                        throw new Exception($"Literal de texto não pode conter quebras de linha na linha {linha}, coluna {coluna}.");
                    }
                    //TO-DO: adicionar suporte para escapes \ e \\ 
                    //TO-DO: adicionar suporte a quebras de linha com \n dentro do literal de texto, e quebra de linha sem quebrar o texto

                    conteudo += c;
                    lex.ProximoCaractere();
                }


                return new Token(TokenType.Texto, $"\"{conteudo}\"", conteudo.ToString(), (linha, coluna));
            }
            /// <summary>
            /// Método interno para geração de token de fim de arquivo o terminar a cadeia de entrada.
            /// </summary>
            /// <returns>Token de Fim de Arquivo.</returns>
            public static Token GeraTokenFimDeArquivo(Lexico lex) => new(
                TokenType.FimDeArquivo,
                string.Empty,
                "FimDoArquivo",
                lex.LinhaColuna
            );

            /// <summary>
            /// Método interno para ler identificadores ou palavras-chave. O método consome os caracteres do identificador até encontrar 
            /// um caractere que não seja letra, dígito ou sublinhado (_).
            /// </summary>
            /// <returns>Token de Identificador ou Palavra-chave</returns>
            public static Token GeraTokenIdentificadorOuPalavraChave(Lexico lex, string entrada)
            {
                (int linha, int coluna) = lex.LinhaColuna;
                int inicio = lex.Posicao;

                while (!lex.IsFimDoCodigo() &&
                    (char.IsLetterOrDigit(lex.GetCaractereAtual()) || lex.GetCaractereAtual() == '_'))
                {
                    lex.ProximoCaractere();
                }

                string lexema = entrada.Substring(inicio, lex.Posicao - inicio);

                if (Lexico.Componentes.Contains(lexema))
                {
                    return new Token(TokenType.Componente, lexema, lexema.ToLowerInvariant(), (linha, coluna));
                }
                else if (Lexico.Controles.Contains(lexema))
                {
                    return new Token(TokenType.Controle, lexema, lexema.ToLowerInvariant(), (linha, coluna));
                }

                return new Token(TokenType.Identificador, lexema, lexema, (linha, coluna));
            }
            /// <summary>
            /// Método estático de fabriação de Token.
            /// </summary>
            /// <param name="tipo"><see cref="TokenType"/> do Token.</param>
            /// <param name="lexema">Cadeia de caracteres que representa o token no código fonte.</param>
            /// <returns>O token gerado a partir dos parâmetros de entrada.</returns>
            public static Token GeraToken(Lexico lex, TokenType tipo, string lexema, (int, int) linhaColuna)
            {
                lex.ProximoCaractere();
                return new Token(tipo, lexema, lexema, linhaColuna);
            }
        }
    }
}
