using System;
using System.IO;
using System.Linq;
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
    [NotifyCanExecuteChangedFor(nameof(SaveNewFileCommand))]
    public partial string? FileName { get; set; } 

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveNewFileCommand))]
    public partial string? DirectoryPath { get; set; }

    [ObservableProperty]
    public partial FileType FileExtension { get; set; } = FileType.wrt;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveFileCommand))]
    private partial bool FileChanged { get; set; } = false; 
    
    [RelayCommand]
    public void ToggleSavePanel() => ShowSavePanel = !ShowSavePanel;

    [RelayCommand]
    private static void GoToHome() => MainWindowViewModel.Navigate(new HomeViewModel());

    private bool IsValidFilePath() => !string.IsNullOrWhiteSpace(FileName) && !string.IsNullOrWhiteSpace(DirectoryPath);
    private static bool CurrentPathIsSet() => TextDocument.CurrentFilePath is not null;
    private bool CanSave() => CurrentPathIsSet() && FileChanged;
    
    public EditorViewModel() {}

    private EditorViewModel(Uri FileUri)
    {
        string LocalPath = FileUri.LocalPath;

        TextDocument.CurrentDirectory = Path.GetDirectoryName(LocalPath);
        TextDocument.CurrentFileName = Path.GetFileNameWithoutExtension(LocalPath);
        TextDocument.CurrentFileExtension = Path.GetExtension(LocalPath);
    }

    public static async Task<EditorViewModel> CreateAsync(Uri FileUri) // Constructors cannot be async. Therefore, to use an async method in a constructor, a factory must be used
    {
        // Not setting the directory path on purpose so the user is prompted to re-enter directory path if they wish to save as a new file. Can easily change this
        EditorViewModel ViewModel = new(FileUri)
        {
            EditorContents = await TextDocument.ReadTextFromFileAsync(),

            FileName = TextDocument.CurrentFileName,
            FileExtension = Enum.TryParse(TextDocument.CurrentFileExtension?.TrimStart('.'), out FileType Result) ? Result : FileType.wrt,

            FileChanged = false
        };

        return ViewModel;
    }

    partial void OnEditorContentsChanged(string? value)
    {
        FileChanged = true;
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

        if (PickResult.Count == 0)
        {
            return;
        }

        DirectoryPath = PickResult[0].Path.AbsolutePath;
    }
    
    [RelayCommand(CanExecute = nameof(IsValidFilePath))]
    private async Task SaveNewFile()
    {
        TextDocument.CurrentDirectory = DirectoryPath;
        TextDocument.CurrentFileName = FileName;
        TextDocument.CurrentFileExtension = $".{FileExtension}";

        if (await TextDocument.WriteToFileAsync(EditorContents))
        {
            ShowSavePanel = false;
            FileChanged = false;

            DirectoryPath = null;
            SaveFileCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveFile()
    {
        await TextDocument.WriteToFileAsync(EditorContents);
        FileChanged = false;
    }

}