using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quill.Models;

namespace Quill.ViewModels;

public partial class EditorViewModel : ViewModelBase
{
    // Feel free to change it to properties to make it work with VS Code, I don't think it matters
    [ObservableProperty]
    public partial TextDocument TextDoc { get; set; } = new();
    
    [ObservableProperty]
    public partial SaveAsState SaveState { get; set; } = new();
    
    public string? SaveDirectory
    { 
        get => SaveState.Directory; 
        set
        {
            SaveState.Directory = value;
            OnPropertyChanged(nameof(SaveDirectory));
        }
    } 


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
    public bool IsSaveDirectorySet() => SaveState.Directory is not null;
    private bool CurrentPathIsSet() => TextDoc.Directory is not null;
    private bool CanSave() => CurrentPathIsSet() && FileChanged;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial bool FileChanged { get; set; } = false; 

    public EditorViewModel() {}

    private EditorViewModel(TextDocument doc, string filePath)
    {
        TextDoc = doc;
    }

    
    // Factory Constructor
    public static async Task<EditorViewModel> CreateAsync(string filePath)
    {
        var doc = await FileService.OpenAsync(filePath);
        
        RecentDocumentsViewModel.Add(doc); // add to recent doc to show on homescreen
        
        return new EditorViewModel(doc, filePath);
    }


    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (TextDoc.FilePath is null)
            return; 

        await FileService.SaveAsync(TextDoc.FilePath, TextDoc);
        FileChanged = false;
    }
    
    [RelayCommand]
    private async Task ChooseFolderAsync()
    {
        var path = await FileService.PickFolderAsync();

        if (path is null)
            return;

        SaveDirectory = path;
        SaveNewFileCommand.NotifyCanExecuteChanged();
    }
    
    [RelayCommand(CanExecute = nameof(IsSaveDirectorySet))]
    private async Task SaveNewFileAsync()
    {        
        TextDoc = new()
        {
            FileName = SaveState.FileName,
            Content =  TextDoc.Content,
            Extension = $".{SaveState.FileExtension}",
            Directory = SaveState.Directory
        };
        
        RecentDocumentsViewModel.Add(TextDoc!);

        await FileService.SaveAsync(SaveState.FilePath!, TextDoc);
        
        SaveState.Directory = null;

        FileChanged = false;
        ToggleSavePanel();
    }

}