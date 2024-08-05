using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using TMPro;
//using UnityEditor.UIElements;

namespace Assets.Core
{
    public class MainMenuUIController : MonoBehaviour, IPointerDownHandler
    {
        /// <summary Multiplayer issues>
        /// ??? ToggleGroup by default only allows one toggle to be active so:
        /// Will each remote player make a unique selection in their own Toggle group or
        /// is it better to just have buttons or toggles not in a group for remotes to select
        /// Need to sort out and define local player for the host and from each remote player PC in multiplayer lobby
        ///  can try using Mirror; with GameObject localPlayer = NetworkClient.localPlayer.gameObject;
        /// ToDo this...
        /// </summary>
        public static MainMenuUIController instance;
        public GameObject mainMenuCanvas;
        public GameObject TipCanvas;
        public GameObject mainMenuButton;
        public GameObject uiCameraGO;
        public GameObject galaxyCenter;
        public bool PastMainMenu = false; // see TimeManager
        public GalaxyType selectedGalaxyType;
        public GalaxySize selectedGalaxySize;
        public TechLevel selectedTechLevel;
        public CivEnum selectedLocalCivEnum;
        public CivEnum selectedRemote0CivEnum;//ToDo for multiplayer lobby
        public CivEnum selectedRemote1CivEnum;
        public CivEnum selectedRemote2CivEnum;
        public CivEnum selectedRemote3CivEnum;
        public CivEnum selectedRemote4CivEnum;
        public CivEnum selectedRemote5CivEnum;
        public bool isSinglePlayer;
        [SerializeField]
        private GameObject panelLobby;
        [SerializeField]
        private GameObject panelMuliplayer;
        [SerializeField]
        private GameObject panelCivSelection;
        [SerializeField]
        private GameObject panelGamePara;
        [SerializeField]
        private GameObject singlePlayToggleGroup;
        [SerializeField]
        private GameObject mulitplayerToggleGroup;
        //[SerializeField]
        public GameObject youPrefab, computerPrefab, notInGamePrefab;
        //[SerializeField]
        public GameObject playerFed, playerRom, playerKling, playerCard, playerDom, playerBorg, playerTerran;
        //[SerializeField]
        //private GameObject[] listPlayerGOs = [MainMenuUIController.instance.playerFed,
        //    MainMenuUIController.instance.playerRom, MainMenuUIController.instance.playerKling, MainMenuUIController.instance.playerCard,
        //    MainMenuUIController.instance.playerDom, MainMenuUIController.instance.playerBorg, MainMenuUIController.instance.playerTerran ];

        [SerializeField]
        private GameObject textListContents;
        //private static TextMeshProUGUI[] textMeshProUGUIs;
        private Toggle _activeHostToggle;
        private Toggle _activeRemote0; // ToDo multiplayer lobby
        private Toggle _activeRemote1;
        private Toggle _activeRemote2;
        private Toggle _activeRemote3;
        private Toggle _activeRemote4;
        private Toggle _activeRemote5;
        private Toggle _activeRemote6;
        public Toggle Fed, Rom, Kling, Card, Dom, Borg, Terran;
        public ToggleGroup SinglePlayerCivilizationGroup;
        public ToggleGroup MultiplayerCivilizationGroup;// Does this need to be a group in the multiplayer setting, maybe not

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
            SinglePlayerCivilizationGroup.enabled = true;
            SinglePlayerCivilizationGroup = singlePlayToggleGroup.GetComponent<ToggleGroup>();
            SinglePlayerCivilizationGroup.RegisterToggle(Fed);
            SinglePlayerCivilizationGroup.RegisterToggle(Kling);
            SinglePlayerCivilizationGroup.RegisterToggle(Rom);
            SinglePlayerCivilizationGroup.RegisterToggle(Card);
            SinglePlayerCivilizationGroup.RegisterToggle(Dom);
            SinglePlayerCivilizationGroup.RegisterToggle(Borg);
            SinglePlayerCivilizationGroup.RegisterToggle(Terran);
            Fed.isOn = true;
            Kling.isOn = false;
            Rom.isOn = false;
            Card.isOn = false;
            Dom.isOn = false;
            Borg.isOn = false;
            Terran.isOn = false;
            playerFed.SetActive(true);
            playerRom.SetActive(true);
            playerKling.SetActive(true);
            playerCard.SetActive(true);
            playerDom.SetActive(true);
            playerBorg.SetActive(true);
            playerTerran.SetActive(true);
            youPrefab.SetActive(true);
            computerPrefab.SetActive(true);
            notInGamePrefab.SetActive(true);
            // Pending Multiplayer lobby if needed
            //MultiplayerCivilizationGroup.enabled = true;
            //MultiplayerCivilizationGroup = mulitplayerToggleGroup.GetComponent<ToggleGroup>();
            //MultiplayerCivilizationGroup.RegisterToggle(Fed);
            //MultiplayerCivilizationGroup.RegisterToggle(Kling);
            //MultiplayerCivilizationGroup.RegisterToggle(Rom);
            //MultiplayerCivilizationGroup.RegisterToggle(Card);
            //MultiplayerCivilizationGroup.RegisterToggle(Dom);
            //MultiplayerCivilizationGroup.RegisterToggle(Borg);
            //MultiplayerCivilizationGroup.RegisterToggle(Terran);
        }
        private void Start()
        {
            Fed.isOn = true;
            Fed.Select();
            Fed.OnSelect(null); // turns background selected color on, go figure.
            Kling.isOn = false;
            Rom.isOn = false;
            Card.isOn = false;
            Dom.isOn = false;
            Borg.isOn = false;
            Terran.isOn = false;

        }
        private void Update()
        {
            _activeHostToggle = SinglePlayerCivilizationGroup.ActiveToggles().ToArray().FirstOrDefault();
            if (_activeHostToggle != null)
                ActiveToggle();

        // ToDo do we need a multiplayer toggle group
            //foreach (var toggle in MultiplayerCivilizationGroup.ActiveToggles().ToArray())
            //{
            //    // ToDo: !!! need to get local player for SetLocalCivilization(int of civ) and
            //    // CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum...);
            //    // can try using Mirror; with GameObject localPlayer = NetworkClient.localPlayer.gameObject;
            //    if (toggle.name == "TOGGLE_FED")
            //    {
            //        Fed = _activeRemote0;
            //    }
            //    else if (toggle.name == "TOGGLE_ROM")
            //    {
            //        Rom = _activeRemote1;
            //    }
            //    else if (toggle.name == "TOGGLE_KLING")
            //    {
            //        Kling = _activeRemote2;
            //    }

            //    else if (toggle.name == "TOGGLE_CARD")
            //    {
            //        Card = _activeRemote3;
            //    }
            //    else if (toggle.name == "TOGGLE_DOM")
            //    {
            //        Dom = _activeRemote4;
            //    }
            //    else if (toggle.name == "TOGGLE_BORG")
            //    {
            //        Borg = _activeRemote5;
            //    }
            //    else if (toggle.name == "TOGGLE_TERRAN")
            //    {
            //        Terran = _activeRemote6;
            //    }
            //}
        }

