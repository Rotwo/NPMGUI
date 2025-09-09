using Newtonsoft.Json;
using NPMGUI.Core.Data.PackageManagement;

namespace NPMGUI.Core.Models
{
    public class AppConfig
    {
        [JsonProperty("package_manager")] public PackageManager? PackageManager { get; set; } = null;
    }
}
