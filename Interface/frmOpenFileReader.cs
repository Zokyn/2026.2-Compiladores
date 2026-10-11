using Lexico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface
{
    public partial class frmOpenFileReader : Form
    {
        public string FileName => txtFilePath.Text;
        public DialogResult Result = DialogResult.Cancel;
        public frmOpenFileReader()
        {
            InitializeComponent();

            // Configura frm
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            LerCodigoFonte();
        }
        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            LerCodigoFonte();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LerCodigoFonte(string? path = null)
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
            try
            {
                using var dialog = new OpenFileDialog
                {
                    Title = "Abrir código-fonte de entrada",
                    InitialDirectory = root,
                    Filter = "Código-fonte Declara (*.dcl;*.txt)|*.dcl;*.txt|Todos os arquivos (*.*)|*.*",
                    RestoreDirectory = true
                };
                if (dialog.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }
                txtFilePath.Text = dialog.FileName;
            }
            catch
            {
                throw new Exception("Dialog teve um comportamento inesperado");

            }
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
    }
}
