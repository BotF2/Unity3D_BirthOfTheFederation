using Assets.Core;
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
    private TMP_Text transmissionTMP;
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

    public void LoadFirstContactUI(DiplomacyController ourDiplomacyController)
    {
        controller = ourDiplomacyController;
        TimeManager.Instance.PauseTime(); // ToDo: put a pause indicator on screen
        GameObject aNull = new GameObject();
        MenuManager.Instance.OpenMenu(Menu.DiplomacyMenu, aNull);
       // YourStarSysUIManager.Instance.CloseUnLoadStarSysUI();
        //FleetUIController.Instance.CloseUnLoadFleetUI();
        //FleetSelectionUI.Instance.UnLoadShipManagerUI();
        if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivOne.CivData.CivEnum))
            LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivTwo, ourDiplomacyController);
        else if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivTwo.CivData.CivEnum))
            LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivOne, ourDiplomacyController);
        DiplomacyUIToggle.SetActive(true);
        Destroy(aNull);

    }
    private void LoadCivDataInUI(CivController othersController, DiplomacyController ourDiplomacyController)
    {
        theirNameTMP.text = othersController.CivData.CivShortName;
        theirInsignia.sprite = othersController.CivData.InsigniaSprite;
        theirRaceImage.sprite = othersController.CivData.CivRaceSprite;
        relationTMP.text = ourDiplomacyController.DiplomacyData.DiplomacyEnumOfCivs.ToString();
        relationPointsTMP.text = ourDiplomacyController.DiplomacyData.DiplomacyPointsOfCivs.ToString();
        transmissionTMP.text = othersController.CivData.Decription;
    }
    public void CloseUnLoadFirstContactUI()
    {
        SwitchToTab(0);
        DiplomacyUIToggle.SetActive(false);
        TimeManager.Instance.ResumeTime();
    }
    public void SwitchToTab(int TabID)
    {
        foreach (GameObject tabGO in UI_PanelGOs)
        {
            tabGO.SetActive(false);
        }
        UI_PanelGOs[TabID].SetActive(true);

        foreach (Image image in TabButtonMasks)
        {
            image.gameObject.SetActive(true);
        }
        TabButtonMasks[TabID].gameObject.SetActive(false);

    }
}
