using NPMGUI.Core.DTOs;
using NPMGUI.Core.Models;
using System.Diagnostics;
using System.IO;
using NPMGUI.Core.Factories;
using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Services.ConfigLoader;
using NPMGUI.Core.Services.PackageManagement;
using NPMGUI.Core.Services.PackageService;
using NPMGUI.Core.Setup;

namespace NPMGUI.Core
{
    public class NPMGUICore
    {
        private string? _workDir;
        private readonly IConfigLoader _configLoader;
        private readonly IPackageService _packageService;
        private readonly PackageManagerFactory _factory;
                
        public bool IsReady => IsValidWorkDir(_workDir);

        public NPMGUICore(IConfigLoader configLoader, IPackageService packageService, PackageManagerFactory factory)
        {
            _configLoader = configLoader;
            _packageService = packageService;
            _factory = factory;
        }
        
        public void Setup(string? workDir)
        {
            if (!IsValidWorkDir(workDir))
                throw new InvalidOperationException("Invalid work directory");

            _workDir = workDir;
        }

        public void Shutdown()
        {
            _configLoader.Save(_workDir!);
        }

        private static bool IsValidWorkDir(string? workDir) => workDir != null && Path.Exists(workDir) && File.Exists(Path.Combine(workDir, "package.json"));

        public PackageListing ListPackages()
        {
            var packages = _packageService.FindDependenciesOnDir(_workDir!);
            return packages;
        }

        public ScriptsListing ListScripts()
        {
            var scripts = _packageService.FindScriptsOnDir(_workDir!);
            return scripts;
        }

        public ProcessExecution InstallPackage(string packageName, string packageVersion, bool isDevDependency = false, Action<string>? onOutput = null,  Action<string>? onError = null)
        {
            return _packageService.InstallPackage(packageName, packageVersion, _workDir!, isDevDependency, onOutput, onError);
        }

        public ProcessExecution RunScript(string script, Action<string>? onOutput = null,  Action<string>? onError = null)
        {
            return _packageService.RunScript(script, _workDir!, onOutput, onError);
        }
    }
}
