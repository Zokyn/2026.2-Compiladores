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
            ConfiguraCodeBlock();
            ConfigurarGrid();

            /*string? defaultPath = Environment.GetEnvironmentVariable("FILEPATH");
            if (defaultPath is null) return;
            ProcessaArquivo(defaultPath);*/
        }

        private void ConfiguraCodeBlock()
        {
            txtCodigo.BackColor = Color.FromArgb(40, 44, 52);
            txtCodigo.ForeColor = Color.FromArgb(171, 178, 191);
            txtCodigo.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            txtCodigo.TextAlign = HorizontalAlignment.Left;
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
            var linhas = tokens.Select((t, i) => new TokenItem
            {
                Id = i + 1,
                Tipo = Utils.GetNomeByType(t.Tipo),
                Lexema = t.Lexema,
                Linha = t.Posicao.Item1,
                Coluna = t.Posicao.Item2,
                Comprimento = t.Lexema.Length,
                Categoria = Utils.GetCategoriaByType(t.Tipo),
                TipoEnum = t.Tipo
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

        private void AbrirCodigoFonte(string? path = null)
        {
            string root;
            if (path == null)
            {
                string? pathDaSolucao = EncontrarCaminhoSolucao();
                if (pathDaSolucao != null)
                    root = pathDaSolucao;
                else
                    root = Environment.CurrentDirectory;
            }
            else
                root = path;

            using var dialog = new OpenFileDialog
            {
                Title = "Abrir código-fonte de entrada",
                InitialDirectory = root,
                Filter = "Código-fonte Declara (*.dcl;*.txt)|*.dcl;*.txt|Todos os arquivos (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                throw new Exception("Dialog teve um comportamento inesperado");
            }

            ProcessaArquivo(dialog.FileName);
        }

        public void ProcessaArquivo(string fileName)
        {
            txtFilePath.Text = fileName;

            txtCodigo.Text = File.ReadAllText(fileName)
                                    .Replace("\r\n", "\n")
                                    .Replace("\n", Environment.NewLine);

            var lexer = new AnalisadorLexico(txtCodigo.Text);

            List<Token> tokens = lexer.Tokenizar();

            PreencherGridTokens(tokens);
        }

        private void SelecionarTokenNoGrid(int index)
        {
            if (dgvTokens.DataSource is not List<TokenItem> tokens ||
                tokens.Count == 0) return;

            int linhaIndex = txtCodigo.GetLineFromCharIndex(index);
            int linha = linhaIndex + 1;
            int coluna = index - txtCodigo.GetFirstCharIndexFromLine(linhaIndex) + 1;

            int pos = tokens.FindIndex(t =>
                t.Linha == linha &&
                coluna >= t.Coluna &&
                coluna < t.Coluna + t.Comprimento);

            if (pos < 0) return;

            dgvTokens.ClearSelection();
            dgvTokens.Rows[pos].Selected = true;
            dgvTokens.CurrentCell = dgvTokens.Rows[pos].Cells[0];
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            AbrirCodigoFonte();
        }
        private void txtCodigo_MouseUp(object sender, MouseEventArgs e)
        {
            if (txtCodigo.SelectionLength > 0) return;

            int index = txtCodigo.GetCharIndexFromPosition(e.Location);
            SelecionarTokenNoGrid(index);
        }

        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtCodigo.SelectionLength > 0) return;
            SelecionarTokenNoGrid(txtCodigo.GetCharIndexFromPosition(e.Location));
            txtCurrentToken.Text = dgvTokens.CurrentRow?.Cells["Lexema"].Value?.ToString() ?? "";
            lblCurrentTokenTypeLink.Text = dgvTokens.CurrentRow?.Cells["Tipo"].Value?.ToString() ?? "";
        }

        private void smiEditarilePath_Click(object sender, EventArgs e)
        {
            using (var dlg = new frmOpenFileReader())
            {

                if (!string.IsNullOrEmpty(dlg.FileName))
                    ProcessaArquivo(dlg.FileName);
            }
        }

        private void btnCodigoClean_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtFilePath.Text = string.Empty;
            dgvTokens.DataSource = null;
        }
    }
}
