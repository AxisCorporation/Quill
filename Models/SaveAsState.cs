using System.IO;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Quill.Models;

public partial class SaveAsState : ObservableObject
{
    [ObservableProperty]
    public partial string FileName { get; set; } = "Untitled";
    [ObservableProperty]
    public partial string? Directory { get; set; }
    [ObservableProperty]
    public partial FileType FileExtension { get; set; } = FileType.wrt;

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