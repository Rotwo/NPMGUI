using System.Collections.Generic;
using System.Threading.Tasks;
using NPMGUI.DTOs;

namespace NPMGUI.Interfaces;

public interface IPackageService
{
    void LoadDependencies();
    Core.DTOs.PackageListing GetDependencies();
    Task<(bool success, List<string> errors)> InstallPackageAsync(Package selectedPackage);
}