using NPMGUI.Core.Services.PackageManagement;

namespace NPMGUI.Core.Factories;

public class PackageManagerFactory(IEnumerable<IPackageManager> managers)
{
    public IPackageManager? Create(string workDir)
    {
        return managers.FirstOrDefault(pm => pm.IsMatch(workDir));
    }
}