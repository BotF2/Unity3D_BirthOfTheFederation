using Assets.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum DiplomacyStatusEnum // between two civs held in the DiplomacyData
{
    War = -20,
    ColdWar = 0,
    Hostile = 20,
    UnFriendly = 40,
    Neutral = 60,
    Friendly = 80,
    Allied = 100,
    Membership = 120
}
public enum NagotiationPoints
{
    DeclarWar,
    Sanctions,
    ThreatenAction,
    OfferTraid,
    OfferCulturalExchange,
    OfferTech,
    OfferAid,
    OfferAlliance
}
public enum SecretService
{
    Sabatoge,
    Disinformation,
    IntellectualTheft,
    GatherIntelligence,
    Combat
}
public enum DiplomaticEventEnum // Diplomacy AI uses to move relations                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
{
    War,
    DiscoveredSabatoge,
    DiscoveredDisinformation,
    DiscoveredIntellectualTheft,
    CulturalExchange,
    Trade,
    ShareTech,
    GiveAid,
    Alliance
}
public enum WarLikeEnum // held by the civ
{
    Warlike =-2,
    Aggressive = -1,
    Neutral = 0,
    Peaceful =1,
    Pacifist = 2
}
public enum XenophobiaEnum // held by the civ
{
    Xenophobia = -2,
    Intolerant = -1,
    Indifferent = 0,
    Sympathetic = 1,
    Compassion = 2
}
public enum RuthlessEnum
{
    Ruthless =-2,
    Callous =-1,
    Regulated =0,
    Ethical = 1,
    Honorable = 2
}
public enum GreedyEnum
{
    Greedy =-2,
    Materialistic =-1,
    Transactional =0,
    Egaliterian =1,
    Idealistic =2
}


public class DiplomacyManager : MonoBehaviour
{
    public static DiplomacyManager Instance;
    public List<DiplomacyController> DiplomacyControllerList = new List<DiplomacyController>();
    public GameObject diplomacyUIGO;


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
    public void SpaceCombatScene(FleetController fleetConA, FleetController fleetConB, StarSysController aNull)
    {
        SceneController.Instance.LoadCombatScene();
        GalaxyMenuUIController.Instance.CloseMenu(Menu.DiplomacyMenu);
        //SubMenuManager.Instance.CloseMenu(Menu.DiplomacyMenu);
        ShipManager.Instance.ShipsFromFleetsForCombat(); //shipType, fleetGOinSys, this);
        //CombatManager.Instance.InstatniateCombat(controller.DiplomacyData.CivOne.CivData.FleetControllers, controller.DiplomacyData.CivTwo.CivData.FleetControllers);
    }
    public void SpaceCombatScene(FleetController fleetConA, FleetController fleetConB)
    {
        SceneController.Instance.LoadCombatScene();
        GalaxyMenuUIController.Instance.CloseMenu(Menu.DiplomacyMenu);
        //SubMenuManager.Instance.CloseMenu(Menu.DiplomacyMenu);
        ShipManager.Instance.ShipsFromFleetsForCombat(); //shipType, fleetGOinSys, this);
        //CombatManager.Instance.InstatniateCombat(controller.DiplomacyData.CivOne.CivData.FleetControllers, controller.DiplomacyData.CivTwo.CivData.FleetControllers);
    }
    public void FirstContactGetNewDiplomacyContoller(EncounterController encounterController)
    {
        // is frist contact diplomacy
        var civPartyOne = encounterController.EncounterData.CivOne;
        var civPartyTwo = encounterController.EncounterData.CivTwo;
        DiplomacyData ourDiplomacyData = new DiplomacyData();
        ourDiplomacyData.CivOne = civPartyOne;
        ourDiplomacyData.CivTwo = civPartyTwo;
        ourDiplomacyData.IsFirstContact = true;
        DiplomacyController diplomacyController = new DiplomacyController(ourDiplomacyData);
        #region testing auto combat
        // For Testing..... 
        //if ((diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.FED
        //    && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.VULCANS)
        //    || (diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.VULCANS
        //    && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.FED))
        //{
        //    diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.War;
        //    diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 0;
        //}
        //else
        //{

        //    diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        //    diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 60; //60 = netural on a -20 to 120 scale
        //}
        #endregion
        DiplomacyControllerList.Add(diplomacyController);
        diplomacyController.FirstContact(civPartyOne, civPartyTwo);
        diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = CalculateDiplomaticStatusOnFirstContact(diplomacyController);
        diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = (int)diplomacyController.DiplomacyData.DiplomacyEnumOfCivs;
        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum) ||
            GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
        {
            //SubMenuManager.Instance.OpenMenu(Menu.DiplomacyMenu, diplomacyUIGO);
            DiplomacyUIController.Instance.LoadDiplomacyUI(diplomacyController);
        }
        //else if (GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
        //{
            
