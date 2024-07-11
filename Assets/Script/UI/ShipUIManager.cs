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
    public FleetController clickedController;
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
        //destinationDropdown.value = 0; // In FleetController
    }


    public void LoadShipUIManager(FleetController fleetController) 
    {
        StarSysUIManager.instance.UnLoadStarSysUI();
        FleetUIManager.instance.UnLoadFleetUI();
        ShipManagerUIRoot.SetActive(true);
        /* destination dropdown */
       // destinationDropdown.value = 0;

        clickedController = fleetController;
        CivName.text = clickedController.FleetData.CivLongName;
        //destinationDropdown.value = index;
        //DropdownItemSelected(destinationDropdown);
        //destinationDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destinationDropdown); });
        //ship dropdown
        //clickedController.shipDropdownGO = ShipDropdownGO;
        //clickedController.shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        NamesToShipDropdown(clickedController.FleetData.ShipsList);
    }
    //private void ReorderDropdownOptions(TMP_Dropdown dropdown)
    //{
    //    List<TMP_Dropdown.OptionData> options = dropdown.options;
    //    options.Reverse();
    //    // Update the UI
    //    dropdown.RefreshShownValue();
    //}
    public void UnLoadShipManagerUI()
    {
        ShipManagerUIRoot.SetActive(false);
    }
    private void NamesToShipDropdown(List<ShipController> shipControllers)
    {
        //var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        //shipDropdown.options.Clear();

        //foreach (var shipCon in shipControllers)
        //{
        //    if (shipCon != null)
        //    {
        //        string text = shipCon.ShipData.ShipName;
        //        text.Replace("(CLONE)", string.Empty);
        //        shipDropdown.options.Add(new TMP_Dropdown.OptionData(text));
        //    }
        
        //}
        //DropdownItemSelected(shipDropdown);
        //shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
    }

}
