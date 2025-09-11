using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NPMGUI.Data;
using NPMGUI.DTOs;
using NPMGUI.Helpers;

namespace NPMGUI.ViewModels;

public partial class ScriptsPageViewModel : PageViewModel
{
    public ScriptsPageViewModel()
    {
        PageName = ApplicationPagesName.Scripts;
        
        CoreService.Instance.OnCoreChanged += (sender, core) => LoadScripts();
        LoadScripts();
    }

    public Dictionary<string, string>? _scripts;
    
    public ObservableCollection<ScriptItem> ScriptsList { get; } = new ();

    public Dictionary<string, string>? Scripts
    {
        get => _scripts;
        set
        {
            _scripts = value;
            ScriptsList.Clear();
            if (_scripts == null) return;
            foreach (var kvp in _scripts)
            {
                ScriptsList.Add(new ScriptItem() { Name = kvp.Key, Command = kvp.Value });
            }
        }
    }

    private void LoadScripts()
    {
        var listing = CoreService.Instance.Core?.ListScripts();
        Scripts = listing?.Scripts;
    }
}