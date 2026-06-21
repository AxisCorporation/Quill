namespace Quill.Models;

public class SaveAsState
{
    public string FileName { get; set; } = "Untitled";
    public string DirectoryPath { get; set; } = "";
    public FileType FileExtension { get; set; } = FileType.wrt;
}