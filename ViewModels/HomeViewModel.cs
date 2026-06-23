using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quill.Models;
using System.Collections.ObjectModel;
namespace Quill.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    // This is redundant and only acts as a placeholder rn
    [ObservableProperty]
    private string _debug  = "Debug Statement!";

    
    public HomeViewModel()
    {
    }

    public ObservableCollection<RecentDocument> RecentDocuments => RecentDocumentsViewModel.Documents;
    //not stor docs directly but shares stored so ui automatically in sync
    [RelayCommand]
    private void OpenFile()
    {
        
    }
}