using NPMGUI.Core.Services.ConfigLoader;

namespace NPMGUI.Core.Setup;

public static class SetupManager
{
    public static void Initialize(string workDir)
    {
        ConfigLoader.Load(workDir);
        // Logger.Initialize();
    }
}
