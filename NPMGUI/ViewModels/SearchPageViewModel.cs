using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NPMGUI.Data;
using NPMGUI.DTOs;
using NPMGUI.Helpers;
using NPMGUI.Interfaces;
using NPMGUI.Views;

namespace NPMGUI.ViewModels;

public partial class SearchPageViewModel : PageViewModel
{
    private readonly IApiService _apiService;
    private readonly IProcessService _processService;
    
    public SearchPageViewModel(IApiService apiService, IProcessService processService)
    {
        _apiService = apiService;
        _processService = processService;
        
        PageName = ApplicationPagesName.Search;

        processService.ProcessListChanged += (sender, args) =>
        {
            Console.WriteLine("ProcessListChanged");
            Console.WriteLine(processService.GetRunningProcesses().ToArray().ToString());
        };
    }
    
    [ObservableProperty]
    private string? _searchText;

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsPackageSelected))]
    private Package? _selectedPackage;
    
    public bool IsPackageSelected =>  SelectedPackage != null;
    
    public ObservableCollection<Result> Results { get; } = new();
    
    
    [RelayCommand]
    private async void Search()
    {
        try
        {
            Results.Clear();
            if (string.IsNullOrWhiteSpace(SearchText))
                return;

            var result = await _apiService.SearchPackageAsync(SearchText);
            if(result?.Results is null) return;

            foreach (var item in result.Results)
            {
                Results.Add(item);
            }
            
            Console.WriteLine(Results.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    private void UnselectPackage() => SelectedPackage = null;

    [RelayCommand]
    private async void InstallPackage(Package selectedPackage)
    {
        List<string> errorMessages = [];
        var action = CoreService.Instance.Core.InstallPackage(selectedPackage.Name, selectedPackage.Version, false, OnOutput, OnError);
        
        void OnError(string obj)
        {
            errorMessages.Add(obj);
            Console.WriteLine(obj);
        }

        void OnOutput(string obj)
        {
            Console.WriteLine(obj);
        }

        _processService.RegisterProcess(action.Process);

        await action.Process.WaitForExitAsync();

        if (errorMessages.Any())
        {
            Dispatcher.UIThread.Post(() =>
            {
                var errorWindow = new ScriptInstallErrorWindow
                {
                    DataContext = new ScriptInstallationErrorViewModel(errorMessages)
                };
                errorWindow.Show();
            });
        }
        
        _processService.UnregisterProcess(action.Process);
    }
}