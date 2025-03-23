using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Core;

public enum EncounterType
{
    Diplomacy,
    Combat,  
    FleetManagement,
    EnterSystem,
    Trade,
    Disinformation,
    Sabatoge,
    Espionage,
    UnenhabitedSystem,
}
public class EncounterManager : MonoBehaviour
{
    public List<EncounterController> EncounterControllers = new List<EncounterController>(); // EncounterController is not MonoBehavior

    public static EncounterManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public EncounterController FirstContactFleetOnFleet(FleetController fleetA, FleetController fleetB)
    {
        // is frist contact ********TODO: use encounters to update diplomacy relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.FleetContollerCivTwo = fleetB;
        EncounterController encounterController = new EncounterController(encounterData);
        EncounterControllers.Add(encounterController);
        encounterController.EncounterData.isCompleted = false;
        return encounterController;
    }
    public EncounterController FirstContactFleetOnStarSys(FleetController fleetCon, StarSysController starSysCon)
    {
        // instantiation is frist contact
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetCon;
        encounterData.StarSysController = starSysCon;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData.FleetControllerCivOne = fleetCon;
        encounterController.EncounterData.StarSysController = starSysCon;
        return encounterController; 

    }
    public void ResolveEncounterType(EncounterType encounter)
    {
        EncounterType encounterType = EncounterType.Diplomacy;


        switch (encounterType)
        {
            case EncounterType.Diplomacy: // this encoutner sends to DiplomacyManager to decide on combat or other diplomacy.
                break;
            case EncounterType.FleetManagement: // this encoutner sends to FleetManager to decide on redistribution of ships or other fleet management tasks.
                break;
            case EncounterType.EnterSystem:
                break;
            case EncounterType.Trade:
                break;
            case EncounterType.Espionage:
                break;
            case EncounterType.UnenhabitedSystem:
                break;
            default:
                break;
        }
    }
    public bool FoundEncoutnerController(CivController civPartyOne, CivController civPartyTwo) //, GameObject hitGO)
    {
        bool found = false;
        for (int i = 0; i < EncounterControllers.Count; i++)
        {
            if (EncounterControllers[i] != null)
            {
                if (EncounterControllers[i].EncounterData.CivOne == civPartyOne && EncounterControllers[i].EncounterData.CivTwo == civPartyTwo
                    || EncounterControllers[i].EncounterData.CivTwo == civPartyOne && EncounterControllers[i].EncounterData.CivOne == civPartyTwo)
                {
                    found = true;
                    break;
                }
            }
        }

        return found;
    }
    public EncounterController GetEncounterController(CivController civPartyOne, CivController civPartyTwo)
    {
         EncounterController encounterController= null;
        for (int i = 0; i < EncounterControllers.Count; i++)
        {
            if (EncounterControllers[i] != null && ((EncounterControllers[i].EncounterData.CivOne == civPartyOne && EncounterControllers[i].EncounterData.CivTwo == civPartyTwo)
                || (EncounterControllers[i].EncounterData.CivOne == civPartyTwo && EncounterControllers[i].EncounterData.CivTwo == civPartyOne)))
            {
                encounterController = EncounterControllers[i];
                break;
            }
        }
        return encounterController;
    }
}
