using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NPMGUI.Core.Services.PackageManagement;

public class NpmPackageManager : PackageManager
{
    public override string Alias => "npm";
    public override string[] LookupFiles => ["package-lock.json"];

    protected override string BuildInstallCommand(string package, bool isDevDependency)
        => $"install {(isDevDependency ? "-D" : "")} {package}";

    protected override string BuildScriptCommand(string script) => $"run {script}";
}