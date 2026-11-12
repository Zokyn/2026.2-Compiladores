using System.Text.RegularExpressions;

namespace Lexico
{

    /// <summary>
    /// Analisador Léxico (Lexer) para o compilador. 
    /// Responsável por ler o código-fonte de entrada e gerar uma lista de tokens.
    /// </summary>
    public partial class Lexer(string entrada)
    {
        public const int TAB_SIZE = 2;
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

        /// <summary>
        /// Retorna verdadeiro se a posição atual mais o valor de deslocamento for maior que o tamanho do código-fonte.
        /// </summary>
        /// <param name="deslocamento">Valor inteiro do deslocamento que somado a posição do leitor, é verificado se 
        /// é o final do código de entrada.</param>
        public bool IsFimDoCodigo(int deslocamento = 0) => _posicao + deslocamento >= _entrada.Length;

        /// <summary>
        /// Dado o conjunto de caracteres de entrada, o Lexer gera uma lista de tokens.
        /// </summary>
        /// <returns>Uma lista de tokens.</returns>
        /// <exception cref="Exception"></exception>
        public List<Token> Tokenizar()
        {
            Regex expressoes = Utils.Expressoes;

            List<Token> tokens = [];
            int linhaAtual = 1, ultimaQuebraLinha = -1;
            foreach (Match m in expressoes.Matches(_entrada))
            {
                _posicao = m.Index;
                int colunaAtual = m.Index - ultimaQuebraLinha;

                string grupoNome = Utils.Grupos.Keys.First(nome => m.Groups[nome].Success);
                Utils.Grupos.TryGetValue(grupoNome, out TokenType? tipo);


                if (grupoNome == "Erro")
                    throw new ErroCaractereInesperado(m.Value[0], linhaAtual, colunaAtual);
                //throw new ErroLexico($"Caractere inesperado '{m.Value}'", linhaAtual, colunaAtual);

                // Se for um valor em branco, segue o cursor para frente e quebra linha, caso necessário.
                if (tipo == TokenType.QuebraLinha)
                {
                    // Percorre o tamanho do quebra de linha
                    for (int i = 0; i < m.Value.Length; i++)
                    {
                        if (m.Value[i] == '\n')
                        {
                            linhaAtual++;
                            // Proximo caractere de \n será uma nova linha
                            ultimaQuebraLinha = m.Index + i;
                        }
                    }
                }
               
                else if (tipo == TokenType.Espacos)
                {   // Demais valore em branco são ignorados
                    continue;
                }
                else
                {   // Tokens válidos são adicionados na lista de tokens.
                    if (tipo.HasValue)
                    {
                        // Se um identificador pode ser um Identificador, Dados, Controle ou Componente,
                        // então verifica se o valor do identificador está nas listas de palavras-chave.
                        if (tipo.Value == TokenType.Identificador)
                        {
                            if (Utils.Componentes.Contains(m.Value)) // pagina, cartao, secao, botao, selo
                                tipo = TokenType.Componente;
                            else if (Utils.Controles.Contains(m.Value)) // para, em, se, enquanto, sendo
                                tipo = TokenType.Controle;
                            else if (m.Value == Utils.PalavraDados) // dados
                                tipo = TokenType.Dados;
                        }
                        // O valor do texto literal é o conteúdo sem as aspas delimitadoras.
                        string valor = tipo.Value == TokenType.TextoLiteral ? m.Value[1..^1] : m.Value;
                        tokens.Add(new Token(tipo.Value, m.Value, valor, Posicao, (linhaAtual, colunaAtual)));
                    }
                    _posicao += m.Length;
                }
            }

            // O fim de arquivo fica na posição logo após o último caractere da entrada.
            tokens.Add(new Token(TokenType.FimDeArquivo, "", "FDA", _entrada.Length, (linhaAtual, 0)));
            return tokens;
        }
    }

}


