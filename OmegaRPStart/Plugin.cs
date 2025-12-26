using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;

namespace OmegaRPStart;

public class Plugin : Plugin<Config>
{
    public static Plugin Announce { get; set; } = null;
    public override string Name => "StartOmegaRP";
    public override string Description => "Robe fancy che succedono all'inizio di un round RP nel server OmegaRP";
    public override string Author => "Matt the toaster (@nomorefunnytagbecauseithadslurs)";
    public override Version Version => new  Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new  Version(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority => LoadPriority.Low;
    
    static EventsHandler EventsHandler { get; set; } = new();
    
    public override void Enable()
    {
        Announce = this;
        CustomHandlersManager.RegisterEventsHandler(EventsHandler);
    }

    public override void Disable()
    {
        Announce = null;
        CustomHandlersManager.UnregisterEventsHandler(EventsHandler);
    }
}