        public void ActiveToggle()
        {
            switch (_activeHostToggle.name.ToUpper())
            {
                case "TOGGLE_FED":
                    Fed = _activeHostToggle;
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.FED);
                    Debug.Log("Active Fed.");
                    SetLocalCivilization(1);
                    PlaceYouYourselfInPlayerList(0);
                    break;
                case "TOGGLE_ROM":
                    Debug.Log("Active Rom.");
                    SetLocalCivilization(1);
                    Rom = _activeHostToggle;
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.ROM);
                    break;
                case "TOGGLE_KLING":
                    Debug.Log("Active Kling.");
                    SetLocalCivilization(2);
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.KLING);
                    Kling = _activeHostToggle;
                    break;
                case "TOGGLE_CARD":
                    Debug.Log("Active Card.");
                    SetLocalCivilization(3);
                    Card = _activeHostToggle;
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.CARD);
                    break;
                case "TOGGLE_DOM":
                    Debug.Log("Active Dom.");
                    SetLocalCivilization(4);
                    Dom = _activeHostToggle;
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.DOM);
                    break;
                case "TOGGLE_BORG":
                    Debug.Log("Active Borg.");
                    SetLocalCivilization(5);
                    Borg = _activeHostToggle;
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.BORG);
                    break;
                case "TOGGLE_TERRAN":
                    Debug.Log("Active Terran.");
                    SetLocalCivilization(158);
                    Terran = _activeHostToggle;
                    CivManager.instance.localPlayer = CivManager.instance.GetCivDataByCivEnum(CivEnum.TERRAN);
                    break;
                default:
                    break;
            }
        }

        private void PlaceYouYourselfInPlayerList(int civInt)
        {
            switch (civInt)
            {
                case 0:
                    playerFed = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                case 1:
                    playerRom = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                case 2:
                    playerKling = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                case 3:
                    playerCard = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                case 4:
                    playerDom = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                case 5:
                    playerBorg = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                case 6:
                    playerTerran = Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
                    break;
                default:
                    break;
            }
            playerFed= Instantiate(youPrefab, textListContents.transform.position, textListContents.transform.rotation);
            
        }

        public void SetSingleVsMultiplayer(bool singleMultiSelection)
        {
            isSinglePlayer = singleMultiSelection;
            panelLobby.SetActive(false); 
            panelMuliplayer.SetActive(!singleMultiSelection);
            panelCivSelection.SetActive(singleMultiSelection);
            singlePlayToggleGroup.SetActive(singleMultiSelection);
        }
        public void LoadSavedGame()
        {
            //ToDo
        }
        public void CancelButton()
        {
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(false);
            panelGamePara.SetActive(false);
            panelLobby.SetActive(true);
        }
        public void SaveButton()
        {
           // UpdateToggles();
            panelLobby.SetActive(false);
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(false);
            panelGamePara.SetActive(true);
        }
        public void NextButton()
        {
            panelLobby.SetActive(false);
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(true);
            mulitplayerToggleGroup.SetActive(true);
            panelGamePara.SetActive(false);
        }
        public void ReturnButton()
        {
            panelLobby.SetActive(false);
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(true);
            panelGamePara.SetActive(false);
        }
        public void SetCivSelectionMenu(CivEnum civEnum)
        {

        }
        public void SetGalaxySize(int index)
        {
            selectedGalaxySize = (GalaxySize)index;
            //GalaxyContro
        }

        public void SetGalaxyType(int index)
        {
            selectedGalaxyType = (GalaxyType)index;
        }

        public void SetTechLevel(int index)
        {
            selectedTechLevel = (TechLevel)index;
        }

        public void SetLocalCivilization(int index)
        {
            selectedLocalCivEnum = (CivEnum)index;
        }
        public void LoadGalaxyScene()
        {
            mainMenuCanvas.SetActive(false);
            uiCameraGO.SetActive(false);
            galaxyCenter.SetActive(true);
            PastMainMenu = true;
            TimeManager.instance.ResumeTime();
            SceneManager.LoadScene("GalaxyScene", LoadSceneMode.Additive);
            CivManager.instance.OnNewGameButtonClicked((int)selectedGalaxySize, (int)selectedTechLevel, (int)selectedGalaxyType,
                (int)selectedLocalCivEnum, isSinglePlayer);

        }
        public void OnPointerDown(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}

