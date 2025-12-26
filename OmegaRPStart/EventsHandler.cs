using LabApi.Events.CustomHandlers;
using MapGeneration;
using MEC;

namespace OmegaRPStart;
public class EventsHandler : CustomEventsHandler
{
    public override void OnServerRoundStarted()
    {
        if (Plugin.Announce.Config != null)
        {
            
            var sub = Plugin.Announce.Config.CustomSubtitles;
            var voice = Plugin.Announce.Config.Words;
            LabApi.Features.Wrappers.Cassie.Message(voice, sub);
            Timing.RunCoroutine(Plugin.Announce.Config.LockDownZone(38, true));
        }
        else
            return;
    }
}