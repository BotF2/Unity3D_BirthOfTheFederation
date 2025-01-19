using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Core;
using System;

public class ASysUIController : MonoBehaviour
{
    public static ASysUIController Instance;
    [SerializeField]
    private GameObject sysMenuPanel;
    [SerializeField]
    private GameObject sysUIPrefab;
    [SerializeField]   
    private StarSysController currentSysCon;

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
        sysMenuPanel.SetActive(false);
    }
    public void CloseButton()
    {
        MenuManager.Instance.OpenMenu(Menu.None, sysMenuPanel);
    }
    //private void SetUISystemsData()
    //{
    //    if (StarSysManager.Instance.ManagersStarSysControllerList.Count > 0)
    //    {
    //        foreach (var sysCon in StarSysManager.Instance.ManagersStarSysControllerList)
    //        {
    //            if (sysCon.StarSysGalaxyUIGameObject != null && GameController.Instance.AreWeLocalPlayer(sysCon.StarSysData.CurrentOwner))
    //                SetupSystemUI(sysCon);
    //        }
    //    }
    //}
    public void SetupSystemUI(StarSysController sysController)
    {
        sysMenuPanel.SetActive(true);
        if (sysController.StarSysGalaxyUIGameObject != null && GameController.Instance.AreWeLocalPlayer(sysController.StarSysData.CurrentOwner)) // && currentSysCon != sysController)
        {
            sysController.StarSysGalaxyUIGameObject.SetActive(true);
            sysController.StarSysGalaxyUIGameObject.transform.SetParent(sysMenuPanel.transform, false);

            if (currentSysCon != sysController)
                   // currentSysCon.StarSysGalaxyUIGameObject.SetActive(false);
                currentSysCon = sysController;

                RectTransform[] minMapDotTransfor = sysController.StarSysGalaxyUIGameObject.GetComponentsInChildren<RectTransform>();
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

                TextMeshProUGUI[] OneTMP = sysController.StarSysGalaxyUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
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

                Button[] listButtons = sysController.StarSysGalaxyUIGameObject.GetComponentsInChildren<Button>();
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
    }
}
