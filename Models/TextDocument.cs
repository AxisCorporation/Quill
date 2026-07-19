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
    
    public ObservableCollection<DocumentStyle> Styles { get; }
        =
        [
            new()
            {
                Name = "Normal"
            },

            new()
            {
                Name = "Heading 1",
                FontSize = 32,
                FontWeight = FontWeight.Bold
            },

            new()
            {
                Name = "Heading 2",
                FontSize = 24,
                FontWeight = FontWeight.Bold
            },

            new()
            {
                Name = "Quote",
                FontStyle = FontStyle.Italic
            },

            new()
            {
                Name = "Code",
                FontFamily = new FontFamily("Consolas")
            }
        ];
    
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
