using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Quill.ViewModels;

namespace Quill.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void OnNewFileClick(object? sender, RoutedEventArgs e)
    {
        MainWindowViewModel.Navigate(new EditorViewModel());
    }

    private async void OnOpenFileClick(object? sender, RoutedEventArgs e)
    {
        var Top = TopLevel.GetTopLevel(this);

        var Result = await Top!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choose a text file",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("Text files") { Patterns = ["*.txt", "*.docx", "*.pdf", "*.wrt"]}
            ]
        });

        if (Result.Count == 0)
        {
            return;
        }


    }
}