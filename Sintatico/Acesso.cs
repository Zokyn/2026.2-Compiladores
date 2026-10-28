namespace Sintatico.Nodes
{
    /* 
     * <acesso>                    ::= IDENTIFICADOR <mais_acesso> 
     * <mais_acesso>               ::= <sufixo> <mais_acesso> | ε
     * <sufixo>                    ::= <sufixo_membro> | <sufixo_indice>
     * <sufixo_membro>             ::= ACESSO IDENTIFICADOR
     * <sufixo_indice>             ::= ABRE_TUPLA NUMERO_INTEIRO FECHA_TUPLA
     */ //Ex.: aluno.livros[0]
    public abstract class Acesso : Node { }
    // Ex.: aluno
    public class AcessoOrigem : Acesso
    {
        public string Valor { get; set; } = "";
        public override string Descricao => $"(Acesso) {Valor}";
    }
    // Ex.: ".livros"
    public class AcessoMembro : Acesso
    {
        public Acesso Origem { get; set; } // O que vem antes do ponto
        public string Membro { get; set; } // O que fica depois do ponto
        public override string Descricao => $"(Acesso de Membro) {Origem.Descricao}.{Membro}";
    }
    // Ex.: "[1]"
    public class AcessoIndice : Acesso
    {
        public Acesso Colecao { get; set; } // O que vem antes do colchetes
        public int Indice { get; set; }     // O que fica entre os colchetes
        public override string Descricao => $"(Acesso de Índice) {Colecao.Descricao}[{Indice}]";
    }
}