        //}
        else DoDiplomacyForAI(encounterController);
  
        // ToDo: do some AI for the non-player civs regarding the encounter with a played playable civ
        
       
    }
    private void DoDiplomacyForAI(EncounterController encounterController) //, GameObject weHitGO)
    {
        //Do SpaceCombatScene or so some other diplomacy without a UI by/for either civ
    }
    public bool FoundADiplomacyController(CivController civPartyOne, CivController civPartyTwo) //, GameObject hitGO)
    {
        bool found = false;
        //List<DiplomacyController> placeholderControllers = new List<DiplomacyController>();
        for (int i = 0; i < DiplomacyControllerList.Count; i++)
        {
            if (DiplomacyControllerList[i] != null) 
            {
                if (DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyOne && DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyTwo
                    || DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyOne && DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyTwo)
                {
                    found = true;
                    break;
                }
            }
        }

        return found;
    }
    public DiplomacyController GetDiplomacyController(CivController civPartyOne, CivController civPartyTwo)
    {
        DiplomacyController diplomacyController = null;
        for (int i = 0; i < DiplomacyControllerList.Count; i++)
        {
            if (DiplomacyControllerList[i] != null && ((DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyOne && DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyTwo)
                || (DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyTwo && DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyOne)))
            {
                diplomacyController = DiplomacyControllerList[i];
                break;
            }
        }
        return diplomacyController;
    }
    public DiplomacyStatusEnum CalculateDiplomaticStatusOnFirstContact(DiplomacyController ourDiploCon)
    {
        CivController civOne = ourDiploCon.DiplomacyData.CivOne;
        CivController civTwo = ourDiploCon.DiplomacyData.CivTwo;
        DiplomacyStatusEnum diplomacyStatus = DiplomacyStatusEnum.Neutral;
        int warLike = Math.Abs((int)civOne.CivData.Warlike - (int)civTwo.CivData.Warlike);
        int xenophobia = Math.Abs((int)civOne.CivData.Xenophbia - (int)civTwo.CivData.Xenophbia);
        int ruthless = Math.Abs((int)civOne.CivData.Ruthelss - (int)civTwo.CivData.Ruthelss);
        int greedy = Math.Abs((int)civOne.CivData.Greedy - (int)civTwo.CivData.Greedy);
        int degreesOfSparation = warLike + xenophobia + ruthless + greedy;
        switch (degreesOfSparation)
        {
            case 0:
                diplomacyStatus = DiplomacyStatusEnum.Friendly;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                diplomacyStatus = DiplomacyStatusEnum.Neutral;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                diplomacyStatus = DiplomacyStatusEnum.UnFriendly;
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                diplomacyStatus = DiplomacyStatusEnum.Hostile;
                break;
            case 13:
            case 14:
            case 15:
            case 16:
                diplomacyStatus = DiplomacyStatusEnum.ColdWar;
                break;
            default:
                diplomacyStatus = DiplomacyStatusEnum.Neutral;
                break;
        }
        return diplomacyStatus;
    }
}


