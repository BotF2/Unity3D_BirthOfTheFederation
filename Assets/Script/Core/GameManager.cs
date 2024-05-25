using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using GalaxyMap;

//using MLAPI;
//using UnityEngine.UI;

namespace Assets.Core
{
    #region Enums
    public enum CivEnum
    {
        FED,
        ROM,
        KLING,
        CARD,
        DOM,
        BORG,
        #region minors
        ACAMARIANS,
        AKAALI,
        AKRITIRIANS,
        ALDEANS,
        ALGOLIANS,
        ALSAURIANS,
        ANDORIANS,
        ANGOSIANS,
        ANKARI,
        ANTEDEANS,
        ANTICANS,
        ARBAZAN,
        ARDANANS,
        ARGRATHI,
        ARKARIANS,
        ATREANS,
        AXANAR,
        BAJORANS,
        BAKU,
        BANDI,
        BANEANS,
        BARZANS,
        BENZITES,
        BETAZOIDS,
        BILANAIANS,
        BOLIANS,
        BOMAR,
        BOSLICS,
        BOTHA,
        BREELLIANS,
        BREEN,
        BREKKIANS,
        BYNARS,
        CAIRN,
        CALDONIANS,
        CAPELLANS,
        CHALNOTH,
        CORIDAN,
        CORVALLENS,
        CYTHERIANS,
        DELTANS,
        DENOBULANS,
        DEVORE,
        DOPTERIANS,
        DOSI,
        DRAI,
        DREMANS,
        EDO,
        ELAURIANS,
        ELAYSIANS,
        ENTHARANS,
        EVORA,
        EXCALBIANS,
        FERENGI,
        FLAXIANS,
        GORN,
        GRAZERITES,
        HAAKONIANS,
        HALKANS,
        HAZARI,
        HEKARANS,
        HIROGEN,
        HORTA,
        IYAARANS,
        JNAII,
        KAELON,
        KAREMMA,
        KAZON,
        KELLERUN,
        KESPRYTT,
        KLAESTRONIANS,
        KRADIN,
        KREETASSANS,
        KRIOSIANS,
        KTARIANS,
        LEDOSIANS,
        LISSEPIANS,
        LOKIRRIM,
        LURIANS,
        MALCORIANS,
        MALON,
        MAQUIS,
        MARKALIANS,
        MERIDIANS,
        MINTAKANS,
        MIRADORN,
        MIZARIANS,
        MOKRA,
        MONEANS,
        NAUSICAANS,
        NECHANI,
        NEZU,
        NORCADIANS,
        NUMIRI,
        NUUBARI,
        NYRIANS,
        OCAMPA,
        ORIONS,
        ORNARANS,
        PAKLED,
        PARADANS,
        QUARREN,
        RAKHARI,
        RAKOSANS,
        RAMATIANS,
        REMANS,
        RIGELIANS,
        RISIANS,
        RUTIANS,
        SELAY,
        SHELIAK,
        SIKARIANS,
        SKRREEA,
        SONA,
        SULIBAN,
        TAKARANS,
        TAKARIANS,
        TAKTAK,
        TALARIANS,
        TALAXIANS,
        TALOSIANS,
        TAMARIANS,
        TANUGANS,
        TELLARITES,
        TEPLANS,
        THOLIANS,
        TILONIANS,
        TLANI,
        TRABE,
        TRILL,
        TROGORANS,
        TZENKETHI,
        ULLIANS,
        VAADWAUR,
        VENTAXIANS,
        VHNORI,
        VIDIIANS,
        VISSIANS,
        VORGONS,
        VORI,
        VULCANS,
        WADI,
        XANTHANS,
        XEPOLITES,
        XINDI,
        XYRILLIANS,
        YADERANS,
        YRIDIANS,
        ZAHL,
        ZAKDORN,
        ZALKONIANS,
        ZIBALIANS,
        #endregion
        TER,
        ZZUNINHABITED1,
        #region More Uninhabited
        ZZUNINHABITED2,
        ZZUNINHABITED3,
        ZZUNINHABITED4,
        ZZUNINHABITED5,
        ZZUNINHABITED6,
        ZZUNINHABITED7,
        ZZUNINHABITED8,
        ZZUNINHABITED9,
        ZZUNINHABITED10,
        #endregion
    }
    public enum GalaxyType
    {
        CANON,
        RANDOM
    }
    public enum GalaxySize
    {
        SMALL,
        MEDIUM,
        LARGE
    }
    public enum TechLevel
    {
        EARLY,
        DEVELOPED,
        ADVANCED,
        SUPREME
    }

    public enum SystemData
    {
        Sys_Int,
        X_Vector3,
        Y_Vector3,
        Z_Vector3,
        Name,
        Civ_Owner,
        Sys_Type,
        Star_Type,
        Planet_1,
        Moons_1,
        Planet_2,
        Moons_2,
        Planet_3,
        Moons_3,
        Planet_4,
        Moons_4,
        Planet_5,
        Moons_5,
        Planet_6,
        Moons_6,
        Planet_7,
        Moons_7,
        Planet_8,
        Moons_8
    }

