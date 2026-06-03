using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        Debug = Debug == "Click 1" ? "Click 2" : "Click 1";
    }
    
    [RelayCommand]
    private void GoToEditor()
    {
        MainWindowViewModel.Navigate(new EditorViewModel());
    }
    
}