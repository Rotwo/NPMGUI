namespace NPMGUI.Core.Interfaces;

public interface IPackageManager
{
    Task<TaskStatus> InstallPackage(string package, string workDir, bool isDevDependency = false);
    bool IsMatch(string workDir);
}