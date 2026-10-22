namespace Sintatico.Nodes
{
    public class Texto : Node
    {
        public string Valor { get; set; } = "";
        public override string Descricao => $"(Texto) \"{Valor}\"";
    }
}
