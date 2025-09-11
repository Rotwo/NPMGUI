using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NPMGUI.Views;

public partial class ScriptInstallErrorWindow : Window
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public ScriptInstallErrorWindow()
    {
        InitializeComponent();
    }
}