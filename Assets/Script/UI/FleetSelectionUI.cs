using Assets.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetSelectionUI : MonoBehaviour
{
    /// <summary>
    /// This is inteneded as a UI to manage all your fleets in one location.
    /// ToDo: make it work
    /// </summary>
    public static FleetSelectionUI Instance;
    public FleetController clickedFleetController;
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject FleetsManagerUIToggle;
    public List<ShipController> shipControllerList;

    public GameObject fleetGroupDropdownGO;
    [SerializeField]
    private TMP_Dropdown fleetGroupDropdown;
    [SerializeField]
    private TMP_Text CivName;
    private Camera galaxyEventCamera;

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
        FleetsManagerUIToggle.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }


    public void LoadShipUIManager(FleetController fleetController)
    {
        YourStarSysUIManager.Instance.CloseUnLoadStarSysUI();
        FleetUIManager.Instance.CloseUnLoadFleetUI();
        FleetsManagerUIToggle.SetActive(true);
        clickedFleetController = fleetController;
        CivName.text = clickedFleetController.FleetData.CivLongName;
    }

    public void UnLoadShipManagerUI()
    {
        FleetsManagerUIToggle.SetActive(false);
    }


}
