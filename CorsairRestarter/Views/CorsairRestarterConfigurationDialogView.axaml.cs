using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CorsairRestarter.ViewModels;

namespace CorsairRestarter.Views;

public partial class CorsairRestarterConfigurationDialogView : ReactiveUserControl<CorsairRestarterConfigurationDialogViewModel>
{
    public CorsairRestarterConfigurationDialogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}