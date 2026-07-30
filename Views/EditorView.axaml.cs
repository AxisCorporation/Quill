using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using AvRichTextBox;
using Quill.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Quill.Views;

public partial class EditorView : UserControl
{

    public EditorView()
    {
        InitializeComponent();

        Loaded += (_, _) =>
        {
            if (DataContext is EditorViewModel vm)
            {
                vm.RichTextBox = MainRTB;
            }
            // var vm = (EditorViewModel) DataContext!;
            //
            // MainRTB.FlowDocument = vm.TextDoc.Content;
            //
            // MainRTB.FlowDocument.Selection.CollapseToEnd();

        };
    }

    private void FontPreviewEntered(object? sender,PointerEventArgs e)
    {
        if (sender is TextBlock text && DataContext is EditorViewModel vm)
        {
            if (text.Text is not null)
            {     
                vm.PreviewFont(text.Text);
            }
        }
    }

    private void SizePreviewEntered(object? sender, PointerEventArgs e)
    {
        if (sender is TextBlock text &&
            DataContext is EditorViewModel vm)
        {
            Console.WriteLine($"Hovered size: {text.Text}");

            if (double.TryParse(text.Text, out double size))
            {
                vm.PreviewSize(size);
            }
        }
    }


}
