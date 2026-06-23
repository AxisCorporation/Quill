using System.IO;
using System.Text.Json.Serialization;
namespace Quill.Models;

public class SaveAsState
{
    public string FileName { get; set; } = "Untitled";
    public string? Directory { get; set; }
    public FileType FileExtension { get; set; } = FileType.wrt;

    [JsonIgnore]
    public string? FilePath
    {
        get => Directory is null ? null 
                : Path.Combine(
                    Directory,
                    $"{FileName}.{FileExtension}"
                    );
        
    }
}