using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController
{
    private EncounterData encounterData;
    public EncounterData EncounterData { get { return encounterData; } set { encounterData = value; } }

    public EncounterController(EncounterData encounterData)
    {
        EncounterData = encounterData;
    }
    // ******** ??? Resolve here or Diplomacy in Diplomacy, FleetqManagement in FleetManagement, etc.
    public void ResolveCombat()
    {
        //Stop Time
        //Debug.Log($"Battle between {EncounterData.FactionA.Name} and {EncounterData.FactionB.Name}");
        // Apply combat logic (e.g., damage, ship destruction, retreat)
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }

    public void ResolveDiplomacy()
    {
        //Debug.Log($"Diplomatic meeting between {encounterData.FactionA.Name} and {encounterData.FactionB.Name}");
        // Change diplomatic status
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }
    public void ResolveFleetManagment()
    {
        //Debug.Log($"Espionage operation between {encounterData.FactionA.Name} and {encounterData.FactionB.Name}");
        // Modify faction intelligence levels
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }
    public void ResolveEnterSystem()
    {
        //Debug.Log($"Espionage operation between {encounterData.FactionA.Name} and {encounterData.FactionB.Name}");
        // Modify faction intelligence levels
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }
    public void ResolveTrade()
    {
        //Debug.Log($"Trade established between {encounterData.FactionA.Name} and {encounterData.FactionB.Name}");
        // Modify resources
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }

    public void ResolveEspionage()
    {
        //Debug.Log($"Espionage operation between {encounterData.FactionA.Name} and {encounterData.FactionB.Name}");
        // Modify faction intelligence levels
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }
    public void ResolveUnenhabitedSystem()
    {
        //Debug.Log($"Espionage operation between {encounterData.FactionA.Name} and {encounterData.FactionB.Name}");
        // Modify faction intelligence levels
        EncounterData.isCompleted = true;
        // destroy the encounter controller
    }
}
