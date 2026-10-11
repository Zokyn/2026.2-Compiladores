using Lexico;
using DotNetEnv;

namespace Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Env.Load();
            InitializeComponent();
            ConfigurarGrid();

            string? defaultPath = Environment.GetEnvironmentVariable("FILEPATH");
            if (defaultPath is null) return;
            ProcessaArquivo(defaultPath);
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
                Categoria = Utils.GetCategoriaByType(t.Tipo)
            }).ToList();

            dgvTokens.DataSource = linhas;
        }

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
                throw new Exception("Dialog teve um comportamento inesperado");
            }

            ProcessaArquivo(dialog.FileName);
        }

        private void ProcessaArquivo(string fileName) 
        {
            txtFilePath.Text = fileName;

            txtCodeBlock.Text = File.ReadAllText(fileName)
                                    .Replace("\r\n", "\n")
                                    .Replace("\n", Environment.NewLine);

            var lexer = new AnalisadorLexico(txtCodeBlock.Text);

            List<Token> tokens = lexer.Tokenizar();

            PreencherGridTokens(tokens);
        }


        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            AbrirArquivo();
        }
    }
}
