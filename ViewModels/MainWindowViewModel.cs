using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Quill.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{


    /* Private static Instance property keeps track of the MainWindowViewModel that is managing our views.
       Typically, a singleton pattern is used involving a private constructor and a public static Instance.
       However, due to needing to protect members which Avalonia forces to be public,  */
    private static MainWindowViewModel? _instance;
    public static MainWindowViewModel Instance { get => _instance ??= new(); }

    [ObservableProperty]
    private ViewModelBase _currentView;
    
    private MainWindowViewModel()
    {
        CurrentView = new HomeViewModel();
    }
    
    public static void Navigate(ViewModelBase vm)
    {
        Instance.CurrentView = vm;
    }
}
