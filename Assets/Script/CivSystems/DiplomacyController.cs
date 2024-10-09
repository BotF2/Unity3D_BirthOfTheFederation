using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;


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
        this.DiplomacyData.DiplomacyPointsOfCivs = 400;
        if (civPartyOne.CivData.CivEnum == CivManager.Instance.LocalPlayerCivEnum)
        {
            civPartyTwo.ResetSprites(civPartyTwo, hitGO);
            civPartyTwo.ResetNames(civPartyTwo, hitGO);
        }
        else if (civPartyTwo.CivData.CivEnum == CivManager.Instance.LocalPlayerCivEnum)
        {
            civPartyOne.ResetSprites(civPartyOne, hitGO);
            civPartyOne.ResetNames(civPartyOne, hitGO);
        }
        DiplomacyUIManager.Instance.diplomacyUIRoot.SetActive(true);
    }
    public void DiplomaticContact(CivController civOne, CivController civTwo)
    {
            
    }
    public void CloseUnLoadDipolmacyUI()
    {

        DiplomacyUIManager.Instance.diplomacyUIRoot.SetActive(false);
    }
}
