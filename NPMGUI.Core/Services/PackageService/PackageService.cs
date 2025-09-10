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
        
        public PackageListing FindDependenciesOnDir(string workingDir)
        {
            var jsonPath = Path.Combine(workingDir, "package.json");

            if(!Directory.Exists(workingDir)) throw new Exception("Invalid path");

            if (!Path.Exists(jsonPath)) throw new Exception("Invalid folder project, not found any package.json");

            using StreamReader reader = new(jsonPath);
            string rawJson = reader.ReadToEnd();

            Console.WriteLine(rawJson);

            var deserialized = JsonConvert.DeserializeObject<Package>(rawJson);
            return new PackageListing
            {
                Dependencies = deserialized?.Dependencies,
                DevDependencies = deserialized?.DevDependencies,
            };
        }

        public async Task<TaskStatus> InstallPackage(string packageName, string  packageVersion, string workDir, bool isDevDependency = false)
        {
            var manager = _factory.Create(workDir);

            if (manager is null)
            {
                throw new InvalidOperationException(
                    $"Not found valid package manager on {workDir}");
            }

            var package = string.Join("@", packageName, packageVersion);
                
            var taskResult = await manager.InstallPackage(package, workDir, isDevDependency);
            return taskResult;
        }
    }
}