    public enum ShipType
    {
        Scout,
        Destroyer,
        Cruiser,
        LtCruiser,
        HvyCruiser,
        Transport,
        OneMore
    }

    public enum StarSystemType
    {
        Blue,
        White,
        Yellow,
        Orange,
        Red,
        Nebula,
        OmarianNebula,
        Station,
        UniComplex,
        BlackHole,
        WormHole,//???

    }
    public enum PlanetType
    {
        H_uninhabitable,
        J_gasGiant,
        M_habitable,
        L_marginalForLife,
        K_marsLike,
        Moon
       
    }
    public enum Orders
    {
        Engage,
        Rush,
        Retreat,
        Formation,
        ProtectTransports,
        TargetTransports
    }
    #endregion

    public class GameManager : MonoBehaviour
    {
        public MainMenuUIController mainMenuUIController;
        //public FleetController fleetController;
        public GalaxyManager galaxyManager;


        List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
        public bool _weAreFriend = false;
        public bool _warpingInIsOver = false; // WarpingInCompleted() called from E_Animator3 sets true and set false again in CombatCompleted state in BeginState
        public bool _isSinglePlayer;
        public CivEnum _localPlayer;
        public GameObject CivilizationPrefab;
        public CivManager civManager;
        //public Civilization _cliantZero;
        //public Civilization _cliantOne;
        //public Civilization _cliantTwo;
        //public Civilization _cliantThree;
        //public Civilization _cliantFour;
        //public Civilization _cliantFive;
        //public Civilization _cliantSix;
        public GalaxySize _galaxySize;
        public GalaxyType _galaxyType;
        public TechLevel _techLevel;
        public int _galaxyStarCount; 
        public int _solarSystemID;
        public Orders _combatOrder;


        public GameObject galaxyMapBackgroundPictureGO;



        public static Dictionary<int, GameObject> CombatObjects = new Dictionary<int, GameObject>();
        public Galaxy galaxy; // = new Galaxy(GameManager.Instance, GalaxyType.ELLIPTICAL, 20);
        public GalaxyView galaxyView;
        public SolarSystemView solarSystemView;
        public Ship ship;
        public CameraMultiTarget cameraMultiTarget;
        public Combat combat;
        // public CameraManagerGalactica cameraManagerGalactica;
        //public Camera galacticCamera; 
        public InstantiateCombatShips instantiateCombatShips;
        public ActOnCombatOrder actOnCombatOrder;
        public ZoomCamera zoomCamera;
        public GameObject Canvas;
        public GameObject CanvasGalactic;
        private GameObject PanelLobby_Menu;
        private GameObject PanelLoadGame_Menu;
        private GameObject PanelSaveGame_Menu;
        private GameObject PanelSettings_Menu;
        private GameObject PanelCredits_Menu;
        private GameObject PanelMain_Menu;
        private GameObject PanelMultiplayerLobby_Menu;
        //private GameObject PanelGalaxy;
        //private GameObject PanelGalactic_Map; 
        public GameObject PanelSystem_Play;
        private GameObject PanelGalactic_Completed;
        private GameObject PanelCombat_Menu;
        private GameObject PanelCombat_Play;
        private GameObject PanelCombat_Completed;
        private GameObject PanelGameOver;

            
        public SinglePlayer _SinglePlayer;
        public MultiPlayer _MultiPlayer;
        public LoadGamePanel _LoadGamePanel;
        public SaveGamePanel _SaveGamePanel;
        public SettingsGamePanel _SettingsGamePanel;
        public ExitQuit _ExitQuit;
        public CreditsGamePanel _CreditsGamePanel;
        public CombatOrderSelection combatOrderSelection;

        public float shipScale = 2000f; // old LoadCombatData Combat
        private char separator = ',';
        public static Dictionary<string, int[]> ShipDataDictionary = new Dictionary<string, int[]>();
        //public static Dictionary<string, string[]> SystemDataDictionary = new Dictionary<string, string[]>();

        public GameObject animFriend1;
        public GameObject animFriend2;
        public GameObject animFriend3;
        public GameObject animEnemy1;
        public GameObject animEnemy2;
        public GameObject animEnemy3;

        public GameObject Friend_0; // prefab empty gameobject to clone instantiat into the grids
        public GameObject Enemy_0;
        private GameObject[] _cameraTargets; // = new GameObject [] { Friend_0, Enemy_0 };
        public int yFactor = 3000; // old LoadCombatData combat, gap in grid between empties on y axis
        public int zFactor = 3000;
        public int offsetFriendLeft = -5500; // listSONames of x axis for friend grid left side (start here), world location
        public int offsetFriendRight = 5800; // listSONames of x axis for friend grid right side, world location
        public int offsetEnemyRight = 5500; // start here
        public int offsetEnemyLeft = -5800;

        #region prefab ships and stations


        public static Dictionary<string, GameObject> PrefabShipDitionary;

