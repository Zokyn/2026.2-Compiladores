using DotNetEnv;
using Lexico;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
            ConfiguraGrid();
            ConfiguraControles();
            //
            LeCodigoFonteLocal();
        }
        private void LeCodigoFonteLocal()
        {
            string? defaultPath = Environment.GetEnvironmentVariable("FILEPATH");

            if (defaultPath is null) return;

            _filename = defaultPath;

            LeArquivo(_filename);

            List<Token>? tokens = ProcessaArquivo();

            if (!string.IsNullOrEmpty(_codigofonte)) EscreveTextoCodigo();

            if (tokens != null && tokens?.Count > 0) PreencheGridTokens(tokens);
        }
        private void ConfiguraCodeBlock()
        {
            txtCodigo.BackColor = Color.FromArgb(40, 44, 52);
            txtCodigo.ForeColor = Color.FromArgb(171, 178, 191);
            txtCodigo.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            txtCodigo.TextAlign = HorizontalAlignment.Left;
        }
        private void ConfiguraGrid()
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
        
        // EXPLORAÇÃO E LEITURA DE ARQUIVO
        private static string? EncontraCaminhoSolucao()
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
        private void LeArquivo(string filename)
        {
            _codigofonte = File.ReadAllText(filename);
        }
        /// <summary>
        /// Abre um OpenFileDialog que serve para selecionar o arquivo de código-fonte de entrada. 
        /// O caminho inicial do dialog é definido como a raiz do projeto, que buscada pelo método
        /// EncontrarCaminhoSolucao e mesmo que não seja encontrada, é usada o diretório atual.
        /// O ideal é que os códigos exemplos sempre estejam na pasta raiz do projeto, para que o 
        /// dialog abra no caminho correto.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private bool AbreExplorarCodigo()
        {
            string? pathDaSolucao = EncontraCaminhoSolucao();

            if (pathDaSolucao != null)
                _root = pathDaSolucao;
            else
                _root = Environment.CurrentDirectory;

            //Abre OpenFileDialog para selecionar o arquivo de código-fonte 
            using var dialog = new OpenFileDialog
            {
                Title = "Abrir código-fonte de entrada",
                InitialDirectory = _root,
                Filter = "Código-fonte Declara (*.dcl;*.txt)|*.dcl;*.txt|Todos os arquivos (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != DialogResult.OK) return false;

            //Fechado o dialog temos um FileName atribuido a ele
            _filename = dialog.FileName;

            LeArquivo(_filename);

            txtFilePath.Text = _filename;

            return true;
        }
        /// <summary>
        /// Uma vez preenchido o _codigofonte com conteúdo do arquivo,
        /// o analisador léxico retorna uma lista de tokens serve como entrada para
        /// os controles da interface e para, posteriormente, o analisador sintático.
        /// </summary>
        /// <returns>Lista de tokens recuperadas a partir do _codigofonte</returns>
        public List<Token>? ProcessaArquivo()
        {
            if (string.IsNullOrEmpty(_codigofonte)) return null;

            var lexer = new AnalisadorLexico(_codigofonte);

            List<Token> tokens = lexer.Tokenizar();

            return tokens;
        }

        // PREENCHIMENTO DE CONTROLES E RESPOSIVIDADE
        /// <summary>Busca o index da linha selecionada (se tiver) e a partir dela, apaga a seleção.</summary>
        private void LimpaSelecaoGridTokens()
        {
            int? indexSelected = dgvTokens.CurrentRow?.Index;

            if (!indexSelected.HasValue) return;
            else
                dgvTokens.Rows[indexSelected.Value].Selected = false;
        }
        /// <summary>Formata o código fonte (se tiver) e insere no txtCodigo.</summary>
        private void EscreveTextoCodigo()
        {
            if (string.IsNullOrEmpty(_codigofonte)) return;

            txtCodigo.Text = _codigofonte.Replace("\r\n", "\n")
                                        .Replace("\n", Environment.NewLine); // codigo formatado
        }
        /// <summary>
        /// A partir de uma lista IReadOnlyList de tokens (se tiver), trata os tokens e os 
        /// converte para TokenItem, um objeto com campos tratados para string prontos para 
        /// serem exibidos como items do DataSource do DataViewGrid dgvTokens. Após inserido
        /// todos os itens, limpa a seleção do grid para evitar que fique descompassado com 
        /// os demais controles da interface.
        /// </summary>
        /// <param name="tokens">Lista read-only preenchida ou não de Tokens a serem convertidos para TokenLinha.</param>
        private void PreencheGridTokens(IReadOnlyList<Token>? tokens)
        {
            if (tokens == null || tokens.Count < 0) return; 

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
        /// <summary>
        /// A partir do index (posição em caracteres) do lexema do Token no bloco de texto
        /// do código-fonte (txtCodigo) encontra o indíce na lista de tokens e utiliza desse
        /// indíce para selecionar o token no DataGridView dgvTokens, já que as linhas são 
        /// imútaveis e não intercaláveis.
        /// </summary>
        /// <param name="index"></param>
        private void SelecionaTokenNoGrid(int index)
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

        // EVENTOS 
        /// <summary>Abre o Explorar de Arquivos pelo SubMenuItem smiEditarFilename.</summary>
        private void smiEditarFilename_Click(object sender, EventArgs e)
        {
            if(!AbreExplorarCodigo()) return;
            List<Token>? tokens = ProcessaArquivo();

            EscreveTextoCodigo();
            PreencheGridTokens(tokens);
        }
        /// <summary>Abre o Explorar de Arquivos pelo Botão btnAbrirCodigo.</summary>
        private void btnAbrirCodigo_Click(object sender, EventArgs e)
        {
            if(!AbreExplorarCodigo()) return;

            btnLimpaCodigo.Enabled = !(txtFilePath.Text == string.Empty);

            List<Token>? tokens = ProcessaArquivo();

            EscreveTextoCodigo();
            PreencheGridTokens(tokens);
        }
        /// <summary>Limpa o Código da interface clicando no Botão btnLimpaCodigo.</summary>
        private void btnLimpaCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtFilePath.Text = string.Empty;
            dgvTokens.DataSource = null;
            txtCurrentToken.Text = string.Empty;
            txtCurrentToken.Enabled = false;
            lblCurrentTokenTypeLink.Text = string.Empty;

            btnLimpaCodigo.Enabled = !(txtFilePath.Text == string.Empty);

        }
        /// <summary>Seleciona token pelo clique em linha do DataGridView dgvTokens.</summary>
        private void dgvTokens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtCodigo.SelectionLength > 0) return;

            txtCurrentToken.Text = dgvTokens.CurrentRow?.Cells["Lexema"].Value?.ToString() ?? "";
            lblCurrentTokenTypeLink.Text = dgvTokens.CurrentRow?.Cells["Tipo"].Value?.ToString() ?? "";
        }
        /// <summary>
        /// Ao soltar o clique no bloco de código, se não estiver selecionando nada, 
        /// o index (posição em caracateres) é passado, a partir da posição
        /// do mouse utilizando GetCharIndexFromPosition(), e seleciona o Token
        /// no DataGridView dgvTokens.
        /// </summary>
        private void txtCodigo_MouseUp(object sender, MouseEventArgs e)
        {
            if (txtCodigo.SelectionLength > 0) return;

            SelecionaTokenNoGrid(txtCodigo.GetCharIndexFromPosition(e.Location));
        }
        /// <summary>
        /// Ao clicar no bloco de código, se não estiver selecionando nada, 
        /// o index (posição em caracateres) é passado, a partir da posição
        /// do mouse utilizando GetCharIndexFromPosition(), e seleciona o Token
        /// no DataGridView dgvTokens, e atribui o Lexema e Tipo aos campos de texto.
        /// </summary>
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtCodigo.SelectionLength > 0) return;

            SelecionaTokenNoGrid(txtCodigo.GetCharIndexFromPosition(e.Location));
            txtCurrentToken.Text = dgvTokens.CurrentRow?.Cells["Lexema"].Value?.ToString() ?? "";
            lblCurrentTokenTypeLink.Text = dgvTokens.CurrentRow?.Cells["Tipo"].Value?.ToString() ?? "";
        }
    }
}
