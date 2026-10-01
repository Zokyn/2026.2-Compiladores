namespace Lexico
{
    public enum TokenType
    {
        /// <summary>
        /// Palavra chave de Componente. 
        /// Ex.: pagina, secao, cartao, botao.
        /// </summary>
        Componente,
        /// <summary>
        /// Palavra chave de Controle. 
        /// Ex.: para, em, se.
        /// </summary>
        Controle,
        /// <summary>
        /// Nome de variáveis, de atributos e membros
        /// </summary>
        Identificador,
        /// <summary>
        /// Literal de texto delimitado por aspas duplas Ex.:"Hello World"
        /// </summary>
        Texto,
        /// <summary>
        /// Símbolo de "dois pontos", ':'. Usado para atribuir valor a um atributo.
        /// </summary>
        Atribuidor,
        /// <summary>
        /// Símbolo de "ponto", '.'. Usado para acessar membros de um objeto.
        /// </summary>
        AcessoMembro,
        /// <summary>
        /// Números inteiros e decimais. Ex.: 42, 3.14, -7, 0.001
        /// </summary>
        Numero,
        /// <summary>
        /// Símbolo de "abre" e "fecha chaves", '{' e '}'. Usado para abrir e fechar um bloco de código.
        /// </summary>
        DelimitadorBloco,
        /// <summary>
        /// Símbolo de "abre" e "fecha parênteses", '(' e ')'. Usado para abrir e fechar um construtor de componente.
        /// </summary>
        DelimitadorConstrutor,
        /// <summary>
        /// Símbolo de "ponto e vírgula" e "virgula", ';' e ','. Usado para separar pares chave-valor de atributos em 
        /// bloco de código e separar argumentos dos atributos em construtores.
        /// </summary>
        Separador,
        /// <summary>
        /// Símbolo de "fim de arquivo" (invísvel), usado para indicar o final do código-fonte.
        /// </summary>
        FimDeArquivo,

        Erro,
    }
}
