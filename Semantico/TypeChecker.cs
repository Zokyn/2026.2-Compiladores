using Lexico;
using Sintatico;
using Sintatico.Nodes;

namespace Semantico
{
    public class TypeChecker 
    {
        private Node _noAtual = null;
        public List<Simbolo> ListaSimbolos { get; } = new List<Simbolo>();
        public List<ErroSemantico> Erros { get; } = new List<ErroSemantico>();
        public void Analisar(Documento documento)
        {
            // Lê o no raiz da árvore 
            LerDeclaracoes(documento);
            foreach (ErroSemantico erro in Erros)
            {
                Console.WriteLine($"Erro Semântico: {erro.Titulo} - {erro.Mensagem} (Linha: {erro.Linha}, Coluna: {erro.Coluna})");
            }
            foreach (Simbolo simbol in ListaSimbolos)
            {
                Console.WriteLine(simbol.Identificador + " : " + simbol.Tipo);
            }
        }
        public void LerDeclaracoes(Documento documento)
        {
            // REGRA DA DECLARA: O primeiro filho deve ser as declarações
            foreach (Node filho in documento.Filhos)
            {
                if (filho is not Declaracoes) Erros.Add(new ErroSemantico("Primeiro nó deve ser uma declaração", "O primeiro nó do documento deve ser restrito às declarações.", filho.Linha, filho.Coluna));

                var declaracoes = filho as Declaracoes;

                if (declaracoes is null || declaracoes.ListaDeclaracao.Count <= 0) return;

                foreach (Declaracao declaracao in declaracoes.ListaDeclaracao)
                {
                    _noAtual = declaracao.Definicao;
                    switch (_noAtual)
                    {
                        case ConjuntoDados conjunto:
                            break;
                        case Construtor construtor:
                            break;
                        case Valor valor:
                            _noAtual = valor;
                            // Valor literal, não possui identificador 
                            if (valor.Construtor != null)
                            {
                                Erros.Add(new ErroSemantico("Declaração não pode ser construtor", $"A declaração '{declaracao.Descricao}' não pode ser um construtor.", valor.Linha, valor.Coluna));
                                break;
                            }
                            if (valor.Acesso != null)
                            {
                                Erros.Add(new ErroSemantico("Declaração não poder ser acesso", $"A declaração '{declaracao.Descricao}' não pode ser um acesso.", valor.Linha, valor.Coluna));
                                break;
                            }
                            if (valor.Construtor == null && valor.Acesso == null && valor.Conteudo == null)
                            {
                                Erros.Add(new ErroSemantico("Declaracao não possui conteúdo", $"A declaração '{declaracao.Descricao}' não possui um conteúdo válido.", valor.Linha, valor.Coluna));
                                break;
                            }

                            try 
                            {
                                var tipo = GetPrimitivoFromLiteral(valor.Conteudo);
                                ListaSimbolos.Add(
                                    new Simbolo(declaracao.Identificador, Primitivo.Texto, valor.Linha, valor.Coluna));
                                break;
                            }
                            catch (ErroSemantico e)
                            {
                                Erros.Add(new ErroSemantico("Declaração não possui um tipo válido", $"A declaração '{declaracao.Descricao}' não possui um tipo válido: {e.Titulo}", valor.Linha, valor.Coluna));
                                break;
                            }

                    }
                }
            }
        }
        public Primitivo GetPrimitivoFromLiteral(Literal literal)
        {
            return GetPrimitivoFromTokenType(literal.Tipo);
        }
        public Primitivo GetPrimitivoFromToken(Token token)
        {
            return GetPrimitivoFromTokenType(token.Tipo);
        }
        public Primitivo GetPrimitivoFromTokenType(TokenType type) => type switch
        {
            TokenType.TextoLiteral => Primitivo.Texto,
            TokenType.NumeroInteiro => Primitivo.Inteiro,
            TokenType.NumeroReal => Primitivo.Real,
            TokenType.Booleano => Primitivo.Booleano,
            _ => throw new ErroSemantico($"TokenType não encontrado", "", _noAtual.Linha, _noAtual.Coluna)
        };

    }

    public class Simbolo(string identificador, Primitivo tipo, int linha, int coluna)
    {
        public string Identificador { get; } = identificador;
        public Primitivo Tipo { get; } = tipo;
        public int Linha { get; } = linha;
        public int Coluna { get; } = coluna;
    }
    public enum Primitivo
    {
        Texto,
        Inteiro,
        Real,
        Booleano,
        Componente,
        ErroSemantico
    }
}
