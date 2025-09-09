using NPMGUI.Core.DTOs;

namespace NPMGUI.Core.Interfaces;

public interface IPackageService
{
    PackageListing FindDependenciesOnDir(string workDir);
}