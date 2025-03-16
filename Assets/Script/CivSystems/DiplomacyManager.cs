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
    public DiplomacyController InstantiateDiplomacyController(CivController civPartyOne, CivController civPartyTwo) //, GameObject hitGO)
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
        diplomacyController.DiplomacyData.IsFirstContact = true;
        // ToDo: use traits to set diplomacy relation out of the gate and for AI actions
        // For Testing..... 
        if ((diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.FED
            && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.VULCANS)
            || (diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.VULCANS
            && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.FED))
        {
            diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.War;
            diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 0;
        }
        else
        {

            diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
            diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 60; //60 = netural on a -20 to 120 scale
        }
        ManagersDiplomacyControllerList.Add(diplomacyController);
        diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = CalculateDiplomaticStatusOnFirstContact(diplomacyController);
        diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = (int)diplomacyController.DiplomacyData.DiplomacyEnumOfCivs;
        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum) ||
            GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
            DiplomacyUIController.Instance.LoadDiplomacyUI(diplomacyController);
        else DoDiplomacyForAI(civPartyOne, civPartyTwo); //, hitGO);
        return diplomacyController;

    }
    private void DoDiplomacyForAI(CivController civOne, CivController civTwo) //, GameObject weHitGO)
    {
        //Do some diplomacy without a UI by/for either civ
    }
    public bool FoundADiplomacyController(CivController civPartyOne, CivController civPartyTwo) //, GameObject hitGO)
    {
        bool found = false;
        //List<DiplomacyController> placeholderControllers = new List<DiplomacyController>();
        for (int i = 0; i < ManagersDiplomacyControllerList.Count; i++)
        {
            if (ManagersDiplomacyControllerList[i] != null && !ManagersDiplomacyControllerList[i].areWePlaceholder)
            {
                if (ManagersDiplomacyControllerList[i].DiplomacyData.CivOne == civPartyOne && ManagersDiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyTwo
                    || ManagersDiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyOne && ManagersDiplomacyControllerList[i].DiplomacyData.CivOne == civPartyTwo)
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
        for (int i = 0; i < ManagersDiplomacyControllerList.Count; i++)
        {
            if (ManagersDiplomacyControllerList[i] != null && ((ManagersDiplomacyControllerList[i].DiplomacyData.CivOne == civPartyOne && ManagersDiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyTwo)
                || (ManagersDiplomacyControllerList[i].DiplomacyData.CivOne == civPartyTwo && ManagersDiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyOne)))
            {
                diplomacyController = ManagersDiplomacyControllerList[i];
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


