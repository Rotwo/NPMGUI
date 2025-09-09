using Newtonsoft.Json;
using NPMGUI.Core.DTOs;
using NPMGUI.Core.Interfaces;
using NPMGUI.Core.Models;

namespace NPMGUI.Core.Helpers
{
    internal class PackageService : IPackageService
    {
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
    }
}
