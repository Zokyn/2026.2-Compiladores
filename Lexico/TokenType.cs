using System.ComponentModel;

namespace Lexico
{
    public enum TokenType
    {
        Componente,    // pagina, secao, cartao, botao
        Identificador,  // controle, componente e dados
        TextoLiteral,   // "AAAAA", "Hello World", "Hi"
        NumeroReal,     // 1.05, .5, 0.0, -3.14, ...
        NumeroInteiro,  // 1, 2, -4, 0, ...
        AbreConstrutor, // (
        FechaConstrutor,// )
        AbreBloco,  // {
        FechaBloco, // }
        AbreTupla,  // [
        FechaTupla, // ]
        Atribuidor, // :
        Separador,  // ,
        Acesso, // .
        FimDeArquivo, // EOF ou FDA

        Comentarios, // > 
        EspacosVazios, // \t, \n, \r, ' '
        Erro,
    }
    
}
