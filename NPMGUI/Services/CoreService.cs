using System;
using Microsoft.Extensions.DependencyInjection;
using NPMGUI.Core;
using NPMGUI.Interfaces;

namespace NPMGUI.Helpers
{
    internal class CoreService : ICoreService
    {
        private static CoreService _instance;
        private static readonly object _lock = new object();
        
        public NPMGUICore? Core;
        public EventHandler<NPMGUICore> OnCoreChanged;

        public bool IsCoreValid
        {
            get;
            private set;
        }

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

        public NPMGUICore? InitializeCore(string workDir)
        {
            if (Core == null)
            {
                var provider = NPMGUI.Core.Setup.CoreStartup.Initialize(workDir);
                Core = provider.GetRequiredService<NPMGUICore>();
                try
                {
                    Core.Setup(workDir);
                    IsCoreValid = true;
                    OnCoreChanged?.Invoke(this, Core);
                    return Core;
                }
                catch (Exception ex)
                {
                    // Invalid work dir provided or internal error
                    IsCoreValid = false;
                    return null;
                }
            }

            return null;
        }

        public NPMGUICore? ReInstantiateCore(string workDir)
        {
            Core = null;
            
            var provider = NPMGUI.Core.Setup.CoreStartup.Initialize(workDir);
            Core = provider.GetRequiredService<NPMGUICore>();
            try
            {
                Core.Setup(workDir);
                IsCoreValid = true;
                OnCoreChanged?.Invoke(this, Core);
                return Core;
            }
            catch (Exception ex)
            {
                // Invalid work dir provided or internal error
                IsCoreValid = false;
                return null;
            }
        }
    }
}
