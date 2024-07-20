using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Core;
using Unity.VisualScripting;
using System.Diagnostics;
using UnityEngine.Rendering;

public class ShipUIManager : MonoBehaviour
{
    public static ShipUIManager instance;
    public FleetController clickedFleetController;
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject ShipManagerUIRoot;
    public List<ShipController> shipControllerList;

    private bool deltaShipList = false;

    public GameObject ShipDropdownGO;
    [SerializeField]
    private TMP_Dropdown shipDropdown;
    [SerializeField]
    private TMP_Text CivName;
    private Camera galaxyEventCamera;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        ShipManagerUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }


    public void LoadShipUIManager(FleetController fleetController) 
    {
        StarSysUIManager.instance.UnLoadStarSysUI();
        FleetUIManager.instance.UnLoadFleetUI();
        ShipManagerUIRoot.SetActive(true);
        clickedFleetController = fleetController;
        CivName.text = clickedFleetController.FleetData.CivLongName;
    }

    public void UnLoadShipManagerUI()
    {
        ShipManagerUIRoot.SetActive(false);
    }


}
