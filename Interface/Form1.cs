using Lexico;

namespace Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvTokens.AutoGenerateColumns = true;
            dgvTokens.ReadOnly = true;
            dgvTokens.AllowUserToAddRows = false;
            dgvTokens.AllowUserToDeleteRows = false;
            dgvTokens.AllowUserToResizeRows = false;
            dgvTokens.RowHeadersVisible = false;
            dgvTokens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTokens.MultiSelect = false;
            dgvTokens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTokens.BackgroundColor = SystemColors.Window;
            dgvTokens.BorderStyle = BorderStyle.None;
            dgvTokens.EnableHeadersVisualStyles = false;
            dgvTokens.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 44, 52);
            dgvTokens.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTokens.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvTokens.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgvTokens.DefaultCellStyle.Font = new Font("Consolas", 10f);
            dgvTokens.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);

            dgvTokens.DataBindingComplete += (_, _) =>
            {
                if (dgvTokens.Columns["TipoEnum"] is { } c) c.Visible = false;
                dgvTokens.Columns["Id"]!.HeaderText = "#";
                foreach (var col in new[] { "Id", "Lexema", "Linha", "Coluna", "Comprimento" })
                    dgvTokens.Columns[col]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            };
        }

        public sealed class TokenLinha
        {
            public int Id { get; init; }
            public string Tipo { get; init; } = "";
            public string Lexema { get; init; } = "";
            public int Linha { get; init; }
            public int Coluna { get; init; }
            public int Comprimento { get; init; }
            public string Categoria { get; init; } = "";

            public TokenType TipoEnum { get; init; }
        }

        private void PreencherGridTokens(IReadOnlyList<Token> tokens)
        {
            var linhas = tokens.Select((t, i) => new TokenLinha
            {
                Id = i + 1,
                TipoEnum = t.Tipo,
                Lexema = t.Lexema,
                Linha = t.Posicao.Item1,
                Coluna = t.Posicao.Item2,
                Comprimento = t.Lexema.Length,
                Categoria = GetCategoriaByType(t.Tipo)
            }).ToList();

            dgvTokens.DataSource = linhas;
        }

        private static string GetCategoriaByType(TokenType tipo) => tipo switch
        {
            TokenType.Controle => "Palavra-chave de Controle",
            TokenType.Componente => "Palavra-chave de Componente",
            TokenType.Identificador => "Identificador",
            TokenType.Texto => "Literal",
            TokenType.Numero => "Número",
            TokenType.Separador => "Símbolo de Separador",
            TokenType.AcessoMembro => "Símbolo de Acesso a membro",
            TokenType.Atribuidor => "Símbolo de Atribuidor",
            TokenType.DelimitadorBloco => "Símbolo de Delimitador de bloco",
            TokenType.DelimitadorConstrutor => "Símbolo de Delimitador de construtor",
            TokenType.FimDeArquivo => "Token de Fim de arquivo",
            TokenType.Erro => "Erro",
            _ => throw new Exception("TokenType sem Categoria definida.")
        };

        private static string? EncontrarCaminhoSolucao()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (dir.GetFiles("*.sln").Length > 0)
                {
                    return dir.FullName;
                }
                dir = dir.Parent;
            }
            return null;
        }

        private void AbrirArquivo()
        {
            string? root = EncontrarCaminhoSolucao();

            using var dialog = new OpenFileDialog
            {
                Title = "Abrir código-fonte de entrada",
                InitialDirectory = root ?? Environment.CurrentDirectory,
                Filter = "Código-fonte Declara (*.dcl;*.txt)|*.dcl;*.txt|Todos os arquivos (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            txtFilePath.Text = dialog.FileName;

            txtCodeBlock.Text = File.ReadAllText(dialog.FileName)
                                    .Replace("\r\n", "\n")
                                    .Replace("\n", Environment.NewLine);

            var lexer = new Analisador(txtCodeBlock.Text);

            List<Token> tokens = lexer.Tokenizar();

            PreencherListaTokens(tokens);

            PreencherGridTokens(tokens);

        }


        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            AbrirArquivo();
        }
    }
}
