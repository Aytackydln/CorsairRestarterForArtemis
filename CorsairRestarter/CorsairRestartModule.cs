using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;

namespace CorsairRestarter;

[PluginFeature(Name = "CorsairRestarter")]
public class CorsairRestartModule : Module
{
    private readonly IPluginManagementService _pluginManagementService;
    private readonly PluginSetting<int> _minutesSetting;
    private readonly PluginSetting<int> _pauseSetting;

    private Timer? _timer;
    private Plugin? _corsairPlugin;

    public CorsairRestartModule(IPluginManagementService pluginManagementService, PluginSettings settings)
    {
        _pluginManagementService = pluginManagementService;
        _minutesSetting = settings.GetSetting("Minutes", 60);
        _pauseSetting = settings.GetSetting("Pause", 0);
    }

    public override void Enable()
    {
        _corsairPlugin = _pluginManagementService.GetAllPlugins()
            .Find(p => p.Guid.ToString() == "926629ab-8170-42f3-be18-22c694aa91cd");
        if (_corsairPlugin == null) return;

        SetTimer();
        _minutesSetting.SettingChanged += OnSettingChanged;
        _pauseSetting.SettingChanged += OnSettingChanged;
    }

    private void OnSettingChanged(object? sender, EventArgs e)
    {
        _timer?.Dispose();
        SetTimer();
    }

    private void SetTimer()
    {
        var period = TimeSpan.FromMinutes(_minutesSetting.Value);
        _timer = new Timer(TimerCallback, null, period, period);
    }

    private void TimerCallback(object? state)
    {
        if (_corsairPlugin == null)
        {
            return;
        }

        _pluginManagementService.DisablePlugin(_corsairPlugin, false);
        Thread.Sleep(TimeSpan.FromSeconds(_pauseSetting.Value));
        _pluginManagementService.EnablePlugin(_corsairPlugin, false);
    }

    public override void Disable()
    {
        _timer?.Dispose();
        _timer = null;
    }

    public override void Update(double deltaTime)
    {
    }

    public override List<IModuleActivationRequirement>? ActivationRequirements => null;
}