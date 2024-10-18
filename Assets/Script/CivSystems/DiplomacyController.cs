using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using UnityEditor;


public class DiplomacyController : MonoBehaviour
{
    private DiplomacyData diplomacyData; // holds civOne and two and diplomacy enum
    public DiplomacyData DiplomacyData { get { return diplomacyData; } set { diplomacyData = value; } }
    private List<string> diplomaticTransmissions;
    private string declareWar = "The A declares war on the B.";
    private string demandCreditsAvoidWar = "The A demand X credits to avoid a state of war with the B.";
    private string offerCreditsImproveRelations = "The A offers the B X credits to improve relations by 200 points.";
    private string demandCredits = "The A demand X credits from the B.";
    private string requestCrditsImproveRelations = "The A request X credits from the B to improve relations by 200 points.";
    private Camera galaxyEventCamera;
    [SerializeField]
    public Canvas DiplomacyUICanvas { get; private set; }


    private void Start()
    {
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        var CanvasGO = GameObject.Find("CanvasDiplomacyUI");
        DiplomacyUICanvas = CanvasGO.GetComponent<Canvas>();
        DiplomacyUICanvas.worldCamera = galaxyEventCamera;
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
            civPartyTwo.ResetSprites(hitGO);
            civPartyTwo.ResetNames(hitGO);
        }
        else if (GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
        {
            civPartyOne.ResetSprites(hitGO);
            civPartyOne.ResetNames(hitGO);
        }
        DiplomacyUIManager.Instance.diplomacyUIRoot.SetActive(true);
    }
    public void NextDiplomaticContact(DiplomacyController controller)
    {
        DiplomacyUIManager.Instance.LoadDiplomacyUI(controller);

    }
    public void CloseUnLoadDipolmacyUI()
    {

        DiplomacyUIManager.Instance.diplomacyUIRoot.SetActive(false);
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
        if (currentStatusPoints > (int)DiplomacyStatusEnum.Hostile && currentStatusPoints < (int)DiplomacyStatusEnum.Friendly)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        }
        else if (currentStatusPoints <= (int)DiplomacyStatusEnum.Hostile && currentStatusPoints > (int)DiplomacyStatusEnum.ColdWar)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Hostile;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.Friendly && currentStatusPoints < (int)DiplomacyStatusEnum.Allied)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Friendly;
        }
        else if (currentStatusPoints <= (int)DiplomacyStatusEnum.ColdWar && currentStatusPoints > (int)DiplomacyStatusEnum.TotalWar)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.ColdWar;
        }
        else if (currentStatusPoints >= (int)DiplomacyStatusEnum.Allied && currentStatusPoints < (int)DiplomacyStatusEnum.Unified)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Allied;
        }
        else if (currentStatusPoints <= (int)DiplomacyStatusEnum.TotalWar)
        {
            this.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.TotalWar;
        }
        else if ((int)this.DiplomacyData.CivOne.CivData.CivInt > 6 || (int)this.DiplomacyData.CivTwo.CivData.CivInt > 6)
        {
            if (currentStatusPoints >= (int)DiplomacyStatusEnum.Unified)
            {
                //DoTo: minor race joins major race, vulcans join the Federation
            }
        }


    }
}
