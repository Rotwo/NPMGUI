using NPMGUI.Core.DTOs;
using NPMGUI.Core.Models;
using System.Diagnostics;
using System.IO;
using NPMGUI.Core.Services.ConfigLoader;
using NPMGUI.Core.Services.PackageManagement;
using NPMGUI.Core.Setup;

namespace NPMGUI.Core
{
    public class NPMGUICore
    {
        private string? _workDir;
        private IConfigLoader _configLoader;
        private IPackageManager _packageManager;

        public bool IsReady => IsValidWorkDir(_workDir);

        public NPMGUICore(string? workDir, IConfigLoader configLoader, IPackageManager packageManager)
        {
            _configLoader = configLoader;
            _packageManager = packageManager;
            
            if (!IsValidWorkDir(workDir))
            {
                throw new InvalidOperationException("Invalid work directory");
            }
            
            _workDir = workDir;
            _configLoader.Load(workDir!);
        }

        private static bool IsValidWorkDir(string? workDir) => workDir != null && Path.Exists(workDir) && File.Exists(Path.Combine(workDir, "package.json"));
        
        
    }
}
