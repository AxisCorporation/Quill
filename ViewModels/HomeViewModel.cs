using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Quill.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    // This is redundant and only acts as a placeholder rn
    [ObservableProperty]
    private string debug  = "Debug Statement!";
    
    // Will change from string to a proper datatype later
    private ObservableCollection<string> RecentDocuments { get; } = new() { "Doc 1", "Doc 2", "Doc 3" };

    [RelayCommand]
    public void NewFile()
    {
        Debug = Debug == "Click 1" ? "Click 2" : "Click 1";
    }
    
}