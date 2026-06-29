using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using AvRichTextBox;

using Quill.ViewModels;

namespace Quill.Views;

public partial class EditorView : UserControl
{
    public EditorView()
    {
        InitializeComponent();

        Loaded += async (a, sender) => 
        {
            var vm =  (EditorViewModel) DataContext!;
            vm.RichTextBox = MainRTB;
            
            if (!string.IsNullOrWhiteSpace(vm.TextDoc.FilePath))
            {
                await LoadFile(MainRTB, vm.TextDoc.FilePath);
                MainRTB.FlowDocument.Selection.CollapseToEnd();
            }
            MainRTB.TextInput += OnTextChanged;
        };
    }

    private void OnTextChanged(object? sender, TextInputEventArgs a)
    {
        if (DataContext is EditorViewModel vm)
        {
            vm.FileChanged = true;
        }
    }

    public async Task LoadFile(RichTextBox richTextBox, string filePath)
    {
        string extension = Path.GetExtension(filePath);

        if (extension == ".txt" || extension == ".pdf")
        {
            richTextBox.FlowDocument.Blocks.RemoveAt(0);

            var para = new Paragraph(richTextBox.FlowDocument);
            para.Inlines.Add(new EditableRun(await File.ReadAllTextAsync(filePath)));

            richTextBox.FlowDocument.Blocks.Add(para);
        }
        else if (extension == ".docx")
        {
            richTextBox.LoadWordDoc(filePath);
        }
        else
        {
            richTextBox.LoadXaml(filePath);
        }

        // Dispatcher.UIThread.Post() schedules a task to be done after all tasks in the queue complete
        // This means that this function "waits" until all other UI tasks (loading and initializing fields) are complete before executing
        Dispatcher.UIThread.Post(() =>
        {
            MainRTB.FlowDocument.Select(0, 0);
        });

    }
}
