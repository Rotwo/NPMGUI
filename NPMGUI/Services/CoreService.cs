using NPMGUI.Core;
using NPMGUI.Interfaces;

namespace NPMGUI.Helpers
{
    internal class CoreService : ICoreService
    {
        public static CoreService _instance;
        private static readonly object _lock = new object();


        public NPMGUICore? core;

        public static CoreService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CoreService();
                    }
                    return _instance;
                }
            }
        }
    }
}
