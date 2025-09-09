using NPMGUI.Core.DTOs;
using NPMGUI.Core.Helpers;
using NPMGUI.Core.Models;
using System.Diagnostics;
using System.IO;
using NPMGUI.Core.Setup;

namespace NPMGUI.Core
{
    public class NPMGUICore
    {
        private string? _workDir;

        public bool IsReady => IsValidWorkDir(_workDir);

        public NPMGUICore(string? workDir)
        {
            if (!IsValidWorkDir(workDir))
            {
                throw new InvalidOperationException("Invalid work directory");
            }
            
            _workDir = workDir;
            SetupManager.Initialize(workDir!);
        }

        private static bool IsValidWorkDir(string? workDir) => workDir != null && Path.Exists(workDir) && File.Exists(Path.Combine(workDir, "package.json"));
        
        
    }
}
