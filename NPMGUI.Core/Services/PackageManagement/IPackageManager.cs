namespace NPMGUI.Core.Services.PackageManagement;

public interface IPackageManager
{
    void InstallPackage(string packageName);
    bool IsMatch(string workDir);
}