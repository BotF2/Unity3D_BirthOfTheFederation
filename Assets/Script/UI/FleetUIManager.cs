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
using System.Linq;

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
    [SerializeField]
    private GameObject selectDestinationCursorButtonGO;
    [SerializeField]
    private TMP_Text selectDestinationBttonText;

    public bool MouseClickSetsDestination = false;// used by FleetController and StarSysController
    [SerializeField]
    private GameObject cancelDestinationButtonGO;
    [SerializeField]
    private TMP_Text destinationName;
    [SerializeField]
    private TMP_Text destinationCoordinates;
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
    public void ClickOnOffDestinationCursor()
    {
        if (MousePointerChanger.Instance.HaveGalaxyMapCursor == false)
        {

            selectDestinationBttonText.text = "Select Destination";
            MouseClickSetsDestination = true;
            MousePointerChanger.Instance.ChangeToGalaxyMapCursor();
            MousePointerChanger.Instance.HaveGalaxyMapCursor = true;
            if (controller.FleetData.Destination != null)
                cancelDestinationButtonGO.SetActive(true);
            selectDestinationCursorButtonGO.SetActive(true);
        }
    }
    public void ClickCancelDestinationButton()
    {
        if (controller.FleetData.Destination != null)
        {
            var canvases = controller.FleetData.Destination.GetComponentsInChildren<Canvas>();
            foreach (Canvas c in canvases)
            {
                if (c.name == "OurSelectedMarkerCanvas")
                {
                    c.gameObject.SetActive(false);
                }

            }
            controller.FleetData.Destination = null;
        }
        MousePointerChanger.Instance.ResetCursor();
        MousePointerChanger.Instance.HaveGalaxyMapCursor = false;
        destinationName.text = "No Destination";
        MouseClickSetsDestination = false;
        cancelDestinationButtonGO.SetActive(false);
        selectDestinationCursorButtonGO.SetActive(true);
    }
    public void SetAsDestination(GameObject hitObject)
    {
        CivEnum civ = CivEnum.ZZUNINHABITED53;
        bool weKnowThem = false;
        if (CivManager.Instance.LocalPlayerCivContoller.CivData.CivEnumsWeKnow.Contains(civ))
            weKnowThem = true;  
        cancelDestinationButtonGO.SetActive(true);
        if (controller.FleetData.Destination != hitObject)
        {
            MousePointerChanger.Instance.ResetCursor();
            MouseClickSetsDestination = false;
            if (controller.FleetData.Destination != null)
            {
                if (controller.FleetData.Destination.GetComponent<StarSysController>() != null)
                    controller.FleetData.Destination.GetComponent<StarSysController>().OurSelectedMarkerCanvas.gameObject.SetActive(false);
                else if (controller.FleetData.Destination.GetComponent<FleetController>() != null)
                    controller.FleetData.Destination.GetComponent<FleetController>().OurSelectedMarkerCanvas.gameObject.SetActive(false);
            }
            if (hitObject.GetComponent<StarSysController>() != null)
                civ = hitObject.GetComponent<StarSysController>().StarSysData.CurrentOwner;
                
            else if (hitObject.GetComponent<FleetController>() != null)
                civ = hitObject.GetComponent<FleetController>().FleetData.CivEnum;
        }
        controller.FleetData.Destination = hitObject;
        destinationCoordinates.text = hitObject.transform.position.ToString();
        if (civ != CivManager.Instance.LocalPlayerCivEnum)
        {
            if (hitObject.GetComponent<FleetController>() != null)
                destinationName.text = "Warp Signture at";
            //ToDo: update location as fleet moves
            //ToDo: code in name of fleet on contact of colliders
            else
            {
                if (weKnowThem)
                    destinationName.text = hitObject.name;
                else destinationName.text = "Galaxy Object at";
            }
        }
        else 
        {
            destinationName.text = hitObject.name.ToString();
        }
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
        PlayerDefinedTargetManager.instance.nameOfLocalFleet = FleetName.text;
        WarpSliderChange(0f);
       
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
        ResetWarpSlider(controller.FleetData.CurrentWarpFactor);

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
        MouseClickSetsDestination = false;
        MousePointerChanger.Instance.ResetCursor();
        fleetUIRoot.SetActive(false);
    }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
