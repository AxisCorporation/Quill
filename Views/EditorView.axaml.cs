using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Quill.ViewModels;

namespace Quill.Views;

public partial class EditorView : UserControl
{
    public EditorView()
    {
        InitializeComponent();
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs a)
    {
        if (DataContext is EditorViewModel vm)
        {
            vm.FileChanged = true;
        }
    }
}
