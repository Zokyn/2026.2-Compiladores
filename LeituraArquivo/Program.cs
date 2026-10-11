namespace LeituraArquivo;
public class Leitor
{
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
        string root = EncontrarCaminhoSolucao() ?? Environment.CurrentDirectory;
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
}