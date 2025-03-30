using Assets.Core;
using System.Collections.Generic;
using UnityEngine;


public class DiplomacyController
{
    private DiplomacyData diplomacyData; // holds civOne and two and diplomacy enum
    public DiplomacyData DiplomacyData { get { return diplomacyData; } set { diplomacyData = value; } }
 
    private static string declareWar = "The A civ declares war on the B.";
    private static string requestSomething = "The A request X something from the B to improve relations by x# points.";
    private static string demandSomething = "The A civ demand X to avoid a degraded state of relations with the B Civ.";
    private static string offerSomething = "The A civ offers the B X to improve relations.";
    private static string demandStopInterferance = "The A civ demand X that B civ stop doing interferance.";

    private List<string> diplomaticTransmissions = new List<string> { declareWar, requestSomething, demandSomething, offerSomething, demandStopInterferance};
    public List<string> DiplomaticTransmissions { get { return diplomaticTransmissions; } set { diplomaticTransmissions = value; } }
    public List<DiplomaticEventEnum> DiplomaticEvents = new List<DiplomaticEventEnum>
    { DiplomaticEventEnum.DeclareWar, DiplomaticEventEnum.Sabatoge, DiplomaticEventEnum.Disinformation, DiplomaticEventEnum.GatherIntel,
        DiplomaticEventEnum.OfferTrade, DiplomaticEventEnum.ShareTech, DiplomaticEventEnum.GiveAid};
        
public DiplomacyController(DiplomacyData diplomacyData)
    {
        DiplomacyData = diplomacyData;
    }

    public void FirstContact(CivController civPartyOne, CivController civPartyTwo)
    {
        TimeManager.Instance.PauseTime();
        this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        this.DiplomacyData.DiplomacyPointsOfCivs = (int)DiplomacyStatusEnum.Neutral;

    }
    public void NextDiplomaticContact(DiplomacyController controller)
    {
       // ToDo: New UI for ongoing diplomacy

    }
    public void CloseUnLoadDipolmacyUI()
    {

        DiplomacyUIController.Instance.DiplomacyUIToggle.SetActive(false);
    }
    public void AddDiplomaticPoints(int points)
    {
        this.DiplomacyData.DiplomacyPointsOfCivs += points;
        ChangedDiplomacyStatus(this.DiplomacyData.DiplomacyPointsOfCivs);
    }
    public void SubtractDiplomaticPoints(int points)
    {
        this.DiplomacyData.DiplomacyPointsOfCivs -= points;
        ChangedDiplomacyStatus(this.DiplomacyData.DiplomacyPointsOfCivs);
    }
    private void ChangedDiplomacyStatus(int currentStatusPoints)
    {
        if (currentStatusPoints < -20)
        {
            currentStatusPoints = -20;
        }
        
        if (currentStatusPoints >= (int)DiplomacyStatusEnum.Neutral && currentStatusPoints < (int)DiplomacyStatusEnum.Friendly)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.Friendly && currentStatusPoints < (int)DiplomacyStatusEnum.Allied)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Friendly;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.Allied && currentStatusPoints < (int)DiplomacyStatusEnum.Membership)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Allied;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.Membership && ((int)this.DiplomacyData.CivOne.CivData.CivInt > 6 || (int)this.DiplomacyData.CivTwo.CivData.CivInt > 6))
        {
            // only minors AI civ can become member of a playable major race
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Membership;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.UnFriendly && currentStatusPoints < (int)DiplomacyStatusEnum.Neutral)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.UnFriendly;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.Hostile && currentStatusPoints < (int)DiplomacyStatusEnum.UnFriendly)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Hostile;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.ColdWar && currentStatusPoints < (int)DiplomacyStatusEnum.Hostile)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.ColdWar;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.War)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.War;
        }
    }
}
