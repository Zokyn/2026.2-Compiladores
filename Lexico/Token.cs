namespace Lexico
{
    public class Token(TokenType tipo, string lexema, string valor, int posicao, (int, int) linhaColuna)
    {
        /// <summary>Tipo do Token, dado o enum <see cref="TokenType">TokenType</see>.</summary>
        public TokenType Tipo { get; } = tipo;
        /// <summary>Cadeia de caracteres retirada do código-fonte (entrada).</summary>
        public string Lexema { get; } = lexema;

        /// <summary>Valor processado do token, quando aplicável, podendo ser 
        /// o valor de texto sem as aspas delimitadoras, número inteiros ou só
        /// o próprio lexema. É a parte relevante para o compilador.</summary>
        public string Valor { get; } = valor;

        /// <summary>
        /// Par-ordenado da posição (linha e coluna) da declaração do token.
        /// </summary>
        public (int linha, int coluna) LinhaColuna { get; } = linhaColuna;
        public int Linha => LinhaColuna.linha;
        public int Coluna => LinhaColuna.coluna;
        public int Posicao { get; } = posicao;

        public override string ToString()
        {
            return $"({Tipo}) '{Valor}' {GetLinhaColunaString()}";
        }

        private string GetLinhaColunaString()
        {
            return $"[{LinhaColuna.Item1},{LinhaColuna.Item2}]";
        }

        private string GetPosicaoExtensaString()
        {
            return $"Linha {LinhaColuna.Item1}, Coluna {LinhaColuna.Item2}";
        }
        private string ToExtensaString()
        {
            return $"Token {Tipo}: {Valor} em {GetPosicaoExtensaString()}.";
        }
    }
}

