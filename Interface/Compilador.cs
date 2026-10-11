using Lexico;
using DotNetEnv;

namespace Interface
{
    public partial class Compilador : Form
    {
        private string _root { get; set; } = "./";
        private string _filename { get; set; } = "";
        private string _codigofonte { get; set; } = string.Empty;
        public Compilador()
        {
            Env.Load();
            InitializeComponent();
            ConfiguraCodeBlock();
            ConfigurarGrid();
            ConfiguraControles();
            //
            LeCodigoFonteLocal();
        }
        private void LeCodigoFonteLocal()
        {
            string? defaultPath = Environment.GetEnvironmentVariable("FILEPATH");
            
            if (defaultPath is null) return;
            
            _filename = defaultPath;
            
            List<Token> tokens = ProcessaArquivo();

            EscreveTextoCodigo();
            PreencherGridTokens(tokens);
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
        private void ConfiguraControles()
        {
            txtCurrentToken.TextAlign = HorizontalAlignment.Center;
            txtCurrentToken.Font = new Font("Segoe UI", 12f, FontStyle.Regular);

            lblCurrentTokenTypeLink.Font = new Font("Arial, sans serif", 14f, FontStyle.Bold);
            lblCurrentTokenTypeLink.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LimpaSelecaoGridTokens()
        {
            int? indexSelected = dgvTokens.CurrentRow?.Index;

            if (!indexSelected.HasValue) return;
            else 
                dgvTokens.Rows[indexSelected.Value].Selected = false;
        }
        private void EscreveTextoCodigo()
        {
            txtCodigo.Text = _codigofonte.Replace("\r\n", "\n")
                                        .Replace("\n", Environment.NewLine); // codigo formatado

            txtCodigo.Text = _codigofonte.Replace("\r\n", "\n")
                                        .Replace("\n", Environment.NewLine); // codigo formatado
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

            LimpaSelecaoGridTokens();
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

        private void AbrirExplorarCodigo(string? path = null)
        {
            if (path == null)
            {
                string? pathDaSolucao = EncontrarCaminhoSolucao();
                if (pathDaSolucao != null)
                    _root = pathDaSolucao;
                else
                    _root = Environment.CurrentDirectory;
            }
            else
                _root = path;

            //Abre OpenFileDialog para selecionar o arquivo de código-fonte 
            using var dialog = new OpenFileDialog
            {
                Title = "Abrir código-fonte de entrada",
                InitialDirectory = _root,
                Filter = "Código-fonte Declara (*.dcl;*.txt)|*.dcl;*.txt|Todos os arquivos (*.*)|*.*",
                RestoreDirectory = true
            };

            // Levanta exception se o Dialog não retornar OK, pois não é esperado outro comportamento
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                throw new Exception("Dialog teve um comportamento inesperado");
            }
            //Fechado o dialog temos um FileName atribuido a ele


            _filename = dialog.FileName; 

            _codigofonte = File.ReadAllText(_filename);

            txtFilePath.Text = _filename;
        }

        public List<Token> ProcessaArquivo()
        {
            var lexer = new AnalisadorLexico(_codigofonte);

            List<Token> tokens = lexer.Tokenizar();

            return tokens;
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

        private void btnAbrirCodigo_Click(object sender, EventArgs e)
        {
            AbrirExplorarCodigo();

            btnLimpaCodigo.Enabled = !(txtFilePath.Text == string.Empty);

            List<Token> tokens = ProcessaArquivo();

            EscreveTextoCodigo();
            PreencherGridTokens(tokens);
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
            AbrirExplorarCodigo();
            List<Token> tokens = ProcessaArquivo();

            EscreveTextoCodigo();
            PreencherGridTokens(tokens);
        }

        private void btnCodigoClean_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtFilePath.Text = string.Empty;
            dgvTokens.DataSource = null;
            txtCurrentToken.Text = string.Empty;
            txtCurrentToken.Enabled = false;
            lblCurrentTokenTypeLink.Text = string.Empty;

            btnLimpaCodigo.Enabled = !(txtFilePath.Text == string.Empty);

        }

        private void dgvTokens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtCodigo.SelectionLength > 0) return;

            txtCurrentToken.Text = dgvTokens.CurrentRow?.Cells["Lexema"].Value?.ToString() ?? "";
            lblCurrentTokenTypeLink.Text = dgvTokens.CurrentRow?.Cells["Tipo"].Value?.ToString() ?? "";
        }
    }
}
