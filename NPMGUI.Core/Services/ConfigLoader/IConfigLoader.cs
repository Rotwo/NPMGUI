using NPMGUI.Core.DTOs;

namespace NPMGUI.Core.Services.ConfigLoader;

public interface IConfigLoader
{
    void Load(string workDir);
    AppConfig GetConfig();
    void Save(string workDir);
}