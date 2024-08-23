using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Core;
using Unity.VisualScripting;
using System.Diagnostics;
using UnityEngine.Rendering;
using static System.Net.Mime.MediaTypeNames;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class FleetUIManager : MonoBehaviour
{
    public static FleetUIManager Instance;
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
    public string noDestination;

    [SerializeField]
    private List<ShipData> shipList;
    private bool deltaShipList = false;

    public GameObject ShipDropdownGO;
    [SerializeField]
    private TMP_Dropdown destinationDropdown;
    private TMP_Dropdown shipDropdown;
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
        fleetUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
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
    public void OnClickShipManager()
    {
        FleetSelectionUI.Instance.LoadShipUIManager(controller);
    }
    public void LoadFleetUI(GameObject go) 
    {
        StarSysUIManager.Instance.UnLoadStarSysUI();
        FleetSelectionUI.Instance.UnLoadShipManagerUI();
        fleetUIRoot.SetActive(true);
        destinationDropdown.options.Clear();
        noDestination = GameManager.Instance.GameData.NoDestination;
        List<string> listings = new List<string>();
        foreach (string location in GameManager.Instance.GameData.DestinationNames)
        {
            listings.Add(location);
        }
        controller = go.GetComponent<FleetController>();
        FleetName.text = controller.FleetData.Name;
        ResetWarpSlider(controller.FleetData.CurrentWarpFactor);
        int ourFleet = -1;
        /* customize the list of destinations
         Remove the current fleet name */
       
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
        int indexOfSelected = -1;
        if (controller.SelectedDestination != "" && controller.SelectedDestination != noDestination)
        {
            for (int i = 0; i < listings.Count; i++)
            {

                if (controller.SelectedDestination == listings[i] && GameManager.Instance.GameData.DestinationDictionary[listings[i]] != null)
                {
                    controller.FleetData.Destination = GameManager.Instance.GameData.DestinationDictionary[listings[i]];
                    controller.SelectedDestination = listings[i];
                    dropdownDestinationText.text = listings[i];
                    indexOfSelected = i;
                    break;
                }
            }
        }
        if (controller.SelectedDestination == "" || controller.SelectedDestination == noDestination)
        {
            controller.FleetData.Destination = null;
            controller.SelectedDestination = noDestination;
            dropdownDestinationText.text = noDestination;
            for (int i = 0; i < listings.Count; i++)
            {
                if (listings[i] == noDestination)
                {
                    indexOfSelected = i;
                    break;
                }
            }
        }

        List<TMP_Dropdown.OptionData> dataItems = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < listings.Count; i++)
        {
            TMP_Dropdown.OptionData newDataItem = new TMP_Dropdown.OptionData();
            newDataItem.text = listings[i];
            dataItems.Add(newDataItem);    
        }
        destinationDropdown.AddOptions(dataItems);
        if (indexOfSelected > -1) 
        destinationDropdown.value = indexOfSelected;
        destinationDropdown.RefreshShownValue();
        destinationDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destinationDropdown); });

        //ship dropdown
        var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        shipDropdown.options.Clear();
        List<TMP_Dropdown.OptionData> newShipItems = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < controller.FleetData.ShipsList.Count; i++)
        {
            if (controller.FleetData.ShipsList[i] != null)
            {
                TMP_Dropdown.OptionData newDataItem = new TMP_Dropdown.OptionData();
                newDataItem.text = controller.FleetData.ShipsList[i].name;
                newDataItem.text.Replace("(CLONE)", string.Empty);
                newShipItems.Add(newDataItem);
            }
        }
        shipDropdown.AddOptions(newShipItems);
        shipDropdown.RefreshShownValue();

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
            if (dropdown.options[index].text != "No Destination Selected" && GameManager.Instance.GameData.DestinationDictionary[dropdown.options[index].text] != null)
            {
                controller.FleetData.Destination = GameManager.Instance.GameData.DestinationDictionary[dropdown.options[index].text];
                controller.SelectedDestination = dropdown.options[index].text;
                dropdownDestinationText.text = dropdown.options[index].text;
                destinationDropdown.value = index;
                destinationDropdown.RefreshShownValue();
            }
            else if(dropdown.options[index].text == "No Destination Selected")
            {
                controller.FleetData.Destination = null;
                controller.SelectedDestination = "No Destination Selected";
                dropdownDestinationText.text = "No Destination Selected";
            }
        }
    }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
