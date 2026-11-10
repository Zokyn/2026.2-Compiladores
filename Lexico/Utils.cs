using System.Text.RegularExpressions;

namespace Lexico
{
    public class Utils
    {
        /// <summary>Lista de Palavras Chaves de Componentes</summary>
        public static readonly List<string> Componentes = new List<string>
        {
            "pagina",
            "secao",
            "cartao",
            "botao",
            "selo"
        };
        /// <summary>Lista de Palavras Chaves de Controles</summary>
        public static readonly List<string> Controles = new List<string>
        {
            "para",
            "em",
            "se",
            "enquanto",
            "sendo"
        };
        /// <summary>Expressão regular com grupos nomeados acomplando todas as expressões regulares de cada TokenType</summary>
        public static Regex Expressoes = new Regex(@"
            (?<Dados> dados )
            | (?<Identificador> [a-zA-Z_][a-zA-Z0-9_]* (?:-[a-zA-Z0-9_]+)* )
            | (?<TextoLiteral>""[^""\n]*"")
            | (?<NumeroReal>[0-9]*\.[0-9]+)
            | (?<NumeroInteiro>[0-9]+)
            | (?<AbreConstrutor> [(] )
            | (?<FechaConstrutor> [)] )
            | (?<AbreBloco> [{] )
            | (?<FechaBloco> [}] )
            | (?<AbreTupla> [\[] )
            | (?<FechaTupla> [\]] )
            | (?<Atribuidor>     [:] )
            | (?<Separador> [,] )
            | (?<SeparadorBloco> [;] )
            | (?<Acesso> [.] )
            | (?<Booleano> \b(?:v(?:erdadeiro|erdade)? | (f(?:also|alsidade)?)\b) )
            | (?<Incremento> \+\+ )
            | (?<Decremento> -- )
            | (?<Comentario> <<<[\s\S]*?>>>|^[ \t]*>[^\r\n]* )
            | (?<QuebraLinha> \G[\r?\n] )
            | (?<Espaco> \G[ \t]+ )
            | (?<Erro>        . )
        ", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);

        public static readonly Dictionary<string, TokenType?> Grupos = new Dictionary<string, TokenType?>
        {
            { "Dados", TokenType.Dados },
            { "Identificador", TokenType.Identificador },
            { "TextoLiteral", TokenType.TextoLiteral },
            { "NumeroInteiro", TokenType.NumeroInteiro },
            { "NumeroReal", TokenType.NumeroReal },
            { "AbreConstrutor", TokenType.AbreConstrutor },
            { "FechaConstrutor", TokenType.FechaConstrutor },
            { "AbreBloco", TokenType.AbreBloco },
            { "FechaBloco", TokenType.FechaBloco },
            { "AbreTupla", TokenType.AbreTupla },
            { "FechaTupla", TokenType.FechaTupla },
            { "Atribuidor", TokenType.Atribuidor },
            { "Separador", TokenType.Separador },
            { "SeparadorBloco", TokenType.SeparadorBloco },
            { "Acesso", TokenType.Acesso },
            { "Incremento", TokenType.Incremento },
            { "Decremento", TokenType.Decremento },
            { "Comentario", TokenType.Comentario },
            { "QuebraLinha", TokenType.QuebraLinha },
            { "Espaco", TokenType.Espacos },
            { "Erro", TokenType.Erro }
            //Fim de Arquivo não tem match
        };

        /// <summary>Palavra-chave de declaração de dados </summary>
        public const string PalavraDados = "dados";
        /// <summary>Método utiltário que retorna uma categoria para o token.</summary>
        /// <returns>Uma string com a categoria do token</returns>
        /// <exception cref="ErroLexico">Erro de TokenType sem categoria definida.</exception>
        public static string GetDetalhesByTokenType(TokenType tipo, int linha, int coluna) => tipo switch
        {
            TokenType.Componente => "Palavra-chave de Componente",
            TokenType.Dados => "Palavra-chave de Declaração de Dados",
            TokenType.Controle => "Palavra-chave de Controle",
            TokenType.Identificador => "Identificador",
            TokenType.TextoLiteral => "Literal",
            TokenType.NumeroReal => "Número Real",
            TokenType.NumeroInteiro => "Número Inteiro",
            TokenType.AbreConstrutor => "Abre Construtor",
            TokenType.FechaConstrutor => "Símbolo de Encerramento de construtor",
            TokenType.AbreBloco => "Símbolo de Abertura de bloco",
            TokenType.FechaBloco => "Símbolo de Encerramento de bloco",
            TokenType.AbreTupla => "Símbolo de Abertura de tupla",
            TokenType.FechaTupla => "Símbolo de Encerramento de tupla",
            TokenType.Atribuidor => "Símbolo de Atribuidor",
            TokenType.Separador => "Símbolo de Separador",
            TokenType.Acesso => "Símbolo de Acesso",
            TokenType.Incremento => "Operador de Incremento",
            TokenType.Decremento => "Operador de Decremento",
            TokenType.Comentario =>  "Comentário",
            TokenType.FimDeArquivo => "Token de Fim de arquivo",
            TokenType.Erro => "Erro",
            _ => throw new ErroLexico("TokenType sem Categoria definida.", linha, coluna)
        };

        public static string GetNomeByTokenType(TokenType tipo) => GetNomeByTokenType(tipo, 0, 0);
        /// <summary>Método utilitário que retorna o nome do TokenType</summary>
        /// <returns>Uma string com o nome do token.</returns>
        /// <exception cref="Exception">Erro de TokenType sem nome definido.</exception>
        public static string GetNomeByTokenType(TokenType tipo, int linha, int coluna) => tipo switch
        {
            TokenType.Componente => "Componente",
            TokenType.Dados => "Dados",
            TokenType.Controle => "Controle",
            TokenType.Identificador => "Identificador",
            TokenType.TextoLiteral => "Texto",
            TokenType.NumeroInteiro => "NumeroInteiro",
            TokenType.NumeroReal => "NumeroReal",
            TokenType.AbreConstrutor => "AbreConstrutor",
            TokenType.FechaConstrutor => "FechaConstrutor",
            TokenType.AbreBloco => "AbreBloco",
            TokenType.FechaBloco => "FechaBloco",
            TokenType.AbreTupla => "AbreTupla",
            TokenType.FechaTupla => "FechaTupla",
            TokenType.Separador => "Separador",
            TokenType.Atribuidor => "Atribuidor",
            TokenType.Acesso => "Acesso",
            TokenType.Booleano => "Booleano",
            TokenType.Incremento => "Incremento",
            TokenType.Decremento => "Decremento",
            TokenType.Comentario => "Comentario",
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => (linha == 0 || coluna == 0) ? throw new Exception("TokenType sem Nome definido.") : throw new ErroLexico("TokenType sem Nome definido.", linha, coluna)
        };
        /// <summary>Método utilitário que retorna um exemplo para o TokenType</summary>
        /// <returns>Uma string com o exemplo do token</returns>
        /// <exception cref="Exception">Erro de TokenType sem exemplo definido.</exception>
        public static string GetExampleByTokenType(TokenType tipo) => tipo switch
        {
            TokenType.Componente => "pagina, secao, cartao, botao, selo",
            TokenType.Dados => "dados",
            TokenType.Controle => "para, em, se, enquanto, sendo",
            TokenType.Identificador => "Controle, _contador, pag-inicial",
            TokenType.TextoLiteral => "\"Alô, mamãe\", \"12345\", \"Hello World\"",
            TokenType.NumeroInteiro => "1, 5, 3",
            TokenType.NumeroReal => "1.0, 2.5, .5",
            TokenType.AbreConstrutor => "(",
            TokenType.FechaConstrutor => ")",
            TokenType.AbreBloco => "{",
            TokenType.FechaBloco => "}",
            TokenType.AbreTupla => "[",
            TokenType.FechaTupla => "]",
            TokenType.Separador => ",",
            TokenType.Atribuidor => ":",
            TokenType.Acesso => ".",
            TokenType.Booleano => "verdeiro, falso",
            TokenType.Incremento => "++",
            TokenType.Decremento => "--",
            TokenType.Comentario => "> comentário de linha",
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Exemplo definida.")
        };
    }
}
