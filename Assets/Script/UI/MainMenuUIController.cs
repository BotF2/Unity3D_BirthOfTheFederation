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
using FischlWorks_FogWar;
using Unity.VisualScripting;
//using UnityEditor.UIElements;

namespace Assets.Core
{
    public class MainMenuUIController : MonoBehaviour
    {
        /// <summary Multiplayer issues>
        /// ??? Unity ToggleGroup by default only allows one toggle to be active so:
        /// Will each remote player make a unique selection in their own Toggle group or
        /// is it better to just have buttons, or toggles, not in a group for remotes to select?
        /// Need to sort out and define local player for the host and from each remote player PC in multiplayer lobby
        /// We can try using (Mirror; with GameObject LocalPlayerCivEnum = NetworkClient.LocalPlayerCivEnum.gameObject;)
        /// ToDo this...
        /// </summary>
        public static MainMenuUIController Instance;

        public MainMenuData MainMenuData = new MainMenuData(); 
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
        //ToDo for multiplayer lobby
        //public CivEnum SelectedRemote0CivEnum;
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
        private GameObject mapToggleGroup;
        [SerializeField]
        private GameObject galaxySizeToggleGroup;
        [SerializeField]
        private GameObject techLevelToggleGroup;
        [SerializeField]
        private TMP_Text playerFed, playerRom, playerKling, playerCard, playerDom, playerBorg, playerTerran;
        private readonly string you = "You", computer = "Computer", notInGame = "Absent";
        private Toggle activeLocalPlayerToggle;
        //ToDo for multiplayer lobby
        //private Toggle _activeRemote0;
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
        public List<Toggle> OnOffToggles;
        private Toggle activeMapToggle;
        public ToggleGroup MapToggleGroup;
        public Toggle CanonToggle, RandomToggle, RingToggle;
        public List<Toggle> MapToggles;
        private Toggle activeGalaxySizeToggle;
        public ToggleGroup GalaxySizeToggleGroup;
        public Toggle SmallGalaxyToggle, MediumGalaxyToggle, LargeGalaxyToggle, PonderousGalaxyToggle;
        public List<Toggle> GalaxySizeToggles;
        private Toggle activeTechLevelToggle;
        public ToggleGroup TechLevelToggleGroup;
        public Toggle EarlyToggle, DevelopedToggle, AdvancedToggle, SupremeToggle;
        public List<Toggle> TechLevelToggles;

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
            MapToggleGroup.enabled = true;
            MapToggleGroup = mapToggleGroup.GetComponent<ToggleGroup>();
            MapToggleGroup.RegisterToggle(CanonToggle);
            MapToggleGroup.RegisterToggle(RandomToggle);
            MapToggleGroup.RegisterToggle(RingToggle);
            FedOnOff.isOn = true;
            RomOnOff.isOn = true;
            KlingOnOff.isOn = true;
            CardOnOff.isOn = true;
            DomOnOff.isOn = true;
            BorgOnOff.isOn = true;
            TerranOnOff.isOn = false;
            MapToggleGroup.enabled = true;
            GalaxySizeToggleGroup = galaxySizeToggleGroup.GetComponent<ToggleGroup>();
            GalaxySizeToggleGroup.RegisterToggle(SmallGalaxyToggle);
            GalaxySizeToggleGroup.RegisterToggle(MediumGalaxyToggle);
            GalaxySizeToggleGroup.RegisterToggle(LargeGalaxyToggle);
            GalaxySizeToggleGroup.RegisterToggle(PonderousGalaxyToggle);
            TechLevelToggleGroup.enabled = true;
            TechLevelToggleGroup = techLevelToggleGroup.GetComponent<ToggleGroup>();
            TechLevelToggleGroup.RegisterToggle(EarlyToggle);
            TechLevelToggleGroup.RegisterToggle(DevelopedToggle);
            TechLevelToggleGroup.RegisterToggle(AdvancedToggle);
            TechLevelToggleGroup.RegisterToggle(SupremeToggle);

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
            //this.MainMenuData = GameObject.Find("MainMenuUIController").GetComponentInChildren<MainMenuUIController>();
            //GalaxySize = mainMenuUIController.SelectedGalaxySize;
            //GalaxyType = mainMenuUIController.SelectedGalaxyType;
            //TechLevelOnLoadGame = mainMenuUIController.SelectedTechLevel;
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
            MainMenuData.InGamePlayableCivList.Add(CivEnum.FED);
            MainMenuData.InGamePlayableCivList.Add(CivEnum.ROM);
            MainMenuData.InGamePlayableCivList.Add(CivEnum.KLING);
            MainMenuData.InGamePlayableCivList.Add(CivEnum.CARD);
            MainMenuData.InGamePlayableCivList.Add(CivEnum.DOM);
            MainMenuData.InGamePlayableCivList.Add(CivEnum.BORG);
            MainMenuData.InGamePlayableCivList.Add(CivEnum.TERRAN);
            CanonToggle.isOn = true;
            CanonToggle.Select();
            CanonToggle.OnSelect(null);
            RandomToggle.isOn = false;
            RingToggle.isOn = false;
            SmallGalaxyToggle.isOn = true;
            SmallGalaxyToggle.Select();
            SmallGalaxyToggle.OnSelect(null);
            MediumGalaxyToggle.isOn = false;
            LargeGalaxyToggle.isOn = false;
            PonderousGalaxyToggle.isOn = false;
            EarlyToggle.isOn = true;
            EarlyToggle.Select();
            EarlyToggle.OnSelect(null);
            DevelopedToggle.isOn = false;
            AdvancedToggle.isOn = false;
            SupremeToggle.isOn = false;
        }
        public void LoadDefault()
        {
            MainMenuData.SelectedGalaxySize = GalaxySize.SMALL;
            MainMenuData.SelectedGalaxyType = GalaxyMapType.CANON;
            MainMenuData.SelectedTechLevel = TechLevel.EARLY;
            GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.FED;
        }
        private void UpdatePlayers()
        {
            activeLocalPlayerToggle = SinglePlayerCivilizationGroup.ActiveToggles().ToArray().FirstOrDefault();
            if (activeLocalPlayerToggle != null)
                ActivePlayerToggle();
            #region Multiplayer toggle group - 
            // ToDo do we need a multiplayer toggle group
            //foreach (var toggle in MultiplayerCivilizationGroup.ActiveToggles().ToArray())
            //{
            //    // ToDo: !!! need to get local player for SetLocalCivilization(int of civ) and
            //    // CivManager.Instance.LocalPlayerCivEnum = CivManager.Instance.GetCivDataByCivEnum(CivEnum...);
            //    // can try using Mirror; with GameObject LocalPlayerCivEnum = NetworkClient.LocalPlayerCivEnum.gameObject;
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
            #endregion Multiplayer toggle group
        }
        public void UpdateMapSelection()
        { 
            activeMapToggle = MapToggleGroup.ActiveToggles().ToArray().FirstOrDefault();
            if (activeMapToggle != null)
            {
                ActiveMapToggle();
            }
        }
        public void UpdateGalaxySizeSelection()
        {
            activeGalaxySizeToggle = GalaxySizeToggleGroup.ActiveToggles().ToArray().FirstOrDefault();
            if (activeMapToggle != null)
            {
                ActiveGalaxySizeToggle();
            }
        }
        public void UpdateTechLevelSelection()
        {
            activeTechLevelToggle = TechLevelToggleGroup.ActiveToggles().ToArray().FirstOrDefault();
            if (activeTechLevelToggle != null)
            {
                ActiveTechLevelToggle();
            }
        }
        private void UpdateNotInGame()
        {
            for (int i = 0; i < OnOffToggles.Count; i++)
            {
                if (OnOffToggles[i].isOn == false)
                {
                    switch (i)
                    {
                        case 0:
                            playerFed.text = notInGame;
                            break;
                        case 1:
                            playerRom.text = notInGame;
                            break;
                        case 2:
                            playerKling.text = notInGame;
                            break;
                        case 3:
                            playerCard.text = notInGame;
                            break;
                        case 4:
                            playerDom.text = notInGame;
                            break;
                        case 5:
                            playerBorg.text = notInGame;
                            break;
                        case 6:
                            playerTerran.text = notInGame;
                            break;
                        default:
                            break;
                    }
                }   
            }
        }