       // public GameObject GALACTIC_Center; // do not need a galactic center system button
        public List<GameObject> AllSystemsList;
        public static Dictionary<string, GameObject> PrefabStarSystemDitionary;
        #endregion
        //public Sprite FedCiv
        #region Animation empties by ship type Now from ActOnCombatOrder.cs?
        //public GameObject FriendScout_Y0_Z0;
        //public GameObject FriendDestroyer_Y0_Z1;
        //public GameObject FriendCapital_Y0_Z2;
        //public GameObject FriendColony_Y1_Z0;
        //public GameObject Friend_Y1_Z1;
        //public GameObject Friend_Y1_Z2;
        //public GameObject EnemyScout_Y0_Z0;
        //public GameObject EnemyDestroyer_Y0_Z1;
        //public GameObject EnemyCapital_Y0_Z2;
        //public GameObject EnemyColony_Y1_Z0;
        //public GameObject Enemy_Y1_Z1;
        //public GameObject Enemy_Y1_Z2;

        public GameObject[] animationEmpties = new GameObject[12]; // Populated in Unity Hierarchy under Combat for animation empty objexts
                                                                   // { FriendScout_Y0_Z0, 
                                                                   //    FriendDestroyer_Y0_Z1, FriendCapitalShip_Y0_Z2, FriendColony_Y1_Z0, Friend_Y1_Z1, Friend_Y1_Z2, 
                                                                   //    EnemyScout_Y0_Z0, EnemyDestroyer_Y0_Z1, EnemyCapital_Y0_Z2, EnemyColony_Y1_Z0, Enemy_Y1_Z1, Enemy_Y1_Z2 };
                                                                   // Unity does not like c# lists
        #endregion

        public static List<string> StartGameObjectNames = new List<string>();
        public static Dictionary<int, GameObject> CurrentGameObjects = new Dictionary<int, GameObject>(); // not used yet

        //ToDo: move all these to combatEngine class?
        public  string[] FriendNameArray; // For current Combat ****
        public  string[] EnemyNameArray;

        public int friends;
        public int enemies;
        public  List<GameObject> FriendShips = new List<GameObject>();  // updated to current combat
        public  List<GameObject> EnemyShips = new List<GameObject>();

        private int friendShipLayer;
        private int enemyShipLayer;


        //public Dictionary<GameObject, GameObject[]> _shipTargetDictionary;  // key ship gameObject, listSONames Destination gameObject (problem, is loaded inside LoadCombat()
     

        public static GameManager Instance { get; private set; } // a static singleton, no other script can instatniate a GameManager, must us the singleton

        //List<Tuple<CombatUnit, CombatWeapon[]>> // will we need to us this here too?
        public enum State { LOBBY_MENU, LOBBY_INIT, LOAD_MENU, SAVE_MENU, SETTINGS_MENU, CREDITS_MENU, MAIN_MENU, MAIN_INIT, MULTIPLAYER_MENU, 
                            SYSTEM_PLAY_INIT, GALACTIC_MAP, GALACTIC_MAP_INIT, SYSTEM_PLAY, GALACTIC_COMPLETED,
                            COMBAT_MENU, COMBAT_INIT, COMBAT_PLAY, COMBAT_COMPLETED, GAMEOVER };


        public TimeManager timeManager;


        public State _state;

        private int _level;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        bool _isSwitchingState = false;

        public bool _statePassedLobbyInit = false;
        public bool _statePassedMain_Init = false;
        public bool _statePassedCombatMenu_Init = false;
        public bool _statePassedCombatInit = false; // COMBAT INIT
        public bool _statePassedCombatPlay = false;
        

        public void InitializeGameManagerWithMainMenuUIController() {
            mainMenuUIController = GameObject.Find("MainMenuUIController").GetComponent<MainMenuUIController>();
            _galaxySize = mainMenuUIController.selectedGalaxySize;
            _galaxyType = mainMenuUIController.selectedGalaxyType;
            _techLevel = mainMenuUIController.selectedTechLevel;
            _localPlayer = mainMenuUIController.selectedCivEnum;
        }

        private void Awake()
        {
            Instance = this; // static reference to single GameManager
            Canvas = GameObject.Find("Canvas"); 
            CanvasGalactic = GameObject.Find("CanvasGalactic");

            InitializeGameManagerWithMainMenuUIController();
        }
        void Start()
        {
           /* SwitchtState(State.LOBBY_MENU);
            if (SaveLoadManager.hasLoaded)
            {
                // get respons with locations... SaveManager.activeSave.(somethings here from save data)
            }
           */


            LoadShipData(Environment.CurrentDirectory + "\\Assets\\" + "ShipData.txt"); // populate prefabs
            //LoadSystemData(Environment.CurrentDirectory + "\\Assets\\" + "SystemData.txt");                                                                            // ToDo: LoadSystemData(Environment.CurrentDirectory + "\\Assets\\" + "SystemData.txt");
            LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"EarlyGameObjectData.txt");
            LoadPrefabs();

