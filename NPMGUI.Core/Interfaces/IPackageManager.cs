namespace NPMGUI.Core.Interfaces;

public interface IPackageManager
{
    void InstallPackage(string packageName);
    bool IsMatch(string workDir);
}