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
            TokenType.OperadorRelacional => "Operador Relacional",
            TokenType.OperadorLogico => "Operador Lógico",
            TokenType.Incremento => "Operador de Incremento",
            TokenType.Decremento => "Operador de Decremento",
            TokenType.FimDeArquivo => "Token de Fim de arquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Categoria definida.")
        };
        public static string GetNomeByType(TokenType tipo) => tipo switch
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
            TokenType.OperadorRelacional => "OperadorRelacional",
            TokenType.OperadorLogico => "OperadorLogico",
            TokenType.Incremento => "Incremento",
            TokenType.Decremento => "Decremento",
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Nome definida.")
        };
        public static string GetExampleByType(TokenType tipo) => tipo switch
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
            TokenType.OperadorRelacional => "==, !=, >=, <=, >, <",
            TokenType.OperadorLogico => "&&, ||",
            TokenType.Incremento => "++",
            TokenType.Decremento => "--",
            TokenType.FimDeArquivo => "FimDeArquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Exemplo definida.")
        };
    }
}