            //_galaxySize = GalaxySize.SMALL;
            //_localPlayer = civManager.CreateLocalPlayer();

            if (_isSinglePlayer)
                _weAreFriend = true; // ToDo: Need to sort out friend and enemy in multiplayer civilizations local player host and clients 
                                     //galacticCamera = cameraManagerGalactica.LoadGalacticCamera();
                                     // Galaxy galaxy = new Galaxy();
                                     // Galaxy = galaxy;
            

        }

        public void BackToLobbyClick()  // from Main Menu
        {
            _statePassedLobbyInit = false;
            SwitchtState(State.LOBBY_MENU);
            _LoadGamePanel.ClosePanel();
        }

        public void SinglePlayerLobbyClicked() // go to main menu through LOBBY_INIT
        {
            SwitchtState(State.LOBBY_INIT); // start process to open main menu
            _isSinglePlayer = true;
        }
        public void MultiPlayerLobbyClicked()
        {
            SwitchtState(State.MULTIPLAYER_MENU);
            _isSinglePlayer = false;
            //ToDo: network manager here IsHost IsLocalPlayer or in BeginState??
        }
        public void LoadSavedGameClicked()
        {
            SwitchtState(State.LOAD_MENU);
            _LoadGamePanel.OpenPanel();
        }
        public void SaveGameClicked()
        {
            SwitchtState(State.SAVE_MENU);
            _SaveGamePanel.OpenPanel();
        }
        public void SettingsClicked()
        {
            SwitchtState(State.SETTINGS_MENU);
            _SettingsGamePanel.OpenPanel();
        }
        public void CreditsClicked()
        {
            SwitchtState(State.CREDITS_MENU);
            _CreditsGamePanel.OpenPanel();
        }
        public void ExitClicked()
        {
            _ExitQuit.ExitTheGame();

        }
        public void ChangeSystemClicked(int systemID, SolarSystemView ssView) //(SolarSystemView ssView)
        {
            PanelLobby_Menu.SetActive(false);
            _solarSystemID = systemID;
            solarSystemView = ssView;
            SwitchtState(State.SYSTEM_PLAY);
            for (int i = 0; i < AllSystemsList.Count; i++)
            {
                if (systemID != i & AllSystemsList[i] != null)
                AllSystemsList[i].SetActive(false);
            }

            // ToDo: get Empire and techlevel from MainMenu
        }
        //public void GalaxyPlayClicked() // BOLDLY GO button in Main Menu
        //{

        //    Debug.Log("civManager " + civManager.gameObject.FleetName);

        //    civManager.CreateNewGameBySelections(0); // (int)_galaxySize);
        //    //PanelGalaxy.SetActive(true);
        //  ///  _localPlayer = civManager.CreateLocalPlayer();
        //    // turned off Galaxys here: SwitchtState(State.MAIN_INIT);
        //    // open Combat for now
        //    SwitchtState(State.GALACTIC_MAP_INIT);


        //}

        public void GalaxyMapClicked() // in system going back to galactic map
        {

           // PanelGalactic_Map = CanvasGalactic.transform.Find("PanelGalactic_Map").gameObject;
            SwitchtState(State.SYSTEM_PLAY_INIT); // end systeme, then load galaxy map
            //PanelGalactic_Map.SetActive(true);
        }
        public void TurnOnGalacticSystems(bool offOn)
        {
            // a loop here through all systems setting them active = true
            //for (int i = 0; i < _galaxyStarCount; i++)
            //{
            //    AllSystemsList[i].SetActive(true);
            //    if (i != 0)
            //     ActiveSystemList.Add(AllSystemsList[i]);
            //}
            //System_FEDERATION.SetActive(offOn);
            //System_ROMULANS.SetActive(offOn);
            // System_KLINGONS.SetActive(offOn);
        }
        public void SetGalaxyMapSize() // 
        {
            switch (_galaxySize)
            {
                case GalaxySize.SMALL:
                    _galaxyStarCount = 6; // 30;
                   // LoadGalacticMapButtons("SMALL"); // system buttons are loaded in GalaxyView.cs
                    break;
                case GalaxySize.MEDIUM:
                    _galaxyStarCount = 40;
                    //LoadGalacticMapButtons("MEDIUM");
                    break;
                case GalaxySize.LARGE:
                    _galaxyStarCount = 50;
                    //LoadGalacticMapButtons("LARGE");
                    break;
                default:
                    break;
            }
        }
        public void LoadGalacticMapButtons(string mapsize)
        {
            //switch (mapsize)
            //{
            //    case "SMALL":                   
            //        break;

            //    case "MEDIUM":
            //        break;

            //    case "LARGE":
            //        break;

            //    default:
            //        break;
            //}
        }

        public void EndGalacticPlayClicked()
        {
            SwitchtState(State.GALACTIC_COMPLETED);
        }

