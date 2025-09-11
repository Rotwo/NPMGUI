using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NPMGUI.Data;
using NPMGUI.DTOs;
using NPMGUI.Interfaces;

namespace NPMGUI.ViewModels;

public partial class SearchPageViewModel : PageViewModel
{
    private readonly IApiService _apiService;
    
    public SearchPageViewModel(IApiService apiService)
    {
        _apiService = apiService;
        
        PageName = ApplicationPagesName.Search;
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
}