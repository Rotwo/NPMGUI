using NPMGUI.Core.DTOs;

namespace NPMGUI.Core.Services.PackageService;

public interface IPackageService
{
    PackageListing FindDependenciesOnDir(string workDir);
   Task<TaskStatus> InstallPackage(string packageName, string packageVersion, string workDir, bool isDevDependency = false);
}