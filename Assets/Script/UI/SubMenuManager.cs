using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Core;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

//public enum Menu
//{
//    None,
//    SystemsMenu,
//    ASystemMenu,
//    BuildMenu,
//    FleetsMenu,
//    AFleetMenu,
//    DiplomacyMenu,
//    ADiplomacyMenu,
//    IntellMenu,
//    EncyclopedianMenu,
//    FirstContactMenu,
//    HabitableSysMenu,
//    Combat
//}

public class SubMenuManager : MonoBehaviour
{
    public static SubMenuManager Instance { get; private set; }
    [SerializeField] private GameObject systemsMenu;
    [SerializeField] private GameObject aSystemMenu;
    public GameObject buildMenu;
    [SerializeField] private GameObject fleetsMenu;
    [SerializeField] private GameObject aFleetMenu;
    [SerializeField] private GameObject diplomacyMenu;
    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private GameObject intellMenu;
    [SerializeField] private GameObject encyclopedianMenu;
    [SerializeField] private GameObject habitableSysMenu;
    [SerializeField] private GameObject combat;
    [SerializeField] private GameObject openMenuWas;

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
    //public void SetActiveBuildMenu(GameObject prefabMenu)
    //{
    //    buildMenu = prefabMenu;
    //    buildMenu.SetActive(true);
    //}
    //public void CloseMenu(Menu enumMenu)
    //{
    //    switch (enumMenu)
    //    {
    //        case Menu.None:
    //            openMenuWas = null;
    //            break;
    //        case Menu.SystemsMenu:
    //            systemsMenu.SetActive(false);
    //            openMenuWas = systemsMenu;
    //            break;
    //        case Menu.ASystemMenu:
    //            aSystemMenu.SetActive(false);
    //            openMenuWas = aSystemMenu;
    //            break;
    //        case Menu.BuildMenu:
    //            buildMenu.SetActive(false);
    //            openMenuWas = buildMenu;
    //            break;
    //        case Menu.FleetsMenu:
    //            fleetsMenu.SetActive(false);
    //            openMenuWas = fleetsMenu;
    //            break;
    //        case Menu.AFleetMenu:
    //            aFleetMenu.SetActive(false);
    //            openMenuWas = aFleetMenu;
    //            break;
    //        case Menu.DiplomacyMenu:
    //            TimeManager.Instance.ResumeTime();
    //            diplomacyMenu.SetActive(false);
    //            interactionMenu.SetActive(false);
    //            openMenuWas = diplomacyMenu;
    //            break;
    //        case Menu.ADiplomacyMenu:
    //            interactionMenu.SetActive(false);
    //            openMenuWas = interactionMenu;
    //            break;
    //        case Menu.IntellMenu:
    //            intellMenu.SetActive(false);
    //            openMenuWas = intellMenu;
    //            break;
    //        case Menu.EncyclopedianMenu:                
    //            encyclopedianMenu.SetActive(false);
    //            openMenuWas = encyclopedianMenu;
    //            break;
    //        case Menu.HabitableSysMenu:
    //            habitableSysMenu.SetActive(false);
    //            openMenuWas = habitableSysMenu;
    //            break;
    //        case Menu.Combat:// change scenes
    //            //combat.SetActive(true);
    //            break;
    //        default:
    //            break;
    //    }
    //}
    public void OpenMenu(Menu menuEnum, GameObject callingMenu)
    {
        if (callingMenu != null)
            callingMenu.SetActive(false);
        if (openMenuWas != null)
            openMenuWas.SetActive(false);
        switch (menuEnum)
        {
            case Menu.None:
                openMenuWas = null;
                break;
            case Menu.SystemsMenu:
                systemsMenu.SetActive(true);
                openMenuWas = systemsMenu;
                break;
            case Menu.ASystemMenu:
                aSystemMenu.SetActive(true);
                openMenuWas = aSystemMenu;
                break;
            case Menu.BuildMenu:
                buildMenu.SetActive(true);
                openMenuWas = buildMenu;
                break;
            case Menu.FleetsMenu:
                fleetsMenu.SetActive(true);
                openMenuWas = fleetsMenu;
                break;
            case Menu.AFleetMenu:
                aFleetMenu.SetActive(true);
                openMenuWas = aFleetMenu;
                break;
            case Menu.DiplomacyMenu:
                TimeManager.Instance.PauseTime();
                diplomacyMenu.SetActive(true);
                openMenuWas = diplomacyMenu;
                break;
            case Menu.ADiplomacyMenu:
                TimeManager.Instance.PauseTime();
                interactionMenu.SetActive(true);
                openMenuWas = interactionMenu;
                break;
            case Menu.IntellMenu:
                intellMenu.SetActive(true);
                openMenuWas = intellMenu;
                break;
            case Menu.EncyclopedianMenu:                
                encyclopedianMenu.SetActive(true);
                openMenuWas = encyclopedianMenu;
                break;
            case Menu.HabitableSysMenu:
                habitableSysMenu.SetActive(true);
                openMenuWas = habitableSysMenu;
                break;
            case Menu.Combat:
                //combat.SetActive(true);
                break;
            default:
                break;
        }
    }
}
