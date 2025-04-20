using Assets.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FleetUIController : MonoBehaviour
{
    public static FleetUIController Instance;
    public FleetController FleetController;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas parentCanvas;
    //[SerializeField]
    //private GameObject fleetUI_Prefab;// GameObject controlles this active UI on/off
    //[SerializeField]
    //private Slider warpSlider;
    //[SerializeField]
    //private TextMeshProUGUI warpSliderText;
    //[SerializeField]
    //private float maxSliderValue = 9.8f;
    //[SerializeField]
    //private GameObject warpUpButtonGO;
    //[SerializeField]
    //private GameObject warpDownButtonGO;
    //[SerializeField]
    //private float warpChange = 0.1f;
    //[SerializeField]
    //private bool warpButtonPress = false;

    //[SerializeField]
    //private List<ShipData> shipList;
    //private bool deltaShipList = false;
    //private TMP_Dropdown shipDropdown;
    //[SerializeField]
    //public GameObject ShipDropdownGO;
    //[SerializeField]
    //private TMP_Text dropdownShipText;
    //[SerializeField]
    //private TMP_Text FleetName;
    //[SerializeField]
    //private GameObject selectDestinationCursorButtonGO;
    //[SerializeField]
    //private GameObject cancelDestinationButtonGO;
    //[SerializeField]
    //private GameObject getDestinationButtonGO;
    //[SerializeField]
    //private TMP_Text selectDestinationBttonText;

    //public bool MouseClickSetsDestination = false;// used by FleetController and StarSysController

    //[SerializeField]
    //private TMP_Text destinationName;
    //[SerializeField]
    //private TMP_Text destinationCoordinates;



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
        //fleetUI_Prefab.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;

    }
    //private void FixedUpdate()
    //{
    //    if (warpButtonPress)
    //    {
    //        FleetController.FleetData.CurrentWarpFactor += warpChange;
    //        warpSlider.value = FleetController.FleetData.CurrentWarpFactor / maxSliderValue;
    //    }
    //}

    //public void WarpSliderChange(float value)
    //{
    //    float localValue = value * maxSliderValue;
    //    warpSliderText.text = localValue.ToString("0.0");
    //    if (FleetController != null)
    //        FleetController.FleetData.CurrentWarpFactor = localValue;
    //}
    //public void ResetWarpSlider(float value)
    //{
    //    maxSliderValue = FleetController.FleetData.MaxWarpFactor;
    //    warpSlider.value = value / maxSliderValue;
    //    warpSliderText.text = value.ToString("0.0");
    //}

    //public void WarpButtonUp(bool _warpButton) // bool comes from WarButtons with attached Event Triger
    //{
    //    if (warpChange < 0)
    //        warpChange = 0.1f;
    //    warpButtonPress = _warpButton;
    //}
    //public void WarpButtonDown(bool _warpButton)
    //{
    //    if (warpChange > 0)
    //        warpChange = -0.1f;
    //    warpButtonPress = _warpButton;
    //}
    //public void ClickOnOffDestinationCursor()
    //{
    //    if (MousePointerChanger.Instance.HaveGalaxyMapCursor == false)
    //    {
    //        getDestinationButtonGO.SetActive(false);
    //        selectDestinationBttonText.text = "Select Destination";
    //        MouseClickSetsDestination = true;
    //        MousePointerChanger.Instance.ChangeToGalaxyMapCursor();
    //        MousePointerChanger.Instance.HaveGalaxyMapCursor = true;
    //        if (FleetController.FleetData.Destination != null)
    //        {
    //            cancelDestinationButtonGO.SetActive(true);
    //            getDestinationButtonGO.SetActive(false);
    //        }

    //        selectDestinationCursorButtonGO.SetActive(true);
    //    }
    //}
    //public void ClickCancelDestinationButton()
    //{
    //    MousePointerChanger.Instance.ResetCursor();
    //    MousePointerChanger.Instance.HaveGalaxyMapCursor = false;
    //    destinationName.text = "No Destination";
    //    destinationCoordinates.text = "";
    //    MouseClickSetsDestination = false;
    //    selectDestinationCursorButtonGO.SetActive(true);
    //    getDestinationButtonGO.SetActive(true);
    //    if (FleetController.FleetData.Destination != null)
    //    {
    //        FleetController.FleetData.Destination = null;
    //        FleetController.DestinationLine.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        cancelDestinationButtonGO.SetActive(false);
    //    }
    //    // Consider destroying a target destination if it was fleet destination when destination is cancelled
    //}
    //public void SetAsDestination(GameObject hitObject, bool aFleet)
    //{
    //    //this.TurnOffCurrentMapDestination();

    //    FleetController.FleetData.Destination = hitObject;
    //    CivEnum civ = CivEnum.ZZUNINHABITED53; // star civ as uninhabited
    //    bool weKnowThem = false;
    //    bool isFleet = aFleet;
    //    int typeOfDestination = 0;
    //    destinationCoordinates.text = "X " + (hitObject.transform.position.x).ToString() + " / Y " + (hitObject.transform.position.y).ToString() + " / Z " + (hitObject.transform.position.z).ToString();
    //    if (hitObject.GetComponent<StarSysController>() != null)
    //    {
    //        StarSysController starSysController = hitObject.GetComponent<StarSysController>();
    //        civ = starSysController.StarSysData.CurrentOwnerCivEnum;
    //        typeOfDestination = (int)starSysController.StarSysData.SystemType;
    //        if (DiplomacyManager.Instance.FoundADiplomacyController(CivManager.Instance.LocalPlayerCivContoller, starSysController.StarSysData.CurrentCivController))
    //        {
    //            weKnowThem = true;
    //        }
    //    }
    //    else if (hitObject.GetComponent<FleetController>() != null)
    //    {
    //        isFleet = true;
    //        civ = hitObject.GetComponent<FleetController>().FleetData.CivEnum;
    //        if (DiplomacyManager.Instance.FoundADiplomacyController(CivManager.Instance.LocalPlayerCivContoller, hitObject.GetComponent<FleetController>().FleetData.CivController))
    //        {
    //            weKnowThem = true;
    //        }
    //    }
    //    else if (hitObject.GetComponent<PlayerDefinedTargetController>() != null)
    //    {
    //        PlayerDefinedTargetController playerTargetController = hitObject.GetComponent<PlayerDefinedTargetController>();
    //        civ = playerTargetController.PlayerTargetData.CivOwnerEnum;
    //        typeOfDestination = (int)playerTargetController.PlayerTargetData.GalaxyObjectType;
    //    }

    //    if (isFleet)
    //        destinationName.text = "Warp Signture";
    //    else
    //    {
    //        switch (typeOfDestination)
    //        {
    //            case (int)GalaxyObjectType.BlueStar:
    //                destinationName.text = "Blue Star at";
    //                break;
    //            case (int)GalaxyObjectType.WhiteStar:
    //                destinationName.text = "White Star at";
    //                break;
    //            case (int)GalaxyObjectType.YellowStar:
    //                destinationName.text = "Yellow Star at";
    //                break;
    //            case (int)GalaxyObjectType.OrangeStar:
    //                destinationName.text = "Orange Star at";
    //                break;
    //            case (int)GalaxyObjectType.RedStar:
    //                destinationName.text = "Red Star at";
    //                break;
    //            case (int)GalaxyObjectType.Nebula:
    //            case (int)GalaxyObjectType.OmarianNebula:
    //            case (int)GalaxyObjectType.OrionNebula:
    //                destinationName.text = "Nebula at";
    //                break;
    //            case (int)GalaxyObjectType.Station:
    //                destinationName.text = "Station at";
    //                break;
    //            case (int)GalaxyObjectType.BlackHole:
    //                destinationName.text = "Black Hole at";
    //                break;
    //            case (int)GalaxyObjectType.WormHole:
    //                destinationName.text = "WormHole at";
    //                break;
    //            case (int)GalaxyObjectType.TargetDestination:
    //                destinationName.text = "Target at";
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    MouseClickSetsDestination = false;
    //    cancelDestinationButtonGO.SetActive(true);
    //    getDestinationButtonGO.SetActive(false);
 

    //}
    //public void GetPlayerDefinedTargetDestination()
    //{
    //    PlayerDefinedTargetManager.instance.PlayerTargetFromData(FleetController.gameObject);
    //}
    //public void OnClickShipManager()
    //{
    //    GameObject notAMenu = new GameObject();
    //    GalaxyMenuUIController.Instance.OpenMenu(Menu.AFleetMenu, notAMenu);
    //    //SubMenuManager.Instance.OpenMenu(Menu.FleetsMenu, notAMenu);
    //    Destroy(notAMenu);
    //}
    //public void LoadFleetUI(GameObject rayHitGO)
    //{
    //    GameObject aFleetNotAMenu = new GameObject();
    //    GalaxyMenuUIController.Instance.OpenMenu(Menu.AFleetMenu, aFleetNotAMenu);

    //    //fleetUI_Prefab.SetActive(true);

    //    List<string> listings = new List<string>();

    //    FleetController = rayHitGO.GetComponent<FleetController>();
    //    cancelDestinationButtonGO.SetActive(false);
    //    FleetName.text = FleetController.FleetData.Name;
    //    PlayerDefinedTargetManager.instance.nameDestination = FleetName.text;
    //    WarpSliderChange(0f);

    //    //ship dropdown
    //    var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
    //    shipDropdown.options.Clear();
    //    List<TMP_Dropdown.OptionData> newShipItems = new List<TMP_Dropdown.OptionData>();
    //    string name;
    //    for (int i = 0; i < FleetController.FleetData.ShipsList.Count; i++)
    //    {
    //        if (FleetController.FleetData.ShipsList[i] != null)
    //        {
    //            TMP_Dropdown.OptionData newDataItem = new TMP_Dropdown.OptionData();
    //            name = FleetController.FleetData.ShipsList[i].name;
    //            name = name.Replace("(CLONE)", string.Empty);
    //            newDataItem.text = name;
    //            newShipItems.Add(newDataItem);
    //        }
    //    }
    //    shipDropdown.AddOptions(newShipItems);
    //    shipDropdown.RefreshShownValue();
    //    FleetController.UpdateMaxWarp();
    //    maxSliderValue = FleetController.FleetData.MaxWarpFactor;
    //    ResetWarpSlider(FleetController.FleetData.CurrentWarpFactor);
    //    Destroy(aFleetNotAMenu);
    //}
    //private void ReorderDropdownOptions(TMP_Dropdown dropdown)
    //{
    //    List<TMP_Dropdown.OptionData> options = dropdown.options;
    //    options.Reverse();
    //    // Update the UI
    //    dropdown.RefreshShownValue();
    //}
    //public void CloseUnLoadFleetUI()
    //{
    //    MouseClickSetsDestination = false;
    //    MousePointerChanger.Instance.ResetCursor();
    //    //fleetUI_Prefab.SetActive(false);//The single fleet UI
    //    GalaxyMenuUIController.Instance.CloseMenu(Menu.AFleetMenu); // The single fleet UI
    //}
    //private string GetDebuggerDisplay()
    //{
    //    return ToString();
    //}

}
