namespace NPMGUI.Core.Services.PackageManagement;

public class NpmPackageManager : PackageManager
{
    public NpmPackageManager()
    {
        Alias = "npm";
        LookupFiles = ["package-lock.json"];
    }
    
    public override void InstallPackage(string packageNam)
    {
        throw new NotImplementedException();
    }
}