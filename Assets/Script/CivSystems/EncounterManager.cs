using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Core;

public enum EncounterType
{
    FirstContact,
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
    //public int EncounterID;
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
        //EncounterID = -9999;
    }
    public EncounterController FirstContactFleetOnFleetNewEncounter(FleetController fleetA, FleetController fleetB)
    {
        // is frist contact ********TODO: if not first then use encounters to update diplomacy relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.FleetContollerCivTwo = fleetB;
        encounterData.EncounterType = EncounterType.FirstContact;
        //encounterData.EncoutnerID = GetNewEncounterID();
        EncounterController encounterController = new EncounterController(encounterData);
        EncounterControllers.Add(encounterController);
        encounterController.EncounterData.isCompleted = false;
        return encounterController;
    }
    public EncounterController FirstContactFleetOnStarSysNewEncounnter(FleetController fleetCon, StarSysController starSysCon)
    {
        // instantiation is frist contact
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetCon;
        encounterData.StarSysController = starSysCon;
        encounterData.EncounterType = EncounterType.FirstContact;
        //encounterData.EncoutnerID = GetNewEncounterID();
        EncounterController encounterController = new EncounterController(encounterData);
        EncounterControllers.Add(encounterController);
        encounterController.EncounterData.FleetControllerCivOne = fleetCon;
        encounterController.EncounterData.StarSysController = starSysCon;
        return encounterController; 

    }
    public EncounterController FleetManagementNewEncounter(FleetController fleetA, FleetController fleetB)
    {
        // is frist contact ********TODO: if not first then use encounters to update diplomacy relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.FleetContollerCivTwo = fleetB;
        encounterData.EncounterType = EncounterType.FleetManagement;
        //encounterData.EncoutnerID = GetNewEncounterID();
        EncounterController encounterController = new EncounterController(encounterData);
        EncounterControllers.Add(encounterController);
        encounterController.EncounterData.isCompleted = false;
        return encounterController;
    }
    public EncounterController FeetsNotSameCivNewEncounter(FleetController fleetA, FleetController fleetB)
    {
        // is frist contact ********TODO: if not first then use encounters to update diplomacy relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.FleetContollerCivTwo = fleetB;
        encounterData.EncounterType = EncounterType.Diplomacy;
        //encounterData.EncoutnerID = GetNewEncounterID();
        EncounterController encounterController = new EncounterController(encounterData);
        EncounterControllers.Add(encounterController);
        encounterController.EncounterData.isCompleted = false;
        return encounterController;
    }
    //private int GetNewEncounterID()
    //{
    //    EncounterID++;
    //    if (EncounterID >= 9999)
    //        EncounterID = -9999;
    //    return EncounterID;
    //}
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
}
