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
            TokenType.DelimitadorBloco => "Símbolo de Delimitador de bloco",
            TokenType.DelimitadorConstrutor => "Símbolo de Delimitador de construtor",
            TokenType.FimDeArquivo => "Token de Fim de arquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Categoria definida.")
        };
    }
}
