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
    public class MainMenuUIController : MonoBehaviour
    {
        /// <summary Multiplayer issues>
        /// ??? ToggleGroup by default only allows one toggle to be active so:
        /// Will each remote player make a unique selection in their own Toggle group or
        /// is it better to just have buttons or toggles not in a group for remotes to select
        /// Need to sort out and define local player for the host and from each remote player PC in multiplayer lobby
        ///  can try using Mirror; with GameObject LocalPlayer = NetworkClient.LocalPlayer.gameObject;
        /// ToDo this...
        /// </summary>
        public static MainMenuUIController Instance;
        [SerializeField]
        private GameObject mainMenuCanvas;
        [SerializeField]
        private GameObject TipCanvas;
        [SerializeField]
        private GameObject mainMenuButton;
        [SerializeField]
        private GameObject uiCameraGO;
        [SerializeField]
        private GameObject galaxyCenter;
        public bool PastMainMenu = false; // see TimeManager
        public GalaxyType SelectedGalaxyType { get; private set; }
        public GalaxySize SelectedGalaxySize { get; private set; }
        public TechLevel SelectedTechLevel { get; private set; }
        public CivEnum SelectedLocalCivEnum;
        //public CivEnum SelectedRemote0CivEnum;//ToDo for multiplayer lobby
        //public CivEnum SelectedRemote1CivEnum;
        //public CivEnum SelectedRemote2CivEnum;
        //public CivEnum SelectedRemote3CivEnum;
        //public CivEnum SelectedRemote4CivEnum;
        //public CivEnum SelectedRemote5CivEnum;
        public bool IsSinglePlayer;
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
        [SerializeField]
        private TMP_Text playerFed, playerRom, playerKling, playerCard, playerDom, playerBorg, playerTerran;
        private string you = "You", computer = "Computer", notInGame = "Absent";
        private Toggle _activeHostToggle;
        //private Toggle _activeRemote0; // ToDo multiplayer lobby
        //private Toggle _activeRemote1;
        //private Toggle _activeRemote2;
        //private Toggle _activeRemote3;
        //private Toggle _activeRemote4;
        //private Toggle _activeRemote5;
        //private Toggle _activeRemote6;
        public Toggle FedLocalPalyerToggle, RomLocalPlayerToggle, KlingLocalPlayerToggle, CardLocalPlayerToggle,
            DomLocalPlayerToggle, BorgLocalPlayerToggle, TerranLocalPlayerToggle;
        
        public ToggleGroup SinglePlayerCivilizationGroup;
        //public ToggleGroup MultiplayerCivilizationGroup;// Can and should this be a group in the multiplayer setting, maybe.
        public Toggle FedOnOff, RomOnOff, KlingOnOff, CardOnOff, DomOnOff, BorgOnOff, TerranOnOff;
        List<Toggle> OnOffToggles = new List<Toggle>();

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
            SinglePlayerCivilizationGroup.enabled = true;
            SinglePlayerCivilizationGroup = singlePlayToggleGroup.GetComponent<ToggleGroup>();
            SinglePlayerCivilizationGroup.RegisterToggle(FedLocalPalyerToggle);
            SinglePlayerCivilizationGroup.RegisterToggle(KlingLocalPlayerToggle);
            SinglePlayerCivilizationGroup.RegisterToggle(RomLocalPlayerToggle);
            SinglePlayerCivilizationGroup.RegisterToggle(CardLocalPlayerToggle);
            SinglePlayerCivilizationGroup.RegisterToggle(DomLocalPlayerToggle);
            SinglePlayerCivilizationGroup.RegisterToggle(BorgLocalPlayerToggle);
            SinglePlayerCivilizationGroup.RegisterToggle(TerranLocalPlayerToggle);
            FedOnOff.isOn = true;
            RomOnOff.isOn = true;
            KlingOnOff.isOn = true;
            CardOnOff.isOn = true;
            DomOnOff.isOn = true;
            BorgOnOff.isOn = true;
            TerranOnOff.isOn = false;

            // Pending Multiplayer lobby if needed
            //MultiplayerCivilizationGroup.enabled = true;
            //MultiplayerCivilizationGroup = mulitplayerToggleGroup.GetComponent<ToggleGroup>();
            //MultiplayerCivilizationGroup.RegisterToggle(FedLocalPalyerToggle);
            //MultiplayerCivilizationGroup.RegisterToggle(KlingLocalPlayerToggle);
            //MultiplayerCivilizationGroup.RegisterToggle(RomLocalPlayerToggle);
            //MultiplayerCivilizationGroup.RegisterToggle(CardLocalPlayerToggle);
            //MultiplayerCivilizationGroup.RegisterToggle(DomLocalPlayerToggle);
            //MultiplayerCivilizationGroup.RegisterToggle(BorgLocalPlayerToggle);
            //MultiplayerCivilizationGroup.RegisterToggle(TerranLocalPlayerToggle);
        }
        private void Start()
        {
            FedLocalPalyerToggle.isOn = true;
            FedLocalPalyerToggle.Select();
            FedLocalPalyerToggle.OnSelect(null); // turns background selected color on, go figure.
            KlingLocalPlayerToggle.isOn = false;
            RomLocalPlayerToggle.isOn = false;
            CardLocalPlayerToggle.isOn = false;
            DomLocalPlayerToggle.isOn = false;
            BorgLocalPlayerToggle.isOn = false;
            TerranLocalPlayerToggle.isOn = false;
            OnOffToggles.Add(FedOnOff); 
            OnOffToggles.Add(RomOnOff); 
            OnOffToggles.Add(KlingOnOff);
            OnOffToggles.Add(CardOnOff);
            OnOffToggles.Add(DomOnOff);
            OnOffToggles.Add(BorgOnOff);
            OnOffToggles.Add(TerranOnOff);

        }
        private void UpdatePlayers()
        {
            _activeHostToggle = SinglePlayerCivilizationGroup.ActiveToggles().ToArray().FirstOrDefault();
            if (_activeHostToggle != null)
                ActiveToggle();

        // ToDo do we need a multiplayer toggle group
            //foreach (var toggle in MultiplayerCivilizationGroup.ActiveToggles().ToArray())
            //{
            //    // ToDo: !!! need to get local player for SetLocalCivilization(int of civ) and
            //    // CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum...);
            //    // can try using Mirror; with GameObject LocalPlayer = NetworkClient.LocalPlayer.gameObject;
            //    if (toggle.name == "TOGGLE_FED")
            //    {
            //        FedLocalPalyerToggle = _activeRemote0;
            //    }
            //    else if (toggle.name == "TOGGLE_ROM")
            //    {
            //        RomLocalPlayerToggle = _activeRemote1;
            //    }
            //    else if (toggle.name == "TOGGLE_KLING")
            //    {
            //        KlingLocalPlayerToggle = _activeRemote2;
            //    }

            //    else if (toggle.name == "TOGGLE_CARD")
            //    {
            //        CardLocalPlayerToggle = _activeRemote3;
            //    }
            //    else if (toggle.name == "TOGGLE_DOM")
            //    {
            //        DomLocalPlayerToggle = _activeRemote4;
            //    }
            //    else if (toggle.name == "TOGGLE_BORG")
            //    {
            //        BorgLocalPlayerToggle = _activeRemote5;
            //    }
            //    else if (toggle.name == "TOGGLE_TERRAN")
            //    {
            //        TerranLocalPlayerToggle = _activeRemote6;
            //    }
            //}
        }

        public void ActiveToggle()
        {
            switch (_activeHostToggle.name.ToUpper())
            {
                case "TOGGLE_FED":
                    FedOnOff.isOn = true;
                    FedOnOff.OnSelect(null);
                    FedLocalPalyerToggle = _activeHostToggle;
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.FED);
                    Debug.Log("Active FedLocalPalyerToggle.");
                    SetLocalCivilization(0);
                    PlaceYouYourselfInPlayerList(0);
                    break;
                case "TOGGLE_ROM":
                    RomOnOff.isOn = true;
                    RomOnOff.OnSelect(null);
                    RomLocalPlayerToggle = _activeHostToggle;
                    Debug.Log("Active RomLocalPlayerToggle.");
                    SetLocalCivilization(1);
                    PlaceYouYourselfInPlayerList(1);
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.ROM);
                    break;
                case "TOGGLE_KLING":
                    KlingOnOff.isOn = true;
                    KlingOnOff.OnSelect(null);
                    KlingLocalPlayerToggle = _activeHostToggle;
                    Debug.Log("Active KlingLocalPlayerToggle.");
                    SetLocalCivilization(2);
                    PlaceYouYourselfInPlayerList(2);
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.KLING);
                    break;
                case "TOGGLE_CARD":
                    CardOnOff.isOn = true; 
                    CardOnOff.OnSelect(null);
                    CardLocalPlayerToggle = _activeHostToggle;
                    Debug.Log("Active CardLocalPlayerToggle.");
                    SetLocalCivilization(3);
                    PlaceYouYourselfInPlayerList(3);
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.CARD);
                    break;
                case "TOGGLE_DOM":
                    DomOnOff.isOn = true;
                    DomOnOff.OnSelect(null);
                    DomLocalPlayerToggle = _activeHostToggle;
                    Debug.Log("Active DomLocalPlayerToggle.");
                    SetLocalCivilization(4);
                    PlaceYouYourselfInPlayerList(4);
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.DOM);
                    break;
                case "TOGGLE_BORG":
                    BorgOnOff.isOn = true;
                    BorgOnOff.OnSelect(null);
                    BorgLocalPlayerToggle = _activeHostToggle;
                    Debug.Log("Active BorgLocalPlayerToggle.");
                    SetLocalCivilization(5);
                    PlaceYouYourselfInPlayerList(5);
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.BORG);
                    break;
                case "TOGGLE_TERRAN":
                    TerranOnOff.isOn = true;
                    TerranOnOff.OnDeselect(null);
                    TerranLocalPlayerToggle = _activeHostToggle;
                    Debug.Log("Active TerranLocalPlayerToggle.");
                    SetLocalCivilization(158);
                    PlaceYouYourselfInPlayerList(158);
                    CivManager.Instance.LocalPlayer = CivManager.Instance.GetCivDataByCivEnum(CivEnum.TERRAN);
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
                    playerFed.text = you;
                    break;
                case 1:
                    playerRom.text = you;
                    break;
                case 2:
                    playerKling.text = you;
                    break;
                case 3:
                    playerCard.text = you;
                    break;
                case 4:
                    playerDom.text = you;
                    break;
                case 5:
                    playerBorg.text = you;
                    break;
                case 158:
                    playerTerran.text = you;
                    break;
                default:
                    break;
            }
        }
            
        
        private void ResetPlayers()
        {
            if (playerFed.text == you)
                playerFed.text = computer;
            if (playerRom.text == you)
                playerRom.text = computer;                 
            if (playerKling.text == you)
                playerKling.text = computer;              
            if (playerCard.text == you)
                playerCard.text = computer;               
            if (playerDom.text == you)
                playerDom.text = computer;                  
            if (playerBorg.text == you)
                playerBorg.text = computer;                  
            if(playerTerran.text == you)
                playerTerran.text = computer;
        }
        public void SetSingleVsMultiplayer(bool singleMultiSelection)
        {
            IsSinglePlayer = singleMultiSelection;
            panelLobby.SetActive(false); 
            panelMuliplayer.SetActive(!singleMultiSelection);
            panelCivSelection.SetActive(singleMultiSelection);
            singlePlayToggleGroup.SetActive(true);
        }
        private void OnTogglePlayerToggleInGame(Toggle civPlayedToggle)
        {
            if (civPlayedToggle.isOn)
            {
                foreach (var tog in OnOffToggles)
                {
                    if (tog == civPlayedToggle)
                        tog.isOn = true;
                }
            }
                
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
            singlePlayToggleGroup.SetActive(true);
            UpdatePlayers();
            panelLobby.SetActive(false);
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(false);
            panelGamePara.SetActive(true);
        }
        public void NextButton()
        {
            //ToDo ***Turned off for now
            //panelLobby.SetActive(false);
            //panelMuliplayer.SetActive(false);
            //panelCivSelection.SetActive(true);
            //mulitplayerToggleGroup.SetActive(true); //**** currently set to mulitplayer toggle, not single player
            //panelGamePara.SetActive(false);
        }
        public void ReturnButton()
        {
            ResetPlayers();
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
            SelectedGalaxySize = (GalaxySize)index;
            //GalaxyContro
        }

        public void SetGalaxyType(int index)
        {
            SelectedGalaxyType = (GalaxyType)index;
        }

        public void SetTechLevel(int index)
        {
            SelectedTechLevel = (TechLevel)index;
        }

        public void SetLocalCivilization(int index)
        {
            SelectedLocalCivEnum = (CivEnum)index;
        }
        public void LoadGalaxyScene()
        {
            mainMenuCanvas.SetActive(false);
            uiCameraGO.SetActive(false);
            galaxyCenter.SetActive(true);
            PastMainMenu = true;
            TimeManager.Instance.ResumeTime();
            SceneManager.LoadScene("GalaxyScene", LoadSceneMode.Additive);
            CivManager.Instance.OnNewGameButtonClicked((int)SelectedGalaxySize, (int)SelectedTechLevel, (int)SelectedGalaxyType,
                (int)SelectedLocalCivEnum, IsSinglePlayer);

        }
        public void OnPointerDown(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}

