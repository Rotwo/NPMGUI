namespace NPMGUI.Core.Data.PackageManagement;

public class PnpmPackageManager : PackageManager
{
    public PnpmPackageManager()
    {
        Alias = "pnpm";
        LookupFiles = ["pnpm-lock.yaml"];
    }
    
    public override void InstallPackage(string packageName)
    {
        throw new NotImplementedException();
    }
}