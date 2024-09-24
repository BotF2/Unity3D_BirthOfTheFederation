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
    private GameObject fleetUIRoot;// GameObject controlles this active UI on/off
    [SerializeField]
    private Slider warpSlider;
    [SerializeField]
    private TextMeshProUGUI warpSliderText;
    [SerializeField]
    private float maxSliderValue = 9.8f;
    [SerializeField]
    private List<ShipData> shipList;
    private bool deltaShipList = false;
    private TMP_Dropdown shipDropdown;
    [SerializeField]
    public GameObject ShipDropdownGO;
    [SerializeField]
    private TMP_Text dropdownShipText;
    [SerializeField]
    private TMP_Text FleetName;
    public string noDestination;
    public bool MouseSetToDestination = false;
    [SerializeField]
    private TMP_Text destinationTextMP;

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
        
    }

    public void WarpSliderChange(float value)
    {
        float localValue = value * maxSliderValue;
        warpSliderText.text = localValue.ToString("0.0");
        controller.FleetData.CurrentWarpFactor = localValue;
    }
    public void ResetWarpSlider(float value)
    {
        maxSliderValue =controller.FleetData.MaxWarpFactor;
        warpSlider.value = value/maxSliderValue;
        warpSliderText.text = value.ToString("0.0");
    }
    public void ActivateSelectionOfDestination()
    {
        if (MousePointerChanger.Instance.HaveGalaxyCursor == false)
        {
            MouseSetToDestination = true;
            MousePointerChanger.Instance.ChangeToGalaxyMapCursor();
        }
        else 
        { 
            MouseSetToDestination = false;
            MousePointerChanger.Instance.ResetCursor();
            controller.FleetData.Destination = null;
        }
    }
    public void SetStarSysAsDestination(GameObject hitObject)
    {
        MousePointerChanger.Instance.ResetCursor();
        if (controller.FleetData.Destination != hitObject)
        {
            if (controller.FleetData.Destination != null)
            {
                if (controller.FleetData.Destination.GetComponent<StarSysController>() != null)
                    controller.FleetData.Destination.GetComponent<StarSysController>().OurSelectedMarkerCanvas.gameObject.SetActive(false);
                else controller.FleetData.Destination.GetComponent<FleetController>().OurSelectedMarkerCanvas.gameObject.SetActive(false);
            }
        }
        controller.FleetData.Destination = hitObject;
        destinationTextMP.text = hitObject.name;
        // do we know them
    }
    public void SetFleetAsDestination(GameObject hitObject)
    {
        MousePointerChanger.Instance.ResetCursor();
        if (controller.FleetData.Destination != hitObject) 
        {
            if (controller.FleetData.Destination != null) 
            {  
                if (controller.FleetData.Destination.GetComponent<FleetController>() != null)
                    controller.FleetData.Destination.GetComponent<FleetController>().OurSelectedMarkerCanvas.gameObject.SetActive(false);
                else controller.FleetData.Destination.GetComponent<StarSysController>().OurSelectedMarkerCanvas.gameObject.SetActive(false);
            }
        }
        controller.FleetData.Destination = hitObject;
        // ToDo: do we know them? change from Unknown
        if (hitObject.GetComponent<FleetController>().FleetData.CivEnum != GameManager.Instance.GameData.LocalPlayerCivEnum)
            destinationTextMP.text = "Warp Signture";
        else destinationTextMP.text = hitObject.name;
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

        List<string> listings = new List<string>();

        controller = go.GetComponent<FleetController>();
        FleetName.text = controller.FleetData.Name;
        
        ResetWarpSlider(controller.FleetData.CurrentWarpFactor);
       
        //ship dropdown
        var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        shipDropdown.options.Clear();
        List<TMP_Dropdown.OptionData> newShipItems = new List<TMP_Dropdown.OptionData>();
        string name;
        for (int i = 0; i < controller.FleetData.ShipsList.Count; i++)
        {
            if (controller.FleetData.ShipsList[i] != null)
            {
                TMP_Dropdown.OptionData newDataItem = new TMP_Dropdown.OptionData();
                name = controller.FleetData.ShipsList[i].name;
                name = name.Replace("(CLONE)", string.Empty);
                newDataItem.text = name;
                newShipItems.Add(newDataItem);
            }
        }
        shipDropdown.AddOptions(newShipItems);
        shipDropdown.RefreshShownValue();
        controller.UpdateMaxWarp();
        maxSliderValue = controller.FleetData.MaxWarpFactor;


    }
    private void ReorderDropdownOptions(TMP_Dropdown dropdown)
    {
        List<TMP_Dropdown.OptionData> options = dropdown.options;
        options.Reverse();
        // Update the UI
        dropdown.RefreshShownValue();
    }
    public void CloseUnLoadFleetUI()
    {
        MouseSetToDestination = false;
        MousePointerChanger.Instance.ResetCursor();
        fleetUIRoot.SetActive(false);
    }
    //void DropdownItemSelected(TMP_Dropdown dropdown)
    //{
    //    int index = dropdown.value;
    //    if (dropdown.name == "Dropdown Destination")
    //    {
    //        if (dropdown.options[index].text != "No Destination Selected" && GameManager.Instance.GameData.DestinationDictionary[dropdown.options[index].text] != null)
    //        {
    //            controller.FleetData.Destination = GameManager.Instance.GameData.DestinationDictionary[dropdown.options[index].text];
    //            controller.SelectedDestination = dropdown.options[index].text;
    //            dropdownDestinationText.text = dropdown.options[index].text;
    //            destinationDropdown.value = index;
    //            destinationDropdown.RefreshShownValue();
    //        }
    //        else if(dropdown.options[index].text == "No Destination Selected")
    //        {
    //            controller.FleetData.Destination = null;
    //            controller.SelectedDestination = "No Destination Selected";
    //            dropdownDestinationText.text = "No Destination Selected";
    //        }
    //    }
    //}
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
