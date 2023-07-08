using Artemis.Core;
using Artemis.UI.Shared;

namespace CorsairRestarter.ViewModels;

public class CorsairRestarterConfigurationDialogViewModel : PluginConfigurationViewModel
{
    private readonly PluginSetting<int> _minutesSetting;
    public int Minutes
    {
        get => _minutesSetting.Value;
        set
        {
            _minutesSetting.Value = value;
            _minutesSetting.Save();
        }
    }
    
    private readonly PluginSetting<int> _pauseSetting;
    public int Pause
    {
        get => _pauseSetting.Value;
        set
        {
            _pauseSetting.Value = value;
            _pauseSetting.Save();
        }
    }

    public CorsairRestarterConfigurationDialogViewModel(Plugin plugin, PluginSettings settings) : base(plugin)
    {
        _minutesSetting = settings.GetSetting("Minutes", 60);
        _pauseSetting = settings.GetSetting("Pause", 0);
    }
}