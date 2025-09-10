using NPMGUI.Core.DTOs;
using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Services.PackageManagement;

namespace NPMGUI.Core.Services.PackageService;

public interface IPackageService
{
    PackageListing FindDependenciesOnDir(string workDir);
    Task<TaskStatus> InstallPackage(string packageName, string packageVersion, string workDir, bool isDevDependency = false);
}