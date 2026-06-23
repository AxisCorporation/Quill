
using System.IO;
using System.Text.Json.Serialization;


namespace Quill.Models;
public class TextDocument
{
    public string FileName { get; set; } = "Untitled";
    public string Content { get; set; } = "";
    public string Extension { get; set; } = ".wrt";
    public string? Directory { get; set; }    

    [JsonIgnore]
    public string? FilePath 
    {
        get => Directory is null ? null
                : Path.Combine(
                    Directory,
                    $"{FileName}{Extension}"
                    );
        
    }
}
