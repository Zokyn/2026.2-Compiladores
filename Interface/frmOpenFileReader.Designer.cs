namespace Interface
{
    partial class frmOpenFileReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblFilePath = new Label();
            btnFileOpen = new Button();
            txtFilePath = new TextBox();
            btnCancel = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(lblFilePath);
            flowLayoutPanel1.Controls.Add(btnFileOpen);
            flowLayoutPanel1.Controls.Add(txtFilePath);
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.MinimumSize = new Size(24, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(16);
            flowLayoutPanel1.Size = new Size(428, 56);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // lblFilePath
            // 
            lblFilePath.Location = new Point(19, 16);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(56, 26);
            lblFilePath.TabIndex = 2;
            lblFilePath.Text = "Caminho";
            lblFilePath.TextAlign = ContentAlignment.MiddleCenter;
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
            // txtFilePath
            // 
            txtFilePath.Location = new Point(112, 19);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(213, 23);
            txtFilePath.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(331, 19);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += this.btnCancel_Click;
            // 
            // frmOpenFileReader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 56);
            Controls.Add(flowLayoutPanel1);
            Name = "frmOpenFileReader";
            Text = "OpenFileReader";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblFilePath;
        private Button btnFileOpen;
        private TextBox txtFilePath;
        private Button btnCancel;
    }
}