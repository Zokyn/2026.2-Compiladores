namespace Interface
{
    partial class Form1
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
            btnFileRead = new Button();
            txtFilePath = new TextBox();
            lblFilePath = new Label();
            btnFileOpen = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            txtCodeBlock = new TextBox();
            splitContainer1 = new SplitContainer();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblTokenList = new Label();
            dgvTokens = new DataGridView();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).BeginInit();
            SuspendLayout();
            // 
            // btnFileRead
            // 
            btnFileRead.Location = new Point(331, 19);
            btnFileRead.Name = "btnFileRead";
            btnFileRead.Size = new Size(75, 23);
            btnFileRead.TabIndex = 0;
            btnFileRead.Text = "Analisar";
            btnFileRead.UseVisualStyleBackColor = true;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(112, 19);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(213, 23);
            txtFilePath.TabIndex = 1;
            // 
            // lblFilePath
            // 
            lblFilePath.AutoSize = true;
            lblFilePath.Location = new Point(19, 16);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(56, 15);
            lblFilePath.TabIndex = 2;
            lblFilePath.Text = "Caminho";
            // 
            // btnFileOpen
            // 
            btnFileOpen.Location = new Point(81, 19);
            btnFileOpen.Name = "btnFileOpen";
            btnFileOpen.Size = new Size(25, 23);
            btnFileOpen.TabIndex = 3;
            btnFileOpen.Text = "...";
            btnFileOpen.UseVisualStyleBackColor = true;
            btnFileOpen.Click += btnFileOpen_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(lblFilePath);
            flowLayoutPanel1.Controls.Add(btnFileOpen);
            flowLayoutPanel1.Controls.Add(txtFilePath);
            flowLayoutPanel1.Controls.Add(btnFileRead);
            flowLayoutPanel1.Location = new Point(12, 377);
            flowLayoutPanel1.MinimumSize = new Size(24, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(16);
            flowLayoutPanel1.Size = new Size(425, 61);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // txtCodeBlock
            // 
            txtCodeBlock.Location = new Point(12, 3);
            txtCodeBlock.Multiline = true;
            txtCodeBlock.Name = "txtCodeBlock";
            txtCodeBlock.ReadOnly = true;
            txtCodeBlock.ScrollBars = ScrollBars.Both;
            txtCodeBlock.Size = new Size(425, 359);
            txtCodeBlock.TabIndex = 5;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtCodeBlock);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel2);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 443;
            splitContainer1.TabIndex = 7;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(lblTokenList);
            flowLayoutPanel2.Controls.Add(dgvTokens);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(353, 450);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // lblTokenList
            // 
            lblTokenList.AutoSize = true;
            lblTokenList.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTokenList.Location = new Point(3, 0);
            lblTokenList.Name = "lblTokenList";
            lblTokenList.Size = new Size(115, 41);
            lblTokenList.TabIndex = 1;
            lblTokenList.Text = "Tokens";
            // 
            // dgvTokens
            // 
            dgvTokens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTokens.Location = new Point(3, 44);
            dgvTokens.Name = "dgvTokens";
            dgvTokens.Size = new Size(350, 354);
            dgvTokens.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTokens).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnFileRead;
        private TextBox txtFilePath;
        private Label lblFilePath;
        private Button btnFileOpen;
        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox txtCodeBlock;
        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblTokenList;
        private DataGridView dgvTokens;
    }
}
