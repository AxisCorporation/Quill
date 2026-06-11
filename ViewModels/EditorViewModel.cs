using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quill.Models;

namespace Quill.ViewModels;

public partial class EditorViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial bool EditingEnabled { get; private set;} = true;

    [ObservableProperty]
    public partial string EditorText { get; set; }

    [ObservableProperty]
    public partial bool ShowSavePanel { get; set; } 

    // This is the same as the file path in TextDocument.CurrentFilePath
    // This should not be changed in code manually
    [ObservableProperty]
    public partial string? ObservableFilePath { get; private set; } 

    [ObservableProperty]
    public partial FileType FileExtension { get; set; } = FileType.wrt;

    [RelayCommand]
    private void GoToHome()
    {
        MainWindowViewModel.Navigate(new HomeViewModel());

        TextDocument.FilePathChanged += (Path) => ObservableFilePath = Path[.. Path.LastIndexOf('.')]; // Path without extension for clean file name display 
    }


    [RelayCommand]
    private async Task OpenFileSaveDialog(UserControl Control)
    {
        var MainWindow = ((IClassicDesktopStyleApplicationLifetime) Application.Current!.ApplicationLifetime!).MainWindow!;
        var Dialog = new Window();
        await Dialog.ShowDialog(MainWindow);
    }
    
    [RelayCommand]
    private async Task SaveFile(UserControl Control)
    {
        // If null, make them choose a file path
        if (TextDocument.CurrentFilePath is null)
        {
            var Top = TopLevel.GetTopLevel(Control)!;
            var PickResult = await Top.StorageProvider.SaveFilePickerWithResultAsync(new FilePickerSaveOptions()
            {
                Title = "Save File"
            });

            if (PickResult.File is not null)
            {
                TextDocument.CurrentFilePath = PickResult.File.Path.AbsolutePath;
            }

        }
    }
}