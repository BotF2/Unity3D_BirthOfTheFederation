using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Core;
using Unity.VisualScripting;
using System.Diagnostics;
using UnityEngine.Rendering;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class FleetUIManager : MonoBehaviour
{
    public static FleetUIManager instance;
    public FleetController controller;
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject fleetUIRoot;
    [SerializeField]
    private Slider warpSlider;
    [SerializeField]
    private TextMeshProUGUI warpSliderText;
    [SerializeField]
    private float maxSliderValue = 9.8f;
    //public List<StarSysData> systemsList;

    [SerializeField]
    private List<ShipData> shipList;
    private bool deltaShipList = false;

    public GameObject ShipDropdownGO;
    [SerializeField]
    private TMP_Dropdown destinationDropdown;
    private TMP_Dropdown shipDropdown;
   // public GameObject Destination;
    [SerializeField]
    private TMP_Text FleetName;
    [SerializeField]
    private TMP_Text dropdownDestinationText;
    [SerializeField]
    private TMP_Text dropdownShipText;
    [SerializeField]
    private TMP_Text sysDestination;
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
        fleetUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
        destinationDropdown.value = 0;
    }

    public void WarpSliderChange(float value)
     {
        float localValue = value * maxSliderValue;
        warpSliderText.text = localValue.ToString("0.0");
        controller.FleetData.CurrentWarpFactor = localValue;
    }
    public void ResetWarpSlider(float value)
    {
        warpSlider.value = value/maxSliderValue;
        warpSliderText.text = value.ToString("0.0");
    }
    public void LoadFleetUI(GameObject go) 
    {
        StarSysUIManager.instance.UnLoadStarSysUI();
        ShipUIManager.instance.UnLoadShipManagerUI();
        fleetUIRoot.SetActive(true);
        destinationDropdown.options.Clear();
        List<string> listings = new List<string>();
        foreach (string location in GameManager.Instance.DestinationNames)
        {
            listings.Add(location);
        }
        controller = go.GetComponent<FleetController>();
        FleetName.text = controller.FleetData.Name;
        ResetWarpSlider(controller.FleetData.CurrentWarpFactor);
        int ourFleet = -1;
        // customize the list of destinations
        // Remove the current fleet name 
        
        if (listings.Contains(controller.FleetData.Name));
        {
            for (int i = 0; i < listings.Count; i++)
            {
                if (listings[i] == controller.FleetData.Name)
                {
                    ourFleet = i;
                    break;
                }
            }
            if (ourFleet > -1)
            listings.Remove(listings[ourFleet]);           
        }
        listings.Reverse();
        int indexOfSelected = 0; 
        int indexForRemove = -1;
        if (controller.SelectedDestination != "" && listings.Contains("Select Destination"))
        {
            for (int i = 0; i < listings.Count; i++)
            {
                if (listings[i] == "Select Destination")
                {
                    indexForRemove = i;
                    break;
                }
                //if (listings[i] == controller.SelectedDestination)
                //{
                //    indexOfSelected = i; // will set dropdown to show this selected
                //}
            }
            if (indexForRemove > -1) 
                listings.RemoveAt(indexForRemove);
        }
        else // if (listings[0] != "Select Destination")
        {
            for (int i = 0; i < listings.Count; i++)
            {
                if (listings[i] == "Select Destination")
                {
                    indexOfSelected = i;
                    break;
                }

            }
        }
        //if (position != 0)
        //{
        //    string item = listings[position];
        //    listings.RemoveAt(position);
        //    listings.Insert(0, item);
        //}

        List<TMP_Dropdown.OptionData> dataItems = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < listings.Count; i++)
        {
            TMP_Dropdown.OptionData newDataItem = new TMP_Dropdown.OptionData();
            newDataItem.text = listings[i];
            dataItems.Add(newDataItem);    
        }
        destinationDropdown.AddOptions(dataItems);
        destinationDropdown.value = indexOfSelected;
        DropdownItemSelected(destinationDropdown); // provide the destination as GameObject
        destinationDropdown.RefreshShownValue();
        destinationDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destinationDropdown); });
        //ship dropdown
        controller.shipDropdownGO = ShipDropdownGO;
        controller.shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        NamesToShipDropdown(controller.FleetData.ShipsList);
    }
    private void ReorderDropdownOptions(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = dropdown.options;
        options.Reverse();
        // Update the UI
        dropdown.RefreshShownValue();
    }
    public void UnLoadFleetUI()
    {
        fleetUIRoot.SetActive(false);
    }
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        if (dropdown.name == "Dropdown Destination")
        {
            if (dropdown.options[index].text != "Select Destination" && GameManager.Instance.DestinationDictionary[dropdown.options[index].text] != null)
            {
                controller.FleetData.Destination = GameManager.Instance.DestinationDictionary[dropdown.options[index].text];
                controller.SelectedDestination = dropdown.options[index].text;
                dropdownDestinationText.text = dropdown.options[index].text;
            }
        }
    }
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

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
