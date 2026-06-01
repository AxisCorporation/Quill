namespace Quill.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public object CurrentView { get; }
    public string Greeting { get; } = "Welcome to Avalonia!";
    
    public MainWindowViewModel()
    {
        CurrentView = new HomeViewModel();
    }
}
