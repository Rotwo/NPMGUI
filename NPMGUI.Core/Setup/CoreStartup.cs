using Microsoft.Extensions.DependencyInjection;
using NPMGUI.Core.Factories;
using NPMGUI.Core.Services.ConfigLoader;
using NPMGUI.Core.Services.PackageManagement;
using NPMGUI.Core.Services.PackageService;

namespace NPMGUI.Core.Setup;

public static class CoreStartup
{
    public static ServiceProvider Initialize(string workDir)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IPackageManager, NpmPackageManager>();
        services.AddSingleton<IPackageManager, PnpmPackageManager>();
        services.AddSingleton<PackageManagerFactory>();
        
        services.AddSingleton<IConfigLoader, ConfigLoader>();
        services.AddSingleton<IPackageService, PackageService>();

        services.AddSingleton<NPMGUICore>();
        
        return services.BuildServiceProvider();
    }
}