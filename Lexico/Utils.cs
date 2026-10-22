using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexico
{
    public class Utils
    {
        public static string GetCategoriaByType(TokenType tipo) => tipo switch
        {
            TokenType.Componente => "Palavra-chave de Componente",
            TokenType.Identificador => "Identificador",
            TokenType.TextoLiteral => "Literal",
            TokenType.NumeroReal => "Número Real",
            TokenType.NumeroInteiro => "Número Inteiro",
            TokenType.AbreConstrutor => "Abre Construtor",
            TokenType.FechaConstrutor => "Símbolo de Encerramento de construtor",
            TokenType.AbreBloco => "Símbolo de Abertura de bloco",
            TokenType.FechaBloco => "Símbolo de Encerramento de bloco",
            TokenType.AbreTupla => "Símbolo de Abertura de tupla",
            TokenType.Atribuidor => "Símbolo de Atribuidor",
            TokenType.Separador => "Símbolo de Separador",
            TokenType.FimDeArquivo => "Token de Fim de arquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Categoria definida.")
        };
        public static string GetNomeByType(TokenType tipo) => tipo switch
        {
            TokenType.Componente => "Componente",
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
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Nome definida.")
        };
        public static string GetExampleByType(TokenType tipo) => tipo switch
        {
            TokenType.Componente => "pagina, secao, cartao, botao",
            TokenType.Identificador => "Controle, _contador, pag-inicial",
            TokenType.TextoLiteral => "\"Alô, mamãe\", \"12345\", \"Hello World\"",
            TokenType.NumeroInteiro => "1, -5, 3",
            TokenType.NumeroReal => "1, 2.5, 0.5",
            TokenType.AbreConstrutor => "(",
            TokenType.FechaConstrutor => ")",
            TokenType.AbreBloco => "{",
            TokenType.FechaBloco => "}",
            TokenType.AbreTupla => "[",
            TokenType.FechaTupla => "]",
            TokenType.Separador => ",",
            TokenType.Atribuidor => ":",
            TokenType.Acesso => "Acesso",
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Exemplo definida.")
        };
    }
}
