using NPMGUI.Core;

namespace NPMGUI.Helpers
{
    internal class CoreInstance
    {
        public static CoreInstance _instance;
        private static readonly object _lock = new object();


        public NPMGUICore? core;

        public static CoreInstance Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CoreInstance();
                    }
                    return _instance;
                }
            }
        }

        public void SetupCore(string? workDir = null)
        {
            if(core == null)
                core = new NPMGUICore(pm: null);

            core.Setup(workDir: workDir);
            core.Configure();
        }
    }
}
