using NPMGUI.Data;

namespace NPMGUI.ViewModels;

public partial class PackagesPageViewModel : PageViewModel
{
    public PackagesPageViewModel()
    {
        PageName = ApplicationPagesName.Packages;
    }
    
    public string Test => "Hello World";
}