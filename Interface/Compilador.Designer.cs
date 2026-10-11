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
            tableLayoutPanel1 = new TableLayoutPanel();
            txtCurrentToken = new TextBox();
            lblTokenAtual = new Label();
            lblCurrentTokenTypeLink = new LinkLabel();
            dgvTokens = new DataGridView();
            menuStrip1 = new MenuStrip();
            smiEditar = new ToolStripMenuItem();
            smiEditarFilename = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flwFileOpen.SuspendLayout();
            flpRight.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
            splitContainer1.Size = new Size(1362, 426);
            splitContainer1.SplitterDistance = 451;
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
            txtCodigo.Size = new Size(452, 390);
            txtCodigo.TabIndex = 5;
            txtCodigo.MouseDown += txtCodigo_MouseDown;
            txtCodigo.MouseUp += txtCodigo_MouseUp;
            // 
            // flpRight
            // 
            flpRight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flpRight.AutoSize = true;
            flpRight.Controls.Add(tableLayoutPanel1);
            flpRight.Controls.Add(dgvTokens);
            flpRight.FlowDirection = FlowDirection.TopDown;
            flpRight.Location = new Point(0, 0);
            flpRight.Margin = new Padding(16);
            flpRight.Name = "flpRight";
            flpRight.Size = new Size(1045, 800);
            flpRight.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.ButtonShadow;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.7086182F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77.29138F));
            tableLayoutPanel1.Controls.Add(txtCurrentToken, 1, 1);
            tableLayoutPanel1.Controls.Add(lblTokenAtual, 1, 0);
            tableLayoutPanel1.Controls.Add(lblCurrentTokenTypeLink, 0, 1);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(899, 94);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // txtCurrentToken
            // 
            txtCurrentToken.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCurrentToken.Location = new Point(207, 40);
            txtCurrentToken.Multiline = true;
            txtCurrentToken.Name = "txtCurrentToken";
            txtCurrentToken.Size = new Size(689, 31);
            txtCurrentToken.TabIndex = 0;
            // 
            // lblTokenAtual
            // 
            lblTokenAtual.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTokenAtual.AutoSize = true;
            lblTokenAtual.CausesValidation = false;
            lblTokenAtual.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTokenAtual.ForeColor = SystemColors.Control;
            lblTokenAtual.Location = new Point(207, 8);
            lblTokenAtual.Name = "lblTokenAtual";
            lblTokenAtual.Size = new Size(689, 21);
            lblTokenAtual.TabIndex = 1;
            lblTokenAtual.Text = "Token Atual";
            lblTokenAtual.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblCurrentTokenTypeLink
            // 
            lblCurrentTokenTypeLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentTokenTypeLink.BackColor = SystemColors.ButtonFace;
            lblCurrentTokenTypeLink.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentTokenTypeLink.LinkColor = Color.SteelBlue;
            lblCurrentTokenTypeLink.Location = new Point(3, 37);
            lblCurrentTokenTypeLink.Name = "lblCurrentTokenTypeLink";
            lblCurrentTokenTypeLink.Size = new Size(198, 37);
            lblCurrentTokenTypeLink.TabIndex = 2;
            lblCurrentTokenTypeLink.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvTokens
            // 
            dgvTokens.AllowUserToAddRows = false;
            dgvTokens.AllowUserToDeleteRows = false;
            dgvTokens.AllowUserToResizeColumns = false;
            dgvTokens.AllowUserToResizeRows = false;
            dgvTokens.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTokens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTokens.Location = new Point(3, 103);
            dgvTokens.Name = "dgvTokens";
            dgvTokens.Size = new Size(899, 329);
            dgvTokens.TabIndex = 2;
            dgvTokens.CellClick += dgvTokens_CellClick;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { smiEditar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1362, 24);
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
            ClientSize = new Size(1362, 450);
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
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).EndInit();
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
        private TextBox txtCurrentToken;
        private Label lblTokenAtual;
        private LinkLabel lblCurrentTokenTypeLink;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem smiEditar;
        private ToolStripMenuItem smiEditarFilename;
        private FlowLayoutPanel flwFileOpen;
        private Button btnAbrirCodigo;
        private TextBox txtFilePath;
        private Button btnLimpaCodigo;
        private TextBox txtCodigo;
    }
}
