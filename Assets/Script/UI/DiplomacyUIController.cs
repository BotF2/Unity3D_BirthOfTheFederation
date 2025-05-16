using Assets.Core;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DiplomacyUIController : MonoBehaviour
{
    public static DiplomacyUIController Instance;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas parentCanvas;
    private DiplomacyController controller;
    public GameObject DiplomacyUIToggle; // GameObject controlles this active UI on/off
    [SerializeField]
    private GameObject firstContatct;
    [SerializeField]
    private TMP_Text theirNameTMP;
    [SerializeField]
    private Image theirInsignia;
    [SerializeField]
    private Image theirRaceImage;
    [SerializeField]
    private TMP_Text relationTMP;
    [SerializeField]
    private TMP_Text relationPointsTMP;
    [SerializeField]
    private TMP_Text traitOneTMP;
    [SerializeField]
    private TMP_Text traitTwoTMP;
    [SerializeField]
    private TMP_Text traitThreeTMP;
    [SerializeField]
    private TMP_Text traitFourTMP;
    [SerializeField]
    private TMP_Text ourTraitOneTMP;
    [SerializeField]
    private TMP_Text ourTraitTwoTMP;
    [SerializeField]
    private TMP_Text ourTraitThreeTMP;
    [SerializeField]
    private TMP_Text ourTraitFourTMP;
    [SerializeField]
    private TMP_Text transmissionTMP;
    [SerializeField]
    private TMP_Text descriptionTMP;
    [SerializeField]
    private GameObject descriptionPanel;
    [SerializeField]
    private GameObject[] UI_PanelGOs;
    [SerializeField]
    private Image[] TabButtonMasks;



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

    private void Start()
    {
        DiplomacyUIToggle.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }

    public void LoadDiplomacyUI(DiplomacyController ourDiplomacyController)
    {

        //if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivMajor.CivData.CivEnum))
        //    LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivOther, ourDiplomacyController);
        //else if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivOther.CivData.CivEnum))
        //    LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivMajor, ourDiplomacyController);
        

    }
    //private void LoadCivDataInUI(CivController othersController, DiplomacyController ourDiplomacyController)
    //{
  
    //}
    public void CloseUnLoadDiplomacyUI()
    {
        SwitchToTab(0);
        DiplomacyUIToggle.SetActive(false);
        TimeManager.Instance.ResumeTime();
    }
    public void OpenCloseDescritionPanel()
    {
        if (descriptionPanel.activeSelf)
        {
            descriptionPanel.SetActive(false);
        }
        else
        {
            descriptionPanel.SetActive(true);
        }
    }
    public void CombatScene()
    {
       // DiplomacyManager.Instance.SpaceCombatScene();
    }
    public void SwitchToTab(int TabID)
    {
        UI_PanelGOs[TabID].SetActive(true);

        foreach (Image image in TabButtonMasks)
        {
            image.gameObject.SetActive(true);
        }
        TabButtonMasks[TabID].gameObject.SetActive(false);

    }
}
