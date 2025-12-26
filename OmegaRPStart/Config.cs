using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Wrappers;
using MEC;

namespace OmegaRPStart;

public class Config
{
    public string Words { get; set; } =
        "$PITCH_0.25 $STUTTER_0.020_0.13_1 .g4 $PITCH_1 ALERT, ALL PERSONNEL. DIFFERENT KETER AND EUCLID CLASS BREACH IS DETECTED. SITE LOCKDOWN INITIATED. $PITCH_0.60 .G1 $PITCH_0.8 $STUTTER_0.043_0.13_1 DO NOT $STUTTER_0.043_0.13_2 PANIC. $PITCH_1.50 $STUTTER_0.010_0.13_3 .G1 $PITCH_1 FIND SHELTER AND A $STUTTER_0.043_0.13_4 WAIT FOR A SECURITY PERSONNEL AND OR A MTFUNIT TO $STUTTER_0.060_0.13_3 ESCORT YOU OUT OF THE FACILITY.";

    public string CustomSubtitles { get; set; } =
        "Allerta a tutto il personale: Identificate multiple breccie di tipo <color=#ffff00>Euclid</color> e <color=#ff0000>Keter</color>. Lockdown del sito inizializzato.<br><color=#990000><size=40><b>NIENTE PANICO.</b></size></color><br>Cercare rifugio ed aspettare che il <color=#a0a0a0>personale di sicurezza</color> e/o una unità della <color=#00b7eb>Mobile Task Force</color> vi escorti fuori dalla struttura.";
    
    public IEnumerator<float>  LockDownZone(int duration, bool lightsOff)//funzione per simulare il lockdown della struttura (Grazie mrhootyhoot1 per avermi aiutato sul coroutine)
    {
        if (duration > 0)
        {
            if (lightsOff) Map.TurnOffLights(duration);
            DoorStatus(true);
            yield return Timing.WaitForSeconds(duration*1000);
            DoorStatus(false);
        }
        else
        {
            if (Door.List.ElementAt(0).IsLocked)
            {
                if(!LightsController.List.ElementAt(0).LightsEnabled) Map.TurnOnLights();
                DoorStatus(false);
            }
            else
            {
                if (lightsOff) Map.TurnOffLights(float.MaxValue);
                DoorStatus(true);
            }
        }
    }

    private void DoorStatus(bool close)
    {
        if (close)
        {
            foreach (var door in Door.List)
            {
                door.IsLocked = true;
            }
            foreach (var gate in Gate.List)
            {
                gate.IsLocked = true;
            }
            foreach (var checkpoint in CheckpointDoor.List)
            {
                checkpoint.IsLocked = true;
            }
            foreach (var bulkhead in BulkheadDoor.List)
            {
                bulkhead.IsLocked = true;
            }

            foreach (var elevator in ElevatorDoor.List)
            {
                elevator.IsLocked = true;
            }
        }
        else
        {
            foreach (var door in Door.List)
            {
                door.IsLocked = false;
            }
            foreach (var gate in Gate.List)
            {
                gate.IsLocked = false;
            }
            foreach (var checkpoint in CheckpointDoor.List)
            {
                checkpoint.IsLocked = false;
            }
            foreach (var bulkhead in BulkheadDoor.List)
            {
                bulkhead.IsLocked = false;
            }

            foreach (var elevator in ElevatorDoor.List)
            {
                elevator.IsLocked = false;
            }
        }
    }
}
