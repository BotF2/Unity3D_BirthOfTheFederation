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
            sysControllers.Add(sysController);// in list of content folder items
            TextMeshProUGUI[] listTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (var OneTmp in listTMP)
            {
                int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
                OneTmp.enabled = true;
                var name = OneTmp.name.ToString();

                switch (name)
                {
                    case "SysName":
                        OneTmp.text = sysController.StarSysData.SysName;
                        break;
                    case "HeaderPowerUnitText":
                        //if (sysController.StarSysData.PowerStations.Count > 0)  
                        OneTmp.text = sysController.StarSysData.PowerPlantData.Name;
                        //ToDo: can make it race specific here, not defaul "Plasma Reactor"
                        break;
                    case "NumPUnits":
                        OneTmp.text = (sysController.StarSysData.PowerStations.Count).ToString();
                        break;
                    case "NumTotalEOut":
                        OneTmp.text = (sysController.StarSysData.PowerStations.Count * sysController.StarSysData.PowerPlantData.PowerOutput).ToString();
                        break;
                    // ToDo: use techLevelInt in power output 
                    case "NumP Load":
                        OneTmp.text = (sysController.StarSysData.TotalSysPowerLoad.ToString());
                        break;

                    case "NameFactory":
                        OneTmp.text = sysController.StarSysData.FactoryData.Name;
                        break;
                    case "NumFactoryRatio":
                        int count = 0;
                        foreach (var item in sysController.StarSysData.Factories)
                        {
                            TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                            if (TheText.text == "1") // 1 = on and 0 = off
                                count++;
                        }
                        OneTmp.text = count.ToString() + "/" + (sysController.StarSysData.Factories.Count).ToString();
                        break;
                    case "FactoryLoad":
                        // for now all are turned on
                        OneTmp.text = (sysController.StarSysData.FactoryData.PowerLoad * sysController.StarSysData.Factories.Count).ToString();
                        // ToDo: work in tech levels
                        break;

                    // ToDo: Factory build Queue here?

                    case "ShipyardName":
                        OneTmp.text = sysController.StarSysData.ShipyardData.Name;
                        break;
                    case "NumYardsOnRatio":
                        int count1 = 0;
                        foreach (var item in sysController.StarSysData.Shipyards)
                        {
                            TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                            if (TheText.text == "1")
                                count1++;
                        }
                        OneTmp.text = count1.ToString() + "/" + (sysController.StarSysData.Shipyards.Count).ToString();
                        break;
                    case "YardLoad":
                        // for now all are turned on
                        OneTmp.text = (sysController.StarSysData.ShipyardData.PowerLoad * sysController.StarSysData.Shipyards.Count).ToString();
                        // ToDo: work in tech levels
                        break;
                    //ToDo: Yard's build Queue here
                    case "ShieldName":
                        OneTmp.text = sysController.StarSysData.ShieldGeneratorData.Name;
                        break;
                    case "NumShieldRatio":
                        int count2 = 0;
                        foreach (var item in sysController.StarSysData.ShieldGenerators)
                        {
                            TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                            if (TheText.text == "1")
                                count2++;
                        }
                        OneTmp.text = count2.ToString() + "/" + (sysController.StarSysData.ShieldGenerators.Count).ToString();
                        break;
                    case "ShieldLoad":
                        OneTmp.text = (sysController.StarSysData.ShieldGeneratorData.PowerLoad * sysController.StarSysData.ShieldGenerators.Count).ToString();
                        // ToDo: work in tech levels
                        break;
                    case "OBName":
                        OneTmp.text = sysController.StarSysData.OrbitalBatteryData.Name;
                        break;
                    case "NumOBRatio":
                        int count3 = 0;
                        foreach (var item in sysController.StarSysData.OrbitalBatteries)
                        {
                            TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                            if (TheText.text == "1")
                                count3++;
                        }
                        OneTmp.text = count3.ToString() + "/" + (sysController.StarSysData.OrbitalBatteries.Count).ToString();
                        break;
                    case "OBLoad":
                        OneTmp.text = (sysController.StarSysData.OrbitalBatteryData.PowerLoad * sysController.StarSysData.OrbitalBatteries.Count).ToString();
                        // ToDo: work in tech levels
                        break;
                    case "ResearchName":
                        OneTmp.text = sysController.StarSysData.ResearchCenterData.Name;
                        break;
                    case "NumResearchRatio":
                        int count4 = 0;
                        foreach (var item in sysController.StarSysData.ResearchCenters)
                        {
                            TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                            if (TheText.text == "1")
                                count4++;
                        }
                        OneTmp.text = count4.ToString() + "/" + (sysController.StarSysData.ResearchCenters.Count).ToString();
                        break;
                    case "ResearchLoad":
                        OneTmp.text = (sysController.StarSysData.ResearchCenterData.PowerLoad * sysController.StarSysData.ResearchCenters.Count).ToString();
                        // ToDo: work in tech levels
                        break;
                    default:
                        break;
                }
            }

            Button[] listButton = sysController.StarSysUIGameObject.GetComponentsInChildren<Button>();
            foreach (var OneButt in listButton)
            {
                switch (OneButt.name)
                {
                    case "BuildButton":
                        //OneButt.onClick.AddListener(() => buildListUI.SetActive(true));
                        //OneButt.onClick.AddListener(() => sysController.BuildClick());
                        break;
                    case "FactoryButtonOn":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "FactoryButtonOff":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "YardButtonOn":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "YardButtonOff":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "ShieldButtonOn":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "ShieldButtonOff":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "OBButtonOn":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "OBButtonOff":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "ResearchButtonOn":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    case "ResearchButtonOff":
                        OneButt.onClick.AddListener(() => sysController.FacilityOnClick(sysController, OneButt.name));
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void UpdateFactories(StarSysController sysController, int plusMinus)
    {
        foreach (var SysCon in sysControllers)
        {
            if (sysController == SysCon)
            {
                TextMeshProUGUI[] listTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (var OneTmp in listTMP)
                {
                    OneTmp.enabled = true;
                    var itemName = OneTmp.name.ToString();
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
                            OneTmp.text = count.ToString() + "/" + (sysController.StarSysData.Factories.Count).ToString();
                            break;
                        case "FactoryLoad":
                            int deltaTotalLoad = 0;
                            int load = Int32.Parse(OneTmp.text);
                            if (plusMinus == -1) {
                                load -= sysController.StarSysData.FactoryData.PowerLoad;
                                deltaTotalLoad = -(sysController.StarSysData.FactoryData.PowerLoad);
                            }
                            else {
                                load += sysController.StarSysData.FactoryData.PowerLoad;
                                deltaTotalLoad = sysController.StarSysData.FactoryData.PowerLoad;
                            }
                            OneTmp.text = load.ToString();
                            UpdateSystemPowerLoad(sysController, deltaTotalLoad);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateYards(StarSysController sysController)
    {
        foreach (var SysCon in sysControllers)
        {
            if (sysController == SysCon)
            {
                TextMeshProUGUI[] listTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (var OneTmp in listTMP)
                {
                    OneTmp.enabled = true;
                    var itemName = OneTmp.name.ToString();
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
                            OneTmp.text = count1.ToString() + "/" + (sysController.StarSysData.Shipyards.Count).ToString();
                            break;
                        case "YardLoad":
                            // for now all are turned on
                            OneTmp.text = (sysController.StarSysData.ShipyardData.PowerLoad * count1).ToString();
                            // ToDo: work in tech levels
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateShields(StarSysController sysController)
    {
        foreach (var SysCon in sysControllers)
        {
            if (sysController == SysCon)
            {
                TextMeshProUGUI[] listTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (var OneTmp in listTMP)
                {
                    OneTmp.enabled = true;
                    var itemName = OneTmp.name.ToString();
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
                            OneTmp.text = count2.ToString() + "/" + (sysController.StarSysData.ShieldGenerators.Count).ToString();
                            break;
                        case "ShieldLoad":
                            OneTmp.text = (sysController.StarSysData.ShieldGeneratorData.PowerLoad * count2).ToString();
                            // ToDo: work in tech levels
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateOBs(StarSysController sysController)
    {
        foreach (var SysCon in sysControllers)
        {
            if (sysController == SysCon)
            {
                TextMeshProUGUI[] listTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (var OneTmp in listTMP)
                {
                    OneTmp.enabled = true;
                    var itemName = OneTmp.name.ToString();
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
                            OneTmp.text = count3.ToString() + "/" + (sysController.StarSysData.OrbitalBatteries.Count).ToString();
                            break;
                        case "OBLoad":
                            OneTmp.text = (sysController.StarSysData.OrbitalBatteryData.PowerLoad * count3).ToString();
                            // ToDo: work in tech levels
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public void UpdateResearchCenters(StarSysController sysController)
    {
        foreach (var SysCon in sysControllers)
        {
            if (sysController == SysCon)
            {
                TextMeshProUGUI[] listTMP = sysController.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (var OneTmp in listTMP)
                {
                    OneTmp.enabled = true;
                    var itemName = OneTmp.name.ToString();
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
                            OneTmp.text = count4.ToString() + "/" + (sysController.StarSysData.ResearchCenters.Count).ToString();
                            break;
                        case "ResearchLoad":
                            OneTmp.text = (sysController.StarSysData.ResearchCenterData.PowerLoad * count4).ToString();
                            // ToDo: work in tech levels
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    //public void UpdateSystemPowerLoad(StarSysController sysCon, int facilityDeltaLoad)
    //{
    //    if (sysControllers.Contains(sysCon))
    //    {
    //        foreach (var item in sysControllers)
    //        {
    //            if (sysCon == item)
    //            {
    //                sysCon.StarSysData.TotalSysPowerLoad += facilityDeltaLoad;
    //            }
    //        }

    //    }
    //}
    private void UpdateSystemPowerLoad(StarSysController sysCon, int delta)
    {
        TextMeshProUGUI[] listTMP = sysCon.StarSysUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var OneTmp in listTMP)
        {
            int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
            OneTmp.enabled = true;
            if ("NumP Load" == OneTmp.name)
            {
                sysCon.StarSysData.TotalSysPowerLoad += delta;
                OneTmp.text = sysCon.StarSysData.TotalSysPowerLoad.ToString();
            }
        }
    }
    private bool IntParse()
    {
        throw new NotImplementedException();
    }

}


