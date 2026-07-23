
using Avalonia.Media;
using AvRichTextBox;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quill.Models;
using Quill.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quill.ViewModels;

public partial class EditorViewModel : ViewModelBase
{
    private FontFamily _currentFont = new("Meiryo");
    private double _currentFontSize = 12;
    public RichTextBox? RichTextBox { get; set; }

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
            OnPropertyChanged();
        }
    } 


    [ObservableProperty]
    public partial bool EditingEnabled { get; private set;} = true;
    
    [ObservableProperty]
    public partial bool ShowSavePanel { get; set; }

    public ObservableCollection<string> AvailableFonts { get; } = new();

    public ObservableCollection<double> FontSizes { get; } = new()
    {
        10, 12, 14, 16, 18, 20, 24, 28, 32
    };

    [ObservableProperty]
    private string? _selectedFont;


    partial void OnSelectedFontChanged(string? value)
    {
        if (value is null)
            return;

        _currentFont = new FontFamily(value);

        ApplyFontToSelection();
    }

    [ObservableProperty]
    private double _selectedFontSize = 12;


    partial void OnSelectedFontSizeChanged(double value)
    {
        ApplyFontSize();
    }

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

    public EditorViewModel() 
    {
        LoadFonts();
    }

    private EditorViewModel(TextDocument doc)
    {
        TextDoc = doc;
        LoadFonts();
    }

    
    // Factory Constructor
    public static async Task<EditorViewModel> CreateAsync(string filePath)
    {
        var doc = await FileService.OpenAsync(filePath);
        
        await RecentDocumentsViewModel.Add(doc); // add to recent doc to show on homescreen
        
        return new EditorViewModel(doc);
    }


    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (TextDoc.FilePath is null)
            return; 

        await FileService.SaveAsync(TextDoc.FilePath, TextDoc.Content);

        FileChanged = false;
    }
    
    [RelayCommand]
    private async Task ChooseFolderAsync()
    {
        var path = await FileService.PickFolderAsync();

        if (path is null)
            return;

        SaveState.Directory = path;
        SaveNewFileCommand.NotifyCanExecuteChanged();
    }
    
    [RelayCommand(CanExecute = nameof(IsSaveDirectorySet))]
    private async Task SaveNewFileAsync()
    {        
        // TextDoc = new()
        // {
        //     FileName = SaveState.FileName,
        //     Extension = $".{SaveState.FileExtension}",
        //     Directory = SaveState.Directory
        // };
        
        await RecentDocumentsViewModel.Add(TextDoc);

        await FileService.SaveAsync(SaveState.FilePath!, TextDoc.Content!);
        
        SaveState.Directory = null;

        FileChanged = false;
        ToggleSavePanel();
    }

    private void LoadFonts()
    {
        var fonts = FontManager.Current.SystemFonts
            .Select(f => f.Name)
            .OrderBy(n => n);

        foreach (var font in fonts)
            AvailableFonts.Add(font);

        SelectedFont = AvailableFonts.FirstOrDefault();
    }

    private void ApplyFontSize()
    {
        if (RichTextBox is null)
            return;

        _currentFontSize = SelectedFontSize;

        RichTextBox.FlowDocument.Selection.ApplyFormatting(
            Avalonia.Controls.Documents.TextElement.FontSizeProperty,
            _currentFontSize
        );
    }

    private void ApplyFontToSelection()
    {
        if (RichTextBox is null)
            return;


        RichTextBox.FlowDocument.Selection.ApplyFormatting(
            Avalonia.Controls.Documents.TextElement.FontFamilyProperty,
            _currentFont
        );
    }


    public void PreviewFont(string font)
    {
        if (RichTextBox is null)
            return;


        RichTextBox.FlowDocument.Selection.ApplyFormatting(
            Avalonia.Controls.Documents.TextElement.FontFamilyProperty,
            new FontFamily(font)
        );
    }

    public void PreviewSize(double size)
    {
        if (RichTextBox is null)
            return;


        RichTextBox.FlowDocument.Selection.ApplyFormatting(
            Avalonia.Controls.Documents.TextElement.FontSizeProperty,
            size
        );
    }

}