using System.Text.Json.Serialization;
using Newtonsoft.Json;
using NPMGUI.Core.Models;

namespace NPMGUI.Core.Configuration;

public static class ConfigLoader
{
    private static AppConfig? _config;
    private const string APP_CONFIG_FOLDER = ".npmgui";
    private const string CONFIG_FILENAME = "config.json";
    
    public static void Load(string workDir)
    {
        var configFile = Path.Combine(workDir, APP_CONFIG_FOLDER, CONFIG_FILENAME);

        if (!File.Exists(configFile))
        {
            Console.WriteLine($"Config file not found: {configFile}");
            Console.WriteLine("Creating new config file...");
            
            _config = new AppConfig();;
            Save(workDir);
            
            return;
        }
        
        var json = File.ReadAllText(configFile);
        _config = JsonConvert.DeserializeObject<AppConfig>(json);
    } 
    
    public static AppConfig GetConfig()
    {
        if (_config == null)
            throw new InvalidOperationException("Configuration not loaded.");

        return _config;
    }

    public static void Save(string workDir)
    {
        var configDir = Path.Combine(workDir, APP_CONFIG_FOLDER);
        var configFile = Path.Combine(configDir, CONFIG_FILENAME);
        var json = JsonConvert.SerializeObject(_config);

        if (!Directory.Exists(configDir))
        {
            DirectoryInfo di = Directory.CreateDirectory(configDir);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
        }
        
        File.WriteAllText(configFile, json);
        Console.WriteLine($"Config file saved to: {configFile}");
    }
}