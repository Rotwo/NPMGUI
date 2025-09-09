using Newtonsoft.Json;
using NPMGUI.Core.DTOs;
using NPMGUI.Core.Factories;
using NPMGUI.Core.Models;

namespace NPMGUI.Core.Services.PackageService
{
    public class PackageService : IPackageService
    {
        private PackageManagerFactory _packageManagerFactory = new();

        public PackageListing FindDependenciesOnDir(string workingDir)
        {
            var jsonPath = Path.Combine(workingDir, "/package.json");

            if(!Directory.Exists(workingDir)) throw new Exception("Invalid path");

            if (!Path.Exists(jsonPath)) throw new Exception("Invalid folder project, not found any project.json");

            using StreamReader reader = new(jsonPath);
            string rawJson = reader.ReadToEnd();

            Console.WriteLine(rawJson);

            var deserialized = JsonConvert.DeserializeObject<Package>(rawJson);
            return new PackageListing
            {
                Dependencies = deserialized.Dependencies,
                DevDependencies = deserialized.DevDependencies,
            };
        }

        public void InstallPackage(string package, string workDir)
        {
            var pm = _packageManagerFactory.Create(workDir);
            if(pm == null)
                throw new Exception("Package Manager not found");
            pm.InstallPackage(package);
        }
    }
}
