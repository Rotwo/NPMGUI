using NPMGUI.Core.Services.PackageManagement;

namespace NPMGUI.Core.Factories;

public class PackageManagerFactory
{
    private readonly List<PackageManager> _managers;
    
    public PackageManagerFactory()
    {
        _managers = new List<PackageManager>
        {
            new NpmPackageManager(),
            new PnpmPackageManager(),
        };
    }
    
    public PackageManager? Create(string workDir)
    {
        return _managers.Find(pm => pm.IsMatch(workDir));
    }
}