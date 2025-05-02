using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Core;

public enum EncounterType
{
    FirstContact,
    Diplomacy, // civ to civ and civs can be local player or AI
    Combat,  //? is this a subtype of Diplomacy as seen by Diplomacy
    FleetManagement, // fleet to fleet by one civ (local player or AI)
    EnterSystem,
    UninhabitedSystem,
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
    }
    public void RegisterEncounter(FleetController fleetConA, FleetController fleetConB)
    {
        EncounterController encounterController;
        //FleetController hitFleetController = collider.gameObject.GetComponent<FleetController>();// the collider is on the hit gameObject this fleet hit
        if (fleetConA != null) // it is a FleetController and not a StarSystem or other with collider                                                                                                                                                    leetController
        {
            //have we met before?
            if (!DiplomacyManager.Instance.FoundADiplomacyController(fleetConB.FleetData.CivController, fleetConA.FleetData.CivController))
            {
                // First Contact
                encounterController = FirstContactFleetOnFleetInstantiateEncounterController(fleetConA, fleetConB);
                encounterController.ResolveFirstContact(encounterController);
                EncounterControllers.Add(encounterController);
            }
            else if (fleetConB.FleetData.CivEnum == fleetConA.FleetData.CivEnum)
            {
                // Fleet management will be handeld by FleetManager FleetControllers and not by a new enounter controller at this time.
                FleetManager.Instance.FleetToFleetManagement(fleetConA, fleetConB); 
            }
            else if (fleetConB.FleetData.CivEnum != fleetConA.FleetData.CivEnum)// Not First Contact
            {
                encounterController = FeetsNotSameCivNotFirstEncounter(fleetConA, fleetConB);
                encounterController.ResolveDiplomacy(fleetConA.FleetData.CivController, fleetConB.FleetData.CivController);
                EncounterControllers.Add(encounterController);
            }
        }
    }
    public void RegisterEncounter(FleetController fleetConA, StarSysController sysCon)
    {
        EncounterController encounterController;
        int firstUninhabited = (int)CivEnum.ZZUNINHABITED1; // all lower than this are inhabited (including Borg UniComplex and inhabitable Nebulas)

        if ((int)sysCon.StarSysData.CurrentOwnerCivEnum < firstUninhabited) // it is inhabited
        {

            if (fleetConA != null) // it is a FleetController and not a StarSystem or other with collider                                                                                                                                                    leetController
            {
                //have we met before?
                if (!DiplomacyManager.Instance.FoundADiplomacyController(sysCon.StarSysData.CurrentCivController, fleetConA.FleetData.CivController))
                {
                    // First Contact
                    encounterController = FirstContactFleetOnStarSysInstantiateEncounnterController(fleetConA, sysCon);
                    encounterController.ResolveFirstContact(encounterController); // make a Diplomacy controller
                    EncounterControllers.Add(encounterController);
                }
                else if (sysCon.StarSysData.CurrentCivController.CivData.CivEnum != fleetConA.FleetData.CivEnum)
                { // not first contact
                    encounterController = FeetToSysNotSameCivNotFirstEncounter(fleetConA, sysCon);
                    encounterController.ResolveDiplomacy(fleetConA.FleetData.CivController, sysCon.StarSysData.CurrentCivController);
                    EncounterControllers.Add(encounterController);
                }
            }
            else if ((int)sysCon.StarSysData.CurrentOwnerCivEnum >= firstUninhabited)
            {
                //React to Uninhabited system contact
                encounterController = FeetsUninhabitedSysEncounter(fleetConA, sysCon);
                encounterController.ResolveUninhabitedSystem(fleetConA.FleetData.CivController, sysCon);   
                EncounterControllers.Add(encounterController);

                foreach (ShipController shipController in fleetConA.FleetData.GetShipList())
                {
                    if (shipController.ShipData.ShipType == ShipType.Transport)
                    {
                        // ToDo: Colonies Opption/ UI?
                    }
                }
            }
            sysCon.gameObject.SetActive(true);
        }
    }
    public void RegisterEncounter(FleetController fleetConA, PlayerDefinedTargetController playerTargetCon)
    {
        //  no encoutner for player target just now

    }
    private EncounterController FirstContactFleetOnFleetInstantiateEncounterController(FleetController fleetA, FleetController fleetB)
    {
        // is frist contact ********TODO: if not first contact then use encounters to update diplomacy relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.CivOne = fleetA.FleetData.CivController;
        encounterData.FleetContollerCivTwo = fleetB;
        encounterData.CivTwo = fleetB.FleetData.CivController;
        encounterData.EncounterType = EncounterType.FirstContact;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData = encounterData;
        encounterController.EncounterData.isCompleted = false;

        return encounterController;
    }
    private EncounterController FirstContactFleetOnStarSysInstantiateEncounnterController(FleetController fleetCon, StarSysController starSysCon)
    {
        // instantiation is frist contact
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetCon;
        encounterData.CivOne = fleetCon.FleetData.CivController;
        encounterData.StarSysController = starSysCon;
        encounterData.CivTwo = starSysCon.StarSysData.CurrentCivController;
        encounterData.EncounterType = EncounterType.FirstContact;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData = encounterData;

        return encounterController; 

    }
    public EncounterController FleetManagementNewEncounter(FleetController fleetA, FleetController fleetB)
    {
        // *** not tracking fleet mamagement encounters at this time, call in EncounterManager commented out but call sent to FleetManager.
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.FleetContollerCivTwo = fleetB;
        encounterData.EncounterType = EncounterType.FleetManagement;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData = encounterData;
        encounterController.EncounterData.isCompleted = false;
        return encounterController;
    }
    public EncounterController FeetsNotSameCivNotFirstEncounter(FleetController fleetA, FleetController fleetB)
    {
        // encounters to update diplomacy relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.CivOne = fleetA.FleetData.CivController;
        encounterData.FleetContollerCivTwo = fleetB;
        encounterData.CivTwo = fleetB.FleetData.CivController;
        encounterData.EncounterType = EncounterType.Diplomacy;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData.isCompleted = false;
        encounterController.EncounterData = encounterData;
 
        return encounterController;
    }
    public EncounterController FeetToSysNotSameCivNotFirstEncounter(FleetController fleetA, StarSysController sysCon)
    {
        // new encounters to update diplomacy controller, update relations!
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.CivOne = fleetA.FleetData.CivController;
        encounterData.StarSysController = sysCon;
        encounterData.CivTwo = sysCon.StarSysData.CurrentCivController;
        encounterData.EncounterType = EncounterType.Diplomacy;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData = encounterData;
        encounterController.EncounterData.isCompleted = false;

        return encounterController;
    }
    public EncounterController FeetsUninhabitedSysEncounter(FleetController fleetA, StarSysController uninhabitedSysCon)
    {
        EncounterData encounterData = new EncounterData();
        encounterData.FleetControllerCivOne = fleetA;
        encounterData.CivOne = fleetA.FleetData.CivController;
        encounterData.StarSysController = uninhabitedSysCon;
        encounterData.CivTwo = uninhabitedSysCon.StarSysData.CurrentCivController;
        encounterData.EncounterType = EncounterType.UninhabitedSystem;
        EncounterController encounterController = new EncounterController(encounterData);
        encounterController.EncounterData = encounterData;

        // ToDo work out claming system in HabitableSysUIController!!
        return encounterController;
    }

    public void ResolveEncounterType(EncounterType encounter)
    {
        EncounterType encounterType = EncounterType.Diplomacy;


        switch (encounterType)
        {
            case EncounterType.FirstContact:
                break;
            case EncounterType.Diplomacy: // this encoutner sends to DiplomacyManager to decide on combat or other diplomacy.
                break;
            case EncounterType.Combat: // this encoutner sends to CombatManager to decide on combat or other combat tasks.
                break;
            case EncounterType.FleetManagement: // this encoutner sends to FleetManager to decide on redistribution of ships or other fleet management tasks.
                break;
            case EncounterType.EnterSystem:
                break;
            case EncounterType.UninhabitedSystem:
                break;
            default:
                break;
        }
    }
}
