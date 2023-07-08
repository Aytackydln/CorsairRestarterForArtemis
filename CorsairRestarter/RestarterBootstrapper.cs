using Artemis.Core;
using Artemis.UI.Shared;
using CorsairRestarter.ViewModels;

namespace CorsairRestarter;

public class RestarterBootstrapper : PluginBootstrapper
{
    public override void OnPluginEnabled(Plugin plugin)
    {
        base.OnPluginEnabled(plugin);

        plugin.ConfigurationDialog = new PluginConfigurationDialog<CorsairRestarterConfigurationDialogViewModel>();
    }
}