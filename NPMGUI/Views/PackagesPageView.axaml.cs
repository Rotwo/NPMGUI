using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NPMGUI.Views;

public partial class PackagesPageView : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public PackagesPageView()
    {
        InitializeComponent();
    }
}