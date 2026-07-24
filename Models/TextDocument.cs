using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;
using Avalonia.Media;
using AvRichTextBox;

namespace Quill.Models;

public class TextDocument
{
    public string FileName { get; set; } = "Untitled";
    public string Extension { get; set; } = ".wrt";
    public string? Directory { get; set; }
    
    // actual contents
    public FlowDocument Content { get; set; } = new();

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
