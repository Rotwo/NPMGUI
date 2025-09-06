using Newtonsoft.Json;
using NPMGUI.Core.Models;

namespace NPMGUI.Core.Helpers
{
    internal class AppHelper
    {
        public static AppHelper _instance;
        private static readonly object _lock = new object();
        private const string configFile = "config.json";

        private AppHelper()
        {

        }

        public static AppHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppHelper();
                    }
                    return _instance;
                }
            }
        }

        public void SetupProjectConfig(string workDir)
        {
            Console.WriteLine("Setting up project config");
            
            var pointDir = Path.Join(workDir, ".npmgui");

            if (!Directory.Exists(pointDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(pointDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                var config = new Config
                {
                    PackageManager = PackageManager.None
                };

                var configPath = Path.Join(pointDir, configFile);
                using (StreamWriter writer = File.CreateText(configPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, config);
                }
            }
            else
            {
                if (Directory.Exists(pointDir) && !File.Exists(Path.Join(pointDir, configFile))) {
                    var config = new Config
                    {
                        PackageManager = PackageManager.None
                    };

                    var configPath = Path.Join(pointDir, configFile);
                    using (StreamWriter writer = File.CreateText(configPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, config);
                    }
                }
            }
        }
    }
}
