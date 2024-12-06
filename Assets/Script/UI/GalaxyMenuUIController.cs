using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalaxyMenuUIController : MonoBehaviour
{
    public static GalaxyMenuUIController Instance { get; private set; } 
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
    //private TMP_Text sysSolFed, sysRomRom, sysKronosKling, sysCardCard, syOmarianDom, sysUniComplexBorg, sysSunTerran;
   // private readonly string sol = "Sol Earth", romulus = "Romulus", kronos = "Kronos", card = "Cardassia", dom ="Omarian Nebula", borg = "Unicomplex", terran = "Sun Terra";
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
       // settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(false);
        SysBackground.SetActive(false);
        FleetBackground.SetActive(false);
        DiplomacyBackground.SetActive(false);
        IntelBackground.SetActive(false);
        EncyclopediaBackground.SetActive(false);
       // SettingBackground.SetActive(false);
    }

    public void CloseAnOpenGalaxyUI()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
       // settingsMenuView.SetActive(false);
        //closeOpenMenuButton.SetActive(false);
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
            SetSysNames();
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
            //  settingsMenuView.SetActive(false);
            closeOpenMenuButton.SetActive(true);
            SysBackground.SetActive(false);
            FleetBackground.SetActive(false);
            DiplomacyBackground.SetActive(false);
            IntelBackground.SetActive(false);
            // SettingBackground.SetActive(false);
            FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
            FleetUIController.Instance.CloseUnLoadFleetUI();
        }
        else 
        {
            encyclopediaMenuView.SetActive(false);
            EncyclopediaBackground.SetActive(false);
        }
    }
    private void SetSysNames()
    {
        //switch (i)
        //{
        //    case 0:
        //        playerFed.text = notInGame;
        //        break;
        //    case 1:
        //        playerRom.text = notInGame;
        //        break;
        //    case 2:
        //        playerKling.text = notInGame;
        //        break;
        //    case 3:
        //        playerCard.text = notInGame;
        //        break;
        //    case 4:
        //        playerDom.text = notInGame;
        //        break;
        //    case 5:
        //        playerBorg.text = notInGame;
        //        break;
        //    case 6:
        //        playerTerran.text = notInGame;
        //        break;
        //    default:
        //        break;
        //}
    }
}
