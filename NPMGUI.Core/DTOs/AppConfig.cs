using Newtonsoft.Json;
using NPMGUI.Core.Services.PackageManagement;

namespace NPMGUI.Core.DTOs
{
    public class AppConfig
    {
        [JsonProperty("package_manager")] public PackageManager? PackageManager { get; set; } = null;
    }
}
