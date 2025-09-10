using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NPMGUI.Helpers;

namespace NPMGUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        
        // Initialize core on current work directory
        var workDir = Environment.CurrentDirectory;
        CoreService.Instance.InitializeCore(workDir);
    }

    private async void Select_Folder_Button(object? sender, RoutedEventArgs e)
    {
        try
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select a project folder"
            };

            var window = this.VisualRoot as Window;

            if (window == null) return;
            var result = await dialog.ShowAsync(window); // 'window' is your Avalonia Window
            if (result != null)
            {
                Console.WriteLine($"Selected folder: {result}");
                CoreService.Instance.ReInstantiateCore(result);
            }
        }
        catch (Exception ex)
        {
            throw; // TODO handle exception
        }
    }
    
}
