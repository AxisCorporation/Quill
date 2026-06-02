namespace Quill.ViewModels;

// Not completely sure if this is the best way to do this, but the implementation seems kinda nice
public interface INavigation
{
    void Navigate(ViewModelBase vm);
}