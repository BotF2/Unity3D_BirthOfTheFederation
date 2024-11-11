using Assets.Core;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.ShaderGraph.Internal;



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
    private TMP_Text relationTMP;
    [SerializeField]
    private TMP_Text relationPointsTMP;
    [SerializeField]
    private TMP_Text transmissionTMP;

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

    public void LoadDiplomacyUI(DiplomacyController ourDiplomacyController)
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
        relationTMP.text = ourDiplomacyController.DiplomacyData.DiplomacyEnumOfCivs.ToString();
        relationPointsTMP.text = ourDiplomacyController.DiplomacyData.DiplomacyPointsOfCivs.ToString();
        transmissionTMP.text = othersController.CivData.Decription;

    }
    public void CloseUnLoadFleetUI()
    {
        diplomacyUIToggle.SetActive(false);
        TimeManager.Instance.ResumeTime();
    }
}
