using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NPMGUI.Core.Services.PackageManagement;

public class PnpmPackageManager : PackageManager
{
    public override string Alias => "pnpm";
    public override string[] LookupFiles => ["pnpm-lock.yaml"];

    protected override string BuildInstallCommand(string package, bool isDevDependency)
        => $"add {(isDevDependency ? "-D" : "")} {package}";
    
    protected override string BuildScriptCommand(string script) => $"run {script}";
}