using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Quill.Models;

public class FileService
{
    private static readonly FileService _instance = new();

    public static FileService Instance => _instance;

    private FileService()
    {
        
    }
    
    public TopLevel? MainWindow { get; set; }
    
    public async Task<TextDocument> OpenAsync(string path)
    {
        string json = await File.ReadAllTextAsync(path);

        var doc = JsonSerializer.Deserialize<TextDocument>(json);

        return doc ?? new TextDocument();
    }

    public async Task SaveAsync(string path, TextDocument document)
    {
        var json = JsonSerializer.Serialize(document, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(path, json);
    }
    
    public async Task<IStorageFile?> OpenFileAsync()
    {
        if (MainWindow is null)
            return null;

        var result = await MainWindow.StorageProvider
            .OpenFilePickerAsync(
                new FilePickerOpenOptions
                {
                    Title = "Choose a file",
                    AllowMultiple = false
                });

        return result.Count > 0
            ? result[0]
            : null;
    }
    
    public async Task<string?> PickFolderAsync()
    {
        if (MainWindow is null)
            return null;

        var folders = await MainWindow.StorageProvider.OpenFolderPickerAsync(
            new FolderPickerOpenOptions
            {
                Title = "Choose save folder",
                AllowMultiple = false
            });

        if (folders.Count == 0)
            return null;

        return folders[0].Path.LocalPath;
    }
}