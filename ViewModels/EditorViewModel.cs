using System;
using System.IO;
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
    public partial string? EditorContents { get; set; }

    [ObservableProperty]
    public partial bool ShowSavePanel { get; set; } 

    // This is the same as the file path in TextDocument.CurrentFilePath
    // This should not be changed in code manually
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveFileCommand))]
    public partial string? FileName { get; set; } 

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveFileCommand))]
    public partial string? DirectoryPath { get; set; }

    [ObservableProperty]
    public partial FileType FileExtension { get; set; } = FileType.wrt;

    private bool IsValidFilePath() => !string.IsNullOrWhiteSpace(FileName) && !string.IsNullOrWhiteSpace(DirectoryPath);

    [RelayCommand]
    private void GoToHome()
    {
        MainWindowViewModel.Navigate(new HomeViewModel());
    }

    [RelayCommand]
    private async Task ChooseFolder(UserControl Control)
    {
        var Top = TopLevel.GetTopLevel(Control)!;
        var PickResult = await Top.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Select folder to save file in.",
            AllowMultiple = false
        });

        DirectoryPath = PickResult?[0].Path.AbsolutePath;

        TextDocument.CurrentDirectory = DirectoryPath;
    }
    
    [RelayCommand(CanExecute = nameof(IsValidFilePath))]
    private async Task SaveFile()
    {
        TextDocument.CurrentFileName = FileName;
        TextDocument.CurrentFileExtension = FileExtension.ToString();
        if (await TextDocument.WriteToFileAsync(EditorContents))
        {
            ShowSavePanel = false;
        }
    }

    [RelayCommand]
    public void ToggleSavePanel() => ShowSavePanel = !ShowSavePanel;

}