using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quill.Models;

namespace Quill.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    // This is redundant and only acts as a placeholder rn
    [ObservableProperty]
    private string _debug  = "Debug Statement!";

    
    public HomeViewModel()
    {
    }
    
    // Will change from string to a proper datatype later
    public ObservableCollection<string> RecentDocuments { get; } = new() { "Doc 1", "Doc 2", "Doc 3" };

    [RelayCommand]
    private void NewFile()
    {
        MainWindowViewModel.Navigate(new EditorViewModel());
    }

    [RelayCommand]
    private async void OpenFile()
    {
        var file = await FileService.Instance.OpenFileAsync();

        if (file is null) return;
        
        // We can add Validation for file extension either here or add file
        // patterns separately in the OpenFile function itself, for now I am not doing that

        var path = file.Path.LocalPath;
        MainWindowViewModel.Navigate(await EditorViewModel.CreateAsync(path));
    }
    

    
}