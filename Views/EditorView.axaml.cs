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

        Loaded += (_, _) =>
        {
            // var vm = (EditorViewModel) DataContext!;
            //
            // MainRTB.FlowDocument = vm.TextDoc.Content;
            //
            // MainRTB.FlowDocument.Selection.CollapseToEnd();
            
        };
    }

    private void OnTextChanged(object? sender, TextInputEventArgs a)
    {
        if (DataContext is EditorViewModel vm)
        {
            vm.FileChanged = true;
        }
    }


}
