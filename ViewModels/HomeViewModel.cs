using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quill.Models;

namespace Quill.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    // This is redundant and only acts as a placeholder rn
    [ObservableProperty]
    private string _debug  = "Debug Statement!";
    //not store docs directly but shares stored so ui automatically in sync
    public ObservableCollection<RecentDocument> RecentDocuments => RecentDocumentsViewModel.Documents;

    public HomeViewModel()
    {
    }
    
    [RelayCommand]
    private static void NewFile()
    {
        MainWindowViewModel.Navigate(new EditorViewModel());
    }

    [RelayCommand]
    private static async Task OpenFile()
    {
        var file = await FileService.OpenFileAsync();

        if (file is null) return;
        
        // We can add Validation for file extension either here or add file
        // patterns separately in the OpenFile function itself, for now I am not doing that

        var path = file.Path.LocalPath;
        MainWindowViewModel.Navigate(await EditorViewModel.CreateAsync(path));
    }    
}