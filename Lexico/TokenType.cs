using System.ComponentModel;

namespace Lexico
{
    public enum TokenType
    {
        Componente,    // pagina, secao, cartao, botao, selo
        Dados,          // dados
        Controle,       // para, em, se, enquanto, sendo
        Identificador,  // controle, componente e dados
        TextoLiteral,   // "AAAAA", "Hello World", "Hi"
        NumeroReal,     // 1.05, .5, 0.0, ...
        NumeroInteiro,  // 1, 2, 0, ...
        AbreConstrutor, // (
        FechaConstrutor,// )
        AbreBloco,  // {
        FechaBloco, // }
        AbreTupla,  // [
        FechaTupla, // ]
        Atribuidor, // :
        Separador,  // ,
        Acesso, // .
        OperadorRelacional, // ==, !=, >=, <=, >, <
        OperadorLogico, // &&, ||
        Incremento, // ++
        Decremento, // --
        FimDeArquivo, // EOF ou FDA

        Comentarios, // > 
        EspacosVazios, // \t, \n, \r, ' '
        Erro,
    }
    
}
