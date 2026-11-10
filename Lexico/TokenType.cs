using System.ComponentModel;

namespace Lexico
{
    public enum TokenType
    {
        [Description("Palavra-Chave Componente")]
        Componente,    // pagina, secao, cartao, botao, selo
        /// <summary>
        /// Palavra-Chave de Dados. ex: dados.
        /// </summary>
        [Description("Palavra-Chave Dados")]
        Dados,
        /// <summary>
        /// Palavras-Chave de Controle. ex: para, em, se, enquanto, sendo.
        /// </summary>
        [Description("Palavra-Chave de Controle")]
        Controle,
        /// <summary>
        /// Identificadores, nomes usados para referenciar componentes, 
        /// controles e dados. ex: nome, idade, endereco, telefone, email, etc.
        /// </summary>
        [Description("Identificador")]
        Identificador,  // controle, componente e dados
        /// <summary>
        /// Uma cadeia de texto literal, delimitado por aspas duplas.
        /// ex: "Alô, mamãe", "Hello World", "Hi"
        /// </summary>
        [Description("Texto Literal")]
        TextoLiteral,
        /// <summary>
        /// Um número real, podendo ser inteiro ou decimal, com ponto como 
        /// separador decimal. ex: 1.05, .5, 0.0, ...
        /// </summary>
        [Description("Numero Real")]
        NumeroReal,
        /// <summary>
        /// Um número inteiro. 
        /// ex: 1, 2, 0, -1, -2, ...
        /// </summary>
        [Description("Numero Inteiro")]
        NumeroInteiro,
        /// <summary>
        /// Símbolo de abertura de bloco construtor, representado por (.
        /// </summary>
        [Description("Abre Construtor")]
        AbreConstrutor,
        /// <summary>
        /// Símbolo de fechamento de bloco construtor, representado por ).
        /// </summary>
        [Description("Fecha Construtor")]
        FechaConstrutor,
        /// <summary>
        /// Símbolo de abertura de bloco de componente, representado por {.
        /// </summary>
        [Description("Abre Bloco")]
        AbreBloco,
        /// <summary>
        /// Símbolo de fechamento de bloco de componente, representado por }.
        /// </summary>
        [Description("Fecha Bloco")]
        FechaBloco,
        /// <summary>
        /// Símbolo de abertura de tupla, representado por [.
        /// </summary>
        [Description("Abre Tupla")]
        AbreTupla,
        /// <summary>
        /// Símbolo de fechamneto de tupla, representado por ].
        /// </summary>
        [Description("Fecha Tupla")]
        FechaTupla,
        /// <summary>
        /// Símbolo de atribuição, representado por :.
        /// </summary>
        [Description("Símbolo Atribuidor")]
        Atribuidor,
        /// <summary>
        /// Símbolo de separação de itens, representado por ,.
        /// </summary>
        [Description("Separador de Itens")]
        Separador,
        /// <summary>
        /// Símbolo de separação de blocos, representado por ;.
        /// </summary>
        [Description("Separador de Bloco")]
        SeparadorBloco,
        /// <summary>
        /// Símbolo de acesso a membros de um componente, representado por ..
        /// </summary>
        [Description("Símbolo de Acesso")]
        Acesso,
        /// <summary>
        /// Palavra-chave de booleano, podendo ser variações entre v, verdadeiro, verdade, f, falso, falsidade
        /// </summary>
        [Description("Palavra-Chave de Booleano")]
        Booleano,
        /// <summary>
        /// Símbolo de incremento, representado por ++.
        /// </summary>
        [Description("Incremento")]
        Incremento,
        /// <summary>
        /// Símbolo de decremento, representado por --.
        /// </summary>
        [Description("Decremento")]
        Decremento,
        /// <summary>
        /// Símbolo demarcador de Fim de Arquivo. Não possui representação no código-fonte, uma vez que é gerado
        /// pelo analisador léxico e inserido no final da lista de tokens. Demarca o final do arquivo-fonte.
        /// </summary>
        [Description("FimDeArquivo")]
        FimDeArquivo,
        /// <summary>
        /// Símbolo inicial de comentário, representado por >. O comentário é encerrado pelo final da linha, 
        /// ou seja, pelo caractere de nova linha \n ou delimitado por <<< >>> para comentários multilinhas. 
        /// </summary>
        [Description("Comentários")]
        Comentario,
        /// <summary>
        /// Especificamente o demarcador de quebra de linha.
        /// </summary>
        [Description("Quebra de Linha")]
        QuebraLinha,
        /// <summary>
        /// Espaços, tabulação, nova linha, retorno de carro e espaço em branco. Lidos como \t, e múltiplos ' '.
        /// </summary>
        [Description("Espaços")]
        Espacos,
        /// <summary>
        /// Denotador de erro. Usado apenas para debug do analisador léxico e levantamento de exceções.
        /// </summary>
        [Description("Erro")]
        Erro,
    }
    
}
