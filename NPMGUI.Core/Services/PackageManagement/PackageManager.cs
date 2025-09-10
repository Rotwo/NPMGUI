using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Helpers;

namespace NPMGUI.Core.Services.PackageManagement;

public abstract class PackageManager() : IPackageManager
{
    public abstract string Alias { get; }
    public abstract string[] LookupFiles { get; }

    protected abstract string BuildInstallCommand(string package, bool isDevDependency);

    public async Task<TaskStatus> InstallPackage(string package, string workDir, bool isDevDependency = false)
    {
        var command = BuildInstallCommand(package, isDevDependency);
        return await ProcessExecutor.RunAsync(Alias, command, workDir);
    }

    public bool IsMatch(string workDir)
    {
        bool isMatch = LookupFiles.All(f => File.Exists(Path.Combine(workDir, f)));
        return isMatch;
    }
}