using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using NPMGUI.Data;
using NPMGUI.DTOs;
using NPMGUI.Helpers;

namespace NPMGUI.ViewModels;

public partial class PackagesPageViewModel : PageViewModel
{
    public PackagesPageViewModel()
    {
        PageName = ApplicationPagesName.Packages;

        CoreService.Instance.OnCoreChanged += (sender, core) => LoadPackages();
        LoadPackages();
    }

    public Dictionary<string, string>? _dependencies;
    public Dictionary<string, string>? _devDependencies;

    public ObservableCollection<DependencyItem> DependenciesList { get; } = new ();
    public ObservableCollection<DependencyItem> DevDependenciesList { get; } = new ();

    public Dictionary<string, string>? Dependencies
    {
        get => _dependencies;
        set
        {
            _dependencies = value;
            DependenciesList.Clear();
            if (_dependencies == null) return;
            foreach (var kvp in _dependencies)
            {
                DependenciesList.Add(new DependencyItem { Name =kvp.Key, Version = kvp.Value});
            }
        }
    }
    
    public Dictionary<string, string>? DevDependencies
    {
        get => _devDependencies;
        set
        {
            _devDependencies = value;
            DevDependenciesList.Clear();
            if (_devDependencies == null) return;
            foreach (var kvp in _devDependencies)
            {
                DevDependenciesList.Add(new DependencyItem { Name =kvp.Key, Version = kvp.Value});
            }
        }
    }

    private void LoadPackages()
    {
        Console.WriteLine("Loading packages ----------------------------------------------------:");

        var packages = CoreService.Instance.Core?.ListPackages();
        
        Console.WriteLine(JsonSerializer.Serialize(packages));
        
        Dependencies = packages?.Dependencies;
        DevDependencies = packages?.DevDependencies;
    }
}