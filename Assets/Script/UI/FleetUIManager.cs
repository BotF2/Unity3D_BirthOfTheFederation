using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private Rigidbody rb;
    public GameObject sysDropdownGO;
    public GameObject shipDropdownGO;
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text dropdownSysText;
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
    }
    public void WarpSliderChange(float value)
    {
        float localValue = value * maxSliderValue;
        warpSliderText.text = localValue.ToString("0.00");
        WarpFactor = value;
    }
    public void LoadFleetUI(string name, string destination)
    {
        fleetUIRoot.SetActive(true);
        Name.text = name;
        sysDestination.text = destination;
    }
    public void UnLoadFleetUI()
    {
        fleetUIRoot.SetActive(false);
    }
}
