using System;
using NPMGUI.Data;
using NPMGUI.ViewModels;

namespace NPMGUI.Factories;

public class PageFactory(Func<ApplicationPagesName, PageViewModel> factory)
{
    public PageViewModel GetPageViewModel(ApplicationPagesName pageName) => factory.Invoke(pageName);
}