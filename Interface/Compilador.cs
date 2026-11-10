using DotNetEnv;
using Lexico;
using Sintatico;
using Sintatico.Nodes;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace Interface
{
    public partial class Compilador : Form
    {
        private string _root { get; set; } = "./";
        private string _filename { get; set; } = "";
        private string _codigofonte { get; set; } = string.Empty;
        private List<Token>? _tokens { get; set; } = null;
        private ResultadoParser? _result { get; set; } = null;

        public List<ErroSintatico>? ListaErros => _result?.Erros;
        public static string[] ColumnNames = {
                "Id", "Tipo", "Posicao", "Lexema", "Inicio", "Fim", "Comprimento", "Linha", "Coluna",
            };
        public static ListViewGroup group = new ListViewGroup(key: "ErrosSintaticos", headerText: "Erros Sintáticos");
        public static int Offset = 4;
        public Compilador()
        {
            Env.Load();
            InitializeComponent();
            ConfiguraCodeBlock();
            ConfiguraGridTokens();
            ConfiguraTokensLabels();
            ConfiguraListaErrosSintaticos();
            //
            LeCodigoFonteLocal();
            PreencheEstruturasControle();
        }
        private void LeCodigoFonteLocal()
        {
            string? defaultPath = Environment.GetEnvironmentVariable("FILEPATH");

            if (defaultPath is null) return;

            _filename = defaultPath;

            _codigofonte = File.ReadAllText(_filename);

            List<Token>? tokens = ProcessaArquivo();

            if (tokens != null && tokens?.Count > 0) _tokens = tokens;
        }
        private void ConfiguraCodeBlock()
        {
            txtCodigo.BackColor = Color.FromArgb(40, 44, 52);
            txtCodigo.ForeColor = Color.FromArgb(171, 178, 191);
            txtCodigo.Font = new Font("Segoe UI", 10f, FontStyle.Bold);

        }
        private void ConfiguraGridTokens()
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
                foreach (var col in ColumnNames)
                {
                    if (!dgvTokens.Columns.Contains(col)) continue;

                    dgvTokens.Columns[col]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            };
        }
        private void ConfiguraTokensLabels()
        {
            txtCurrentToken.TextAlign = HorizontalAlignment.Center;
            txtCurrentToken.Font = new Font("Segoe UI", 12f, FontStyle.Regular);

            lblCurrentTokenTypeLink.Font = new Font("Arial, sans serif", 14f, FontStyle.Bold);
            lblCurrentTokenTypeLink.BorderStyle = BorderStyle.FixedSingle;
        }
        private void ConfiguraListaErrosSintaticos()
        {
            lstErrosSintaticos.Items.Clear();
            lstErrosSintaticos.Columns.Clear();
            lstErrosSintaticos.Groups.Add(group);
            lstErrosSintaticos.Columns.Add("Titulo", 480, HorizontalAlignment.Left);
            lstErrosSintaticos.Columns.Add("Linha", 60, HorizontalAlignment.Center);
            lstErrosSintaticos.Columns.Add("Coluna", 60, HorizontalAlignment.Center);

            lstErrosSintaticos.View = View.Details;
            lstErrosSintaticos.FullRowSelect = true;
            lstErrosSintaticos.GridLines = true;
            lstErrosSintaticos.ShowGroups = true;
            lblErroSintaticoList.Text = "";
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

            _codigofonte = File.ReadAllText(_filename);

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

            var lexer = new Lexer(_codigofonte);

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
            int totalLinhas = txtCodigo.Lines.Length;

            if (totalLinhas == 0) totalLinhas = 1;

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < totalLinhas; i++)
            {
                sb.AppendLine(i.ToString());
            }

            rtbCodigoLines.Text = sb.ToString();
        }
        /// <summary>
        /// A partir de uma lista IReadOnlyList de tokens (se tiver), trata os tokens e os 
        /// converte para TokenItem, um objeto com campos tratados para string prontos para 
        /// serem exibidos como items do DataSource do DataViewGrid dgvTokens. Após inserido
        /// todos os itens, limpa a seleção do grid para evitar que fique descompassado com 
        /// os demais controles da interface.
        /// </summary>
        /// <param name="tokens">Lista read-only preenchida ou não de Tokens a serem convertidos para TokenLinha.</param>
        private void PreencheGridTokens()
        {
            dgvTokens.Rows.Clear();

            if (_tokens == null || _tokens.Count < 0) return;

            var linhas = _tokens.Select((t, i) => new TokenItem
            {
                Id = i + 1,
                Posicao = t.Posicao,
                Tipo = Utils.GetNomeByTokenType(t.Tipo, t.Linha, t.Coluna),
                Lexema = t.Lexema,
                Linha = t.Linha,
                Coluna = t.Coluna,
                Comprimento = t.Lexema.Length,
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

        // Analise Sintática
        private static TreeNode ConverterNodeParaTreeNode(Node node)
        {
            var treeNode = new TreeNode(node.Descricao);

            treeNode.Tag = node;
            foreach (var child in node.Filhos)
            {
                treeNode.Nodes.Add(ConverterNodeParaTreeNode(child));
            }

            return treeNode;

        }

        private void PreencheArvoreSintatica()
        {
            bool preencheuArvore = false;
            try
            {
                if (_tokens == null || _tokens.Count == 0) return;

                Parser p = new Parser(_tokens);
                ResultadoParser result = p.Parse();

                Node? raiz = result.Arvore;

                _result = result;
                treeParser.BeginUpdate();
                treeParser.Nodes.Clear();

                if (raiz != null)
                {
                    treeParser.Nodes.Add(ConverterNodeParaTreeNode(raiz));
                    treeParser.ExpandAll();
                }

                treeParser.EndUpdate();
                preencheuArvore = true;
            }
            catch (ErroSintatico es)
            {
                Console.WriteLine($"Erro sintático: {es.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
            finally
            {
                treeParser.Visible = preencheuArvore;
                lblTreeNaoCarregada.Visible = !preencheuArvore;
            }

        }

        private void ListaErrosSintaticos()
        {
            if (_result == null) return;

            List<ErroSintatico> erros = _result.Erros;

            int count = 0;
            foreach (ErroSintatico erro in erros)
            {
                ListViewItem lstItem = new ListViewItem();
                count++;
                lstItem.SubItems.Add(new ListViewSubItem(lstItem, count.ToString()));
                lstItem.SubItems.Add(new ListViewSubItem(lstItem, erro.Linha.ToString()));
                lstItem.SubItems.Add(new ListViewSubItem(lstItem, erro.Coluna.ToString()));

                lstItem.Group = group;
                lstItem.Text = erro.Titulo;
                lstErrosSintaticos.Items.Add(lstItem);
            }

            switch (erros.Count)
            {
                case 0:
                    lblErroSintaticoList.Text = "Nenhum erros sintáticos";
                    break;
                case 1:
                    lblErroSintaticoList.Text = "Erro sintático";
                    break;
                default:
                    lblErroSintaticoList.Text = $"{erros.Count} erros sintáticos";
                    break;
            }
        }

        private void PreencheEstruturasControle()
        {
            if (_tokens != null)
            {
                EscreveTextoCodigo();
                PreencheGridTokens();
                PreencheArvoreSintatica();
                ListaErrosSintaticos();
            }
            else
            {
                dgvTokens.DataSource = null;
                treeParser.Nodes.Clear();
                lstErrosSintaticos.Items.Clear();
            }
        }

        // EVENTOS 
        /// <summary>Abre o Explorar de Arquivos pelo SubMenuItem smiEditarFilename.</summary>
        private void smiEditarFilename_Click(object sender, EventArgs e)
        {
            _tokens = null;
            if (!AbreExplorarCodigo()) return;
            _tokens = ProcessaArquivo();

            PreencheEstruturasControle();
        }
        /// <summary>Abre o Explorar de Arquivos pelo Botão btnAbrirCodigo.</summary>
        private void btnAbrirCodigo_Click(object sender, EventArgs e)
        {
            if (!AbreExplorarCodigo()) return;

            btnLimpaCodigo.Enabled = !(txtFilePath.Text == string.Empty);

            _tokens = ProcessaArquivo();

            PreencheEstruturasControle();
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

        private void dgvTokens_SelectionChanged(object sender, EventArgs e)
        {
            txtCodigo.HideSelection = false;

            if (dgvTokens.CurrentRow is null) return;

            var token = _tokens[dgvTokens.CurrentRow.Index];

            if (token is not null)
            {
                txtCurrentToken.Text = token.Lexema;
                lblCurrentTokenTypeLink.Text = Utils.GetNomeByTokenType(token.Tipo);

                int? startPosition = token.Posicao;
                int? comprimento = token.Lexema.Length;

                if (!startPosition.HasValue || !comprimento.HasValue) return;
                txtCodigo.Select(startPosition.Value, comprimento.Value);
                dgvTokens.Focus();
            }

        }

        private void treeParser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Node)
            {
                lblParserItemColuna.Text = $"{((Node)e.Node.Tag).Coluna}";
                lblParserItemLinha.Text = $"{((Node)e.Node.Tag).Linha}";
                lblParserItemDescricao.Text = $"{((Node)e.Node.Tag).Descricao}";
            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstErrosSintaticos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaErros == null || ListaErros.Count <= 0)
            {
                txtErroBox.Text = "Nenhum Erro Sintático";
                return;
            }

            if (lstErrosSintaticos.SelectedItems.Count <= 0)
            {
                txtErroBox.Text = "Nenhum Erro Selecionado";
                return;
            }
            
            int index = lstErrosSintaticos.SelectedItems[0].Index;

            string conteudoTexto = ListaErros[index].Titulo + "\n\t" + ListaErros[index].Message;

            int posicao = ListaErros[index].Posicao;

            // Atribui erro sintático ao textBox 
            txtErroBox.Text = conteudoTexto;
            // Seleciona posição do erro no txtCodigo
            txtCodigo.Select(posicao, 1);
        }
    }
}
