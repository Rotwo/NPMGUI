using NPMGUI.Core.DTOs;
using NPMGUI.Core.Helpers;
using NPMGUI.Core.Models;
using System.Diagnostics;
using System.IO;

namespace NPMGUI.Core
{
    public class NPMGUICore(PackageManager? pm)
    {
        private PackageManager _pm = pm ?? PackageManager.None;
        private string? _workPath = null;
        private readonly PackageHelper _helper = new();

        public StatusCode Setup(string? workDir)
        {
            var checkDir = workDir ?? Environment.CurrentDirectory;
            var validPath = CheckValidPath(checkDir);
            if(!validPath)
            {
                return StatusCode.Invalid_Path;
            }
            _workPath = checkDir;
            return StatusCode.Success;
        }

        private static bool CheckValidPath(string path)
        {
            // Use current dir as workDir and find if it is a valid NPM project
            var checkDir = path;
            var packageJsonFilePath = Path.Join(checkDir, "package.json");

            return File.Exists(packageJsonFilePath);
        }

        public void Configure()
        {
            if (_workPath == null)
            {
                Console.WriteLine("Unable to find work path");
                return;
            }
            
            // Create internal path .npmgui to store configs such as package manager
            AppHelper.Instance.SetupProjectConfig(_workPath);
        }

        public PackageListing? ListPackages ()
        {
            if (_workPath == null)
            {
                Console.WriteLine("Unable to find work path");
                return null;
            }
            
            var packages = _helper.FindPackagesOnDir(_workPath);
            return packages;
        }

        private void FindPackageManager()
        {

        }
    }
}
