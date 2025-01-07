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

public class GalaxyMenuUIController : MonoBehaviour
{
    public static GalaxyMenuUIController Instance { get; private set; }
    [SerializeField]
    private GameObject buildListUI;
    [SerializeField]
    private GameObject sysMenuView;
    [SerializeField] 
    private GameObject fleetsMenuView;
    [SerializeField] 
    private GameObject diplomacyMenuView;
    [SerializeField] 
    private GameObject intelMenuView;
    [SerializeField] 
    private GameObject encyclopediaMenuView;
    [SerializeField]

    private GameObject closeOpenMenuButton;
    [SerializeField]
    private GameObject SysBackground;
    [SerializeField]
    private GameObject FleetBackground;
    [SerializeField]
    private GameObject DiplomacyBackground;
    [SerializeField]
    private GameObject IntelBackground;
    [SerializeField]
    private GameObject EncyclopediaBackground;
    [SerializeField]
    private List<StarSysController> sysControllers;

    //[SerializeField]
    //private List<string>  sysNames;
    //[SerializeField]
    //private GameObject sysUIprefab;
    //[SerializeField]
    //private int powerPerEnergyPlant = 10;

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
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false); 
        intelMenuView.SetActive(false) ;
        encyclopediaMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(false);
        SysBackground.SetActive(false);
        FleetBackground.SetActive(false);
        DiplomacyBackground.SetActive(false);
        IntelBackground.SetActive(false);
        EncyclopediaBackground.SetActive(false);
        buildListUI.SetActive(false);
    }

    public void CloseTheOpenGalaxyUI()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
       // settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(false);
        SysBackground.SetActive(false);
        FleetBackground.SetActive(false);
        DiplomacyBackground.SetActive(false);
        IntelBackground.SetActive(false);
        EncyclopediaBackground.SetActive(false);
      //  SettingBackground.SetActive(false);

    }
    public void OpenSystems()
    {
        if (!sysMenuView.activeSelf)
        { 
            sysMenuView.SetActive(true);
            SysBackground.SetActive(true);
            fleetsMenuView.SetActive(false);
            diplomacyMenuView.SetActive(false);
            intelMenuView.SetActive(false);
            encyclopediaMenuView.SetActive(false);
            // settingsMenuView.SetActive(false);
            closeOpenMenuButton.SetActive(true);
            FleetBackground.SetActive(false);
            DiplomacyBackground.SetActive(false);
            IntelBackground.SetActive(false);
            EncyclopediaBackground.SetActive(false);
            SetUISystemsData();
            FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            FleetUIController.Instance.CloseUnLoadFleetUI();
        }
        else
        {
            sysMenuView.SetActive(false);
            SysBackground.SetActive(false);

        }
    }

    public void OpenFleets()
    {
        if (!fleetsMenuView.activeSelf)
        {
            sysMenuView.SetActive(false);
            fleetsMenuView.SetActive(true);
            FleetBackground.SetActive(true);
            diplomacyMenuView.SetActive(false);
            intelMenuView.SetActive(false);
            encyclopediaMenuView.SetActive(false);
            //  settingsMenuView.SetActive(false);
            closeOpenMenuButton.SetActive(true);
            SysBackground.SetActive(false);
            DiplomacyBackground.SetActive(false);
            IntelBackground.SetActive(false);
            EncyclopediaBackground.SetActive(false);
            //  SettingBackground.SetActive(false);
            FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            FleetUIController.Instance.CloseUnLoadFleetUI();
        }
        else
        {
            fleetsMenuView.SetActive(false);
            FleetBackground.SetActive(false);
        }
    }
    public void OpenDiplomacy()
    {
        if (!diplomacyMenuView.activeSelf)
        {
            sysMenuView.SetActive(false);
            fleetsMenuView.SetActive(false);
            diplomacyMenuView.SetActive(true);
            DiplomacyBackground.SetActive(true);
            intelMenuView.SetActive(false);
            encyclopediaMenuView.SetActive(false);
            // settingsMenuView.SetActive(false);
            closeOpenMenuButton.SetActive(true);
            SysBackground.SetActive(false);
            FleetBackground.SetActive(false);
            IntelBackground.SetActive(false);
            EncyclopediaBackground.SetActive(false);
            // SettingBackground.SetActive(false);
            FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            FleetUIController.Instance.CloseUnLoadFleetUI();
        } 
        else 
        {
            diplomacyMenuView.SetActive(false);
            DiplomacyBackground.SetActive(false);
        }
    }

    public void OpenIntel()
    {
        if (!intelMenuView.activeSelf)
        {
            sysMenuView.SetActive(false);
            fleetsMenuView.SetActive(false);
            diplomacyMenuView.SetActive(false);
            intelMenuView.SetActive(true);
            IntelBackground.SetActive(true);
            encyclopediaMenuView.SetActive(false);
            // settingsMenuView.SetActive(false);
            closeOpenMenuButton.SetActive(true);
            SysBackground.SetActive(false);
            FleetBackground.SetActive(false);
            DiplomacyBackground.SetActive(false);
            EncyclopediaBackground.SetActive(false);
            // SettingBackground.SetActive(false);
            FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            FleetUIController.Instance.CloseUnLoadFleetUI();
        }
        else 
        {
            intelMenuView.SetActive(false);
            IntelBackground.SetActive(false);
        }

    }

    public void OpenEncyclopedia()
    {
        if (!encyclopediaMenuView.activeSelf)
        {
            sysMenuView.SetActive(false);
            fleetsMenuView.SetActive(false);
            diplomacyMenuView.SetActive(false);
            intelMenuView.SetActive(false);
            encyclopediaMenuView.SetActive(true);
            EncyclopediaBackground.SetActive(true);

            closeOpenMenuButton.SetActive(true);
            SysBackground.SetActive(false);
            FleetBackground.SetActive(false);
            DiplomacyBackground.SetActive(false);
            IntelBackground.SetActive(false);

            FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            FleetUIController.Instance.CloseUnLoadFleetUI();
        }
        else 
        {
            encyclopediaMenuView.SetActive(false);
            EncyclopediaBackground.SetActive(false);
        }
    }
    private void SetUISystemsData()
    {
        if (StarSysManager.Instance.ManagersStarSysControllerList.Count > 0)
        {
            foreach (var sysCon in StarSysManager.Instance.ManagersStarSysControllerList)
            {
                if (sysCon.StarSysUIGameObject != null && GameController.Instance.AreWeLocalPlayer(sysCon.StarSysData.CurrentOwner))
                    SetupSystemUI(sysCon);
            }
        }
    }
    public void RemoveSystem(StarSysController sysController)
    {
        sysControllers.Remove(sysController);
    }

    public void SetupSystemUI(StarSysController sysController)
    {
        if (sysController.StarSysUIGameObject != null)
        {
            sysControllers.Add(sysController);// add to list of content folder items
            RectTransform[] minMapDotTransfor = sysController.StarSysUIGameObject.GetComponentsInChildren<RectTransform>();
            for (int i = 0; i < minMapDotTransfor.Length; i++)
            {
                if (minMapDotTransfor[i].name == "RedDot")
                {
                    float x = sysController.StarSysData.GetPosition().x * 14/600;
                    float y = 0f;
                    float z = sysController.StarSysData.GetPosition().z * 2/90; 
                    minMapDotTransfor[i].Translate(new Vector3(x,z,y), Space.Self); // flip z and y from main galaxy map to UI mini map
                    break;
                }
            }

            TextMeshProUGUI[] OneTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < OneTMP.Length; i++) 
            {
                int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
                OneTMP[i].enabled = true;
                var name = OneTMP[i].name.ToString();

                switch (name)
                {
                    case "SysName":
                        OneTMP[i].text = sysController.StarSysData.SysName;
                        break;
                    case "HeaderPowerUnitText":
                        //if (sysController.StarSysData.PowerStations.Count > 0)  
                        OneTMP[i].text = sysController.StarSysData.PowerPlantData.Name;
                        //ToDo: can make it race specific here, not defaul "Plasma Reactor"
                        break;
                    case "NumPUnits":
                        OneTMP[i].text = (sysController.StarSysData.PowerStations.Count).ToString();
                        break;
                    case "NumTotalEOut":
                        OneTMP[i].text = (sysController.StarSysData.PowerStations.Count * sysController.StarSysData.PowerPlantData.PowerOutput).ToString();
                        break;
                    // ToDo: use techLevelInt in power output 
                    case "NumP Load":
                        OneTMP[i].text = (sysController.StarSysData.TotalSysPowerLoad.ToString());
                        break;

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

                    // ToDo: Factory build Queue here?

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
                    default:
                        break;
                }
            }

            Button[] listButtons = sysController.StarSysUIGameObject.GetComponentsInChildren<Button>();
            //for (int k = 0; k < listButtons.Length; k++) 
            foreach (var listButton in listButtons)
            {
                switch (listButton.name)
                {
                    case "BuildButton":
                        //listButton.onClick.AddListener(() => buildListUI.SetActive(true));
                        //listButton.onClick.AddListener(() => sysController.BuildClick());
                        break;
                    case "FactoryButtonOn":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "FactoryButtonOff":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "YardButtonOn":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "YardButtonOff":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "ShieldButtonOn":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "ShieldButtonOff":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "OBButtonOn":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "OBButtonOff":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "ResearchButtonOn":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    case "ResearchButtonOff":
                        listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void UpdateFactories(StarSysController sysController, int plusMinus)
    {
        for (int j = 0; j < sysControllers.Count; j++)
        { 
            if (sysController == sysControllers[j])
            {
                TextMeshProUGUI[] OneTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < OneTMP.Length; i++) 
                {
                    OneTMP[i].enabled = true;
                    var itemName = OneTMP[i].name.ToString();
                    int count = 0;
                    switch (itemName)
                    {

                        case "NumFactoryRatio":
                            foreach (var item in sysController.StarSysData.Factories)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1") // 1 = on and 0 = off
                                    count++;
                            }
                            OneTMP[i].text = count.ToString() + "/" + (sysController.StarSysData.Factories.Count).ToString();
                            break;
                        case "FactoryLoad":
                            int load = Int32.Parse(OneTMP[i].text);
                            load += plusMinus * sysController.StarSysData.FactoryData.PowerLoad;
                            
                            OneTMP[i].text = load.ToString();
                            UpdateSystemPowerLoad(sysController);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateYards(StarSysController sysController, int plusMinus)
    {
        for (int j = 0; j < sysControllers.Count; j++)
        {
            if (sysController == sysControllers[j])
            {
                TextMeshProUGUI[] OneTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < OneTMP.Length; i++)
                //foreach (var OneTMP[i] in listTMP)
                {
                    OneTMP[i].enabled = true;
                    var itemName = OneTMP[i].name.ToString();
                    int count1 = 0;
                    switch (itemName)
                    {
                        case "NumYardsOnRatio":

                            foreach (var item in sysController.StarSysData.Shipyards)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count1++;
                            }
                            OneTMP[i].text = count1.ToString() + "/" + (sysController.StarSysData.Shipyards.Count).ToString();
                            break;
                        case "YardLoad":
                            int load = Int32.Parse(OneTMP[i].text);
 
                            load += plusMinus * sysController.StarSysData.ShipyardData.PowerLoad;
                            
                            OneTMP[i].text = load.ToString();
                            UpdateSystemPowerLoad(sysController);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateShields(StarSysController sysController, int plusMinus)
    {
        for (int j = 0; j < sysControllers.Count; j++)
        {
            if (sysController == sysControllers[j])
            {
                TextMeshProUGUI[] OneTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < OneTMP.Length; i++) 
                //foreach (var OneTmp in listTMP)
                {
                    OneTMP[i].enabled = true;
                    var itemName = OneTMP[i].name.ToString();
                    int count2 = 0;
                    switch (itemName)
                    {
                        case "NumShieldRatio":
                            foreach (var item in sysController.StarSysData.ShieldGenerators)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count2++;
                            }
                            OneTMP[i].text = count2.ToString() + "/" + (sysController.StarSysData.ShieldGenerators.Count).ToString();
                            break;
                        case "ShieldLoad":
                            int load = Int32.Parse(OneTMP[i].text);

                            load += plusMinus * sysController.StarSysData.ShieldGeneratorData.PowerLoad;

                            OneTMP[i].text = load.ToString();
                            UpdateSystemPowerLoad(sysController);
                            break;
                            // ToDo: work in tech levels
                     
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateOBs(StarSysController sysController, int plusMinus)
    {
        for (int j = 0; j < sysControllers.Count; j++)
        {
            if (sysController == sysControllers[j])
            {
                TextMeshProUGUI[] OneTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < OneTMP.Length; i++)
                //foreach (var OneTmp in listTMP)
                {
                    OneTMP[i].enabled = true;
                    var itemName = OneTMP[i].name.ToString();
                    int count3 = 0;
                    switch (itemName)
                    {
                        case "NumOBRatio":
                            foreach (var item in sysController.StarSysData.OrbitalBatteries)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count3++;
                            }
                            OneTMP[i].text = count3.ToString() + "/" + (sysController.StarSysData.OrbitalBatteries.Count).ToString();
                            break;
                        case "OBLoad":
                            int load = Int32.Parse(OneTMP[i].text);

                            load += plusMinus * sysController.StarSysData.OrbitalBatteryData.PowerLoad;

                            OneTMP[i].text = load.ToString();
                            UpdateSystemPowerLoad(sysController);
                            break;
                            // ToDo: work in tech levels
                            
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateResearchCenters(StarSysController sysController, int plusMinus)
    {
        for (int j = 0; j < sysControllers.Count; j++)
        {
            if (sysController == sysControllers[j])
            {
                TextMeshProUGUI[] OneTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < OneTMP.Length; i++)
               // foreach (var OneTmp in listTMP)
                {
                    OneTMP[i].enabled = true;
                    var itemName = OneTMP[i].name.ToString();
                    int count4 = 0;
                    switch (itemName)
                    {
                        case "NumResearchRatio":
                            foreach (var item in sysController.StarSysData.ResearchCenters)
                            {
                                TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                if (TheText.text == "1")
                                    count4++;
                            }
                            OneTMP[i].text = count4.ToString() + "/" + (sysController.StarSysData.ResearchCenters.Count).ToString();
                            break;
                        case "ResearchLoad":
                            int load = Int32.Parse(OneTMP[i].text);

                            load += plusMinus * sysController.StarSysData.ResearchCenterData.PowerLoad;

                            OneTMP[i].text = load.ToString();
                            UpdateSystemPowerLoad(sysController);
                            break;
                            // ToDo: work in tech levels
                            
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void UpdateSystemPowerLoad(StarSysController sysCon)
    {
        TextMeshProUGUI[] OneTMP = sysCon.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < OneTMP.Length; i++) 
       // foreach (var OneTmp in listTMP)
        {
            int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
            OneTMP[i].enabled = true;
            if ("NumP Load" == OneTMP[i].name)
            {
                OneTMP[i].text = sysCon.StarSysData.TotalSysPowerLoad.ToString();
            }
        }
    }
}


