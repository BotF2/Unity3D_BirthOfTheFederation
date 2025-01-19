using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Core;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public enum Menu
{
    None,
    SystemsMenu,
    ASystemMenu,
    BuildMenu,
    FleetsMenu,
    AFleetMenu,
    DiplomacyMenu,
    IntellMenu,
    EncyclopedianMenu,
    FirstContactMenu,
    HabitableSysMenu,
    Combat
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    [SerializeField] private GameObject systemsMenu;
    [SerializeField] private GameObject aSystemMenu;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject fleetsMenu;
    [SerializeField] private GameObject aFleetMenu;
    [SerializeField] private GameObject diplomacyMenu;
    [SerializeField] private GameObject intellMenu;
    [SerializeField] private GameObject encyclopedianMenu;
    [SerializeField] private GameObject firstContactMenu;
    [SerializeField] private GameObject habitableSysMenu;
    [SerializeField] private GameObject combat;
    [SerializeField] private GameObject openMenu;

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
    public void OpenMenu(Menu menuEnum, GameObject callingMenu)
    {
        if (callingMenu != null)
            callingMenu.SetActive(false);
        if (openMenu != null)
            openMenu.SetActive(false);
        switch (menuEnum)
        {
            case Menu.None:
                openMenu = null;
                break;
            case Menu.SystemsMenu:
                systemsMenu.SetActive(true);
                openMenu = systemsMenu;
                break;
            case Menu.ASystemMenu:
                aSystemMenu.SetActive(true);
                openMenu = aSystemMenu;
                break;
            case Menu.BuildMenu:
                buildMenu.SetActive(true);
                openMenu = buildMenu;
                break;
            case Menu.FleetsMenu:
                fleetsMenu.SetActive(true);
                openMenu = fleetsMenu;
                break;
            case Menu.AFleetMenu:
                aFleetMenu.SetActive(true);
                openMenu = aFleetMenu;
                break;
            case Menu.DiplomacyMenu:
                diplomacyMenu.SetActive(true);
                openMenu = diplomacyMenu;
                break;
            case Menu.IntellMenu:
                intellMenu.SetActive(true);
                openMenu = intellMenu;
                break;
            case Menu.EncyclopedianMenu:                
                encyclopedianMenu.SetActive(true);
                openMenu = encyclopedianMenu;
                break;
            case Menu.FirstContactMenu:
                firstContactMenu.SetActive(true);
                openMenu = firstContactMenu;
                break;
            case Menu.HabitableSysMenu:
                habitableSysMenu.SetActive(true);
                openMenu = habitableSysMenu;
                break;
            case Menu.Combat:
                //combat.SetActive(true);
                break;
            default:
                break;
        }
    }
    //public void SetUpButtonListeners(GameObject UIgameObject, StarSysController sysController)
    //{
    //    Button[] listButtons = UIgameObject.GetComponentsInChildren<Button>();
    //    //for (int k = 0; k < listButtons.Length; k++) 
    //    int one = 1;
    //    foreach (var listButton in listButtons)
    //    {
    //        switch (listButton.name)
    //        {
    //            case "BuildButton":
    //                //listButton.onClick.AddListener(() => buildListUI.SetActive(true));
    //                //listButton.onClick.AddListener(() => sysController.BuildClick());
    //                break;
    //            case "FactoryButtonOn":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "FactoryButtonOff":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "YardButtonOn":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "YardButtonOff":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "ShieldButtonOn":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "ShieldButtonOff":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "OBButtonOn":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "OBButtonOff":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "ResearchButtonOn":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            case "ResearchButtonOff":
    //                listButton.onClick.RemoveAllListeners();
    //                listButton.onClick.AddListener(() => sysController.FacilityOnClick(sysController, listButton.name));
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
}
