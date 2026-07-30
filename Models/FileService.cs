using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using AvRichTextBox;
using Avalonia.Media;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Quill.Models;

public static class FileService
{
    private static JsonSerializerOptions _serializerWriteOptions = new() // Initializaing serializer options is costly, better to reuse according to docs
    {
        WriteIndented = true
    };
    
    public static TopLevel MainWindow => (Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!
                                         .MainWindow!;

    public static async Task SaveAsync(string path, FlowDocument document)
    {
        var rtb = new RichTextBox
        {
            FlowDocument = document
        };

        string content;
        string extension = Path.GetExtension(path);

        if (extension == ".txt")
        {
            content = rtb.FlowDocument.Text;
        }
        else if (extension == ".pdf")
        {
            SavePdf(path, document);
        return;
        }
        else if (extension == ".docx")
        {
            rtb.SaveWordDoc(path);
            return;
        }
        else
        {
            content = rtb.SaveXamlString();
        }

        await File.WriteAllTextAsync(path, content);
    }

    private static void SavePdf(string path, FlowDocument document)
    {
        QuestPDF.Settings.License = LicenseType.Community;

    Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);

            page.Content().Column(column =>
            {
                foreach (var block in document.Blocks)
                {
                    if (block is not Paragraph paragraph)
                        continue;

                    column.Item().Text(text =>
                    {
                        foreach (var inline in paragraph.Inlines)
                        {
                            if (inline is not EditableRun run)
                                continue;

                            var span = text.Span(run.Text);

                            span.FontFamily(run.FontFamily.Name);
                            span.FontSize((float)run.FontSize);

                            if (run.FontWeight == Avalonia.Media.FontWeight.Bold)
                                span.Bold();

                            if (run.FontStyle == FontStyle.Italic)
                                span.Italic();

                            if (run.TextDecorations == TextDecorations.Underline)
                                span.Underline();
                        }
                    });
                }
            });
        });
    })
    .GeneratePdf(path);
    }
    
    public static async Task<IStorageFile?> OpenFileAsync()
    {
        if (MainWindow is null)
            return null;

        var result =
            await MainWindow.StorageProvider
                .OpenFilePickerAsync(
                    new FilePickerOpenOptions
                    {
                        Title = "Choose a file",
                        AllowMultiple = false,
                        FileTypeFilter =
                        [
                            new FilePickerFileType("Quill Document")
                            {
                                Patterns = ["*.wrt"]
                            }
                        ]
                    });

        return result.Count > 0
            ? result[0]
            : null;
    }
    
    public static async Task<TextDocument> OpenAsync(string path)
    {
        var document = new TextDocument
        {
            FileName =
                Path.GetFileNameWithoutExtension(path),

            Extension =
                Path.GetExtension(path),

            Directory =
                Path.GetDirectoryName(path)
        };

        var tempEditor = new RichTextBox
        {
            FlowDocument = new FlowDocument()
        };

        tempEditor.LoadXaml(path);

        document.Content = tempEditor.FlowDocument;

        return document;
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