using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Quill.Models;
using Quill.ViewModels;
using System;
using System.Threading.Tasks;

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

    private async void OnRecentDocumentClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.CommandParameter is not RecentDocument doc)
            return;

        var editor = await EditorViewModel.CreateAsync(
            new Uri(doc.FilePath));

        MainWindowViewModel.Navigate(editor);
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

        MainWindowViewModel.Navigate(await EditorViewModel.CreateAsync(Result[0].Path));
    }
}