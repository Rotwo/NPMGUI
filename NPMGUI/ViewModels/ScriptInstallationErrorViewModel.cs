using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NPMGUI.ViewModels;

public partial class ScriptInstallationErrorViewModel : ViewModelBase
{
    [ObservableProperty]
    private List<string> _errorMessages = [];
    
    public ScriptInstallationErrorViewModel(List<string> errorMessages)
    {
        _errorMessages = errorMessages;
    }

    [RelayCommand]
    private void CloseWindow(Window window)
    {
        window.Close();
    }
}