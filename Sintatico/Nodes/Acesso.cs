namespace Sintatico.Nodes
{
    /* 
     * <acesso>                    ::= IDENTIFICADOR <mais_acesso> 
     * <mais_acesso>               ::= <sufixo> <mais_acesso> | ε
     * <sufixo>                    ::= <sufixo_membro> | <sufixo_indice>
     * <sufixo_membro>             ::= ACESSO IDENTIFICADOR
     * <sufixo_indice>             ::= ABRE_TUPLA NUMERO_INTEIRO FECHA_TUPLA
     */ //Ex.: aluno.livros[0]
    // Ex.: aluno
    public class Acesso : Node
    {
        public string Identificador { get; set; } = "";
        public List<Sufixo> Sufixos { get; } = new();
        public override string Descricao =>
            $"(Acesso) {Identificador}";
    }
    public abstract class Sufixo : Node
    {
        public abstract string Texto { get; }
    }

    // Ex.: ".livros"
    public class SufixoMembro : Sufixo
    {
        public string Membro { get; set; } = "";
        public override string Texto => $".{Membro}";
        public override string Descricao => $"(Sufixo de Membro) {Texto}";
    }
    // Ex.: "[1]"

    public class SufixoIndice : Sufixo
    {
        public int Indice { get; set; }
        public override string Texto => $"[{Indice}]";
        public override string Descricao => $"(Sufixo de Índice) {Texto}";
    }
}
