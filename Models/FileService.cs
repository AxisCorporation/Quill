using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace Quill.Models;

public static class FileService
{
    private static JsonSerializerOptions _serializerWriteOptions = new() // Initializaing serializer options is costly, better to reuse according to docs
    {
        WriteIndented = true
    };
    
    public static TopLevel MainWindow => (Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!
                                         .MainWindow!;
    
    public static async Task<TextDocument> OpenAsync(string path)
    {
        string json = await File.ReadAllTextAsync(path);

        var doc = JsonSerializer.Deserialize<TextDocument>(json);

        return doc ?? new TextDocument();
    }

    public static async Task SaveAsync(string path, TextDocument document)
    {
        var json = JsonSerializer.Serialize(document, _serializerWriteOptions);

        await File.WriteAllTextAsync(path, json);
    }
    
    public static async Task<IStorageFile?> OpenFileAsync()
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
    
    public static async Task<string?> PickFolderAsync()
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