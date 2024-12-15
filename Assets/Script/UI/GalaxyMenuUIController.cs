using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Core;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

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
    //[SerializeField]
    //private List<StarSysController> sysControllers;
    [SerializeField]
    private GameObject contentFolderParent;
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
                if (sysCon.StarSysUIController != null)
                    UpdateSystemUI(sysCon);
                else
                    StarSysManager.Instance.NewSystemUI(sysCon);

            }
        }
    }

    public void UpdateSystemUI(StarSysController sysController)
    {
        if (sysController.StarSysUIController != null)
        {
            TextMeshProUGUI[] listTMP = sysController.StarSysUIController.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (var OneTmp in listTMP)
            {
                int techLevelInt = (int)CivManager.Instance.LocalPlayerCivContoller.CivData.TechLevel / 100; // Early Tech level = 100, Supreme = 900;
                OneTmp.enabled = true;
                switch (OneTmp.name)
                {
                    case "Sys Name Text (TMP)":
                        OneTmp.text = sysController.StarSysData.SysName;
                        break;

                    case "HeaderPowerUnitText (TMP)":
                        if (sysController.StarSysData.PowerStations.Count > 0)  
                            OneTmp.text = sysController.StarSysData.PlantData.Name;
                    //ToDo: can make it race specific here, not defaul "Plasma Reactor"
                        break;
                    case "NumTotalUnits (TMP)":
                        OneTmp.text = (sysController.StarSysData.PowerStations.Count).ToString();
                        break;
                    case "NumTotalEOut (TMP)":
                        OneTmp.text = (sysController.StarSysData.PowerStations.Count * sysController.PowerPerPlant).ToString() + "u";
                        break;
                    // ToDo: use techLevelInt in power output 

                    //case "HeaderFactoryText (TMP)":
                    //  ToDo: can make it race specific here, not defaul "Replication Plants"
                    //    break;
                    //case "NumPlantsRatioText (TMP)":
                    //    // ToDo: This can be the number active over the total number of plants
                    //    OneTmp.text = (sysController.StarSysData.Factories.Count).ToString();
                    //    break;
                    case "Energy Into Factories Text (TMP)":
                        OneTmp.text = (sysController.StarSysData.PowerStations.Count).ToString() + "u";
                        // ToDo: work in tech levels
                        break;

                    // ToDo: Factory build Queue here?

                    //case "Shipyard Name Text (TMP)":
                    //    // default shipyard for everyone
                    //    break;
                    //case "NumYardsOnRatio (TMP)":
                    //    // ToDo: This can be the number active over the total number of yards
                    //    OneTmp.text = (sysController.StarSysData.Shipyards.Count).ToString();
                    //    break;
                    //case "Energy Into Yards Text (TMP)":
                    //    OneTmp.text = (sysController.StarSysData.Shipyards.Count).ToString() + "u";
                    //    // ToDo: work in tech levels
                    //    break;
                    // ToDo: Yard's build Queue here
                    //case "NumShieldsRatioText (TMP)":
                    //    // ToDo: This can be the number active over the total number of yards
                    //    OneTmp.text = (sysController.StarSysData.ShieldGenerators.Count).ToString();
                    //    break;
                    //case "Energy Into Shields Text (TMP)":
                    //    OneTmp.text = (sysController.StarSysData.ShieldGenerators.Count).ToString() + "u";
                    //    // ToDo: work in tech levels
                    //    break;
                    //case "NumOBRatioText (TMP)":
                    //    // ToDo: This can be the number active over the total number of OB
                    //    OneTmp.text = (sysController.StarSysData.OrbitalBatteries.Count).ToString();
                    //    break;
                    //case "Energy Into OB Text (TMP)":
                    //    OneTmp.text = (sysController.StarSysData.OrbitalBatteries.Count).ToString() + "u";
                    //    // ToDo: work in tech levels
                    //    break;
                    //case "HeaderResearchText (TMP)":
                    //    // ToDo: make it race specific
                    //    break;
                    //case "NumResearchRatioText (TMP)":
                    //    // ToDo: This can be the number active over the total number of centers
                    //    OneTmp.text = (sysController.StarSysData.ResearchCenters.Count).ToString();
                    //    break;
                    //case "Energy Into Research Text (TMP)":
                    //    OneTmp.text = (sysController.StarSysData.ResearchCenters.Count).ToString() + "u";
                    //    // ToDo: work in tech levels
                    //    break;
                    default:
                        break;
                }
                Button[] listButton = sysController.StarSysUIController.GetComponentsInChildren<Button>();
                foreach (var OneButt in listButton)
                {
                    switch (OneButt.name)
                    {
                        case "BuildButton":
                            OneButt.onClick.AddListener(() => buildListUI.SetActive(true));
                            OneButt.onClick.AddListener(() => sysController.BuildClick());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

