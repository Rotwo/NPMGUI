using CommunityToolkit.Mvvm.ComponentModel;
using NPMGUI.Data;

namespace NPMGUI.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPagesName _pageName;
}