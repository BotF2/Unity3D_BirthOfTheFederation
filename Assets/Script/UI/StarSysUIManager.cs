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
    public StarSysController controller;
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
    private TMP_Text CivName;

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

        //????save last civ and its menu list or clear and start over each time
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
                GameObject starSysPanel = (GameObject)Instantiate(starSysPanelPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
                starSysPanel.transform.SetParent(starSysListGO.transform, true);
                var sysNames = starSysPanel.GetComponentsInChildren<TextMeshProUGUI>();
                sysNames[0].text = sysController.StarSysData.SysName;
            }
        }
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
