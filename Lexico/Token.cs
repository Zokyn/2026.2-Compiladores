namespace Lexico
{
    public class Token
    {
        /// <summary>Tipo do Token, dado o enum <see cref="TokenType">TokenType</see>.</summary>
        public TokenType Tipo { get; }
        /// <summary>Cadeia de caracteres retirada do código-fonte (entrada).</summary>
        public string Lexema { get; }

        /// <summary>Valor processado do token, quando aplicável, podendo ser 
        /// o valor de texto sem as aspas delimitadoras, número inteiros ou só
        /// o próprio lexema. É a parte relevante para o compilador.</summary>
        public string Valor { get; }

        /// <summary>
        /// Par-ordenado da posição (linha e coluna) da declaração do token.
        /// </summary>
        public (int, int) Posicao { get; }
        public int Linha => Posicao.Item1;
        public int Coluna => Posicao.Item2;

        public Token(TokenType tipo, string lexema, string valor, (int, int) posicao)
        {
            Tipo = tipo;
            Lexema = lexema;
            Valor = valor;
            Posicao = posicao;
        }

        public override string ToString()
        {
            return $"({Tipo}) '{Valor}' {GetPosicaoString()}";
        }

        private string GetPosicaoString()
        {
            return $"[{Posicao.Item1},{Posicao.Item2}]";
        }

        private string GetPosicaoExtensaString()
        {
            return $"Linha {Posicao.Item1}, Coluna {Posicao.Item2}";
        }
        private string ToExtensaString()
        {
            return $"Token {Tipo}: {Valor} em {GetPosicaoExtensaString()}.";
        }
    }
}

