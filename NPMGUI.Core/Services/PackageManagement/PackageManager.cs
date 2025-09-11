using NPMGUI.Core.DTOs;
using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Helpers;

namespace NPMGUI.Core.Services.PackageManagement;

public abstract class PackageManager() : IPackageManager
{
    public abstract string Alias { get; }
    public abstract string[] LookupFiles { get; }

    protected abstract string BuildInstallCommand(string package, bool isDevDependency);
    protected abstract string BuildScriptCommand(string script);

    public ProcessExecution InstallPackage(
        string package,
        string workDir,
        bool isDevDependency = false,
        Action<string>? onOutput = null,
        Action<string>? onError = null)
    {
        var command = BuildInstallCommand(package, isDevDependency);
        return ProcessExecutor.RunAsync(Alias, command, workDir, onOutput, onError);
    }

    public ProcessExecution RunScript(
        string script,
        string workDir,
        Action<string>? onOutput = null,
        Action<string>? onError = null)
    {
        var command = BuildScriptCommand(script);
        return ProcessExecutor.RunAsync(Alias, command, workDir, onOutput, onError);
    }

    public bool IsMatch(string workDir)
    {
        var isMatch = LookupFiles.All(f => File.Exists(Path.Combine(workDir, f)));
        return isMatch;
    }
}