using NPMGUI.Data;

namespace NPMGUI.ViewModels;

public partial class ScriptsPageViewModel : PageViewModel
{
    public ScriptsPageViewModel()
    {
        PageName = ApplicationPagesName.Scripts;
    }
    
    public string Test => "Hello World";
}