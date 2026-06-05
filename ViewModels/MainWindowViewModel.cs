using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Quill.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private static MainWindowViewModel? _instance;
    public static MainWindowViewModel Instance { get => _instance ??= new(); }

    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; private set;}
    
    private MainWindowViewModel()
    {
        CurrentView = new HomeViewModel();
    }
    
    public static void Navigate(ViewModelBase vm)
    {
        Instance.CurrentView = vm;
    }
}
