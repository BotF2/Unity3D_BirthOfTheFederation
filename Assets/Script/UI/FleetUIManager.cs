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

    [SerializeField]
    //private GameObject DestinationDropdownGO;
    public GameObject ShipDropdownGO;
    [SerializeField]
    private TMP_Dropdown destinationDropdown;
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
        destinationDropdown.value = 0;
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
        destinationDropdown.value = 0;
        FleetName.text = go.name;
        controller = go.GetComponent<FleetController>();
        
        //DestinationDropdown.options.Clear();
        destinationDropdown = GameManager.Instance.DestinationDropdown;
        if (destinationDropdown.options[0].text != "Select Destination")
        {
            destinationDropdown.options.Insert(0, new TMP_Dropdown.OptionData("Select Destination"));
            destinationDropdown.value = 0;
            destinationDropdown.RefreshShownValue();
        }
        var listing = destinationDropdown.options;
        int index =0;
        if (controller.SelectedDestination != "")
        {
            foreach (var option in listing)
            {
                if (option.text == controller.SelectedDestination)
                {
                    index = destinationDropdown.options.IndexOf(option);
                }
            }
        }
        destinationDropdown.value = index;

        DropdownItemSelected(destinationDropdown);
        destinationDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destinationDropdown); });

        controller.shipDropdownGO = ShipDropdownGO;
        controller.shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        //controller.shipDropdown 
        NamesToShipDropdown(controller.FleetData.ShipsList);
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
