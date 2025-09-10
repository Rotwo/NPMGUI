using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NPMGUI.Data;
using NPMGUI.Factories;
using NPMGUI.Helpers;
using NPMGUI.Views;

namespace NPMGUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private PageFactory _pageFactory;
    
    [ObservableProperty]
    private PageViewModel _currentPage;
    
    [ObservableProperty]
    private bool _isCoreAvailable;
    
    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        if (CoreService.Instance.Core != null) _isCoreAvailable = CoreService.Instance.Core.IsReady;
        CoreService.Instance.OnCoreChanged += (sender, core) => IsCoreAvailable = core.IsReady;
        
        GoToPackages();
    }

    [RelayCommand]
    private void GoToPackages() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Packages);

    [RelayCommand]
    private void GoToSearch() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Search);
    
    [RelayCommand]
    private void GoToScripts() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Scripts);
}
