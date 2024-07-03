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
public class StarSysUIManager : MonoBehaviour
{
    public static StarSysUIManager instance;
    public StarSysController controller;
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject starSysUIRoot;
    //[SerializeField]
    //private Slider warpSlider;
    //[SerializeField]
    //private TextMeshProUGUI warpSliderText;
    //[SerializeField]
    //private float maxSliderValue = 9.8f;
    public List<StarSysData> systemsList;

    //[SerializeField]
    //private List<ShipData> shipList;
    private bool deltaShipList = false;

    //public GameObject ShipDropdownGO;
    //[SerializeField]
    //private TMP_Dropdown destinationDropdown;
    //private TMP_Dropdown shipDropdown;
    //public Transform Destination;
    [SerializeField]
    private TMP_Text CivName;
    //[SerializeField]
    //private TMP_Text dropdownDestinationText;
    //[SerializeField]
    //private TMP_Text dropdownShipText;
    //[SerializeField]
    //private TMP_Text sysDestination;
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
        starSysUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
        //destinationDropdown.value = 0;
    }

    public void WarpSliderChange(float value)
     {
        //float localValue = value * maxSliderValue;
        //warpSliderText.text = localValue.ToString("0.0");
        //controller.FleetData.CurrentWarpFactor = localValue;
    }
    public void ResetWarpSlider(float value)
    {
        //warpSlider.value = value/maxSliderValue;
        //warpSliderText.text = value.ToString("0.0");
    }
    public void LoadStarSysUI(GameObject go) 
    {
        FleetUIManager.instance.UnLoadFleetUI();
        starSysUIRoot.SetActive(true);
        controller = go.GetComponent<StarSysController>();
        var civEnum = controller.StarSysData.CurrentOwner;
        var civData = CivManager.instance.GetCivDataByCivEnum(civEnum);
        CivName.text = civData.CivLongName;
    }
    public void UnLoadStarSysUI()
    {
        starSysUIRoot.SetActive(false);
    }
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        //int index = dropdown.value;
        //if (dropdown.name == "Dropdown Destination")
        //{
        //    if (dropdown.options[index].text != "Select Destination" && GameManager.Instance.DestinationDictionary[dropdown.options[index].text] != null)
        //    {
        //        controller.FleetData.Destination = GameManager.Instance.DestinationDictionary[dropdown.options[index].text];
        //        controller.SelectedDestination = dropdown.options[index].text;
        //        dropdownDestinationText.text = dropdown.options[index].text;
        //    }
        //}
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
        ////DropdownItemSelected(shipDropdown);
        ////shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
