using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Core;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System;
using System.Linq;

public class GalaxyMenuUIController : MonoBehaviour
{
    public static GalaxyMenuUIController Instance; // { get; private set; }
    [SerializeField]
    private GameObject buildListUI;
    [SerializeField]
    private GameObject systemsMenuView;
    [SerializeField]
    private GameObject sysListContainer;
    [SerializeField]
    private GameObject aSystemView;
    [SerializeField]
    private GameObject aSysContainer;
    [SerializeField]
    private GameObject fleetsMenuView;
    [SerializeField] 
    private GameObject diplomacyMenuView;
    [SerializeField] 
    private GameObject intelMenuView;
    [SerializeField] 
    private GameObject encyclopediaMenuView;
    [SerializeField]
    private GameObject aNull;
    [SerializeField]
    private GameObject closeMenuButton;
    [SerializeField]
    private GameObject sysBackground;
    [SerializeField]
    private GameObject fleetBackground;
    [SerializeField]
    private GameObject diplomacyBackground;
    [SerializeField]
    private GameObject intelBackground;
    [SerializeField]
    private GameObject encyclopediaBackground;
    [SerializeField]
    private List<StarSysController> sysControllers;
    [SerializeField]
    private List<GameObject> listOfUIgos;
    [SerializeField]
    private GameObject powerOverload;


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
    void Start()
    {
        systemsMenuView.SetActive(false);
        aSystemView.SetActive(false);   
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false); 
        intelMenuView.SetActive(false) ;
        encyclopediaMenuView.SetActive(false);
        closeMenuButton.SetActive(false);
        sysBackground.SetActive(false);
        fleetBackground.SetActive(false);
        diplomacyBackground.SetActive(false);
        intelBackground.SetActive(false);
        encyclopediaBackground.SetActive(false);
        buildListUI.SetActive(false);
        SetUISystemsData();//get our system ui game objects to match your sys controllers
    }

    public void CloseTheOpenGalaxyUI()
    {
        MenuManager.Instance.OpenMenu(Menu.None, aNull);
        closeMenuButton.SetActive(false);
        sysBackground.SetActive(false);
        fleetBackground.SetActive(false);
        diplomacyBackground.SetActive(false);
        intelBackground.SetActive(false);
        encyclopediaBackground.SetActive(false);
    }
    public void OpenSystems()
    {
        if (!systemsMenuView.activeSelf)
        {
            sysBackground.SetActive(true);
            closeMenuButton.SetActive(true);
            fleetBackground.SetActive(false);
            diplomacyBackground.SetActive(false);
            intelBackground.SetActive(false);
            MenuManager.Instance.OpenMenu(Menu.SystemsMenu, aNull);    
            SetUISystemsData();
        }
        else
        {
            MenuManager.Instance.OpenMenu(Menu.None, systemsMenuView);

            sysBackground.SetActive(false);
        }

    }
    public void OpenASystemUI(StarSysController theSysCon) // now system ui open single system view when our system is clicked on galaxy map
    {
        if (listOfUIgos.Contains(theSysCon.StarSysListUIGameObject))
        {
            aSystemView.SetActive(true);
            closeMenuButton.SetActive(true);
            systemsMenuView.SetActive(false);
            fleetBackground.SetActive(false);
            diplomacyBackground.SetActive(false);
            intelBackground.SetActive(false);
            encyclopediaBackground.SetActive(false);
            buildListUI.SetActive(false);
            theSysCon.StarSysListUIGameObject.SetActive(true);
            theSysCon.StarSysListUIGameObject.transform.SetParent(aSysContainer.transform, false);
            sysBackground.SetActive(true);
        }
    }
    public void OpenFleets()
    {
        if (!fleetsMenuView.activeSelf)
        {
            MenuManager.Instance.OpenMenu (Menu.FleetsMenu, aNull);
            fleetBackground.SetActive(true);
            closeMenuButton.SetActive(true);
            sysBackground.SetActive(false);
            diplomacyBackground.SetActive(false);
            intelBackground.SetActive(false);
            encyclopediaBackground.SetActive(false);
        }
        else
        {
            MenuManager.Instance.OpenMenu(Menu.None, fleetsMenuView);
            fleetBackground.SetActive(false);
        }
    }
    public void OpenDiplomacy()
    {
        if (!diplomacyMenuView.activeSelf)
        {
            MenuManager.Instance.OpenMenu(Menu.DiplomacyMenu, aNull);
            diplomacyBackground.SetActive(true);
            closeMenuButton.SetActive(true);
            sysBackground.SetActive(false);
            fleetBackground.SetActive(false);
            intelBackground.SetActive(false);
            encyclopediaBackground.SetActive(false);
        } 
        else 
        {
            MenuManager.Instance.OpenMenu(Menu.None, diplomacyMenuView);    

            diplomacyBackground.SetActive(false);
        }
    }

    public void OpenIntel()
    {
        if (!intelMenuView.activeSelf)
        {
            MenuManager.Instance.OpenMenu(Menu.IntellMenu, aNull);
            intelBackground.SetActive(true);
            closeMenuButton.SetActive(true);
            sysBackground.SetActive(false);
            fleetBackground.SetActive(false);
            diplomacyBackground.SetActive(false);
            encyclopediaBackground.SetActive(false);
        }
        else
        { 
            MenuManager.Instance.OpenMenu(Menu.None, intelMenuView);
            intelBackground.SetActive(false);
        }

    }

    public void OpenEncyclopedia()
    {
        if (!encyclopediaMenuView.activeSelf)
        {
            MenuManager.Instance.OpenMenu(Menu.EncyclopedianMenu, aNull);
            encyclopediaBackground.SetActive(true);
            closeMenuButton.SetActive(true);
            sysBackground.SetActive(false);
            fleetBackground.SetActive(false);
            diplomacyBackground.SetActive(false);
            intelBackground.SetActive(false);
        }
        else 
        {
            MenuManager.Instance.OpenMenu(Menu.None, encyclopediaMenuView);
            encyclopediaBackground.SetActive(false);
        }
    }
    private void SetUISystemsData()
    {
        if (StarSysManager.Instance.StarSysControllerList.Count > 0)
        {
            foreach (var sysCon in StarSysManager.Instance.StarSysControllerList)
            {
                if (sysCon.StarSysListUIGameObject != null && GameController.Instance.AreWeLocalPlayer(sysCon.StarSysData.CurrentOwner))
                {
                    SetupSystemUI(sysCon);
                    break;
                }
            }
        }
    }
    public void RemoveSystem(StarSysController sysController)
    {
        sysControllers.Remove(sysController);
        listOfUIgos.Remove(sysController.StarSysListUIGameObject);
    }

    public void SetupSystemUI(StarSysController sysController)
    {
        if (sysController.StarSysListUIGameObject != null)
        {

            if (!sysControllers.Contains(sysController) && !listOfUIgos.Contains(sysController.StarSysListUIGameObject))
            {
                sysController.StarSysListUIGameObject.SetActive(true);
                sysController.StarSysListUIGameObject.transform.SetParent(sysListContainer.transform, false);

                sysControllers.Add(sysController);// add to list for content (queue) folder systems
                listOfUIgos.Add(sysController.StarSysListUIGameObject);
                RectTransform[] minMapDotTransfor = sysController.StarSysListUIGameObject.GetComponentsInChildren<RectTransform>();
                for (int i = 0; i < minMapDotTransfor.Length; i++)
                {
                    if (minMapDotTransfor[i].name == "RedDot")
                    {
                        float x = sysController.StarSysData.GetPosition().x * 0.12f; // 0.12f is our cosmologic constant, fudge factor
                        float y = 0f;
                        float z = sysController.StarSysData.GetPosition().z * 0.12f;
                        minMapDotTransfor[i].Translate(new Vector3(x, z, y), Space.Self); // flip z and y from main galaxy map to UI mini map
                        break;
                    }
                }

                TextMeshProUGUI[] OneTMP = sysController.StarSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < OneTMP.Length; i++)
                {
                    int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
                    OneTMP[i].enabled = true;
                    var name = OneTMP[i].name.ToString();

                    switch (name)
                    {
                        case "SysName": // text in the button for sys activation
                            OneTMP[i].text = sysController.StarSysData.SysName;
                            break;
                        case "HeaderPowerUnitText":
                            //if (sysController.StarSysData.PowerPlants.Count > 0)  
                            OneTMP[i].text = sysController.StarSysData.PowerPlantData.Name;
                            //ToDo: can make it race specific here, not defaul "Plasma Reactor"
                            break;
                        case "NumPUnits":
                            OneTMP[i].text = (sysController.StarSysData.PowerPlants.Count).ToString();
                            break;
                        case "NumTotalEOut":
                            sysController.StarSysData.TotalSysPowerOutput = sysController.StarSysData.PowerPlants.Count * sysController.StarSysData.PowerPlantData.PowerOutput;
                            OneTMP[i].text = (sysController.StarSysData.TotalSysPowerOutput).ToString();                         
                            break;
                        // ToDo: use techLevelInt in power output 
                        //case "NumP Load": *** do this last to get total power load sum with UpdateSystemPowerLoad(sysController);

                        case "NameFactory":
                            OneTMP[i].text = sysController.StarSysData.FactoryData.Name;
                            break;
                        case "NumFactoryRatio":
                            int count = 0;
                            foreach (var item in sysController.StarSysData.Factories)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1") // 1 = on and 0 = off
                                    count++;
                            }
                            OneTMP[i].text = count.ToString() + "/" + (sysController.StarSysData.Factories.Count).ToString();
                            break;
                        case "FactoryLoad":
                            // for now all are turned on
                            OneTMP[i].text = (sysController.StarSysData.FactoryData.PowerLoad * sysController.StarSysData.Factories.Count).ToString();
                            // ToDo: work in tech levels
                            break;

                        case "ShipyardName":
                            OneTMP[i].text = sysController.StarSysData.ShipyardData.Name;
                            break;
                        case "NumYardsOnRatio":
                            int count1 = 0;
                            foreach (var item in sysController.StarSysData.Shipyards)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count1++;
                            }
                            OneTMP[i].text = count1.ToString() + "/" + (sysController.StarSysData.Shipyards.Count).ToString();
                            break;
                        case "YardLoad":
                            // for now all are turned on
                            OneTMP[i].text = (sysController.StarSysData.ShipyardData.PowerLoad * sysController.StarSysData.Shipyards.Count).ToString();
                            // ToDo: work in tech levels
                            break;
                        //ToDo: Yard's build Queue here
                        case "ShieldName":
                            OneTMP[i].text = sysController.StarSysData.ShieldGeneratorData.Name;
                            break;
                        case "NumShieldRatio":
                            int count2 = 0;
                            foreach (var item in sysController.StarSysData.ShieldGenerators)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count2++;
                            }
                            OneTMP[i].text = count2.ToString() + "/" + (sysController.StarSysData.ShieldGenerators.Count).ToString();
                            break;
                        case "ShieldLoad":
                            OneTMP[i].text = (sysController.StarSysData.ShieldGeneratorData.PowerLoad * sysController.StarSysData.ShieldGenerators.Count).ToString();
                            // ToDo: work in tech levels
                            break;
                        case "OBName":
                            OneTMP[i].text = sysController.StarSysData.OrbitalBatteryData.Name;
                            break;
                        case "NumOBRatio":
                            int count3 = 0;
                            foreach (var item in sysController.StarSysData.OrbitalBatteries)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count3++;
                            }
                            OneTMP[i].text = count3.ToString() + "/" + (sysController.StarSysData.OrbitalBatteries.Count).ToString();
                            break;
                        case "OBLoad":
                            OneTMP[i].text = (sysController.StarSysData.OrbitalBatteryData.PowerLoad * sysController.StarSysData.OrbitalBatteries.Count).ToString();
                            // ToDo: work in tech levels
                            break;
                        case "ResearchName":
                            OneTMP[i].text = sysController.StarSysData.ResearchCenterData.Name;
                            break;
                        case "NumResearchRatio":
                            int count4 = 0;
                            foreach (var item in sysController.StarSysData.ResearchCenters)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count4++;
                            }
                            OneTMP[i].text = count4.ToString() + "/" + (sysController.StarSysData.ResearchCenters.Count).ToString();
                            break;
                        case "ResearchLoad":
                            OneTMP[i].text = (sysController.StarSysData.ResearchCenterData.PowerLoad * sysController.StarSysData.ResearchCenters.Count).ToString();
                            // ToDo: work in tech levels
                            break;
                        case "PowerOverload":
                            OneTMP[i].gameObject.SetActive(false);
                            powerOverload = OneTMP[i].gameObject; //.SetActive(true);
                            break;
                        default:
                            break;

                    }
                    UpdateSystemPowerLoad(sysController);
                }
                Image[] listOfImages = sysController.StarSysListUIGameObject.GetComponentsInChildren<Image>();
                for (int i = 0; i < listOfImages.Length; i++)
                {
                   // int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.StartingTechLevel / 100; // Early Tech level = 100, Supreme = 900;
                    listOfImages[i].enabled = true;
                    var name = listOfImages[i].name.ToString();
                    switch (name)
                    {
                        case "PowerUnitImage":
                            listOfImages[i].sprite = ThemeManager.Instance.CurrentTheme.PowerPlantImage;
                  
                            break;
                        case "FactoryImage":
                            listOfImages[i].sprite = ThemeManager.Instance.CurrentTheme.FactoryImage;
                     
                            break;
                        case "shipyardImage":
                            listOfImages[i].sprite = ThemeManager.Instance.CurrentTheme.ShipyardImage;
             
                            break;
                        case "ShieldPlantImage":
                            listOfImages[i].sprite = ThemeManager.Instance.CurrentTheme.ShieldImage;
                 
                            break;
                        case "OrbitalBatteriesImage":
                            listOfImages[i].sprite = ThemeManager.Instance.CurrentTheme.OrbitalBatteriesImage;
                         
                            break;
                        case "ResearchImage":
                            listOfImages[i].sprite = ThemeManager.Instance.CurrentTheme.ResearchCenterImage;                          
                            break;
                        case "PowerOverload":
                            powerOverload = listOfImages[i].gameObject;
                            listOfImages[i].gameObject.SetActive(false);       
                            break;
                        default:
                            break;
                    }
                }

                Button[] listButtons = sysController.StarSysListUIGameObject.GetComponentsInChildren<Button>();
                foreach (var listButton in listButtons)
                {
                    switch (listButton.name)
                    {
                        case "BuildButton":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.BuildClick(sysController));
                            break;
                        case "ShipButton":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.ShipClick(sysController));
                            break;
                        case "FactoryButtonOn":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "FactoryButtonOff":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "YardButtonOn":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "YardButtonOff":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "ShieldButtonOn":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "ShieldButtonOff":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "OBButtonOn":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "OBButtonOff":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "ResearchButtonOn":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        case "ResearchButtonOff":
                            listButton.onClick.RemoveAllListeners();
                            listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                            break;
                        default:
                            break;
                    }
                }
            }
            sysController.StarSysListUIGameObject.SetActive(true);
            sysController.StarSysListUIGameObject.transform.SetParent(sysListContainer.transform, false);
        }
    }
    public void UpdateFacilityUI(StarSysController sysController, int plusMinus, string loadName, string ratioName, StarSysFacilities facilityType)
    {
        int newFacilityLoad = 0;
        List<GameObject> facilities = new List<GameObject>();
        switch (facilityType)
        {
            case StarSysFacilities.Factory:
                newFacilityLoad = sysController.StarSysData.FactoryData.PowerLoad;
                facilities = sysController.StarSysData.Factories;
                break;
            case StarSysFacilities.Shipyard:
                newFacilityLoad = sysController.StarSysData.ShipyardData.PowerLoad;
                facilities = sysController.StarSysData.Shipyards;
                break;
            case StarSysFacilities.ShieldGenerator:
                newFacilityLoad = sysController.StarSysData.ShieldGeneratorData.PowerLoad;
                facilities = sysController.StarSysData.ShieldGenerators;
                break;
            case StarSysFacilities.OrbitalBattery:
                newFacilityLoad = sysController.StarSysData.OrbitalBatteryData.PowerLoad;
                facilities = sysController.StarSysData.OrbitalBatteries;
                break;
            case StarSysFacilities.ResearchCenter:
                newFacilityLoad = sysController.StarSysData.ResearchCenterData.PowerLoad;
                facilities = sysController.StarSysData.ResearchCenters;
                break;
            default:
                break;
        }
        int numOn = 0;
        TextMeshProUGUI[] OneTMP = sysController.StarSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < OneTMP.Length; i++)
        {
            OneTMP[i].enabled = true;
            var itemName = OneTMP[i].name.ToString();
            bool bobbyHasABrainWorm = false;
            // ToDo: work in tech levels
            if (loadName == OneTMP[i].name)
            {
                int load = Int32.Parse(OneTMP[i].text);
                load += plusMinus * newFacilityLoad;
                OneTMP[i].text = load.ToString();
            }
            if (ratioName == OneTMP[i].name)
            {
                for (int j = 0; j < facilities.Count; j++)
                {
                    TextMeshProUGUI TheText = facilities[j].GetComponent<TextMeshProUGUI>();
                    if (TheText.text == "1")
                        numOn++;
                }
                OneTMP[i].text = numOn.ToString() + "/" + (facilities.Count).ToString();
                bobbyHasABrainWorm = true;
            }
            else if (bobbyHasABrainWorm)
            {
                break;
            }
        }
        UpdateSystemPowerLoad(sysController);
    }

    public void UpdateSystemPowerLoad(StarSysController sysCon)
    {
        int load = 0;
        for (int i = 0; i < sysCon.StarSysData.Factories.Count; i++)
        {
            if (sysCon.StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text == "1")
            {
                load += sysCon.StarSysData.FactoryData.PowerLoad;
            }
        }
        for (int i = 0; i < sysCon.StarSysData.Shipyards.Count; i++)
        {
            if (sysCon.StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text == "1")
            {
                load += sysCon.StarSysData.ShipyardData.PowerLoad;
            }
        }
        for (int i = 0; i < sysCon.StarSysData.ShieldGenerators.Count; i++)
        {
            if (sysCon.StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text == "1")
            {
                load += sysCon.StarSysData.ShieldGeneratorData.PowerLoad;
            }
        }
        for (int i = 0; i < sysCon.StarSysData.OrbitalBatteries.Count; i++)
        {
            if (sysCon.StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text == "1")
            {
                load += sysCon.StarSysData.OrbitalBatteryData.PowerLoad;
            }
        }
        for (int i = 0; i < sysCon.StarSysData.ResearchCenters.Count; i++)
        {
            if (sysCon.StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text == "1")
            {
                load += sysCon.StarSysData.ResearchCenterData.PowerLoad;
            }
        }
        sysCon.StarSysData.TotalSysPowerLoad = load;
        TextMeshProUGUI[] OneTMP = sysCon.StarSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < OneTMP.Length; i++)
        {
            int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
            OneTMP[i].enabled = true;
           // if ()
            if ("NumP Load" == OneTMP[i].name)
            {
                OneTMP[i].text = load.ToString();
            }
        }
    }
    public void FlashPowerOverload()
    {
        StartCoroutine(FlashRoutine());
    }
    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            powerOverload.SetActive(true); // Show image
            yield return new WaitForSeconds(0.5f);
            powerOverload.SetActive(false); // Hide image
            yield return new WaitForSeconds(0.5f);
        }
    }
}


