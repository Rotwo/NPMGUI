using Microsoft.Extensions.DependencyInjection;
using NPMGUI.Core.Services.ConfigLoader;
using NPMGUI.Core.Services.PackageManagement;
using NPMGUI.Core.Services.PackageService;

namespace NPMGUI.Core.Setup;

public static class CoreStartup
{
    public static ServiceProvider Initialize(string workDir)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConfigLoader, ConfigLoader>();
        services.AddSingleton<IPackageManager, PackageManager>();
        services.AddSingleton<IPackageService, PackageService>();

        services.AddSingleton<NPMGUICore>();
        
        return services.BuildServiceProvider();
    }
}