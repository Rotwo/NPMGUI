using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NPMGUI.DTOs;
using NPMGUI.Interfaces;
using NPMGUI.ViewModels;

namespace NPMGUI.Views;

public partial class SearchPageView : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public SearchPageView()
    {
        InitializeComponent();
    }

    private void OnSearchKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (DataContext is SearchPageViewModel vm)
            {
                vm.SearchCommand.Execute(null);
            }
        }
    }

    private void OnPackageClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Result result)
        {
            var selectedPackage = result.Package;
            if (DataContext is SearchPageViewModel vm)
            {
                vm.SelectedPackage = selectedPackage;
            }
        }
    }
}