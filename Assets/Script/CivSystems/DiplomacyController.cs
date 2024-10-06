using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;


public class DiplomacyController : MonoBehaviour
{
    private DiplomacyData diplomacyData; // holds civOne and two and diplomacy enum
    public DiplomacyData DiplomacyData { get { return diplomacyData; } set { diplomacyData = value; } }

    private Camera galaxyEventCamera;
    [SerializeField]
    public Canvas DiplomacyUICanvas { get; private set; }


    private void Start()
    {
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        var CanvasGO = GameObject.Find("CanvasDiplomacyUI");
        DiplomacyUICanvas = CanvasGO.GetComponent<Canvas>();
        DiplomacyUICanvas.worldCamera = galaxyEventCamera;        
    }

    public void FirstContact(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        TimeManager.Instance.PauseTime();
        civPartyOne.CivData.AddToCivControllersWeKnow(civPartyTwo);
        civPartyTwo.CivData.AddToCivControllersWeKnow(civPartyOne);
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
