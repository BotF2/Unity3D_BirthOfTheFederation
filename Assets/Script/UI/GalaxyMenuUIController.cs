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

    public void CloseOpenGalaxyUI()
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
       // SettingBackground.SetActive(false);
        FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
        FleetUIController.Instance.CloseUnLoadFleetUI();    
    }

    public void OpenFleets()
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
    public void OpenDiplomacy()
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

    public void OpenIntel()
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

    public void OpenEncyclopedia()
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

    //public void OpenSettings()
    //{
    //    sysMenuView.SetActive(false);
    //    fleetsMenuView.SetActive(false);
    //    diplomacyMenuView.SetActive(false);
    //    intelMenuView.SetActive(false);
    //    encyclopediaMenuView.SetActive(false);
    //    settingsMenuView.SetActive(true);
    //    SettingBackground.SetActive(true);
    //    closeOpenMenuButton.SetActive(true);
    //    SysBackground.SetActive(false);
    //    FleetBackground.SetActive(false);
    //    DiplomacyBackground.SetActive(false);
    //    IntelBackground.SetActive(false);
    //    EncyclopediaBackground.SetActive(false);
    //    FirstContactUIController.Instance.CloseUnLoadFirstContactUI();
    //    FleetUIController.Instance.CloseUnLoadFleetUI();
    //}


}