        private void ActivePlayerToggle()
        {
            switch (activeLocalPlayerToggle.name.ToUpper())
            {
                case "TOGGLE_FED":
                    FedOnOff.isOn = true;
                    FedOnOff.OnSelect(null);
                    FedLocalPalyerToggle = activeLocalPlayerToggle;
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.FED;
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.FED;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.FED); // To early
                    Debug.Log("Active FedLocalPalyerToggle.");
                    SetLocalCivilization(0);
                    PlaceTheYouInPlayerList(0);
                    break;
                case "TOGGLE_ROM":
                    RomOnOff.isOn = true;
                    RomOnOff.OnSelect(null);
                    RomLocalPlayerToggle = activeLocalPlayerToggle;
                    Debug.Log("Active RomLocalPlayerToggle.");
                    SetLocalCivilization(1);
                    PlaceTheYouInPlayerList(1);
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.ROM;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.ROM);
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.ROM;
                    break;
                case "TOGGLE_KLING":
                    KlingOnOff.isOn = true;
                    KlingOnOff.OnSelect(null);
                    KlingLocalPlayerToggle = activeLocalPlayerToggle;
                    Debug.Log("Active KlingLocalPlayerToggle.");
                    SetLocalCivilization(2);
                    PlaceTheYouInPlayerList(2);
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.KLING;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.KLING);
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.KLING;
                    break;
                case "TOGGLE_CARD":
                    CardOnOff.isOn = true; 
                    CardOnOff.OnSelect(null);
                    CardLocalPlayerToggle = activeLocalPlayerToggle;
                    Debug.Log("Active CardLocalPlayerToggle.");
                    SetLocalCivilization(3);
                    PlaceTheYouInPlayerList(3);
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.CARD;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.CARD);
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.CARD;
                    break;
                case "TOGGLE_DOM":
                    DomOnOff.isOn = true;
                    DomOnOff.OnSelect(null);
                    DomLocalPlayerToggle = activeLocalPlayerToggle;
                    Debug.Log("Active DomLocalPlayerToggle.");
                    SetLocalCivilization(4);
                    PlaceTheYouInPlayerList(4);
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.DOM;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.DOM);
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.DOM;
                    break;
                case "TOGGLE_BORG":
                    BorgOnOff.isOn = true;
                    BorgOnOff.OnSelect(null);
                    BorgLocalPlayerToggle = activeLocalPlayerToggle;
                    Debug.Log("Active BorgLocalPlayerToggle.");
                    SetLocalCivilization(5);
                    PlaceTheYouInPlayerList(5);
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.BORG;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.BORG);
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.BORG;
                    break;
                case "TOGGLE_TERRAN":
                    TerranOnOff.isOn = true;
                    TerranOnOff.OnDeselect(null);
                    TerranLocalPlayerToggle = activeLocalPlayerToggle;
                    Debug.Log("Active TerranLocalPlayerToggle.");
                    SetLocalCivilization(6);
                    PlaceTheYouInPlayerList(6);
                    CivManager.Instance.LocalPlayerCivEnum = CivEnum.TERRAN;
                    //CivManager.Instance.SetLocalPlayerCivController(CivEnum.TERRAN);
                    GameManager.Instance.GameData.LocalPlayerCivEnum = CivEnum.TERRAN;
                    break;
                default:
                    break;
            }
        }
        public void ActiveMapToggle()
        {
            switch (activeMapToggle.name.ToUpper())
            {
                case "TOGGLE_CANON":
                    CanonToggle.isOn = true;
                    CanonToggle.OnSelect(null);
                    CanonToggle = activeMapToggle;
                    //CanonToggle.GetComponent<Image>().color = activeColor;
                    SetMapGalaxyType((int)GalaxyMapType.CANON);
                    break;
                case "TOGGLE_RANDOM":
                    RandomToggle.isOn = true;
                    RandomToggle.OnSelect(null);
                    RandomToggle = activeMapToggle;
                    //RandomToggle.GetComponent<Image>().color = activeColor;
                    SetMapGalaxyType((int)GalaxyMapType.RANDOM);
                    break;
                case "TOGGLE_RING":
                    RingToggle.isOn = true;
                    RingToggle.OnSelect(null);
                    RingToggle = activeMapToggle;
                   // RingToggle.GetComponent<Image>().color = activeColor;
                    SetMapGalaxyType((int)(GalaxyMapType.RING));
                    break;
                default:
                    break;
            }
        }
        public void ActiveGalaxySizeToggle()
        {
            switch (activeGalaxySizeToggle.name.ToUpper())
            {
                case "TOGGLE_SMALL":
                    SmallGalaxyToggle.isOn = true;
                    SmallGalaxyToggle.OnSelect(null);
                    SmallGalaxyToggle = activeGalaxySizeToggle;
                    SetGalaxySize((int)GalaxySize.SMALL);
                    break;
                case "TOGGLE_MEDIUM":
                    MediumGalaxyToggle.isOn = true;
                    MediumGalaxyToggle.OnSelect(null);
                    MediumGalaxyToggle = activeGalaxySizeToggle;
                    SetGalaxySize((int)GalaxySize.MEDIUM);
                    break;
                case "TOGGLE_LARGE":
                    LargeGalaxyToggle.isOn = true;
                    LargeGalaxyToggle.OnSelect(null);
                    LargeGalaxyToggle = activeGalaxySizeToggle;
                    SetGalaxySize((int)GalaxySize.MEDIUM);
                    break;
                case "TOGGLE_PONDEROUS":
                    PonderousGalaxyToggle.isOn = true;
                    PonderousGalaxyToggle.OnSelect(null);
                    PonderousGalaxyToggle = activeGalaxySizeToggle;
                    SetGalaxySize((int)GalaxySize.PONDEROUS);
                    break;
                default:
                    break;
            }
        }
        public void ActiveTechLevelToggle()
        {
            switch (activeTechLevelToggle.name.ToUpper())
            {
                case "TOGGLE_EARLY":
                    EarlyToggle.isOn = true;
                    EarlyToggle.OnSelect(null);
                    EarlyToggle = activeTechLevelToggle;
                    SetTechLevel((int)TechLevel.EARLY);
                    break;
                case "TOGGLE_DEVELOPED":
                    DevelopedToggle.isOn = true;
                    DevelopedToggle.OnSelect(null);
                    DevelopedToggle = activeTechLevelToggle;
                    SetTechLevel((int)TechLevel.DEVELOPED);
                    break;
                case "TOGGLE_ADVANCED":
                    AdvancedToggle.isOn = true;
                    AdvancedToggle.OnSelect(null);
                    AdvancedToggle = activeTechLevelToggle;
                    SetTechLevel((int)TechLevel.ADVANCED);
                    break;
                case "TOGGLE_SUPREME":
                    SupremeToggle.isOn = true;
                    SupremeToggle.OnSelect(null);
                    SupremeToggle = activeTechLevelToggle;
                    SetTechLevel((int)TechLevel.SUPREME);
                    break;
                default:
                    break;
            }
        }
        private void PlaceTheYouInPlayerList(int civInt)
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
                case 6:
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
        private void SetSingleVsMultiplayer(bool singleMultiSelection)
        {
            IsSinglePlayer = singleMultiSelection;
            panelLobby.SetActive(false); 
            panelMuliplayer.SetActive(!singleMultiSelection);
            panelCivSelection.SetActive(singleMultiSelection);
            singlePlayToggleGroup.SetActive(true);
        }
        private void FedOnOffToggleReset()
        { 
            if(FedLocalPalyerToggle.isOn == true)
                FedOnOff.isOn = true;
        }
        private void RomOnOffToggleReset()
        {
            if(RomLocalPlayerToggle.isOn == true)
                RomOnOff.isOn = true;
        }
        private void KlinOnOffToggleReset()
        {
            if (KlingLocalPlayerToggle.isOn == true)
                KlingOnOff.isOn = true;
        }
        private void CardOnOffToggleReset()
        {
            if(CardLocalPlayerToggle.isOn == true)
                CardOnOff.isOn = true;
        }
        private void DomOnOffToggleReset()
        {
            if(DomLocalPlayerToggle.isOn == true)
                DomOnOff.isOn = true;
        }
        private void BorgOnOffToggleReset()
        {
            if(BorgLocalPlayerToggle.isOn == true)
                BorgOnOff.isOn = true;
        }
        private void TerranOnOffToggleReset()
        {
            if(TerranLocalPlayerToggle.isOn == true)
                TerranOnOff.isOn = true;
        }

        private void FedPlayToggleReset()
        {
            if (FedOnOff.isOn == false && FedLocalPalyerToggle.isOn == true)        
                RomLocalPlayerToggle.isOn = true;
        }
        private void RomPlayToggleReset()
        {
            if(RomOnOff.isOn == false && RomLocalPlayerToggle.isOn == true)
                KlingLocalPlayerToggle.isOn = true;
        }
        private void KlingPlayToggleReset()
        {
            if(KlingOnOff.isOn == false && KlingLocalPlayerToggle.isOn == true)
                CardLocalPlayerToggle.isOn = true;
        }
        private void CardPlayToggleReset()
        {
            if(CardOnOff.isOn == false && CardLocalPlayerToggle.isOn == true)
                DomLocalPlayerToggle.isOn = true;
        }
        private void DomPlayToggleReset()
        {
            if(DomOnOff.isOn == false && DomLocalPlayerToggle.isOn == true)
                BorgLocalPlayerToggle.isOn = true;
        }
        private void BorgPlayerToggleReset()
        {
            if(BorgOnOff.isOn == false && BorgLocalPlayerToggle.isOn == true)
                TerranLocalPlayerToggle.isOn = true;
        }
        private void TerranPlayerToggleReset()
        {
            if(TerranOnOff.isOn==false && TerranLocalPlayerToggle.isOn == true)
               FedLocalPalyerToggle.isOn=true;
        }
        private void LoadSavedGame()
        {
            //ToDo
        }
        private void CancelButton()
        {
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(false);
            panelGamePara.SetActive(false);
            panelLobby.SetActive(true);
        }
        private void SaveButton()
        {
            singlePlayToggleGroup.SetActive(true);
            UpdatePlayers();
            UpdateNotInGame();
            panelLobby.SetActive(false);
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(false);
            panelGamePara.SetActive(true);
        }
        private void NextButton()
        {
            //ToDo ***Mulitplayer NEXT Turned off for now
            //panelLobby.SetActive(false);
            //panelMuliplayer.SetActive(false);
            //panelCivSelection.SetActive(true);
            //mulitplayerToggleGroup.SetActive(true); //**** currently set to mulitplayer toggle, not single player
            //panelGamePara.SetActive(false);
        }
        private void ReturnButton()
        {
            ResetPlayers();
            panelLobby.SetActive(false);
            panelMuliplayer.SetActive(false);
            panelCivSelection.SetActive(true);
            panelGamePara.SetActive(false);
        }
        private void SetCivSelectionMenu(CivEnum civEnum)
        {

        }
        private void SetGalaxySize(int index)
        {
            this.MainMenuData.SelectedGalaxySize = (GalaxySize)index;
        }

        private void SetMapGalaxyType(int index)
        {
            this.MainMenuData.SelectedGalaxyType = (GalaxyMapType)index;
        }

        private void SetTechLevel(int index)
        {
            this.MainMenuData.SelectedTechLevel = (TechLevel)index;
        }

        private void SetLocalCivilization(int index)
        {
            //SelectedLocalCivEnum = (CivEnum)index;
            CivManager.Instance.LocalPlayerCivEnum = (CivEnum)((int)index);
        }
        private void LoadGalaxyScene()
        {

            UpdateMapSelection();
            UpdateGalaxySizeSelection();
            UpdateTechLevelSelection();
            PlayableCivOffInGameList();
            CivManager.Instance.UpdatePlayableCivGameList(MainMenuData.InGamePlayableCivList, (int)MainMenuData.SelectedGalaxySize, this.MainMenuData.SelectedGalaxyType);
            mainMenuCanvas.SetActive(false);
            uiCameraGO.SetActive(false);
            galaxyCenter.SetActive(true);
            PastMainMenu = true;
            TimeManager.Instance.ResumeTime();
            SceneManager.LoadScene("GalaxyScene", LoadSceneMode.Additive);
            CivManager.Instance.OnNewGameButtonClicked((int)MainMenuData.SelectedGalaxySize, (int)MainMenuData.SelectedTechLevel, (int)MainMenuData.SelectedGalaxyType,
                (int)CivManager.Instance.LocalPlayerCivEnum, IsSinglePlayer);
         
        }
        private void PlayableCivOffInGameList()
        {
            for (int i = 0; i < OnOffToggles.Count; i++)
            {
                if (OnOffToggles[i].isOn == false)
                {
                    MainMenuData.InGamePlayableCivList[i] = CivEnum.ZZUNINHABITED1;
                }
            }

        }
    }
}

