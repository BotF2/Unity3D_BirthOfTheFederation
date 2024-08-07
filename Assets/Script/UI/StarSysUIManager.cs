using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Core;
using Unity.VisualScripting;
using System.Diagnostics;
using UnityEngine.Rendering;
using System.Linq;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class StarSysUIManager : MonoBehaviour
{
    public static StarSysUIManager Instance;
    public StarSysController controller; // system we clicked
    public Canvas parentCanvas;
    [SerializeField]
    private GameObject starSysUIRoot;
    [SerializeField]
    private GameObject starSysPanelPrefab;
    [SerializeField]
    private GameObject starSysListGO;
    public List<StarSysController> sysControllerList;

    //[SerializeField]
    //private List<ShipData> shipList;
    private bool deltaShipList = false;

    [SerializeField]
    public TMP_Text CivName;
    private string lastCivUser;
    [SerializeField]
    private TMP_Text tPopulation;
    [SerializeField]
    private TMP_Text tForms;
    [SerializeField]
    private TMP_Text tPower;
    [SerializeField]
    private TMP_Text tStations;
    [SerializeField]
    private TMP_Text tProduction;
    [SerializeField]
    private TMP_Text tFactories;
    [SerializeField]
    private TMP_Text tTech;
    [SerializeField]
    private TMP_Text tResearch;

    private Camera galaxyEventCamera;


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
        starSysUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        //parentCanvas.worldCamera = galaxyEventCamera; not working
        //destinationDropdown.value = 0;
    }

    public void LoadStarSysUI(GameObject go) 
    {
        FleetUIManager.Instance.UnLoadFleetUI();
        FleetSelectionUI.Instance.UnLoadShipManagerUI();
        starSysUIRoot.SetActive(true);

        controller = go.GetComponent<StarSysController>();
        var civEnum = controller.StarSysData.CurrentOwner;
        var civData = CivManager.Instance.GetCivDataByCivEnum(civEnum);

        CivName.text = civData.CivLongName;
        LoadSystemsList(civData.CivEnum);
    }
    private void LoadSystemsList(CivEnum civEnum)
    {
        sysControllerList.Clear(); 
        foreach (var sysController in StarSysManager.Instance.StarSysControllerList)
        {
            if (sysController.StarSysData.CurrentOwner == civEnum)
            {
                sysControllerList.Add(sysController);
            }
        }
        if (lastCivUser != CivName.text)
        {
            for (int i = 0; i < starSysListGO.transform.childCount; i++)
            {
                UnityEngine.Object.Destroy(starSysListGO.transform.GetChild(i).gameObject); 
            }
        }
        List<string> nameOfSys = new List<string>();
        foreach(var tmp in starSysListGO.GetComponentsInChildren<TextMeshProUGUI>())
        {
            nameOfSys.Add(tmp.text);
        }
        int t_Pop = 0;
        int t_Farms = 0;
        int t_Power = 0;
        int t_Stations = 0;
        int t_Production = 0;
        int t_Factories = 0;
        int t_Tech = 0;
        int t_Research = 0;
        
        for (var i = 0; i < sysControllerList.Count; i++)
        {
            if (!nameOfSys.Contains(sysControllerList[i].StarSysData.SysName))
            { 
                var sysController = sysControllerList[i];
                GameObject starSysPanel = (GameObject)Instantiate(starSysPanelPrefab, starSysListGO.transform);
                starSysPanel.transform.SetParent(starSysListGO.transform, true);
                var sysTMPs = starSysPanel.GetComponentsInChildren<TextMeshProUGUI>();
                sysTMPs[0].text = sysController.StarSysData.SysName;
                sysTMPs[1].text = sysController.StarSysData.Population.ToString();
                sysTMPs[2].text = sysController.StarSysData.Farms.ToString();
                sysTMPs[3].text = sysController.StarSysData.power.ToString();
                sysTMPs[4].text = sysController.StarSysData.PowerStations.ToString();               
                sysTMPs[5].text = sysController.StarSysData.production.ToString();
                sysTMPs[6].text = sysController.StarSysData.Factories.ToString();
                sysTMPs[7].text = sysController.StarSysData.tech.ToString();
                sysTMPs[8].text = sysController.StarSysData.Research.ToString();
                
                // ToDo: ship lists, bunkers, orbital batteries
                t_Pop += sysController.StarSysData.Population;
                t_Farms += sysController.StarSysData.Farms;
                t_Power += sysController.StarSysData.power;
                t_Stations += sysController.StarSysData.PowerStations;
                t_Production += sysController.StarSysData.production;
                t_Factories += sysController.StarSysData.Factories;
                t_Tech += sysController.StarSysData.tech;
                t_Research += sysController.StarSysData.Research; 
            }
            tPopulation.text = t_Pop.ToString();
            tPopulation.maskable = false;
            tForms.text = t_Farms.ToString();
            tPower.text = t_Power.ToString();
            tStations.text = t_Stations.ToString();
            tProduction.text = t_Production.ToString();
            tFactories.text = t_Factories.ToString();
            tTech.text = t_Tech.ToString();
            tTech.maskable = false;
            tResearch.text = t_Research.ToString();
        }

        lastCivUser = CivName.text;
    }
    public void UnLoadStarSysUI()
    {
        starSysUIRoot.SetActive(false);
    }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
