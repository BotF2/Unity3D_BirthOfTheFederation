using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Core;

public class FleetUIManager : MonoBehaviour
{
    public static FleetUIManager instance;
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject fleetUIRoot;
    public GameObject ButtonCloseFleetUI;
    //public GameObject buttonLoadFleetUI;
    [SerializeField]
    private Slider warpSlider;
    [SerializeField]
    private TextMeshProUGUI warpSliderText;
    [SerializeField]
    private float maxSliderValue = 100.0f;
    public List<StarSysData> systemsList;
    [SerializeField]
    private List<ShipData> shipList;
    private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
    //public Transform Destination;
    private float WarpFactor = 9;
    private float fudgeFactor = 0.05f; // so we see warp factor as in Star Trek but move in game space
    private float dropOutOfWarpDistance = 0.5f;
    private float maxWarpFactor;
    // public bool warpTravel = false;// do we need this warp factor >0
    //private Rigidbody rb;
    public GameObject DestinationDropdownGO;
    public GameObject ShipDropdownGO;
    public Transform Destination;
    [SerializeField]
    private TMP_Text Name;
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
        //Canvas FleetUICanvas = GameObject.FindGameObjectWithTag("CanvasFleetUI").GetComponent<Canvas>() as Canvas;
        parentCanvas.worldCamera = galaxyEventCamera;
        systemsList = StarSysManager.instance.StarSysDataList;

        var destDropdown = DestinationDropdownGO.GetComponent<TMP_Dropdown>();
        destDropdown.options.Clear();
        List<string> sysList = new List<string>();
        // fill destDropdown sys sysList
        foreach (var item in systemsList)
        {
            destDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.SysName });
        }
        DropdownItemSelected(destDropdown);
        destDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destDropdown); });

        //var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        //shipDropdown.options.Clear();

        //// fill destDropdown sys sysList
        //foreach (var item in shipList)
        //{
        //    shipDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.shipName });
        //}
        //DropdownItemSelected(shipDropdown);
        //shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });


    }
    public void WarpSliderChange(float value)
    {
        float localValue = value * maxSliderValue;
        warpSliderText.text = localValue.ToString("0.00");
        WarpFactor = value;
    }
    public void LoadFleetUI(string name, GameObject ourDestinationDropdownGO, List<ShipData> curretnShips)
    {
        Ships(curretnShips) ;
        Name.text = name;
        this.DestinationDropdownGO = ourDestinationDropdownGO;
        fleetUIRoot.SetActive(true);

    }
    public void UnLoadFleetUI()
    {
        fleetUIRoot.SetActive(false);
    }
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        if (dropdown.name == "Dropdown Systems")
        {
            dropdownDestinationText.text = dropdown.options[index].text;
            var sys = systemsList[index];
            Destination = sys.SysTransform;
        }
        else if (dropdown.name == "Dropdown Ships")
        {
            dropdownShipText.text = dropdown.options[index].text;
            var ship = shipList[index]; // Can we or should we do stuff here??

        }
    }
    private void Ships(List<ShipData> ships)
    {
        var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        shipDropdown.options.Clear();

        // fill destDropdown sys sysList
        foreach (var item in ships)
        {
            shipDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.shipName });
        }
        DropdownItemSelected(shipDropdown);
        shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
    }
}
