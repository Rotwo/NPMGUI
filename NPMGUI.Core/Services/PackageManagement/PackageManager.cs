namespace NPMGUI.Core.Services.PackageManagement;

public abstract class PackageManager() : IPackageManager
{
    public string Alias { get; set; }
    public string[] LookupFiles { get; set; }


    public abstract void InstallPackage(string packageName);
    public bool IsMatch(string workDir)
    {
        bool isMatch = LookupFiles.All(f => File.Exists(Path.Combine(workDir, f)));
        return isMatch;
    }
}