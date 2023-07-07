using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;

namespace CorsairRestarter;

[PluginFeature(Name = "CorsairRestarter")]
public class CorsairRestartModule : Module
{
    private readonly IPluginManagementService _pluginManagementService;

    private Timer? _timer;
    private Plugin? _corsairPlugin;

    public CorsairRestartModule(IPluginManagementService _pluginManagementService)
    {
        this._pluginManagementService = _pluginManagementService;
    }

    public override void Enable()
    {
        _corsairPlugin = _pluginManagementService.GetAllPlugins()
            .Find(p => p.Guid.ToString() == "926629ab-8170-42f3-be18-22c694aa91cd");
        if (_corsairPlugin != null)
        {
            _timer = new Timer(TimerCallback, null, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
        }
    }

    private void TimerCallback(object? state)
    {
        if (_corsairPlugin == null)
        {
            return;
        }
        _pluginManagementService.DisablePlugin(_corsairPlugin, false);
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

    public override List<IModuleActivationRequirement>? ActivationRequirements { get; }
}