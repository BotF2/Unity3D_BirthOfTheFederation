using Assets.Core;
using System.Collections.Generic;
using UnityEngine;


public class DiplomacyController : MonoBehaviour
{
    private DiplomacyData diplomacyData; // holds civOne and two and diplomacy enum
    public DiplomacyData DiplomacyData { get { return diplomacyData; } set { diplomacyData = value; } }
    public bool areWePlaceholder = false;
    private List<string> diplomaticTransmissions;
    private string declareWar = "The A civ declares war on the B.";
    private string demandCreditsAvoidWar = "The A civ demand X something to avoid a state of war with the B.";
    private string offerCreditsImproveRelations = "The A civ offers the B X something to improve relations by x# points.";
    private string demandCredits = "The A demand X something from the B.";
    private string requestCrditsImproveRelations = "The A request X something from the B to improve relations by x# points.";


    private void Start()
    {
        //GalaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        //DiplomacyUICanvas.worldCamera = GalaxyEventCamera;
        diplomaticTransmissions = new List<string>() {
            declareWar,demandCreditsAvoidWar,offerCreditsImproveRelations,demandCredits, requestCrditsImproveRelations};
    }

    public void FirstContact(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        TimeManager.Instance.PauseTime();
        civPartyOne.CivData.AddToCivControllersWeKnow(civPartyTwo);
        civPartyTwo.CivData.AddToCivControllersWeKnow(civPartyOne);
        this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        this.DiplomacyData.DiplomacyPointsOfCivs = (int)DiplomacyStatusEnum.Neutral;
        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum)) // temp to fed
        {
            if (hitGO.GetComponent<FleetController>() != null)
                civPartyTwo.ResetSprites(hitGO);
            if (hitGO.GetComponent<StarSysController>() != null)
                civPartyTwo.ResetNames(hitGO);
        }
        else if (GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
        {
            if (hitGO.GetComponent<FleetController>() != null)
                civPartyOne.ResetSprites(hitGO);
            if (hitGO.GetComponent<StarSysController>() != null)
                civPartyOne.ResetNames(hitGO);
        }
        //FirstContactUIController.current.FirstContactUIToggle.SetActive(true);
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
