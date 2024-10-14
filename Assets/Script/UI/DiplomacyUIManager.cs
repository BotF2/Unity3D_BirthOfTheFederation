using Assets.Core;
using TMPro;
//using UnityEditor.Animations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.ShaderGraph.Internal;



public class DiplomacyUIManager: MonoBehaviour
{
    public static DiplomacyUIManager Instance;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas parentCanvas;
    private DiplomacyController controller;
    public GameObject diplomacyUIRoot; // GameObject controlles this active UI on/off
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
        diplomacyUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }

    public void LoadDiplomacyUI(DiplomacyController ourDiplomacyController)
    {
        controller = ourDiplomacyController;
        TimeManager.Instance.PauseTime();
        StarSysUIManager.Instance.CloseUnLoadStarSysUI();
        FleetUIManager.Instance.CloseUnLoadFleetUI();
        FleetSelectionUI.Instance.UnLoadShipManagerUI();
        diplomacyUIRoot.SetActive(true);
        if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivOne.CivData.CivEnum))
            LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivTwo, ourDiplomacyController);
        else if (GameController.Instance.AreWeLocalPlayer(ourDiplomacyController.DiplomacyData.CivTwo.CivData.CivEnum))
            LoadCivDataInUI(ourDiplomacyController.DiplomacyData.CivOne, ourDiplomacyController);
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
       diplomacyUIRoot.SetActive(false);
       TimeManager.Instance.ResumeTime();    
    }
}
