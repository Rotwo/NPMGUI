using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NPMGUI.Data;
using NPMGUI.Factories;
using NPMGUI.Helpers;
using NPMGUI.ViewModels;
using NPMGUI.Views;

namespace NPMGUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collections = new ServiceCollection();
        
        collections.AddSingleton<MainViewModel>();
        
        collections.AddTransient<PackagesPageViewModel>();
        collections.AddTransient<ScriptsPageViewModel>();
        collections.AddTransient<SearchPageViewModel>();
        
        collections.AddSingleton<Func<ApplicationPagesName, PageViewModel>>(x => name => name switch
        {
            ApplicationPagesName.Packages => x.GetRequiredService<PackagesPageViewModel>(),
            ApplicationPagesName.Search  => x.GetRequiredService<SearchPageViewModel>(),
            ApplicationPagesName.Scripts => x.GetRequiredService<ScriptsPageViewModel>(),
            _ => throw new InvalidOperationException(),
        });
        
        collections.AddSingleton<PageFactory>();
        
        var services = collections.BuildServiceProvider();
        
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>(),
            };
        }
        /* else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }
        */

        base.OnFrameworkInitializationCompleted();
    }
}
