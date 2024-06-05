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
    public List<StarSysData> systemsList;

    [SerializeField]
    private List<ShipData> shipList;
    private bool deltaShipList = false; 

    //private float WarpFactor = 0;
    //private float maxWarpFactor;
    public GameObject DestinationDropdownGO;
    public GameObject ShipDropdownGO;
    private TMP_Dropdown sysDropdown;
    private TMP_Dropdown shipDropdown;
    public Transform Destination;
    [SerializeField]
    private TMP_Text FleetName;
    [SerializeField]
    private TMP_Text dropdownDestinationText;
    [SerializeField]
    private TMP_Text dropdownShipText;
    [SerializeField]
    private TMP_Text sysDestination;
    private Camera galaxyEventCamera;
    [SerializeField]

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
        sysDropdown = DestinationDropdownGO.GetComponent<TMP_Dropdown>();
        sysDropdown.value = 0;
    }

    public void WarpSliderChange(float value)
    {
        float localValue = value * maxSliderValue;
        warpSliderText.text = localValue.ToString("0.0");
        controller.FleetData.CurrentWarpFactor = value;
    }
    public void LoadFleetUI(GameObject go) 
    {
        fleetUIRoot.SetActive(true);
        sysDropdown.value = 0;
        FleetName.text = go.name;
        controller = go.GetComponent<FleetController>();
        NamesToShipDropdown(controller.FleetData.ShipsList);
        //warpSlider.value = controller.FleetData.CurrentWarpFactor;
        if (controller.FleetData.Destination == null)
        {
            Destination = null;
            dropdownDestinationText.text = "";
        }
    }
    public void UnLoadFleetUI()
    {
        fleetUIRoot.SetActive(false);
    }
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        if (dropdown.name == "Dropdown _destination")
        {
            dropdownDestinationText.text = dropdown.options[index].text;
            var sys = systemsList[index];
            if (sys.SysTransform != null)
            {
                controller.FleetData.Destination = sys.SysTransform;
            }
            dropdownDestinationText.text = dropdown.options[index].text;
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
        shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
    }

    public void LoadDestinations(List<StarSysData> starSysDataList)
    {
        systemsList = starSysDataList;

        var destDropdown = DestinationDropdownGO.GetComponent<TMP_Dropdown>();
        destDropdown.options.Clear();

        // fill destDropdown sys sysList
        if (systemsList != null)
        {
            foreach (var sysData in systemsList)
            {
                destDropdown.options.Add(new TMP_Dropdown.OptionData() { text = sysData.GetSysName() });
            }
            //SystemsList.Add()
            //destDropdown.options.Add( new TMP_Dropdown.OptionData()) { t}
        }
        destDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destDropdown); });
    }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
