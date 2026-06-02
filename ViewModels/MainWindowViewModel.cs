using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Quill.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INavigation
{
    [ObservableProperty]
    private ViewModelBase _currentView;
    
    public MainWindowViewModel()
    {
        CurrentView = new HomeViewModel(this);
    }
    
    public void Navigate(ViewModelBase vm)
    {
        CurrentView = vm;
    }
}
