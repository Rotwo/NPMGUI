using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NPMGUI.DTOs;
using NPMGUI.Helpers;
using NPMGUI.Interfaces;

namespace NPMGUI.Services;

public class PackageService(ITasksService tasksService) : IPackageService
{
    private Core.DTOs.PackageListing depsListing = new();

    public void LoadDependencies()
    {
        depsListing = CoreService.Instance.Core?.ListPackages();
    }

    public Core.DTOs.PackageListing GetDependencies()
    {
        return depsListing;
    }
    
    public async Task<(bool success, List<string> errors)> InstallPackageAsync(Package selectedPackage)
    {
        List<string> errorMessages = [];

        var action = CoreService.Instance.Core.InstallPackage(
            selectedPackage.Name,
            selectedPackage.Version,
            false,
            null,
            (string message) => errorMessages.Add(message)
        );

        var task = new Task(TaskType.Install, selectedPackage.Name, action.Process);
        tasksService.RegisterProcess(task);

        await action.Process.WaitForExitAsync();

        tasksService.UnregisterProcess(task);

        return (errorMessages.Count == 0, errorMessages);
    }

}