using CommunityToolkit.Mvvm.Input;

namespace Quill.ViewModels;

public partial class EditorViewModel : ViewModelBase
{
    [RelayCommand]
    private void GoToHome()
    {
        MainWindowViewModel.Navigate(new HomeViewModel());
    }
}