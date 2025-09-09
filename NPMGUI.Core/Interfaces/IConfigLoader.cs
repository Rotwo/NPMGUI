using NPMGUI.Core.DTOs;

namespace NPMGUI.Core.Interfaces;

public interface IConfigLoader
{
    void Load(string workDir);
    AppConfig GetConfig();
    void Save(string workDir);
}