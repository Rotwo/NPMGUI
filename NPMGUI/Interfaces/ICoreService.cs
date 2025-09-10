using NPMGUI.Core;

namespace NPMGUI.Interfaces;

public interface ICoreService
{
    NPMGUICore ReInstantiateCore(string workDir);
}