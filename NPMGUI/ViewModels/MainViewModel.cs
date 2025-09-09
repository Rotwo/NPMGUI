using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NPMGUI.Data;
using NPMGUI.Factories;
using NPMGUI.Views;

namespace NPMGUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private PageFactory _pageFactory;
    
    [ObservableProperty]
    private PageViewModel _currentPage;

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        
        GoToPackages();
    }

    [RelayCommand]
    private void GoToPackages() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Packages);

    [RelayCommand]
    private void GoToSearch() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Search);
    
    [RelayCommand]
    private void GoToScripts() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Scripts);
}
