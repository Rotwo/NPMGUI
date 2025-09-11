using Newtonsoft.Json;
using NPMGUI.Core.DTOs;
using NPMGUI.Core.Factories;
using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Models;

namespace NPMGUI.Core.Services.PackageService
{
    public class PackageService : IPackageService
    {
        private readonly PackageManagerFactory _factory;
        
        public PackageService(PackageManagerFactory factory)
        {
            _factory = factory;
        }

        private static Package? GetProjectPackage(string? workingDir)
        {
            if (workingDir is null) return null;
            
            var jsonPath = Path.Combine(workingDir, "package.json");
            if (!File.Exists(jsonPath)) throw new FileNotFoundException("Package.json not found", jsonPath);
            
            using StreamReader reader = new(jsonPath);
            var rawJson =  reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Package>(rawJson);
        }
        
        public PackageListing FindDependenciesOnDir(string? workingDir)
        {
            var package = GetProjectPackage(workingDir);
            if (package is null)
                return new PackageListing
                {
                    Dependencies = null,
                    DevDependencies = null
                };
            
            return new PackageListing
            {
                Dependencies = package?.Dependencies,
                DevDependencies = package?.DevDependencies,
            };
        }

        public ProcessExecution InstallPackage(string packageName, string  packageVersion, string workDir, bool isDevDependency = false, Action<string>? onOutput = null, Action<string>? onError = null)
        {
            var manager = _factory.Create(workDir);

            if (manager is null)
            {
                throw new InvalidOperationException(
                    $"Not found valid package manager on {workDir}");
            }

            var package = string.Join("@", packageName, packageVersion);
                
            var task = manager.InstallPackage(package, workDir, isDevDependency, onOutput, onError);
            return task;
        }

        public ScriptsListing FindScriptsOnDir(string? workingDir)
        {
            var package = GetProjectPackage(workingDir);
            if (package is null)
                return new ScriptsListing
                {
                    Scripts = null,
                };

            return new ScriptsListing
            {
                Scripts = package?.Scripts
            };
        }

        public ProcessExecution RunScript(string script, string workDir, Action<string>? onOutput = null, Action<string>? onError = null)
        {
            var manager = _factory.Create(workDir);
            if (manager is null) throw new InvalidOperationException($"Not found valid package manager on {workDir}");
            
            var taskResult = manager.RunScript(script, workDir, onOutput, onError);
            return taskResult;
        }
    }
}
