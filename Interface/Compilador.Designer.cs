namespace Interface
{
    partial class Compilador
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            flpCodigoContainer = new FlowLayoutPanel();
            rtbCodigoLines = new RichTextBox();
            txtCodigo = new RichTextBox();
            flwFileOpen = new FlowLayoutPanel();
            btnAbrirCodigo = new Button();
            txtFilePath = new TextBox();
            btnLimpaCodigo = new Button();
            tabEtapas = new TabControl();
            tabLexico = new TabPage();
            tlpLexerMain = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtCurrentToken = new TextBox();
            lblCurrentTokenTypeLink = new LinkLabel();
            lblTokenAtual = new Label();
            dgvTokens = new DataGridView();
            tabSintatico = new TabPage();
            splitContainer2 = new SplitContainer();
            flowLayoutPanel1 = new FlowLayoutPanel();
            treeParser = new TreeView();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblParserItemLinha = new Label();
            lblParserItemColuna = new Label();
            lblParserItemDescricao = new Label();
            lblTreeNaoCarregada = new Label();
            flpListErros = new FlowLayoutPanel();
            lblErroSintaticoList = new Label();
            lstErrosSintaticos = new ListView();
            txtErroBox = new TextBox();
            menuStrip1 = new MenuStrip();
            smiEditar = new ToolStripMenuItem();
            smiEditarFilename = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flpCodigoContainer.SuspendLayout();
            flwFileOpen.SuspendLayout();
            tabEtapas.SuspendLayout();
            tabLexico.SuspendLayout();
            tlpLexerMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).BeginInit();
            tabSintatico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flpListErros.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(flpCodigoContainer);
            splitContainer1.Panel1.Controls.Add(flwFileOpen);
            splitContainer1.Panel1MinSize = 450;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabEtapas);
            splitContainer1.Size = new Size(1120, 426);
            splitContainer1.SplitterDistance = 450;
            splitContainer1.TabIndex = 7;
            // 
            // flpCodigoContainer
            // 
            flpCodigoContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            flpCodigoContainer.AutoSize = true;
            flpCodigoContainer.Controls.Add(rtbCodigoLines);
            flpCodigoContainer.Controls.Add(txtCodigo);
            flpCodigoContainer.Location = new Point(0, 0);
            flpCodigoContainer.Name = "flpCodigoContainer";
            flpCodigoContainer.Size = new Size(451, 429);
            flpCodigoContainer.TabIndex = 7;
            flpCodigoContainer.WrapContents = false;
            // 
            // rtbCodigoLines
            // 
            rtbCodigoLines.Location = new Point(3, 3);
            rtbCodigoLines.Name = "rtbCodigoLines";
            rtbCodigoLines.ReadOnly = true;
            rtbCodigoLines.ScrollBars = RichTextBoxScrollBars.None;
            rtbCodigoLines.Size = new Size(29, 423);
            rtbCodigoLines.TabIndex = 6;
            rtbCodigoLines.Text = "";
            rtbCodigoLines.WordWrap = false;
            // 
            // txtCodigo
            // 
            txtCodigo.Dock = DockStyle.Fill;
            txtCodigo.Location = new Point(38, 3);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(410, 423);
            txtCodigo.TabIndex = 7;
            txtCodigo.Text = "";
            txtCodigo.WordWrap = false;
            txtCodigo.MouseDown += txtCodigo_MouseDown;
            txtCodigo.MouseUp += txtCodigo_MouseUp;
            // 
            // flwFileOpen
            // 
            flwFileOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            flwFileOpen.AutoSize = true;
            flwFileOpen.Controls.Add(btnAbrirCodigo);
            flwFileOpen.Controls.Add(txtFilePath);
            flwFileOpen.Controls.Add(btnLimpaCodigo);
            flwFileOpen.Location = new Point(0, 389);
            flwFileOpen.Margin = new Padding(0);
            flwFileOpen.MinimumSize = new Size(24, 24);
            flwFileOpen.Name = "flwFileOpen";
            flwFileOpen.Padding = new Padding(4);
            flwFileOpen.Size = new Size(450, 37);
            flwFileOpen.TabIndex = 4;
            flwFileOpen.WrapContents = false;
            // 
            // btnAbrirCodigo
            // 
            btnAbrirCodigo.Location = new Point(7, 7);
            btnAbrirCodigo.Name = "btnAbrirCodigo";
            btnAbrirCodigo.Size = new Size(25, 23);
            btnAbrirCodigo.TabIndex = 3;
            btnAbrirCodigo.Text = "...";
            btnAbrirCodigo.UseVisualStyleBackColor = true;
            btnAbrirCodigo.Click += btnAbrirCodigo_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(38, 7);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(320, 23);
            txtFilePath.TabIndex = 1;
            // 
            // btnLimpaCodigo
            // 
            btnLimpaCodigo.Location = new Point(364, 7);
            btnLimpaCodigo.Name = "btnLimpaCodigo";
            btnLimpaCodigo.Size = new Size(75, 23);
            btnLimpaCodigo.TabIndex = 0;
            btnLimpaCodigo.Text = "Limpar";
            btnLimpaCodigo.UseVisualStyleBackColor = true;
            btnLimpaCodigo.Click += btnLimpaCodigo_Click;
            // 
            // tabEtapas
            // 
            tabEtapas.Controls.Add(tabLexico);
            tabEtapas.Controls.Add(tabSintatico);
            tabEtapas.Dock = DockStyle.Fill;
            tabEtapas.Location = new Point(0, 0);
            tabEtapas.Name = "tabEtapas";
            tabEtapas.SelectedIndex = 0;
            tabEtapas.Size = new Size(666, 426);
            tabEtapas.TabIndex = 4;
            // 
            // tabLexico
            // 
            tabLexico.Controls.Add(tlpLexerMain);
            tabLexico.Location = new Point(4, 24);
            tabLexico.Name = "tabLexico";
            tabLexico.Padding = new Padding(3);
            tabLexico.Size = new Size(658, 398);
            tabLexico.TabIndex = 0;
            tabLexico.Text = "Lexico";
            tabLexico.UseVisualStyleBackColor = true;
            // 
            // tlpLexerMain
            // 
            tlpLexerMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpLexerMain.ColumnCount = 1;
            tlpLexerMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLexerMain.Controls.Add(tableLayoutPanel1, 0, 0);
            tlpLexerMain.Controls.Add(dgvTokens, 0, 1);
            tlpLexerMain.Dock = DockStyle.Fill;
            tlpLexerMain.Location = new Point(3, 3);
            tlpLexerMain.Name = "tlpLexerMain";
            tlpLexerMain.RowCount = 2;
            tlpLexerMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tlpLexerMain.RowStyles.Add(new RowStyle());
            tlpLexerMain.Size = new Size(652, 392);
            tlpLexerMain.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.BackColor = SystemColors.ButtonShadow;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(txtCurrentToken, 1, 1);
            tableLayoutPanel1.Controls.Add(lblCurrentTokenTypeLink, 0, 1);
            tableLayoutPanel1.Controls.Add(lblTokenAtual, 1, 0);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90.90909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.Size = new Size(624, 58);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // txtCurrentToken
            // 
            txtCurrentToken.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCurrentToken.Location = new Point(131, 22);
            txtCurrentToken.Multiline = true;
            txtCurrentToken.Name = "txtCurrentToken";
            txtCurrentToken.Size = new Size(490, 31);
            txtCurrentToken.TabIndex = 0;
            // 
            // lblCurrentTokenTypeLink
            // 
            lblCurrentTokenTypeLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentTokenTypeLink.BackColor = SystemColors.ButtonFace;
            lblCurrentTokenTypeLink.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentTokenTypeLink.LinkColor = Color.SteelBlue;
            lblCurrentTokenTypeLink.Location = new Point(3, 19);
            lblCurrentTokenTypeLink.Name = "lblCurrentTokenTypeLink";
            lblCurrentTokenTypeLink.Size = new Size(122, 37);
            lblCurrentTokenTypeLink.TabIndex = 2;
            lblCurrentTokenTypeLink.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTokenAtual
            // 
            lblTokenAtual.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTokenAtual.AutoSize = true;
            lblTokenAtual.CausesValidation = false;
            lblTokenAtual.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTokenAtual.ForeColor = SystemColors.Control;
            lblTokenAtual.Location = new Point(131, 0);
            lblTokenAtual.Name = "lblTokenAtual";
            lblTokenAtual.Size = new Size(490, 19);
            lblTokenAtual.TabIndex = 1;
            lblTokenAtual.Text = "Token Atual";
            lblTokenAtual.TextAlign = ContentAlignment.TopCenter;
            // 
            // dgvTokens
            // 
            dgvTokens.AllowUserToAddRows = false;
            dgvTokens.AllowUserToDeleteRows = false;
            dgvTokens.AllowUserToResizeColumns = false;
            dgvTokens.AllowUserToResizeRows = false;
            dgvTokens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpLexerMain.SetColumnSpan(dgvTokens, 2);
            dgvTokens.Dock = DockStyle.Fill;
            dgvTokens.Location = new Point(3, 67);
            dgvTokens.Name = "dgvTokens";
            dgvTokens.Size = new Size(646, 322);
            dgvTokens.TabIndex = 2;
            dgvTokens.SelectionChanged += dgvTokens_SelectionChanged;
            // 
            // tabSintatico
            // 
            tabSintatico.Controls.Add(splitContainer2);
            tabSintatico.Location = new Point(4, 24);
            tabSintatico.Name = "tabSintatico";
            tabSintatico.Padding = new Padding(3);
            tabSintatico.Size = new Size(658, 398);
            tabSintatico.TabIndex = 1;
            tabSintatico.Text = "Sintatico";
            tabSintatico.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(flowLayoutPanel1);
            splitContainer2.Panel1MinSize = 360;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.AutoScroll = true;
            splitContainer2.Panel2.BackColor = Color.Transparent;
            splitContainer2.Panel2.Controls.Add(flpListErros);
            splitContainer2.Panel2.Padding = new Padding(4);
            splitContainer2.Panel2.Paint += splitContainer2_Panel2_Paint;
            splitContainer2.Panel2MinSize = 64;
            splitContainer2.Size = new Size(652, 392);
            splitContainer2.SplitterDistance = 360;
            splitContainer2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(treeParser);
            flowLayoutPanel1.Controls.Add(tableLayoutPanel2);
            flowLayoutPanel1.Controls.Add(lblTreeNaoCarregada);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(360, 392);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // treeParser
            // 
            treeParser.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            treeParser.BackColor = Color.SlateGray;
            treeParser.Font = new Font("Segoe UI Black", 10F);
            treeParser.ForeColor = SystemColors.ControlLight;
            treeParser.LineColor = Color.Aqua;
            treeParser.Location = new Point(3, 3);
            treeParser.MinimumSize = new Size(190, 300);
            treeParser.Name = "treeParser";
            treeParser.Size = new Size(355, 300);
            treeParser.TabIndex = 0;
            treeParser.AfterSelect += treeParser_AfterSelect;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.36842F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.63158F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 353F));
            tableLayoutPanel2.Controls.Add(lblParserItemLinha, 0, 0);
            tableLayoutPanel2.Controls.Add(lblParserItemColuna, 1, 0);
            tableLayoutPanel2.Controls.Add(lblParserItemDescricao, 2, 0);
            tableLayoutPanel2.Location = new Point(3, 309);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(441, 33);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // lblParserItemLinha
            // 
            lblParserItemLinha.AutoSize = true;
            lblParserItemLinha.BackColor = Color.LightGray;
            lblParserItemLinha.Dock = DockStyle.Fill;
            lblParserItemLinha.Font = new Font("Consolas", 12F, FontStyle.Bold);
            lblParserItemLinha.ForeColor = SystemColors.HotTrack;
            lblParserItemLinha.Location = new Point(3, 0);
            lblParserItemLinha.Name = "lblParserItemLinha";
            lblParserItemLinha.Size = new Size(35, 33);
            lblParserItemLinha.TabIndex = 0;
            lblParserItemLinha.Text = "0";
            lblParserItemLinha.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblParserItemColuna
            // 
            lblParserItemColuna.AutoSize = true;
            lblParserItemColuna.BackColor = Color.LightGray;
            lblParserItemColuna.Dock = DockStyle.Fill;
            lblParserItemColuna.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblParserItemColuna.ForeColor = SystemColors.HotTrack;
            lblParserItemColuna.Location = new Point(44, 0);
            lblParserItemColuna.Name = "lblParserItemColuna";
            lblParserItemColuna.Size = new Size(40, 33);
            lblParserItemColuna.TabIndex = 1;
            lblParserItemColuna.Text = "0";
            lblParserItemColuna.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblParserItemDescricao
            // 
            lblParserItemDescricao.AutoSize = true;
            lblParserItemDescricao.Dock = DockStyle.Fill;
            lblParserItemDescricao.Location = new Point(90, 0);
            lblParserItemDescricao.Name = "lblParserItemDescricao";
            lblParserItemDescricao.Size = new Size(348, 33);
            lblParserItemDescricao.TabIndex = 2;
            lblParserItemDescricao.Text = "label3";
            lblParserItemDescricao.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTreeNaoCarregada
            // 
            lblTreeNaoCarregada.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTreeNaoCarregada.AutoSize = true;
            lblTreeNaoCarregada.BackColor = Color.Transparent;
            lblTreeNaoCarregada.ForeColor = SystemColors.GrayText;
            lblTreeNaoCarregada.Location = new Point(3, 345);
            lblTreeNaoCarregada.Name = "lblTreeNaoCarregada";
            lblTreeNaoCarregada.Size = new Size(272, 15);
            lblTreeNaoCarregada.TabIndex = 3;
            lblTreeNaoCarregada.Text = "Árvore Sintática não carregada. Nada a apresentar.";
            lblTreeNaoCarregada.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpListErros
            // 
            flpListErros.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpListErros.BackColor = Color.Transparent;
            flpListErros.Controls.Add(lblErroSintaticoList);
            flpListErros.Controls.Add(lstErrosSintaticos);
            flpListErros.Controls.Add(txtErroBox);
            flpListErros.Dock = DockStyle.Fill;
            flpListErros.FlowDirection = FlowDirection.TopDown;
            flpListErros.Location = new Point(4, 4);
            flpListErros.Name = "flpListErros";
            flpListErros.Size = new Size(280, 384);
            flpListErros.TabIndex = 9;
            flpListErros.WrapContents = false;
            // 
            // lblErroSintaticoList
            // 
            lblErroSintaticoList.BackColor = Color.Black;
            lblErroSintaticoList.BorderStyle = BorderStyle.FixedSingle;
            lblErroSintaticoList.Dock = DockStyle.Fill;
            lblErroSintaticoList.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblErroSintaticoList.ForeColor = SystemColors.ActiveCaption;
            lblErroSintaticoList.Location = new Point(3, 0);
            lblErroSintaticoList.MinimumSize = new Size(0, 24);
            lblErroSintaticoList.Name = "lblErroSintaticoList";
            lblErroSintaticoList.Size = new Size(280, 24);
            lblErroSintaticoList.TabIndex = 2;
            lblErroSintaticoList.Text = "Erro(s) encontrado(s)";
            lblErroSintaticoList.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstErrosSintaticos
            // 
            lstErrosSintaticos.Activation = ItemActivation.OneClick;
            lstErrosSintaticos.Alignment = ListViewAlignment.Left;
            lstErrosSintaticos.Dock = DockStyle.Fill;
            flpListErros.SetFlowBreak(lstErrosSintaticos, true);
            lstErrosSintaticos.FullRowSelect = true;
            lstErrosSintaticos.GridLines = true;
            lstErrosSintaticos.Location = new Point(3, 27);
            lstErrosSintaticos.MinimumSize = new Size(280, 0);
            lstErrosSintaticos.MultiSelect = false;
            lstErrosSintaticos.Name = "lstErrosSintaticos";
            lstErrosSintaticos.ShowGroups = false;
            lstErrosSintaticos.Size = new Size(280, 247);
            lstErrosSintaticos.TabIndex = 1;
            lstErrosSintaticos.UseCompatibleStateImageBehavior = false;
            lstErrosSintaticos.View = View.List;
            lstErrosSintaticos.ItemActivate += lstErrosSintaticos_DoubleClick;
            // 
            // txtErroBox
            // 
            txtErroBox.BackColor = SystemColors.WindowFrame;
            txtErroBox.Dock = DockStyle.Fill;
            txtErroBox.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            txtErroBox.ForeColor = SystemColors.Info;
            txtErroBox.HideSelection = false;
            txtErroBox.Location = new Point(3, 280);
            txtErroBox.Multiline = true;
            txtErroBox.Name = "txtErroBox";
            txtErroBox.ReadOnly = true;
            txtErroBox.Size = new Size(280, 113);
            txtErroBox.TabIndex = 3;
            txtErroBox.Text = "Nenhum Erro Selecionado.";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { smiEditar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1120, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // smiEditar
            // 
            smiEditar.DropDownItems.AddRange(new ToolStripItem[] { smiEditarFilename });
            smiEditar.Name = "smiEditar";
            smiEditar.Size = new Size(49, 20);
            smiEditar.Text = "Editar";
            // 
            // smiEditarFilename
            // 
            smiEditarFilename.Name = "smiEditarFilename";
            smiEditarFilename.Size = new Size(215, 22);
            smiEditarFilename.Text = "Caminho do Código-fonte";
            smiEditarFilename.Click += smiEditarFilename_Click;
            // 
            // Compilador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1120, 450);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Compilador";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flpCodigoContainer.ResumeLayout(false);
            flwFileOpen.ResumeLayout(false);
            flwFileOpen.PerformLayout();
            tabEtapas.ResumeLayout(false);
            tabLexico.ResumeLayout(false);
            tlpLexerMain.ResumeLayout(false);
            tlpLexerMain.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).EndInit();
            tabSintatico.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flpListErros.ResumeLayout(false);
            flpListErros.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void TxtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private Label lblFilePath;
        private SplitContainer splitContainer1;
        private DataGridView dgvTokens;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTokenAtual;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem smiEditar;
        private ToolStripMenuItem smiEditarFilename;
        private FlowLayoutPanel flwFileOpen;
        private Button btnAbrirCodigo;
        private TextBox txtFilePath;
        private Button btnLimpaCodigo;
        private TextBox txtCurrentToken;
        private LinkLabel lblCurrentTokenTypeLink;
        private TableLayoutPanel tlpLexerMain;
        private TabControl tabEtapas;
        private TabPage tabLexico;
        private TabPage tabSintatico;
        private TreeView treeParser;
        private Label lblErroSintaticoList;
        private ListView lstErrosSintaticos;
        private SplitContainer splitContainer2;
        private TextBox txtErroBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblTreeNaoCarregada;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblParserItemLinha;
        private Label lblParserItemColuna;
        private Label lblParserItemDescricao;
        private FlowLayoutPanel flpCodigoContainer;
        private RichTextBox rtbCodigoLines;
        private RichTextBox txtCodigo;
        private FlowLayoutPanel flpListErros;
    }
}
