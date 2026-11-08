namespace Semantico
{
    public class AnalisadorSemantico 
    {
        public TabelaSimbolos SymbolTable { get; } = new TabelaSimbolos();
        public List<ErroSemantico> Erros { get; } = new List<ErroSemantico>();

        //public void Visite;
    }

    public class TabelaSimbolos
    {
        public List<Simbolo> listaSimbolos { get; } = new List<Simbolo>();
    }

    public class Simbolo
    {

    }
}
