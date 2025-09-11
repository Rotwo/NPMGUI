using NPMGUI.Core.DTOs;
using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Services.PackageManagement;

namespace NPMGUI.Core.Services.PackageService;

public interface IPackageService
{
    PackageListing FindDependenciesOnDir(string workDir);
    ScriptsListing FindScriptsOnDir(string? workingDir);
    ProcessExecution InstallPackage(string packageName, string  packageVersion, string workDir, bool isDevDependency = false, Action<string>? onOutput = null, Action<string>? onError = null);
    ProcessExecution RunScript(string script, string workDir, Action<string>? onOutput = null, Action<string>? onError = null);
}