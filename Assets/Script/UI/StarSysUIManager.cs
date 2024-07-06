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
    public static StarSysUIManager instance;
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

    private Camera galaxyEventCamera;


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
        starSysUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
        //parentCanvas.worldCamera = galaxyEventCamera; not working
        //destinationDropdown.value = 0;
    }

    public void LoadStarSysUI(GameObject go) 
    {
        FleetUIManager.instance.UnLoadFleetUI();
        starSysUIRoot.SetActive(true);

        controller = go.GetComponent<StarSysController>();
        var civEnum = controller.StarSysData.CurrentOwner;
        var civData = CivManager.instance.GetCivDataByCivEnum(civEnum);

        CivName.text = civData.CivLongName;
        LoadSystemsList(civData.CivEnum);
    }
    private void LoadSystemsList(CivEnum civEnum)
    {
        sysControllerList.Clear(); 
        foreach (var sysController in StarSysManager.instance.StarSysControllerList)
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
                
                
                //sysTMPs[10].text = sysController.StarSysData. ToDo: ship dropdown here
            }
        }
        lastCivUser = CivName.text;
    }
    public void UnLoadStarSysUI()
    {
        starSysUIRoot.SetActive(false);
    }

    private void NamesToShipDropdown(List<ShipController> shipControllers)
    {
        //var shipDropdown = ShipDropdownGO.GetComponent<TMP_Dropdown>();
        //shipDropdown.options.Clear();

        //foreach (var shipCon in shipControllers)
        //{
        //    if (shipCon != null)
        //    {
        //        string text = shipCon.ShipData.ShipName;
        //        text.Replace("(CLONE)", string.Empty);
        //        shipDropdown.options.Add(new TMP_Dropdown.OptionData(text));
        //    }
        
        //}
        ////DropdownItemSelected(shipDropdown);
        ////shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
