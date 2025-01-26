using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;
using UnityEngine.UI;

public class HabitableSysUIController: MonoBehaviour
{
    public static HabitableSysUIController Instance;
    private Camera galaxyEventCamera;
    private StarSysController starSysController;
    [SerializeField]
    private Canvas parentCanvas;
    public GameObject HabitableSysUIToggle;
    [SerializeField]
    private TMP_Text sysCurrentOwnerNameTMP;
    [SerializeField]
    private CivEnum visitingFleetCivEnum;
    [SerializeField]
    private CivData visitingCivData;

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
        HabitableSysUIToggle.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }
    public void LoadHabitableSysUI(StarSysController starSysController, FleetController discoveringFleetController)
    {
        int firstUninhabited = (int)CivEnum.ZZUNINHABITED1;
        this.starSysController = starSysController;
        if ((int)this.starSysController.StarSysData.CurrentOwner >= firstUninhabited) // not already been clamed
        {
            TimeManager.Instance.PauseTime(); // ToDo: put a pause indicator on screen
                                              //ToDo: manage open UIs so we keep a UI with interaction pending when a fleet reaches a new target and player need more than one UI still open
            GameObject aNull = new GameObject();
            MenuManager.Instance.OpenMenu(Menu.HabitableSysMenu, aNull);
            //YourStarSysUIManager.Instance.CloseUnLoadStarSysUI();
            //FleetUIController.Instance.CloseUnLoadFleetUI();
            ////FleetSelectionUI.current.UnLoadShipManagerUI();
            //FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            //HabitableSysUIToggle.SetActive(true);
            visitingFleetCivEnum = discoveringFleetController.FleetData.CivEnum;
        }
    }
    public void CloseUnLoadHabitableSysUI()
    {
        //SwitchToTab(0);
        HabitableSysUIToggle.SetActive(false);
        TimeManager.Instance.ResumeTime();
    }
    private void ClamSystem()
    {
        starSysController.StarSysData.CurrentOwner = visitingFleetCivEnum;
        visitingCivData = CivManager.Instance.GetCivDataByCivEnum(visitingFleetCivEnum);
        visitingCivData.StarSysOwned.Add(starSysController);
        sysCurrentOwnerNameTMP.text = visitingCivData.CivShortName;
        starSysController.StarSysData.CurrentCivController = visitingCivData.CivControllersWeKnow[0];       
    }
}
