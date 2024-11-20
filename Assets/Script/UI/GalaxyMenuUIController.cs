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
    private GameObject settingsMenuView;
    [SerializeField] 
    private GameObject closeOpenMenuButton;

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
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(false);
    }

    public void CloseOpenGalaxyUI()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(false);
    }
    public void OpenSystems()
    {
        sysMenuView.SetActive(true);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(true);
    }
    //public void CloseSystems()
    //{ sysMenuView.SetActive(false); }
    public void OpenFleets()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(true);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(true);
    }
    //public void CloseFleets()
    //{
    //    fleetsMenuView.SetActive(false);
    //}
    public void OpenDiplomacy()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(true);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(true);
    }
    //public void CloseDiplomacy() { diplomacyMenuView.SetActive(false ); }

    public void OpenIntel()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(true);
        encyclopediaMenuView.SetActive(false);
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(true);
    }
    //public void CloseIntel()
    //{
    //    intelMenuView.SetActive(false);
    //}
    public void OpenEncyclopedia()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(true);
        settingsMenuView.SetActive(false);
        closeOpenMenuButton.SetActive(true);
    }
    //public void CloseEncyclopdeia()
    //{
    //    encyclopediaMenuView.SetActive (true);
      
    //}
    public void OpenSettings()
    {
        sysMenuView.SetActive(false);
        fleetsMenuView.SetActive(false);
        diplomacyMenuView.SetActive(false);
        intelMenuView.SetActive(false);
        encyclopediaMenuView.SetActive(false);
        settingsMenuView.SetActive(true);
        closeOpenMenuButton.SetActive(true);
    }

    //public void CloseSettings()
    //{
    //    settingsMenuView.SetActive(false);
    //}
}
