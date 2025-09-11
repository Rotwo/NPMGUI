using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NPMGUI.Data;
using NPMGUI.Factories;
using NPMGUI.Helpers;
using NPMGUI.Interfaces;
using NPMGUI.Views;

namespace NPMGUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private PageFactory _pageFactory;
    private readonly IProcessService _processService;
    
    [ObservableProperty]
    private PageViewModel _currentPage;
    
    [ObservableProperty]
    private bool _isCoreAvailable;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProcessesRunningText))]
    private int _processesRunningCount = 0;
    
    public string ProcessesRunningText => $"{ProcessesRunningCount} {(ProcessesRunningCount != 1 ? "processes" : "process")} running";
    
    public MainViewModel(PageFactory pageFactory, IProcessService processService)
    {
        _pageFactory = pageFactory;
        _processService = processService;
        
        if (CoreService.Instance.Core != null) _isCoreAvailable = CoreService.Instance.Core.IsReady;
        CoreService.Instance.OnCoreChanged += (sender, core) => IsCoreAvailable = core.IsReady;
        
        GoToPackages();

        _processService.ProcessListChanged += (sender, args) =>
            ProcessesRunningCount = _processService.GetRunningProcesses().Count;
    }

    [RelayCommand]
    private void GoToPackages() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Packages);

    [RelayCommand]
    private void GoToSearch() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Search);
    
    [RelayCommand]
    private void GoToScripts() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPagesName.Scripts);
}
