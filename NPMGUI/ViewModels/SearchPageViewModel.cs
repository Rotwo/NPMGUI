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
using NPMGUI.Services;
using NPMGUI.Views;

namespace NPMGUI.ViewModels;

public partial class SearchPageViewModel : PageViewModel
{
    private readonly IApiService _apiService;
    private readonly ITasksService _tasksService;
    private readonly IPackageService _packageService;
    
    public SearchPageViewModel(IApiService apiService, ITasksService tasksService, IPackageService packageService)
    {
        _apiService = apiService;
        _tasksService = tasksService;
        _packageService = packageService;
        
        PageName = ApplicationPagesName.Search;

        _tasksService.TaskListChanged += (sender, args) =>
            CanInstallSelectedPackage = SelectedPackage != null && _tasksService.FindTaskByParams(TaskType.Install, SelectedPackage.Name) == null;
        _canInstallSelectedPackage = true;
    }
    
    [ObservableProperty]
    private string? _searchText;

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsPackageSelected))]
    private Package? _selectedPackage;
    
    public bool IsPackageSelected => SelectedPackage != null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(InstallPackageCommand))]
    private bool _canInstallSelectedPackage;
    
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

    [RelayCommand(CanExecute = nameof(CanInstallSelectedPackage))]
    private async void InstallPackage(Package selectedPackage)
    {
        var (success, errors) = await _packageService.InstallPackageAsync(selectedPackage);

        if (!success)
        {
            Dispatcher.UIThread.Post(() =>
            {
                var errorWindow = new ScriptInstallErrorWindow
                {
                    DataContext = new ScriptInstallationErrorViewModel(errors)
                };
                errorWindow.Show();
            });
        }
    }
}