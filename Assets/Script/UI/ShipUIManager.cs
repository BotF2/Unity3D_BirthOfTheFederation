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
    public FleetController controller;
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject ShipManagerUIRoot;
    //[SerializeField]
    //private Slider warpSlider;
    //[SerializeField]
    //private TextMeshProUGUI warpSliderText;
    //[SerializeField]
    //private float maxSliderValue = 9.8f;
    public List<ShipController> shipControllerList;

    //[SerializeField]
    //private List<ShipData> shipList;
    private bool deltaShipList = false;

    public GameObject ShipDropdownGO;
    [SerializeField]
    //private TMP_Dropdown destinationDropdown;
    private TMP_Dropdown shipDropdown;
    //public GameObject Destination;
    [SerializeField]
    private TMP_Text FleetName;
    //[SerializeField]
    //private TMP_Text dropdownDestinationText;
    [SerializeField]
    private TMP_Text dropdownShipText;
    [SerializeField]
    private TMP_Text sysDestination;
    private Camera galaxyEventCamera;
    //[SerializeField]

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


    public void LoadShipUIManager(GameObject go) 
    {
        StarSysUIManager.instance.UnLoadStarSysUI();
        FleetUIManager.instance.UnLoadFleetUI();
        ShipManagerUIRoot.SetActive(true);
        /* destination dropdown */
       // destinationDropdown.value = 0;

        controller = go.GetComponent<FleetController>();
        FleetName.text = controller.FleetData.Name;
        //ResetWarpSlider(controller.FleetData.CurrentWarpFactor);
        //destinationDropdown = GameManager.Instance.DestinationDropdown;
        //var listing = destinationDropdown.options;
        //for (int i = 0; i < destinationDropdown.options.Count; i++)
        //{
        //    if (controller.FleetData.Name.Contains(destinationDropdown.options[i].text))
        //    {
        //        destinationDropdown.options.Remove(destinationDropdown.options[i]);
        //    }
        //}
        //ReorderDropdownOptions(destinationDropdown);
        ////listing = destinationDropdown.options;
        //if (destinationDropdown.options[0].text != "Select Destination")
        //{

        //    destinationDropdown.options.Insert(0, new TMP_Dropdown.OptionData("Select Destination"));
        //    destinationDropdown.value = 0;
        //    destinationDropdown.RefreshShownValue();
        //}
        ////var listing = destinationDropdown.options;
        //int index =0;
        //if (controller.SelectedDestination != "")
        //{
        //    foreach (var option in listing)
        //    {
        //        if (option.text == controller.SelectedDestination)
        //        {
        //            index = destinationDropdown.options.IndexOf(option);
        //        }
        //    }
        //}
        //destinationDropdown.value = index;

        //DropdownItemSelected(destinationDropdown);
        //destinationDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destinationDropdown); });
        //ship dropdown
        controller.shipDropdownGO = ShipDropdownGO;
        controller.shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        NamesToShipDropdown(controller.FleetData.ShipsList);
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
    //void DropdownItemSelected(TMP_Dropdown dropdown)
    //{
    //    int index = dropdown.value;
    //    if (dropdown.name == "Dropdown Destination")
    //    {
    //        if (dropdown.options[index].text != "Select Destination" && GameManager.Instance.DestinationDictionary[dropdown.options[index].text] != null)
    //        {
    //            controller.FleetData.Destination = GameManager.Instance.DestinationDictionary[dropdown.options[index].text];
    //            controller.SelectedDestination = dropdown.options[index].text;
    //            dropdownDestinationText.text = dropdown.options[index].text;
    //        }
    //    }
    //}
    private void NamesToShipDropdown(List<ShipController> shipControllers)
    {
        var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        shipDropdown.options.Clear();

        foreach (var shipCon in shipControllers)
        {
            if (shipCon != null)
            {
                string text = shipCon.ShipData.ShipName;
                text.Replace("(CLONE)", string.Empty);
                shipDropdown.options.Add(new TMP_Dropdown.OptionData(text));
            }
        
        }
        //DropdownItemSelected(shipDropdown);
        //shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
    }

}
