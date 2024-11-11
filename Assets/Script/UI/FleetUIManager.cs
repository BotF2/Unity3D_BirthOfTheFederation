using Assets.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FleetUIManager : MonoBehaviour
{
    public static FleetUIManager Instance;
    public FleetController ourUIFleetController;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas parentCanvas;
    [SerializeField]
    private GameObject fleetUIToggle;// GameObject controlles this active UI on/off
    [SerializeField]
    private Slider warpSlider;
    [SerializeField]
    private TextMeshProUGUI warpSliderText;
    [SerializeField]
    private float maxSliderValue = 9.8f;
    [SerializeField]
    private GameObject warpUpButtonGO;
    [SerializeField]
    private GameObject warpDownButtonGO;
    [SerializeField]
    private float warpChange = 0.1f;
    [SerializeField]
    private bool warpButtonPress = false;

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
    private GameObject cancelDestinationButtonGO;
    [SerializeField]
    private GameObject setDestinationButtonGO;
    [SerializeField]
    private TMP_Text selectDestinationBttonText;

    public bool MouseClickSetsDestination = false;// used by FleetController and StarSysController

    [SerializeField]
    private TMP_Text destinationName;
    [SerializeField]
    private TMP_Text destinationCoordinates;



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
        fleetUIToggle.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;

    }
    private void FixedUpdate()
    {
        if (warpButtonPress)
        {
            ourUIFleetController.FleetData.CurrentWarpFactor += warpChange;
            warpSlider.value = ourUIFleetController.FleetData.CurrentWarpFactor / maxSliderValue;
        }
    }

    public void WarpSliderChange(float value)
    {
        float localValue = value * maxSliderValue;
        warpSliderText.text = localValue.ToString("0.0");
        if (ourUIFleetController != null)
            ourUIFleetController.FleetData.CurrentWarpFactor = localValue;
    }
    public void ResetWarpSlider(float value)
    {
        maxSliderValue = ourUIFleetController.FleetData.MaxWarpFactor;
        warpSlider.value = value / maxSliderValue;
        warpSliderText.text = value.ToString("0.0");
    }

    public void WarpButtonUp(bool _warpButton) // bool comes from WarButtons with attached Event Triger
    {
        if (warpChange < 0)
            warpChange = 0.1f;
        warpButtonPress = _warpButton;
    }
    public void WarpButtonDown(bool _warpButton)
    {
        if (warpChange > 0)
            warpChange = -0.1f;
        warpButtonPress = _warpButton;
    }
    public void ClickOnOffDestinationCursor()
    {
        if (MousePointerChanger.Instance.HaveGalaxyMapCursor == false)
        {
            setDestinationButtonGO.SetActive(false);
            selectDestinationBttonText.text = "Select Destination";
            MouseClickSetsDestination = true;
            MousePointerChanger.Instance.ChangeToGalaxyMapCursor();
            MousePointerChanger.Instance.HaveGalaxyMapCursor = true;
            if (ourUIFleetController.FleetData.Destination != null)
            {
                cancelDestinationButtonGO.SetActive(true);
                setDestinationButtonGO.SetActive(false);
            }

            selectDestinationCursorButtonGO.SetActive(true);
        }
    }
    public void ClickCancelDestinationButton()
    {
        MousePointerChanger.Instance.ResetCursor();
        MousePointerChanger.Instance.HaveGalaxyMapCursor = false;
        destinationName.text = "No Destination";
        destinationCoordinates.text = "";
        MouseClickSetsDestination = false;
        selectDestinationCursorButtonGO.SetActive(true);
        setDestinationButtonGO.SetActive(true);
        if (ourUIFleetController.FleetData.Destination != null)
        {
            ourUIFleetController.FleetData.Destination = null;
            ourUIFleetController.DestinationLine.gameObject.SetActive(false);
        }
        else
        {
            cancelDestinationButtonGO.SetActive(false);
        }
    }
    public void SetAsDestination(GameObject hitObject, bool aFleet)
    {
        //this.TurnOffCurrentMapDestination();

        ourUIFleetController.FleetData.Destination = hitObject;
        CivEnum civ = CivEnum.ZZUNINHABITED53; // star civ as uninhabited
        bool weKnowThem = false;
        bool isFleet = aFleet;
        int typeOfDestination = 0;
        destinationCoordinates.text = "X " + (hitObject.transform.position.x).ToString() + " / Y " + (hitObject.transform.position.y).ToString() + " / Z " + (hitObject.transform.position.z).ToString();
        if (hitObject.GetComponent<StarSysController>() != null)
        {
            StarSysController starSysController = hitObject.GetComponent<StarSysController>();
            civ = starSysController.StarSysData.CurrentOwner;
            typeOfDestination = (int)starSysController.StarSysData.SystemType;
        }
        else if (hitObject.GetComponent<FleetController>() != null)
        {
            isFleet = true;
            civ = hitObject.GetComponent<FleetController>().FleetData.CivEnum;
        }
        else if (hitObject.GetComponent<PlayerDefinedTargetController>() != null)
        {
            PlayerDefinedTargetController playerTargetController = hitObject.GetComponent<PlayerDefinedTargetController>();
            civ = playerTargetController.PlayerTargetData.CivOwnerEnum;
            typeOfDestination = (int)playerTargetController.PlayerTargetData.GalaxyObjectType;
        }
        // Fix this
        if (CivManager.Instance.LocalPlayerCivContoller.CivData.CivEnumsWeKnow.Contains(civ))
        {
            weKnowThem = true;
        }
        if (isFleet)
            destinationName.text = "Warp Signture";
        else
        {
            switch (typeOfDestination)
            {
                case (int)GalaxyObjectType.BlueStar:
                case (int)GalaxyObjectType.WhiteStar:
                case (int)GalaxyObjectType.YellowStar:
                case (int)GalaxyObjectType.OrangeStar:
                case (int)GalaxyObjectType.RedStar:
                    destinationName.text = "Star at";
                    break;
                case (int)GalaxyObjectType.Nebula:
                case (int)GalaxyObjectType.OmarianNebula:
                case (int)GalaxyObjectType.OrionNebula:
                    destinationName.text = "Nebula at";
                    break;
                case (int)GalaxyObjectType.Station:
                    destinationName.text = "Station at";
                    break;
                case (int)GalaxyObjectType.BlackHole:
                    destinationName.text = "Black Hole at";
                    break;
                case (int)GalaxyObjectType.WormHole:
                    destinationName.text = "WormHole at";
                    break;
                case (int)GalaxyObjectType.TargetDestination:
                    destinationName.text = "Target at";
                    break;
                default:
                    break;
            }
        }
        MouseClickSetsDestination = false;
        cancelDestinationButtonGO.SetActive(true);
    }
    public void GetPlayerDefinedTargetDestination()
    {
        PlayerDefinedTargetManager.instance.PlayerTargetFromData(ourUIFleetController.gameObject);
    }
    public void OnClickShipManager()
    {
        FleetSelectionUI.Instance.LoadShipUIManager(ourUIFleetController);
    }
    public void LoadFleetUI(GameObject rayHitGO)
    {
        YourStarSysUIManager.Instance.CloseUnLoadStarSysUI();
        TheirSysDiplomacyUIManager.Instance.CloseUnLoadFleetUI();
        FleetSelectionUI.Instance.UnLoadShipManagerUI();
        fleetUIToggle.SetActive(true);

        List<string> listings = new List<string>();

        ourUIFleetController = rayHitGO.GetComponent<FleetController>();
        cancelDestinationButtonGO.SetActive(false);
        FleetName.text = ourUIFleetController.FleetData.Name;
        PlayerDefinedTargetManager.instance.nameDestination = FleetName.text;
        WarpSliderChange(0f);

        //ship dropdown
        var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        shipDropdown.options.Clear();
        List<TMP_Dropdown.OptionData> newShipItems = new List<TMP_Dropdown.OptionData>();
        string name;
        for (int i = 0; i < ourUIFleetController.FleetData.ShipsList.Count; i++)
        {
            if (ourUIFleetController.FleetData.ShipsList[i] != null)
            {
                TMP_Dropdown.OptionData newDataItem = new TMP_Dropdown.OptionData();
                name = ourUIFleetController.FleetData.ShipsList[i].name;
                name = name.Replace("(CLONE)", string.Empty);
                newDataItem.text = name;
                newShipItems.Add(newDataItem);
            }
        }
        shipDropdown.AddOptions(newShipItems);
        shipDropdown.RefreshShownValue();
        ourUIFleetController.UpdateMaxWarp();
        maxSliderValue = ourUIFleetController.FleetData.MaxWarpFactor;
        ResetWarpSlider(ourUIFleetController.FleetData.CurrentWarpFactor);

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
        fleetUIToggle.SetActive(false);
    }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }

}
