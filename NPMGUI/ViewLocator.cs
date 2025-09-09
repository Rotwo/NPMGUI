using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using NPMGUI.ViewModels;
using NPMGUI.Views;


namespace NPMGUI;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;
        
        var viewName = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.InvariantCulture);
        var type = Type.GetType(viewName);
        
        if(type is null)
            return null;
        
        var control = (Control) Activator.CreateInstance(type)!;
        control.DataContext = data;
        return control;
    }

    public bool Match(object? data) => data is ViewModelBase;
}