        public void CombatPlayClicked()
        {
            SwitchtState(State.COMBAT_INIT);
        }
        public void ResetFriendAndEnemyDictionaries()
        {
            FriendShips.Clear();
            EnemyShips.Clear();
        }
        public void SwitchtState(State newState, float delay = 0)
        {
            StartCoroutine(SwitchDelay(newState, delay));
            Instance = this;
            EndState();
            BeginState(newState);
            _isSwitchingState = false;
        }
        IEnumerator SwitchDelay(State newState, float delay)
        {
            _isSwitchingState = true;
            yield return new WaitForSeconds(delay);
            //EndState();
            _state = newState;
            //BeginState(newState);
            _isSwitchingState = false;
        }
        // Unity Inspector only sees non static public void methodes with no parameter or paramater float, int, string, bool or UnityEntine.Object


        //  MARC CODE
        public GameObject UICamera;
        public GameObject GalaxyCamera;
        void BeginState(State newState)
        {

            switch (newState)
            {
                case State.LOBBY_MENU:
                    PanelMain_Menu.SetActive(false); // turn off if returning to lobby
                    PanelLoadGame_Menu.SetActive(false);
                    PanelSaveGame_Menu.SetActive(false);
                    PanelSettings_Menu.SetActive(false);
                    PanelCredits_Menu.SetActive(false);
                    PanelLobby_Menu.SetActive(true); // Lobby first             
                    break;

                case State.LOBBY_INIT:
                    SwitchtState(State.MAIN_MENU);
                    _statePassedLobbyInit = true;
                    switch (_isSinglePlayer) // Do we need this? Methods, SinglePlayerLobbyClicked() MultipPalyerLobbyClicked(), already set bool and called LobbyInit
                    {
                        case true:
                            break;
                        case false: //Do something here, start multiplayer?
                            break;
                        default:
                            //break;
                    }
                    break;
                case State.LOAD_MENU:
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    //PanelSaveGame_Menu.SetActive(false);
                    PanelLoadGame_Menu.SetActive(true);
                    break;
                case State.SAVE_MENU:
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelSaveGame_Menu.SetActive(true);
                    break;
                case State.SETTINGS_MENU:
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelSettings_Menu.SetActive(true);
                    break;
                case State.CREDITS_MENU:
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelCredits_Menu.SetActive(true);
                    break;
                case State.MAIN_MENU:
                    PanelLoadGame_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(true);
                    break;
                case State.MULTIPLAYER_MENU:
                    PanelLobby_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(true);
                    break;
                case State.MAIN_INIT:
                    switch (_galaxyType) // ToDo: get input from Main Menu
                    {
                        case GalaxyType.CANON:
                            // canon type galaxy.cs SolarSystemsMap dictionary
                            SetGalaxyMapSize(); // set number of stars this._galaxyStarCount int
                            break;
                        case GalaxyType.RANDOM:
                            SetGalaxyMapSize();                   
                            break;
                    }
                   
                    switch (_localPlayer) // is set in CivSelection.cs for GameManager.localPlayer
                    {
                        //case Civilization.FED: // we already know local player from CivSelection.cs so do we change to a race UI/ ship/ economy here??
                        //    // set 
                        //    break;
                        //case Civilization.TER:
                        //    break;
                        //case Civilization.ROM:
                        //    break;
                        //case Civilization.KLING:
                        //    break;
                        //case Civilization.CARD:
                        //    break;
                        //case Civilization.DOM:
                        //    break;
                        //case Civilization.BORG:
                        //    break;
                        default:
                            break;
                    }
                    PanelMain_Menu.SetActive(false);
                    PanelLobby_Menu.SetActive(false);
                    PanelLoadGame_Menu.SetActive(false);
                    PanelSaveGame_Menu.SetActive(false);
                    _statePassedMain_Init = true;
                    SwitchtState(State.GALACTIC_MAP);
                    break;
                case State.GALACTIC_MAP:
                    PanelMain_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(false);
                    _statePassedMain_Init = true;
                    PanelSystem_Play.SetActive(false);
                    break;
                case State.GALACTIC_MAP_INIT:
                    SwitchtState(State.SYSTEM_PLAY);
                    break;
                case State.SYSTEM_PLAY:
                    galaxyMapBackgroundPictureGO.SetActive(true);
                    UICamera.SetActive(false);
                    GalaxyCamera.SetActive(true);
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(false);
                    _statePassedMain_Init = true;
                    break;
                case State.SYSTEM_PLAY_INIT:
                    PanelSystem_Play.SetActive(false);
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(false);
                    _statePassedMain_Init = true;

                    break;
                case State.GALACTIC_COMPLETED:
                    PanelSystem_Play.SetActive(false);
                    PanelLobby_Menu.SetActive(false);
                    PanelSystem_Play.SetActive(false);
                    SwitchtState(State.COMBAT_MENU);
                    break;
                case State.COMBAT_MENU:
                    //PanelLobby_Menu.SetActive(false);
                    //PanelCombat_Menu.SetActive(true);
                    //PanelSystem_Play.SetActive(false);                    
                    LoadFriendAndEnemyNames(); // for combat
                    break;
                case State.COMBAT_INIT:
                    //PanelLobby_Menu.SetActive(false);
                    //_statePassedCombatMenu_Init = true;
                    //FriendShips = combat.UpdateFriendCombatants().ToList();
                    //EnemyShips = combat.UpdateEnemyCombatants().ToList();
                    //actOnCombatOrder.CombatOrderAction(_combatOrder, FriendShips, EnemyShips);
                    //instantiateCombatShips.SetCombatOrder(_combatOrder);
                    //instantiateCombatShips.PreCombatSetup(FriendNameArray, true);
                    //instantiateCombatShips.PreCombatSetup(EnemyNameArray, false);
                    //_statePassedCombatInit = true;
                    //SetCameraTargets();
                    //zoomCamera.ZoomIn();
                    //PanelCombat_Menu.SetActive(false);
                    //PanelCombat_Play.SetActive(true);
                    //SwitchtState(State.COMBAT_PLAY);
                    break;
                case State.COMBAT_PLAY:
                    //PanelLobby_Menu.SetActive(false);
                    //_statePassedCombatPlay = true;
                    break;
                case State.COMBAT_COMPLETED:
                    //PanelLobby_Menu.SetActive(false);
                    //_warpingInIsOver = false;
                    //PanelCombat_Completed.SetActive(true);
                    //if (false)// requirments for game over here
                    //    SwitchtState(State.GAMEOVER);
                    //else
                    //{
                    //    SwitchtState(State.SYSTEM_PLAY);
                    //    _statePassedCombatInit = true;
                    //    _statePassedCombatMenu_Init = false;
                    //    zoomCamera.TurnOfZoomerUpdate();
                    //}
                    //break;
                case State.GAMEOVER:
                    PanelGameOver.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //zoomCamera.CheckUpdateZoom();
            switch (_state)
            {
                case State.LOBBY_MENU:
                    break;
                case State.LOBBY_INIT:
                    break;
                case State.LOAD_MENU:
                    break;
                case State.SAVE_MENU:
                    break;
                case State.SETTINGS_MENU:
                    break;
                case State.CREDITS_MENU:
                    break;
                case State.MAIN_MENU:
                    break;
                case State.MAIN_INIT:
                    _statePassedMain_Init = true;
                    break;
                case State.MULTIPLAYER_MENU:
                    break;
                case State.GALACTIC_MAP:
                    PanelLobby_Menu.SetActive(false);
                    _statePassedMain_Init = true;
                    break;
                case State.GALACTIC_MAP_INIT:
                    PanelLobby_Menu.SetActive(false);
                    //PanelGalactic_Map.SetActive(false);
                    _statePassedMain_Init = true;
                    break;
                case State.SYSTEM_PLAY:
                    PanelLobby_Menu.SetActive(false);
                    //PanelGalactic_Map.SetActive(false);
                    _statePassedMain_Init = true;
                    break;
                case State.SYSTEM_PLAY_INIT:
                    //PanelGalactic_Map.SetActive(false);
                    _statePassedMain_Init = true;
                    break;
                case State.GALACTIC_COMPLETED:
                    PanelLobby_Menu.SetActive(false);
                    break;
                case State.COMBAT_MENU:
                    PanelLobby_Menu.SetActive(false);
                    // ToDo: end combat
                    //if (enemies are == 0 || friends are == 0)
                    //    {
                    //    End Combat
                    //}
                    break;
                case State.COMBAT_INIT:
                    //if (F_Animator3.)
                    //instantiateCombatShips.PreCombatSetup(EnemyNameArray, false);
                    //_statePassedCombatInitRight = true;
                    break;
                case State.COMBAT_PLAY:
                    // _statePassedInit = true;
                    break;
                case State.COMBAT_COMPLETED:
                    break;
                //case State.LOADNEXT:
                //    break;
                case State.GAMEOVER:
                    // _statePassedInit = false;
                    break;
                default:
                    break;
            }
        }
        void EndState()
        {
            switch (_state)
            {
                case State.LOBBY_MENU:
                 //   PanelLobby_Menu.SetActive(false);
                    break;
                case State.LOAD_MENU:
                  //  PanelLoadGame_Menu.SetActive(false);
                    break;
                case State.SAVE_MENU:
                 //   PanelSaveGame_Menu.SetActive(false);
                    break;
                case State.SETTINGS_MENU:
                //    PanelSettings_Menu.SetActive(false);
                    break;
                case State.CREDITS_MENU:
                 //   PanelCredits_Menu.SetActive(false);
                    break;
                case State.LOBBY_INIT: // no init panles to turn off
                    break;
                case State.MAIN_MENU:
                //    PanelMain_Menu.SetActive(false);
                    break;
                case State.MAIN_INIT:

                    break;
                case State.MULTIPLAYER_MENU:
                //    PanelMultiplayerLobby_Menu.SetActive(false);
                    break;
                case State.GALACTIC_MAP:
              //      PanelLobby_Menu.SetActive(false);
                    //PanelGalactic_Map.SetActive(false);
                    break;
                case State.GALACTIC_MAP_INIT:
             //       PanelLobby_Menu.SetActive(false);
                    //PanelGalactic_Map.SetActive(false);
                    break;
                case State.SYSTEM_PLAY:
                    PanelSystem_Play.SetActive(false);
                    break;
                case State.SYSTEM_PLAY_INIT:
                //    PanelLobby_Menu.SetActive(false);
                    break;
                case State.GALACTIC_COMPLETED:
                    PanelSystem_Play.SetActive(false);
                    PanelGalactic_Completed.SetActive(false);
                    break;
                case State.COMBAT_MENU:
                    //panelGalactic_Play.SetActive(false);
                    PanelCombat_Menu.SetActive(false);
                    break;
                case State.COMBAT_INIT:
                    PanelCombat_Menu.SetActive(false);
                    // panelGalactic_Completed.SetActive(false);
                    break;
                case State.COMBAT_PLAY:
                    PanelCombat_Play.SetActive(false);
                    break;
                case State.COMBAT_COMPLETED:
                    PanelCombat_Completed.SetActive(false);
                    break;
                //case State.LOADNEXT:
                //    break;
                case State.GAMEOVER:
                    // panelCombat_Play.SetActive(false); // ToDo: get Combat to return to Galactic on Combat_Completed
                    PanelGameOver.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        public void SetCameraTargets()
         {
            //List<GameObject> _cameraTargets = new List<GameObject>() { Friend_0, Enemy_0}; // dummies
           
            //List<GameObject> multiTargets = instantiateCombatShips.GetCameraTargets(); // get list - array for CameraMultiTarget
            //List<GameObject> survivingTargets = new List<GameObject>();
            //if (multiTargets.Count() > 0)
            //{
            //    for (int i = 0; i < multiTargets.Count; i++)
            //    {
            //        if (multiTargets[i] != null)
            //        {
            //            survivingTargets.Add(multiTargets[i]);
            //        }
            //    }
                
            //    _cameraTargets.AddRange(survivingTargets);
            //}
          
            //cameraMultiTarget.SetTargets(_cameraTargets.ToArray()); // start multiCamera - main camers before warp in of ships
        }
        public void ProvideFriendCombatShips(int numIndex, GameObject daObject)
        {
            FriendShips.Add(daObject); // geting friend combat ship dictionary for combat
        }
        public void ProvideEnemyCombatShips(int numIndex, GameObject daObject)
        {
            EnemyShips.Add(daObject);
        }
        public void WarpingInCompleted()
        {
            _warpingInIsOver = true;
        }
        public void SetShipLayer()
        {
            List<GameObject> allDaShipObjectInCombat = new List<GameObject>();
            allDaShipObjectInCombat = FriendShips;
            for (int i = 0; i < EnemyShips.Count; i++)
            {
                allDaShipObjectInCombat.Add(EnemyShips[i]);
            }
            
            foreach (var shipGameObject in allDaShipObjectInCombat)
            {
                var arrayOfName = shipGameObject.name.ToUpper().Split('_');
                shipGameObject.layer = SetShipLayer(arrayOfName[0]);
                
            } 
        }

        public int SetShipLayer(string civ)
        {
            switch (civ)
            {
                case "FED":
                    {
                        return 10;

                    }
                case "TER":
                    {
                        return 11;

                    }
                case "ROM":
                    {
                        return 12;

                    }
                case "KLING":
                    {
                        return 13;

                    }
                case "CARD":
                    {
                        return 14;

                    }
                case "DOM":
                    {
                        return 15;

                    }
                case "BORG":
                    {
                        return 16;

                    }
                default:
                    return 10;

            }
        }
        public Transform GetShipTravelTarget(GameObject aShip)
        {
            return aShip.transform;
        }
        public void LoadFriendAndEnemyNames()
        {
            //string[] _friendNameArray = new string[] { "FED_CRUISER_II", "FED_CRUISER_III", "FED_DESTROYER_II", "FED_DESTROYER_II",
            //    "FED_DESTROYER_I", "FED_SCOUT_II", "FED_SCOUT_IV" , "FED_COLONYSHIP_I" };
            //FriendNameArray = _friendNameArray;
            //string[] _enemyNameArray = new string[] {"KLING_DESTROYER_I", "KLING_DESTROYER_I", "KLING_CRUISER_II", "KLING_SCOUT_II", "KLING_COLONYSHIP_I","CARD_SCOUT_I",
            //    "ROM_CRUISER_III", "ROM_CRUISER_II", "ROM_SCOUT_III"}; //"KLING_DESTROYER_I",
            
            //EnemyNameArray = _enemyNameArray;
        }

        #region Read Tech era in TechSelection.cs (Ship)GameObjectData.txt
        public void LoadStartGameObjectNames(string filename) //****  from TechSelection.cs ToDo: read for selected tech level
        {
            List<string> _startGameObjectNames = new List<string>();
            var file = new FileStream(filename, FileMode.Open, FileAccess.Read);

            var _dataPoints = new List<string>();
            using (var reader = new StreamReader(file))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        continue;
                    _dataPoints.Add(line.Trim());
                    if (line.Length > 0)
                    {
                        var coll = line.Split(separator);

                        string currentValueZero = coll[0];

                        string[] shipDataArray = new string[] { currentValueZero };

                        _startGameObjectNames.Add(coll[0].ToString().ToUpper());
                    }
                }

                reader.Close();
                StartGameObjectNames = _startGameObjectNames;
            }
        }
        public void LoadPrefabs()
        {
            Dictionary<string, GameObject> tempPrefabDitionary = new Dictionary<string, GameObject>() // !! only try to load prefabs that exist
            {
                //{ "FED_DESTROYER_I", Fed_Destroyer_i }, { "FED_SCOUT_II", Fed_Scout_ii },
                //{ "FED_CRUISER_II", Fed_Cruiser_ii }, { "FED_DESTROYER_II", Fed_Destroyer_ii }, // { "FED_SCOUT_II", Fed_Scout_ii },
                //{ "FED_CRUISER_III", Fed_Cruiser_iii }, {"FED_SCOUT_IV", Fed_Scout_iv},//{ "FED_DESTROYER_III", Fed_Destroyer_iii }, { "FED_SCOUT_III", Fed_Scout_iii },
                //{ "FED_COLONYSHIP_I", Fed_Colonyship_i }, 
                //{ "KLING_DESTROYER_I", Kling_Destroyer_i},
                //{ "KLING_CRUISER_II", Kling_Cruiser_ii }, { "KLING_SCOUT_II", Kling_Scout_ii }, {"KLING_COLONYSHIP_I", Kling_Colonyship_i},
                //{ "CARD_SCOUT_I", Card_Scout_i },
                //{ "ROM_SCOUT_III", Rom_Scout_iii },
                //{ "ROM_CRUISER_II", Rom_Cruiser_ii }, { "ROM_CRUISER_III", Rom_Cruiser_iii }
            };
            if (PrefabShipDitionary == null) // do not load twice
                PrefabShipDitionary = tempPrefabDitionary;

            Dictionary<string, GameObject> systemPrefabDitionary = new Dictionary<string, GameObject>() // !! only try to load prefabs that exist
            {
                
            };
            
            if (PrefabStarSystemDitionary == null)
            {
                PrefabStarSystemDitionary = systemPrefabDitionary;
            }
        }

        #endregion
        public void LoadShipData(string filename)
        {
            #region Read ShipData.txt 

            Dictionary<string, int[]> _shipDataDictionary = new Dictionary<string, int[]>();
            var file = new FileStream(filename, FileMode.Open, FileAccess.Read);

            var _dataPoints = new List<string>();
            using (var reader = new StreamReader(file))
            {
                //Note1("string", int, int, int, int, int"---------------  reading __to_PLZ_DB.txt (from file)");
                //string infotext = "---------------  reading __to_PLZ_DB.txt (from file)";
                //Console.WriteLine(infotext);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        continue;
                    _dataPoints.Add(line.Trim());
                    //int[] _shipInts = new int[4];
                    if (line.Length > 0)
                    {
                        var coll = line.Split(separator);

                        _ = int.TryParse(coll[2], out int currentValueOne);
                        _ = int.TryParse(coll[4], out int currentValueTwo);
                        _ = int.TryParse(coll[6], out int currentValueThree);
                        _ = int.TryParse(coll[8], out int currentValueFour);
                        _ = int.TryParse(coll[10], out int currentValueFive);
                        int[] shipDataArray = new int[] { currentValueOne, currentValueTwo, currentValueThree, currentValueFour, currentValueFive };

                        _shipDataDictionary.Add(coll[0].ToString(), shipDataArray);
                        //_shipInts.Clear();
                    }
                }

                reader.Close();
                ShipDataDictionary = _shipDataDictionary;
                //StaticStuff staticStuffToLoad = new StaticStuff();
                //staticStuffToLoad.LoadStaticShipData(_shipDataDictionary);
            }
            #endregion
        }


        //private Vector3 HomeSystemTrans(string objectName)
        //{
        //    //ToDo: where is everyone?
        //    var coll = objectName.Split(separator);

        //    string currentValueZero = coll[0].ToUpper();
        //    switch (currentValueZero)
        //    {
        //        case "SOL":
        //            return new Vector3(0, 0, 0);    
        //        case "TERRA":
        //            return new Vector3(0, 0, 1);
        //        case "ROMULUS":
        //            return new Vector3(0, 0, 2);
        //        case "KRONOS":
        //            return new Vector3(0, 0, 3);
        //        case "CARDASSIA":
        //            return new Vector3(0, 0, 4);
        //        case "OMARIAN":
        //            return new Vector3(0, 0, 5);
        //        case "UNIMATRIX":
        //            return new Vector3(0, 0, 6);
        //        default:
        //            return new Vector3(0, 0, 07);
        //    }
        //}
    
    }
}