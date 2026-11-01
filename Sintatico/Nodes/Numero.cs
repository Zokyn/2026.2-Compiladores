namespace Sintatico.Nodes
{
    // NUMERO_INTEIRO | NUMERO_REAL
    public class Numero : Node
    {
        public string Valor { get; set; } = "";
        public bool Real { get; set; }
        public override string Descricao => Real ? $"(Número Real) {Valor}f" : $"(Número Inteiro) {Valor}";
    }
}
