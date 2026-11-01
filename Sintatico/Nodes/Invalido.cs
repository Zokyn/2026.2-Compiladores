namespace Sintatico.Nodes
{
    // Ocupa o lugar de um nó que não pôde ser reconhecido por erro sintático
    public class Invalido : Node
    {
        public override string Descricao => "(Inválido)";
        public override string ToString() => Descricao;
    }
}
