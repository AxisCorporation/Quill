using System;
using Avalonia.Controls;
using Avalonia.Input;
using AvRichTextBox;
using DocumentFormat.OpenXml.Drawing.Charts;
using Quill.ViewModels;

namespace Quill.Views;

public partial class EditorView : UserControl
{
    public EditorView()
    {
        InitializeComponent();

        Loaded += (a, sender) => 
        {
            var vm =  (EditorViewModel) DataContext!;
            vm.RichTextBox = MainRTB;
            
            if (!string.IsNullOrWhiteSpace(vm.TextDoc.FilePath))
            {
                MainRTB.LoadXaml(vm.TextDoc.FilePath);
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
}
