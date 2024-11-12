using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TheirSysDiplomacyUIManager : MonoBehaviour
{
    public static TheirSysDiplomacyUIManager Instance;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas parentCanvas;
    private DiplomacyController controller;
    public GameObject diplomacyUIToggle; // GameObject controlles this active UI on/off
    [SerializeField]
    private TMP_Text theirNameTMP;
    [SerializeField]
    private GameObject theirInsigniaGO;
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
    private GameObject[] TabUIs;
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

    private Camera GetGalaxyEventCamera()
    {
        return galaxyEventCamera;
    }

    private void Start()
    {
        diplomacyUIToggle.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }

    public void LoadTheirSysDiplomacyUI(DiplomacyController ourDiplomacyController)
    {
        controller = ourDiplomacyController;
        controller.GalaxyEventCamera = galaxyEventCamera.GetComponent<Camera>();
        TimeManager.Instance.PauseTime(); // ToDo: put a pause indicator on screen
        YourStarSysUIManager.Instance.CloseUnLoadStarSysUI();
        FleetUIManager.Instance.CloseUnLoadFleetUI();
        FleetSelectionUI.Instance.UnLoadShipManagerUI();
        if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivOne.CivData.CivEnum))
            LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivTwo, ourDiplomacyController);
        else if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivTwo.CivData.CivEnum))
            LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivOne, ourDiplomacyController);
        diplomacyUIToggle.SetActive(true);

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
    public void CloseUnLoadFleetUI()
    {
        SwitchToTab(0);
        diplomacyUIToggle.SetActive(false);
        TimeManager.Instance.ResumeTime();
    }
    public void SwitchToTab(int TabID)
    {
        foreach (GameObject tabGO in TabUIs)
        {
            tabGO.SetActive(false);
        }
        TabUIs[TabID].SetActive(true);

        foreach (Image image in TabButtonMasks)
        {
            image.gameObject.SetActive(true);
        }
        TabButtonMasks[TabID].gameObject.SetActive(false);

    }
}
