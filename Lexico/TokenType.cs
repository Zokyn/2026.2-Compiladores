using System.ComponentModel;

namespace Lexico
{
    public enum TokenType
    {
        Identificador,  // controle, componente e dados
        TextoLiteral,   // "AAAAA", "Hello World", "Hi"
        NumeroReal,     // 1.05, .5, 0.0, -3.14, ...
        NumeroInteiro,  // 1, 2, -4, 0, ...
        AbreConstruror, // (
        FechaConstrutor,// )
        AbreBloco,  // {
        FechaBloco, // }
        AbreTupla,  // [
        FechaTupla, // ]
        Atribuidor, // :
        Separador,  // ,
        FimDeArquivo,

        Comentarios, // > 
        EspacosVazios, // \t, \n, \r, ' '
        Erro,
    }
    
}
