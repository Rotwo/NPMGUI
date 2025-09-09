namespace NPMGUI.Core.Setup;

using NPMGUI.Core.Configuration;

public static class SetupManager
{
    public static void Initialize(string workDir)
    {
        ConfigLoader.Load(workDir);
        // Logger.Initialize();
    }
}
