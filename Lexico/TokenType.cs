using System.ComponentModel;

namespace Lexico
{
    public enum TokenType
    {
        [Description("Palavra-Chave Componente")]
        Componente,    // pagina, secao, cartao, botao, selo
        /// <summary>
        /// Tipo de Token da Palavra-Chave de Dados (dados).
        /// </summary>
        [Description("Palavra-Chave Dados")]
        Dados,
        /// <summary>
        /// Tipo de Token das Palavras-Chave de Controle (para, em, se, enquanto, sendo).
        /// </summary>
        [Description("Palavra-Chave de Controle")]
        Controle,
        [Description("Identificador")]
        Identificador,  // controle, componente e dados
        [Description("Texto Literal")]
        TextoLiteral,   // "AAAAA", "Hello World", "Hi"
        [Description("Numero Real")]
        NumeroReal,     // 1.05, .5, 0.0, ...
        [Description("Numero Inteiro")]
        NumeroInteiro,  // 1, 2, 0, ...
        [Description("Abre Construtor")]
        AbreConstrutor, // (
        [Description("Fecha Construtor")]
        FechaConstrutor,// )
        [Description("Abre Bloco")]
        AbreBloco,  // {
        [Description("Fecha Bloco")]
        FechaBloco, // }
        [Description("Abre Tupla")]
        AbreTupla,  // [
        [Description("Fecha Tupla")]
        FechaTupla, // ]
        [Description("Símbolo Atribuidor")]
        Atribuidor, // :
        [Description("Separador de Itens")]
        Separador,  // ,
        [Description("Separador de Bloco")]
        SeparadorBloco,
        [Description("Símbolo de Acesso")]
        Acesso, // .
        [Description("Operador Relacional")]
        OperadorRelacional, // ==, !=, >=, <=, >, <
        [Description("Operador Lógico")]
        OperadorLogico, // &&, ||
        [Description("Incremento")]
        Incremento, // ++
        [Description("Decremento")]
        Decremento, // --
        [Description("FimDeArquivo")]
        FimDeArquivo, // EOF ou FDA
        [Description("Comentários")]
        Comentarios, // > 
        [Description("Espaços Vazios")]
        EspacosVazios, // \t, \n, \r, ' '
        [Description("Erro")]
        Erro,
    }
    
}
