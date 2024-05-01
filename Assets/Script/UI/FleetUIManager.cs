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
    public List<ShipData> shipList;
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
    private string Name;
    [SerializeField]
    private TMP_Text dropdownSysText;
    [SerializeField]
    private TMP_Text dropdownShipText;
    [SerializeField]
    private TMP_Text sysDestination;

    private Camera galaxyEventCamera;
    [SerializeField]
    //private Canvas openFleetUIButtonCanvas;
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
    }
    public void LoadFleetUI(string name)
    {
        fleetUIRoot.SetActive(true);
        FleetUIManager.instance.Name = name;
    }
    public void UnLoadFleetUI()
    {
        fleetUIRoot.SetActive(false);
    }
}
