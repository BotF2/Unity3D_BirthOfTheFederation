using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalaxyMenuUIController : MonoBehaviour
{
    public static GalaxyMenuUIController Instance { get; private set; } 
    [SerializeField]
    private GameObject sysMenuPanel;
    [SerializeField] 
    private GameObject fleetsMenuPanel;
    [SerializeField] 
    private GameObject diplomacyMenuPanel;
    [SerializeField] 
    private GameObject intelMenuPanel;
    [SerializeField] 
    private GameObject encyclopediaMenuPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField] 
    private GameObject closePanelButton;

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
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(false); 
        intelMenuPanel.SetActive(false) ;
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(false);
    }

    public void CloseOpenGalaxyUI()
    {
        sysMenuPanel.SetActive(false);
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(false);
        intelMenuPanel.SetActive(false);
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(false);
    }
    public void OpenSystems()
    {
        sysMenuPanel.SetActive(true);
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(false);
        intelMenuPanel.SetActive(false);
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(true);
    }
    //public void CloseSystems()
    //{ sysMenuPanel.SetActive(false); }
    public void OpenFleets()
    {
        sysMenuPanel.SetActive(false);
        fleetsMenuPanel.SetActive(true);
        diplomacyMenuPanel.SetActive(false);
        intelMenuPanel.SetActive(false);
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(true);
    }
    //public void CloseFleets()
    //{
    //    fleetsMenuPanel.SetActive(false);
    //}
    public void OpenDiplomacy()
    {
        sysMenuPanel.SetActive(false);
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(true);
        intelMenuPanel.SetActive(false);
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(true);
    }
    //public void CloseDiplomacy() { diplomacyMenuPanel.SetActive(false ); }

    public void OpenIntel()
    {
        sysMenuPanel.SetActive(false);
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(false);
        intelMenuPanel.SetActive(true);
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(true);
    }
    //public void CloseIntel()
    //{
    //    intelMenuPanel.SetActive(false);
    //}
    public void OpenEncyclopedia()
    {
        sysMenuPanel.SetActive(false);
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(false);
        intelMenuPanel.SetActive(false);
        encyclopediaMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        closePanelButton.SetActive(true);
    }
    //public void CloseEncyclopdeia()
    //{
    //    encyclopediaMenuPanel.SetActive (true);
      
    //}
    public void OpenSettings()
    {
        sysMenuPanel.SetActive(false);
        fleetsMenuPanel.SetActive(false);
        diplomacyMenuPanel.SetActive(false);
        intelMenuPanel.SetActive(false);
        encyclopediaMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        closePanelButton.SetActive(true);
    }

    //public void CloseSettings()
    //{
    //    settingsPanel.SetActive(false);
    //}
}
