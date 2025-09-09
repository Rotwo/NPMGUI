using NPMGUI.Core.DTOs;
using NPMGUI.Core.Models;
using System.Diagnostics;
using System.IO;
using NPMGUI.Core.Factories;
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
        
        private IPackageManager _packageManager;
        
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
            
            _packageManager = _factory.Create(workDir!);
            if (_packageManager is null)
                throw new InvalidOperationException("No valid package manager found for this directory");

            _configLoader.Load(workDir!);
        }

        private static bool IsValidWorkDir(string? workDir) => workDir != null && Path.Exists(workDir) && File.Exists(Path.Combine(workDir, "package.json"));
        
        
    }
}
