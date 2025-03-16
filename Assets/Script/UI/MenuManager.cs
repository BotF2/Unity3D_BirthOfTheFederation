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
    public GameObject buildMenu;
    [SerializeField] private GameObject fleetsMenu;
    [SerializeField] private GameObject aFleetMenu;
    [SerializeField] private GameObject diplomacyMenu;
    [SerializeField] private GameObject intellMenu;
    [SerializeField] private GameObject encyclopedianMenu;
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
    public void SetBuildMenu(GameObject prefabMenu)
    {
        buildMenu = prefabMenu;
        buildMenu.SetActive(true);
    }
    public void CloseMenu(Menu enumMenu)
    {
        switch (enumMenu)
        {
            case Menu.None:
                openMenu = null;
                break;
            case Menu.SystemsMenu:
                systemsMenu.SetActive(false);
                openMenu = systemsMenu;
                break;
            case Menu.ASystemMenu:
                aSystemMenu.SetActive(false);
                openMenu = aSystemMenu;
                break;
            case Menu.BuildMenu:
                buildMenu.SetActive(false);
                openMenu = buildMenu;
                break;
            case Menu.FleetsMenu:
                fleetsMenu.SetActive(false);
                openMenu = fleetsMenu;
                break;
            case Menu.AFleetMenu:
                aFleetMenu.SetActive(false);
                openMenu = aFleetMenu;
                break;
            case Menu.DiplomacyMenu:
                TimeManager.Instance.ResumeTime();
                diplomacyMenu.SetActive(false);
                openMenu = diplomacyMenu;
                break;
            case Menu.IntellMenu:
                intellMenu.SetActive(false);
                openMenu = intellMenu;
                break;
            case Menu.EncyclopedianMenu:                
                encyclopedianMenu.SetActive(false);
                openMenu = encyclopedianMenu;
                break;
            case Menu.HabitableSysMenu:
                habitableSysMenu.SetActive(false);
                openMenu = habitableSysMenu;
                break;
            case Menu.Combat:
                //combat.SetActive(true);
                break;
            default:
                break;
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
                TimeManager.Instance.PauseTime();
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
}
