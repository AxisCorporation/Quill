using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using AvRichTextBox;

namespace Quill.Models;

public static class FileService
{
    private static JsonSerializerOptions serializerWriteOptions = new() // Initializaing serializer options is costly, better to reuse according to docs
    {
        WriteIndented = true
    };
    
    public static TopLevel MainWindow => (Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!
                                         .MainWindow!;
    
    public static async Task<TextDocument> OpenAsync(string path)
    {
        string json = await File.ReadAllTextAsync(path);

        TextDocument textDoc = new()
        {
            FileName = Path.GetFileNameWithoutExtension(path),
            Extension = Path.GetExtension(path),
            Directory = Path.GetDirectoryName(path)
        };

        return textDoc;
    }

    public static async Task SaveAsync(string path, string xaml)
    {
        // var json = JsonSerializer.Serialize(document, serializerWriteOptions);

        await File.WriteAllTextAsync(path, xaml);
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