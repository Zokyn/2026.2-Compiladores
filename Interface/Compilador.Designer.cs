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
            flwFileOpen = new FlowLayoutPanel();
            btnAbrirCodigo = new Button();
            txtFilePath = new TextBox();
            btnLimpaCodigo = new Button();
            txtCodigo = new TextBox();
            flpRight = new FlowLayoutPanel();
            tabEtapas = new TabControl();
            tabLexico = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtCurrentToken = new TextBox();
            lblCurrentTokenTypeLink = new LinkLabel();
            lblTokenAtual = new Label();
            dgvTokens = new DataGridView();
            tabSintatico = new TabPage();
            splitContainer2 = new SplitContainer();
            lblTreeNaoCarregada = new Label();
            treeParser = new TreeView();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblErroSintaticoList = new Label();
            lstErrosSintaticos = new ListView();
            menuStrip1 = new MenuStrip();
            smiEditar = new ToolStripMenuItem();
            smiEditarFilename = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flwFileOpen.SuspendLayout();
            flpRight.SuspendLayout();
            tabEtapas.SuspendLayout();
            tabLexico.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).BeginInit();
            tabSintatico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(flwFileOpen);
            splitContainer1.Panel1.Controls.Add(txtCodigo);
            splitContainer1.Panel1MinSize = 450;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flpRight);
            splitContainer1.Size = new Size(1084, 426);
            splitContainer1.SplitterDistance = 450;
            splitContainer1.TabIndex = 7;
            // 
            // flwFileOpen
            // 
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
            // txtCodigo
            // 
            txtCodigo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCodigo.Location = new Point(0, 0);
            txtCodigo.Multiline = true;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.ReadOnly = true;
            txtCodigo.ScrollBars = ScrollBars.Both;
            txtCodigo.Size = new Size(451, 390);
            txtCodigo.TabIndex = 5;
            txtCodigo.MouseDown += txtCodigo_MouseDown;
            txtCodigo.MouseUp += txtCodigo_MouseUp;
            // 
            // flpRight
            // 
            flpRight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flpRight.AutoSize = true;
            flpRight.Controls.Add(tabEtapas);
            flpRight.FlowDirection = FlowDirection.TopDown;
            flpRight.Location = new Point(0, 0);
            flpRight.Margin = new Padding(16);
            flpRight.Name = "flpRight";
            flpRight.Size = new Size(779, 800);
            flpRight.TabIndex = 1;
            // 
            // tabEtapas
            // 
            tabEtapas.Controls.Add(tabLexico);
            tabEtapas.Controls.Add(tabSintatico);
            tabEtapas.Location = new Point(3, 3);
            tabEtapas.Name = "tabEtapas";
            tabEtapas.SelectedIndex = 0;
            tabEtapas.Size = new Size(627, 423);
            tabEtapas.TabIndex = 4;
            // 
            // tabLexico
            // 
            tabLexico.Controls.Add(tableLayoutPanel2);
            tabLexico.Location = new Point(4, 24);
            tabLexico.Name = "tabLexico";
            tabLexico.Padding = new Padding(3);
            tabLexico.Size = new Size(619, 395);
            tabLexico.TabIndex = 0;
            tabLexico.Text = "Lexico";
            tabLexico.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(dgvTokens, 0, 1);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(627, 423);
            tableLayoutPanel2.TabIndex = 3;
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
            tableLayoutPanel1.Size = new Size(621, 58);
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
            tableLayoutPanel2.SetColumnSpan(dgvTokens, 2);
            dgvTokens.Dock = DockStyle.Fill;
            dgvTokens.Location = new Point(3, 67);
            dgvTokens.Name = "dgvTokens";
            dgvTokens.Size = new Size(621, 353);
            dgvTokens.TabIndex = 2;
            dgvTokens.CellClick += dgvTokens_CellClick;
            dgvTokens.CellContentClick += dgvTokens_CellContentClick;
            // 
            // tabSintatico
            // 
            tabSintatico.Controls.Add(splitContainer2);
            tabSintatico.Location = new Point(4, 24);
            tabSintatico.Name = "tabSintatico";
            tabSintatico.Padding = new Padding(3);
            tabSintatico.Size = new Size(619, 395);
            tabSintatico.TabIndex = 1;
            tabSintatico.Text = "Sintatico";
            tabSintatico.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(lblTreeNaoCarregada);
            splitContainer2.Panel1.Controls.Add(treeParser);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(flowLayoutPanel1);
            splitContainer2.Panel2.Paint += splitContainer2_Panel2_Paint;
            splitContainer2.Size = new Size(613, 389);
            splitContainer2.SplitterDistance = 445;
            splitContainer2.TabIndex = 1;
            // 
            // lblTreeNaoCarregada
            // 
            lblTreeNaoCarregada.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTreeNaoCarregada.AutoSize = true;
            lblTreeNaoCarregada.Location = new Point(101, 345);
            lblTreeNaoCarregada.Name = "lblTreeNaoCarregada";
            lblTreeNaoCarregada.Size = new Size(272, 15);
            lblTreeNaoCarregada.TabIndex = 3;
            lblTreeNaoCarregada.Text = "Árvore Sintática não carregada. Nada a apresentar.";
            // 
            // treeParser
            // 
            treeParser.Dock = DockStyle.Fill;
            treeParser.Location = new Point(0, 0);
            treeParser.Name = "treeParser";
            treeParser.Size = new Size(445, 389);
            treeParser.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(lblErroSintaticoList);
            flowLayoutPanel1.Controls.Add(lstErrosSintaticos);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(164, 389);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lblErroSintaticoList
            // 
            lblErroSintaticoList.BackColor = Color.Black;
            lblErroSintaticoList.BorderStyle = BorderStyle.FixedSingle;
            lblErroSintaticoList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblErroSintaticoList.ForeColor = SystemColors.ActiveCaption;
            lblErroSintaticoList.Location = new Point(3, 0);
            lblErroSintaticoList.Name = "lblErroSintaticoList";
            lblErroSintaticoList.Size = new Size(156, 35);
            lblErroSintaticoList.TabIndex = 2;
            lblErroSintaticoList.Text = "Erro(s) encontrado(s)";
            lblErroSintaticoList.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstErrosSintaticos
            // 
            lstErrosSintaticos.Activation = ItemActivation.OneClick;
            lstErrosSintaticos.GridLines = true;
            lstErrosSintaticos.Location = new Point(3, 38);
            lstErrosSintaticos.Name = "lstErrosSintaticos";
            lstErrosSintaticos.Size = new Size(156, 340);
            lstErrosSintaticos.TabIndex = 1;
            lstErrosSintaticos.UseCompatibleStateImageBehavior = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { smiEditar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1084, 24);
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
            ClientSize = new Size(1084, 450);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Compilador";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flwFileOpen.ResumeLayout(false);
            flwFileOpen.PerformLayout();
            flpRight.ResumeLayout(false);
            tabEtapas.ResumeLayout(false);
            tabLexico.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).EndInit();
            tabSintatico.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblFilePath;
        private SplitContainer splitContainer1;
        private FlowLayoutPanel flpRight;
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
        private TextBox txtCodigo;
        private TextBox txtCurrentToken;
        private LinkLabel lblCurrentTokenTypeLink;
        private TableLayoutPanel tableLayoutPanel2;
        private TabControl tabEtapas;
        private TabPage tabLexico;
        private TabPage tabSintatico;
        private TreeView treeParser;
        private Label lblErroSintaticoList;
        private ListView lstErrosSintaticos;
        private Label lblTreeNaoCarregada;
        private SplitContainer splitContainer2;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
