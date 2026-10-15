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
            TokenType.Controle => "Palavra-chave de Controle",
            TokenType.Componente => "Palavra-chave de Componente",
            TokenType.Identificador => "Identificador",
            TokenType.Texto => "Literal",
            TokenType.Numero => "Número",
            TokenType.Separador => "Símbolo de Separador",
            TokenType.AcessoMembro => "Símbolo de Acesso a membro",
            TokenType.Atribuidor => "Símbolo de Atribuidor",
            TokenType.AbreBloco => "Símbolo de Abertura de bloco",
            TokenType.FechaBloco => "Símbolo de Encerramento de bloco",
            TokenType.AbreConstrutor => "Símbolo de Abertura de construtor",
            TokenType.FechaConstrutor => "Símbolo de Encerramento de construtor",
            TokenType.FimDeArquivo => "Token de Fim de arquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Categoria definida.")
        };
        public static string GetNomeByType(TokenType tipo) => tipo switch
        {
            TokenType.Controle => "Controle",
            TokenType.Componente => "Componente",
            TokenType.Identificador => "Identificador",
            TokenType.Texto => "Texto",
            TokenType.Numero => "Numero",
            TokenType.Separador => "Separador",
            TokenType.AcessoMembro => "AcessoMembro",
            TokenType.Atribuidor => "Atribuidor",
            TokenType.AbreBloco => "AbreBloco",
            TokenType.FechaBloco => "FechaBloco",
            TokenType.AbreConstrutor => "AbreConstrutor",
            TokenType.FechaConstrutor => "FechaConstrutor",
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Categoria definida.")
        };
    }
}
