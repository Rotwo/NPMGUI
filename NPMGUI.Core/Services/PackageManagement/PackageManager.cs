using NPMGUI.Core.Interfaces;

namespace NPMGUI.Core.Services.PackageManagement;

public abstract class PackageManager() : IPackageManager
{
    public string Alias { get; set; }
    public string[] LookupFiles { get; set; }
    
    public async virtual Task<TaskStatus> InstallPackage(string package, string workDir, bool isDevDependency = false)
    {
        throw new NotImplementedException();
    }

    public bool IsMatch(string workDir)
    {
        bool isMatch = LookupFiles.All(f => File.Exists(Path.Combine(workDir, f)));
        return isMatch;
    }
}