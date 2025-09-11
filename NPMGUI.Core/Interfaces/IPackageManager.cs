using NPMGUI.Core.DTOs;

namespace NPMGUI.Core.Interfaces;

public interface IPackageManager
{
    ProcessExecution InstallPackage(string package,
        string workDir,
        bool isDevDependency = false,
        Action<string>? onOutput = null,
        Action<string>? onError = null);
    ProcessExecution RunScript(string script,
        string workDir,
        Action<string>? onOutput = null,
        Action<string>? onError = null);
    bool IsMatch(string workDir);
}