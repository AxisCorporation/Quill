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
    // Feel free to change it to properties to make it work with VS Code, I don't think it matters
    [ObservableProperty]
    private TextDocument _textDoc = new();
    
    [ObservableProperty]
    private SaveAsState _saveState = new();
    
    /// <remarks>
    /// Tried to do
    /// [ObservableProperty]
    /// public partial string? DirectoryPath
    /// {
    ///     get => SaveState.DirectoryPath;
    ///     set
    ///     {
    ///         SaveState.DirectoryPath = value;
    ///         OnPropertyChanged();
    ///     }
    /// }
    /// But didn't work, so for now will make do with this, as direct
    /// binding to SaveState.Directory doesn't work. Or change the SaveState abstraction
    /// </remarks>
    [ObservableProperty]
    public partial string? DirectoryPath { get; set; }
    
    // Will make it a property later and bind directly to TextDoc.Directory
    private string? _filePath;

    [ObservableProperty]
    public partial bool EditingEnabled { get; private set;} = true;
    
    [ObservableProperty]
    public partial bool ShowSavePanel { get; set; } 
    
    [RelayCommand]
    public void ToggleSavePanel()
    {
        ShowSavePanel = !ShowSavePanel;
        // Not sure if this is even needed, but why not?
        EditingEnabled = !EditingEnabled;
    }

    [RelayCommand]
    private static void GoToHome() => MainWindowViewModel.Navigate(new HomeViewModel());
    
    // private static bool CurrentPathIsSet() => TextDoc.CurrentFilePath is not null;
    // private bool CanSave() => CurrentPathIsSet() && FileChanged;
    
    public EditorViewModel() {}

    private EditorViewModel(TextDocument doc, string filePath)
    {
        _textDoc = doc;
        _filePath = filePath;
    }
    
    // Factory Constructor
    public static async Task<EditorViewModel> CreateAsync(string filePath)
    {
        var doc = await FileService.Instance.OpenAsync(filePath);
        

        return new EditorViewModel(doc, filePath);
    }


    [RelayCommand]
    private async Task SaveAsync()
    {
        if (_filePath is null)
            return; // later becomes SaveAs Maybe

        await FileService.Instance.SaveAsync(_filePath, TextDoc);
    }
    
    [RelayCommand]
    private async Task ChooseFolderAsync()
    {
        var path = await FileService.Instance.PickFolderAsync();

        if (path is null)
            return;

        SaveState.DirectoryPath = path;
        DirectoryPath = path;
    }
    
    [RelayCommand]
    private async Task SaveNewFileAsync()
    {
        var fullPath =
            Path.Combine(
                SaveState.DirectoryPath,
                $"{SaveState.FileName}.{SaveState.FileExtension}"
            );
        
        // Will probably find a way to change this later as well
        TextDocument tempDoc = new()
        {
            FileName = SaveState.FileName,
            Content =  TextDoc.Content,
            Extension = $".{SaveState.FileExtension.ToString()}",
            Directory = SaveState.DirectoryPath
        };

        await FileService.Instance.SaveAsync(fullPath, tempDoc);
        
        ToggleSavePanel();
    }

}