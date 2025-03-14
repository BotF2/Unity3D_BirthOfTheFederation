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
    public List<DiplomacyController> ManagersDiplomacyControllerList;
    public GameObject diplomacyPrefab;
    public GameObject diplomacyUIGO;
    //[SerializeField]
    //private Canvas canvasTherStarSysUI;


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
    public void InstantiateDipolmacyController(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        // instantiation is frist contact
        GameObject DiplomacyNewGameOb = (GameObject)Instantiate(diplomacyPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
        var diplomacyController = DiplomacyNewGameOb.GetComponent<DiplomacyController>();
        diplomacyController.areWePlaceholder = false;
        DiplomacyData ourDiplomacyData = new DiplomacyData();
        diplomacyController.DiplomacyData = ourDiplomacyData;
        diplomacyController.DiplomacyData.CivOne = civPartyOne;
        diplomacyController.DiplomacyData.CivTwo = civPartyTwo;
        // ToDo: use traits to set diplomacy relation out of the gate and for AI actions
        // For Testing..... 
        if ((diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.FED
            && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.VULCANS)
            || (diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.VULCANS
            && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.FED))
        {
            diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.War;
            SceneController.Instance.LoadCombatScene();
            diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 0;
        }
        else
        {

            diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
            diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 60; //60 = netural on a -20 to 120 scale
        }
        ManagersDiplomacyControllerList.Add(diplomacyController);
        // ........Turned off for testing combat transition
        // diplomacyController.FirstContact(civPartyOne, civPartyTwo, hitGO);
        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum) ||
            GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
            DiplomacyUIController.Instance.LoadFirstContactUI(diplomacyController);
        //else if //*********check for human non-local palyers needing to do diplomacy in their UI ()
        //{
        //    //do Remote human player diplomacy
        //}
        else DoDiplomacyForAI(civPartyOne, civPartyTwo, hitGO);

    }
    private DiplomacyController InstantiatePlaceHolder(CivController civPartyOne, CivController civPartyTwo)
    {
        GameObject DiplomacyNewGameOb = (GameObject)Instantiate(diplomacyPrefab, new Vector3(0, 0, 0),
        Quaternion.identity);
        var diplomacyController = DiplomacyNewGameOb.GetComponent<DiplomacyController>();
        diplomacyController.areWePlaceholder = true;
        return diplomacyController;
    }
    private void DoDiplomacyForAI(CivController civOne, CivController civTwo, GameObject weHitGO)
    {
        //Do some diplomacy without a UI by/for either civ
    }
    public void FistContactDiplomacy(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        if (!FoundADiplomacyController(civPartyOne, civPartyTwo, hitGO))
        {
            //This instantiation is first contact of fleet or system civ 1 and fleet or system civ 2 in game
            InstantiateDipolmacyController(civPartyOne, civPartyTwo, hitGO);
        }
    }
    private bool FoundADiplomacyController(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        bool found = false;
        foreach (var diplomacyController in ManagersDiplomacyControllerList)
        {
            if (diplomacyController != null)
            {
                if (diplomacyController.DiplomacyData.CivOne == civPartyOne && diplomacyController.DiplomacyData.CivTwo == civPartyTwo || diplomacyController.DiplomacyData.CivTwo == civPartyOne && diplomacyController.DiplomacyData.CivOne == civPartyTwo)
                {
                    found = true;
                    break;
                }
            }
        }
        return found;
    }
    public DiplomacyController GetTheDiplomacyController(CivController civOne, CivController civTwo)
    {
        DiplomacyController diplomacyController = InstantiatePlaceHolder(civOne, civTwo);
        diplomacyController.areWePlaceholder = true;
        if (ManagersDiplomacyControllerList.Count == 0)
        {
            return diplomacyController;
        }
        else
        {
            foreach (var aDiplomacyController in ManagersDiplomacyControllerList)
            {
                if ((aDiplomacyController.DiplomacyData.CivOne == civOne && aDiplomacyController.DiplomacyData.CivTwo == civTwo) || (aDiplomacyController.DiplomacyData.CivOne == civTwo && aDiplomacyController.DiplomacyData.CivTwo == civOne))
                {
                    aDiplomacyController.areWePlaceholder = false;
                    diplomacyController = aDiplomacyController;
                }
            }
        }
        return diplomacyController;
    }
    public DiplomacyStatusEnum CalculateDiplomaticStatusOnFirstContact(CivController civOne, CivController civTwo)
    {
        DiplomacyStatusEnum diplomacyStatus = DiplomacyStatusEnum.Neutral;
        int warLike = Math.Abs((int)civOne.CivData.Warlike - (int)civTwo.CivData.Warlike);
        int xenophobia = Math.Abs((int)civOne.CivData.Xenophbia - (int)civTwo.CivData.Xenophbia);
        int ruthless = Math.Abs((int)civOne.CivData.Ruthelss - (int)civTwo.CivData.Ruthelss);
        int greedy = Math.Abs((int)civOne.CivData.Greedy - (int)civTwo.CivData.Greedy);
        int separation = warLike + xenophobia + ruthless + greedy;
        switch (separation)
        {
            case 0:
                diplomacyStatus = DiplomacyStatusEnum.Neutral;
                break;
            case 1:
                diplomacyStatus = DiplomacyStatusEnum.UnFriendly;
                break;
            case 2:
                diplomacyStatus = DiplomacyStatusEnum.Hostile;
                break;
            case 3:
                diplomacyStatus = DiplomacyStatusEnum.ColdWar;
                break;
            default:
                diplomacyStatus = DiplomacyStatusEnum.Neutral;
                break;
        }
        return diplomacyStatus;

    }
}


