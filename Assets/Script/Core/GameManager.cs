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
        TERRAN,
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
    //public enum Civilization
    //{
    //    FED,
    //    TERRAN,
    //    ROM,
    //    KLING,
    //    CARD,
    //    DOM,
    //    BORG
    //}
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
    //public enum HomeSystem
    //{
    //    SOL,
    //    TERRA,
    //    ROMULUS,
    //    KRONOS,
    //    CARDASSIA,
    //    OMARIAN_NEBULA,
    //    DELTA_PRIME
    //}
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
    public enum FriendOrFoe
    {
        friend,
        enemy
    }

    public enum ShipType
    {
        Scout,
        Destroyer,
        Cruiser,
        LtCruiser,
        HvyCruiser,
        Transport,
        Colonyship,
        Construction,
        OneMore
    }
    public enum SystemType
    {
        SolarSystem,
        NebulaSystem,
        ComplexSystem,
        BlackHoleSystem,
    }
    public enum StarType
    {
        Blue,
        White,
        Yellow,
        Orange,
        Red,
        Nebula,
        OmarianNebula,
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
       //// private GameObject SystemGalacticCore;
       // public GameObject System_FEDERATION ;
       //// private GameObject System_TERRANEMPIRE ;
       // public GameObject System_ROMULANS ;
       // public GameObject System_KLINGONS ;
       // public GameObject System_CARDASSIANS ;
       // public GameObject System_DOMINION ;
       // public GameObject System_BORG ;
       // public GameObject System_ACAMARIANS ;
       // public GameObject System_AKAALI ;
       // public GameObject System_AKRITIRIANS ;
            
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
        public GameObject Borg_Destroyer_i; // prefab ships
        public GameObject Borg_Destroyer_ii;
        public GameObject Borg_Cube_ii;
        public GameObject Borg_Scout_i;

        public GameObject Card_Destroyer_i;
        public GameObject Card_Destroyer_ii;
        public GameObject Card_Cruiser_ii;
        public GameObject Card_Scout_i;
        public GameObject Card_Scout_ii;

        public GameObject Dom_Destroyer_i;
        public GameObject Dom_Destroyer_ii;
        public GameObject Dom_Cruiser_ii;
        public GameObject Dom_Scout_i;
        public GameObject Dom_Scout_ii;

        public GameObject Fed_Cruiser_ii;
        public GameObject Fed_Cruiser_iii;
        public GameObject Fed_LtCruiser_iv;
        public GameObject Fed_HvyCruiser_iv;
        public GameObject Fed_Destroyer_i;
        public GameObject Fed_Destroyer_ii;
        public GameObject Fed_Destroyer_iii;
        public GameObject Fed_Destroyer_iv;
        public GameObject Fed_Scout_i;
        public GameObject Fed_Scout_ii;
        public GameObject Fed_Scout_iii;
        public GameObject Fed_Scout_iv;
        public GameObject Fed_Colonyship_i;
        public GameObject Fed_Colonyship_ii;

        public GameObject Kling_Cruiser_ii;
        public GameObject Kling_Destroyer_i;
        public GameObject Kling_Destroyer_ii;
        public GameObject Kling_Scout_i;
        public GameObject Kling_Scout_ii;
        public GameObject Kling_Colonyship_i;

        public GameObject Rom_Destroyer_i;
        public GameObject Rom_Destroyer_ii;
        public GameObject Rom_Cruiser_ii;
        public GameObject Rom_Cruiser_iii;
        public GameObject Rom_Scout_i;
        public GameObject Rom_Scout_ii;
        public GameObject Rom_Scout_iii;

        public static Dictionary<string, GameObject> PrefabShipDitionary;
        #endregion

        #region prefab Star Systems, button gameobjects
        public GameObject FED_StarSystem;
        public GameObject ROM_StarSystem;
        public GameObject KLING_StarSystem;
        public GameObject CARD_StarSystem;
        public GameObject DOM_StarSystem;
        public GameObject BORG_StarSystem;
        public GameObject ACAMARIANS_StarSystem;
        public GameObject AKAALI_StarSystem;
        public GameObject AKRITIRIANS_StarSystem;
        public GameObject ALDEANS_StarSystem;
        public GameObject ALGOLIANS_StarSystem;
        public GameObject ALSAURIANS_StarSystem;
        public GameObject ANDORIANS_StarSystem;
        public GameObject ANGOSIANS_StarSystem;
        public GameObject ANKARI_StarSystem;
        public GameObject ANTEDEANS_StarSystem;
        public GameObject ANTICANS_StarSystem;
        public GameObject ARBAZAN_StarSystem;
        public GameObject ARDANANS_StarSystem;
        public GameObject ARGRATHI_StarSystem;
        public GameObject ARKARIANS_StarSystem;
        public GameObject ATREANS_StarSystem;
        public GameObject AXANAR_StarSystem;
        public GameObject BAJORANS_StarSystem;
        public GameObject BAKU_StarSystem;
        public GameObject BANDI_StarSystem;
        public GameObject BANEANS_StarSystem;
        public GameObject BARZANS_StarSystem;
        public GameObject BENZITES_StarSystem;
        public GameObject BETAZOIDS_StarSystem;
        public GameObject BILANAIANS_StarSystem;
        public GameObject BOLIANS_StarSystem;
        public GameObject BOMAR_StarSystem;
        public GameObject BOSLICS_StarSystem;
        public GameObject BOTHA_StarSystem;
        public GameObject BREELLIANS_StarSystem;
        public GameObject BREEN_StarSystem;
        public GameObject BREKKIANS_StarSystem;
        public GameObject BYNARS_StarSystem;
        public GameObject CAIRN_StarSystem;
        public GameObject CALDONIANS_StarSystem;
        public GameObject CAPELLANS_StarSystem;
        public GameObject CHALNOTH_StarSystem;
        public GameObject CORIDAN_StarSystem;
        public GameObject CORVALLENS_StarSystem;
        public GameObject CYTHERIANS_StarSystem;
        public GameObject DELTANS_StarSystem;
        public GameObject DENOBULANS_StarSystem;
        public GameObject DEVORE_StarSystem;
        public GameObject DOPTERIANS_StarSystem;
        public GameObject DOSI_StarSystem;
        public GameObject DRAI_StarSystem;
        public GameObject DREMANS_StarSystem;
        public GameObject EDO_StarSystem;
        public GameObject ELAURIANS_StarSystem;
        public GameObject ELAYSIANS_StarSystem;
        public GameObject ENTHARANS_StarSystem;
        public GameObject EVORA_StarSystem;
        public GameObject EXCALBIANS_StarSystem;
        public GameObject FERENGI_StarSystem;
        public GameObject FLAXIANS_StarSystem;
        public GameObject GORN_StarSystem;
        public GameObject GRAZERITES_StarSystem;
        public GameObject HAAKONIANS_StarSystem;
        public GameObject HALKANS_StarSystem;
        public GameObject HAZARI_StarSystem;
        public GameObject HEKARANS_StarSystem;
        public GameObject HIROGEN_StarSystem;
        public GameObject HORTA_StarSystem;
        public GameObject IYAARANS_StarSystem;
        public GameObject JNAII_StarSystem;
        public GameObject KAELON_StarSystem;
        public GameObject KAREMMA_StarSystem;
        public GameObject KAZON_StarSystem;
        public GameObject KELLERUN_StarSystem;
        public GameObject KESPRYTT_StarSystem;
        public GameObject KLAESTRONIANS_StarSystem;
        public GameObject KRADIN_StarSystem;
        public GameObject KREETASSANS_StarSystem;
        public GameObject KRIOSIANS_StarSystem;
        public GameObject KTARIANS_StarSystem;
        public GameObject LEDOSIANS_StarSystem;
        public GameObject LISSEPIANS_StarSystem;
        public GameObject LOKIRRIM_StarSystem;
        public GameObject LURIANS_StarSystem;
        public GameObject MALCORIANS_StarSystem;
        public GameObject MALON_StarSystem;
        public GameObject MAQUIS_StarSystem;
        public GameObject MARKALIANS_StarSystem;
        public GameObject MERIDIANS_StarSystem;
        public GameObject MINTAKANS_StarSystem;
        public GameObject MIRADORN_StarSystem;
        public GameObject MIZARIANS_StarSystem;
        public GameObject MOKRA_StarSystem;
        public GameObject MONEANS_StarSystem;
        public GameObject NAUSICAANS_StarSystem;
        public GameObject NECHANI_StarSystem;
        public GameObject NEZU_StarSystem;
        public GameObject NORCADIANS_StarSystem;
        public GameObject NUMIRI_StarSystem;
        public GameObject NUUBARI_StarSystem;
        public GameObject NYRIANS_StarSystem;
        public GameObject OCAMPA_StarSystem;
        public GameObject ORIONS_StarSystem;
        public GameObject ORNARANS_StarSystem;
        public GameObject PAKLED_StarSystem;
        public GameObject PARADANS_StarSystem;
        public GameObject QUARREN_StarSystem;
        public GameObject RAKHARI_StarSystem;
        public GameObject RAKOSANS_StarSystem;
        public GameObject RAMATIANS_StarSystem;
        public GameObject REMANS_StarSystem;
        public GameObject RIGELIANS_StarSystem;
        public GameObject RISIANS_StarSystem;
        public GameObject RUTIANS_StarSystem;
        public GameObject SELAY_StarSystem;
        public GameObject SHELIAK_StarSystem;
        public GameObject SIKARIANS_StarSystem;
        public GameObject SKRREEA_StarSystem;
        public GameObject SONA_StarSystem;
        public GameObject SULIBAN_StarSystem;
        public GameObject TAKARANS_StarSystem;
        public GameObject TAKARIANS_StarSystem;
        public GameObject TAKTAK_StarSystem;
        public GameObject TALARIANS_StarSystem;
        public GameObject TALAXIANS_StarSystem;
        public GameObject TALOSIANS_StarSystem;
        public GameObject TAMARIANS_StarSystem;
        public GameObject TANUGANS_StarSystem;
        public GameObject TELLARITES_StarSystem;
        public GameObject TEPLANS_StarSystem;
        public GameObject THOLIANS_StarSystem;
        public GameObject TILONIANS_StarSystem;
        public GameObject TLANI_StarSystem;
        public GameObject TRABE_StarSystem;
        public GameObject TRILL_StarSystem;
        public GameObject TROGORANS_StarSystem;
        public GameObject TZENKETHI_StarSystem;
        public GameObject ULLIANS_StarSystem;
        public GameObject VAADWAUR_StarSystem;
        public GameObject VENTAXIANS_StarSystem;
        public GameObject VHNORI_StarSystem;
        public GameObject VIDIIANS_StarSystem;
        public GameObject VISSIANS_StarSystem;
        public GameObject VORGONS_StarSystem;
        public GameObject VORI_StarSystem;
        public GameObject VULCANS_StarSystem;
        public GameObject WADI_StarSystem;
        public GameObject XANTHANS_StarSystem;
        public GameObject XEPOLITES_StarSystem;
        public GameObject XINDI_StarSystem;
        public GameObject XYRILLIANS_StarSystem;
        public GameObject YADERANS_StarSystem;
        public GameObject YRIDIANS_StarSystem;
        public GameObject ZAHL_StarSystem;
        public GameObject ZAKDORN_StarSystem;
        public GameObject ZALKONIANS_StarSystem;
        public GameObject ZIBALIANS_StarSystem;
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

        #region travel points as game object
        //private GameObject[] _friendScouts;
        //private GameObject[] _friendFarScouts;
        //private GameObject[] _friendDestroyer;
        //private GameObject[] _friendFarDestroyer;
        //private GameObject[] _friendCapital;
        //private GameObject[] _friendFarCapital;
        //private GameObject[] _friendColony;
        //private GameObject[] _friendFarColony;

        //private GameObject[] _enemyScouts;
        //private GameObject[] _enemyFarScouts;
        //private GameObject[] _enemyDestroyer;
        //private GameObject[] _enemyFarDestroyer;
        //private GameObject[] _enemyCapital;
        //private GameObject[] _enemyFarCapital;
        //private GameObject[] _enemyColony;
        //private GameObject[] _enemyFarColony;
        public Dictionary<GameObject, GameObject[]> _shipTargetDictionary;  // key ship gameObject, listSONames destination gameObject (problem, is loaded inside LoadCombat()
        #endregion

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
        //private GameObject canvas;





        public void InitializeGameManagerWithMainMenuUIController() {


            mainMenuUIController = GameObject.Find("MainMenuUIController").GetComponent<MainMenuUIController>();


            //Assign player selected value;
            _galaxySize = mainMenuUIController.selectedGalaxySize;
            _galaxyType = mainMenuUIController.selectedGalaxyType;
            _techLevel = mainMenuUIController.selectedTechLevel;
            _localPlayer = mainMenuUIController.selectedCivEnum;

            //GalaxyPlayClicked();
        }





        void oldUiInitialization() {
            //PanelLobby_Menu = Canvas.transform.Find("PanelLobby_Menu").gameObject;
            //PanelLoadGame_Menu = Canvas.transform.Find("PanelLoadGame_Menu").gameObject;
            //PanelSaveGame_Menu = Canvas.transform.Find("PanelSaveGame_Menu").gameObject;
            //PanelSettings_Menu = Canvas.transform.Find("PanelSettings_Menu").gameObject;
            //PanelCredits_Menu = Canvas.transform.Find("PanelCredits_Menu").gameObject;
            //PanelMain_Menu = Canvas.transform.Find("PanelMain_Menu").gameObject;
            //PanelMultiplayerLobby_Menu = Canvas.transform.Find("PanelMultiplayerLobby_Menu").gameObject;
            //PanelGalaxy = CanvasGalactic.transform.Find("PanelGalaxy").gameObject;

        }








        private void Awake()
        {
            Instance = this; // static reference to single GameManager
            Canvas = GameObject.Find("Canvas"); // What changed? Now we have to code that unity use to assign in the Inspector.
            CanvasGalactic = GameObject.Find("CanvasGalactic");

            oldUiInitialization();

           // PanelSystem_Play = Canvas.transform.Find("PanelSystemPlay").gameObject;
            //PanelGalactic_Completed = Canvas.transform.Find("PanelGalactic_Completed").gameObject;
            //PanelCombat_Menu = Canvas.transform.Find("PanelCombat_Menu").gameObject;
            //PanelCombat_Play = Canvas.transform.Find("PanelCombat_Play").gameObject;
            //PanelCombat_Completed = Canvas.transform.Find("PanelCombat_Completed").gameObject;
            //PanelGameOver = Canvas.transform.Find("PanelGameOver").gameObject;



            // GameObjects for the system buttons
            AllSystemsList = new List<GameObject> {  FED_StarSystem,
                                                     ROM_StarSystem,
                                                     KLING_StarSystem,
                                                     CARD_StarSystem,
                                                     DOM_StarSystem,
                                                     BORG_StarSystem,
                                                     ACAMARIANS_StarSystem,
                                                    #region
                                                     AKAALI_StarSystem,
                                                     AKRITIRIANS_StarSystem,
                                                     ALDEANS_StarSystem,
                                                     ALGOLIANS_StarSystem,
                                                     ALSAURIANS_StarSystem,
                                                     ANDORIANS_StarSystem,
                                                     ANGOSIANS_StarSystem,
                                                     ANKARI_StarSystem,
                                                     ANTEDEANS_StarSystem,
                                                     ANTICANS_StarSystem,
                                                     ARBAZAN_StarSystem,
                                                     ARDANANS_StarSystem,
                                                     ARGRATHI_StarSystem,
                                                     ARKARIANS_StarSystem,
                                                     ATREANS_StarSystem,
                                                     AXANAR_StarSystem,
                                                     BAJORANS_StarSystem,
                                                     BAKU_StarSystem,
                                                     BANDI_StarSystem,
                                                     BANEANS_StarSystem,
                                                     BARZANS_StarSystem,
                                                     BENZITES_StarSystem,
                                                     BETAZOIDS_StarSystem,
                                                     BILANAIANS_StarSystem,
                                                     BOLIANS_StarSystem,
                                                     BOMAR_StarSystem,
                                                     BOSLICS_StarSystem,
                                                     BOTHA_StarSystem,
                                                     BREELLIANS_StarSystem,
                                                     BREEN_StarSystem,
                                                     BREKKIANS_StarSystem,
                                                     BYNARS_StarSystem,
                                                     CAIRN_StarSystem,
                                                     CALDONIANS_StarSystem,
                                                     CAPELLANS_StarSystem,
                                                     CHALNOTH_StarSystem,
                                                     CORIDAN_StarSystem,
                                                     CORVALLENS_StarSystem,
                                                     CYTHERIANS_StarSystem,
                                                     DELTANS_StarSystem,
                                                     DENOBULANS_StarSystem,
                                                     DEVORE_StarSystem,
                                                     DOPTERIANS_StarSystem,
                                                     DOSI_StarSystem,
                                                     DRAI_StarSystem,
                                                     DREMANS_StarSystem,
                                                     EDO_StarSystem,
                                                     ELAURIANS_StarSystem,
                                                     ELAYSIANS_StarSystem,
                                                     ENTHARANS_StarSystem,
                                                     EVORA_StarSystem,
                                                     EXCALBIANS_StarSystem,
                                                     FERENGI_StarSystem,
                                                     FLAXIANS_StarSystem,
                                                     GORN_StarSystem,
                                                     GRAZERITES_StarSystem,
                                                     HAAKONIANS_StarSystem,
                                                     HALKANS_StarSystem,
                                                     HAZARI_StarSystem,
                                                     HEKARANS_StarSystem,
                                                     HIROGEN_StarSystem,
                                                     HORTA_StarSystem,
                                                     IYAARANS_StarSystem,
                                                     JNAII_StarSystem,
                                                     KAELON_StarSystem,
                                                     KAREMMA_StarSystem,
                                                     KAZON_StarSystem,
                                                     KELLERUN_StarSystem,
                                                     KESPRYTT_StarSystem,
                                                     KLAESTRONIANS_StarSystem,
                                                     KRADIN_StarSystem,
                                                     KREETASSANS_StarSystem,
                                                     KRIOSIANS_StarSystem,
                                                     KTARIANS_StarSystem,
                                                     LEDOSIANS_StarSystem,
                                                     LISSEPIANS_StarSystem,
                                                     LOKIRRIM_StarSystem,
                                                     LURIANS_StarSystem,
                                                     MALCORIANS_StarSystem,
                                                     MALON_StarSystem,
                                                     MAQUIS_StarSystem,
                                                     MARKALIANS_StarSystem,
                                                     MERIDIANS_StarSystem,
                                                     MINTAKANS_StarSystem,
                                                     MIRADORN_StarSystem,
                                                     MIZARIANS_StarSystem,
                                                     MOKRA_StarSystem,
                                                     MONEANS_StarSystem,
                                                     NAUSICAANS_StarSystem,
                                                     NECHANI_StarSystem,
                                                     NEZU_StarSystem,
                                                     NORCADIANS_StarSystem,
                                                     NUMIRI_StarSystem,
                                                     NUUBARI_StarSystem,
                                                     NYRIANS_StarSystem,
                                                     OCAMPA_StarSystem,
                                                     ORIONS_StarSystem,
                                                     ORNARANS_StarSystem,
                                                     PAKLED_StarSystem,
                                                     PARADANS_StarSystem,
                                                     QUARREN_StarSystem,
                                                     RAKHARI_StarSystem,
                                                     RAKOSANS_StarSystem,
                                                     RAMATIANS_StarSystem,
                                                     REMANS_StarSystem,
                                                     RIGELIANS_StarSystem,
                                                     RISIANS_StarSystem,
                                                     RUTIANS_StarSystem,
                                                     SELAY_StarSystem,
                                                     SHELIAK_StarSystem,
                                                     SIKARIANS_StarSystem,
                                                     SKRREEA_StarSystem,
                                                     SONA_StarSystem,
                                                     SULIBAN_StarSystem,
                                                     TAKARANS_StarSystem,
                                                     TAKARIANS_StarSystem,
                                                     TAKTAK_StarSystem,
                                                     TALARIANS_StarSystem,
                                                     TALAXIANS_StarSystem,
                                                     TALOSIANS_StarSystem,
                                                     TAMARIANS_StarSystem,
                                                     TANUGANS_StarSystem,
                                                     TELLARITES_StarSystem,
                                                     TEPLANS_StarSystem,
                                                     THOLIANS_StarSystem,
                                                     TILONIANS_StarSystem,
                                                     TLANI_StarSystem,
                                                     TRABE_StarSystem,
                                                     TRILL_StarSystem,
                                                     TROGORANS_StarSystem,
                                                     TZENKETHI_StarSystem,
                                                     ULLIANS_StarSystem,
                                                     VAADWAUR_StarSystem,
                                                     VENTAXIANS_StarSystem,
                                                     VHNORI_StarSystem,
                                                     VIDIIANS_StarSystem,
                                                     VISSIANS_StarSystem,
                                                     VORGONS_StarSystem,
                                                     VORI_StarSystem,
                                                     VULCANS_StarSystem,
                                                     WADI_StarSystem,
                                                     XANTHANS_StarSystem,
                                                     XEPOLITES_StarSystem,
                                                     XINDI_StarSystem,
                                                     XYRILLIANS_StarSystem,
                                                     YADERANS_StarSystem,
                                                     YRIDIANS_StarSystem,
                                                     ZAHL_StarSystem,
                                                     ZAKDORN_StarSystem,
                                                     ZALKONIANS_StarSystem,
                                                     ZIBALIANS_StarSystem,
                                                     //GALACTIC_Center,
                                                     #endregion
            };



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

        //    Debug.Log("civManager " + civManager.gameObject.name);

        //    civManager.CreateNewGame(0); // (int)_galaxySize);
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
                    //PanelGalaxy.SetActive(false);
                    PanelLobby_Menu.SetActive(true); // Lobby first             
                    break;

                case State.LOBBY_INIT:
                    //panelMain_Menu.SetActive(true);
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
                    //civManager.CreateNewGame(1);
                    switch (_galaxyType) // ToDo: get input from Main Menu
                    {
                        case GalaxyType.CANON:
                            // canon type galaxy.cs SolarSystemsMap dictionary
                            SetGalaxyMapSize(); // set number of stars this._galaxyStarCount int
                            break;
                        case GalaxyType.RANDOM:
                            // generate type galaxy.cs SolarSystemsMap dictionary
                            SetGalaxyMapSize();
                            //GenerateGalaxyMap();                           
                            break;
                    }
                   
                    switch (_localPlayer) // is set in CivSelection.cs for GameManager.localPlayer
                    {
                        //case Civilization.FED: // we already know local player from CivSelection.cs so do we change to a race UI/ ship/ economy here??
                        //    // set 
                        //    break;
                        //case Civilization.TERRAN:
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
                    //CanvasWorld = GameObject.Find("CanvasWorld");
                    //CanvasWorld.SetActive(true);
                    //PanelGalactic_Map.SetActive(true);

                    _statePassedMain_Init = true;
                    //galaxyView.InstantiateSystemButton(_galaxyStarCount);
                    SwitchtState(State.GALACTIC_MAP);
                    break;
                case State.GALACTIC_MAP:
                    //CanvasGalactic = GameObject.Find("CanvasGalactic");
                    //PanelGalaxy = CanvasGalactic.transform.Find("PanelGalaxy").gameObject;
                  //  PanelLobby_Menu.SetActive(false);
                    //PanelGalaxy.SetActive(true);
                    PanelMain_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(false);
                    _statePassedMain_Init = true;
                   // CanvasWorld.SetActive(true);
                    //PanelGalactic_Map.SetActive(true);

                    PanelSystem_Play.SetActive(false);
                    //solarSystemView.ShowNextSolarSystemView( _solarSystemID);
                    break;

                case State.GALACTIC_MAP_INIT:
                   // PanelLobby_Menu.SetActive(false);
                    //CameraGalacticaHolder.SetActive(false);
                    SwitchtState(State.SYSTEM_PLAY);
                    break;




                // GALAXY GAMEPLAY
                case State.SYSTEM_PLAY:

                    galaxyMapBackgroundPictureGO.SetActive(true);
                    UICamera.SetActive(false);
                    GalaxyCamera.SetActive(true);


                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(false);
                    
                    // PanelGalactic_Map.SetActive(false);
                    //CanvasWorld.SetActive(false);
                    //PanelSystem_Play.SetActive(true);
                    _statePassedMain_Init = true;
                    //int firstSolarSystemID = 0; // ToDo: First system 0 to be galaxy and system 1 tie this to home system based on civ set in Main Menu/ or where we left off?

                    break;
                case State.SYSTEM_PLAY_INIT:
                    solarSystemView.TurnOffSolarSystemview(galaxy, _solarSystemID);//solarSystemView);
                   // TurnOnGalacticSystems(true);
                    PanelSystem_Play.SetActive(false);
                    PanelLobby_Menu.SetActive(false);
                    PanelMain_Menu.SetActive(false);
                    PanelMultiplayerLobby_Menu.SetActive(false);
                    _statePassedMain_Init = true;
                 
                    //PanelGalactic_Map.SetActive(true);
                    //SwitchtState(State.GALACTIC_MAP);
                    //int firstSolarSystemID = 0; // ToDo: First system 0 to be galaxy and system 1 tie this to home system based on civ set in Main Menu/ or where we left off?

                    break;
                case State.GALACTIC_COMPLETED:
                    PanelSystem_Play.SetActive(false);
                    PanelLobby_Menu.SetActive(false);
                    PanelSystem_Play.SetActive(false);
                    //PanelGalactic_Map.SetActive(false);
                    //CanvasWorld.SetActive(false);
                    //PanelCombat_Menu.SetActive(true);
                    //panelCombat_Completed.SetActive(true);
                    SwitchtState(State.COMBAT_MENU);
                    break;
                case State.COMBAT_MENU:
                    PanelLobby_Menu.SetActive(false);
                    PanelCombat_Menu.SetActive(true);
                    PanelSystem_Play.SetActive(false);                    
                    PanelSystem_Play.SetActive(false);
                    //PanelGalactic_Map.SetActive(false);
                    LoadFriendAndEnemyNames(); // for combat
                    // combat order toggle in CombatOderSelection code updates GameManager _combatOrder field
                    // _combatOrder = combatOrderSelection.ImplementCombatOrder();
                    break;
                case State.COMBAT_INIT:
                    //_combatWarpIn = true; // turn false again in E_animator3 call to GameManager WarpInOver()
                    PanelLobby_Menu.SetActive(false);
                    _statePassedCombatMenu_Init = true;
                    FriendShips = combat.UpdateFriendCombatants().ToList();
                    EnemyShips = combat.UpdateEnemyCombatants().ToList();
                    actOnCombatOrder.CombatOrderAction(_combatOrder, FriendShips, EnemyShips);
                    instantiateCombatShips.SetCombatOrder(_combatOrder);
                    instantiateCombatShips.PreCombatSetup(FriendNameArray, true);
                    instantiateCombatShips.PreCombatSetup(EnemyNameArray, false);
                    _statePassedCombatInit = true;
                    SetCameraTargets();
                    zoomCamera.ZoomIn();
                    PanelCombat_Menu.SetActive(false);
                    PanelCombat_Play.SetActive(true);
                    SwitchtState(State.COMBAT_PLAY);
                    break;
                case State.COMBAT_PLAY:
                    PanelLobby_Menu.SetActive(false);
                    _statePassedCombatPlay = true;
                    break;
                case State.COMBAT_COMPLETED:
                    PanelLobby_Menu.SetActive(false);
                    _warpingInIsOver = false;
                    // panelCombat_Play.SetActive(true);
                    PanelCombat_Completed.SetActive(true);
                    if (false)// requirments for game over here
                        SwitchtState(State.GAMEOVER);
                    else
                    {
                        SwitchtState(State.SYSTEM_PLAY);
                        _statePassedCombatInit = true;
                        _statePassedCombatMenu_Init = false;
                        zoomCamera.TurnOfZoomerUpdate();
                    }
                    break;
                //case State.LOADNEXT: // was for load levels
                //    // no levels to load
                //    SwitchtState(State.COMBAT_PLAY);
                //    break;
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
            List<GameObject> _cameraTargets = new List<GameObject>() { Friend_0, Enemy_0}; // dummies
           
            List<GameObject> multiTargets = instantiateCombatShips.GetCameraTargets(); // get list - array for CameraMultiTarget
            List<GameObject> survivingTargets = new List<GameObject>();
            if (multiTargets.Count() > 0)
            {
                for (int i = 0; i < multiTargets.Count; i++)
                {
                    if (multiTargets[i] != null)
                    {
                        survivingTargets.Add(multiTargets[i]);
                    }
                }
                
                _cameraTargets.AddRange(survivingTargets);
            }
          
            cameraMultiTarget.SetTargets(_cameraTargets.ToArray()); // start multiCamera - main camers before warp in of ships
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
            //var _keys = EnemyShips.Keys.ToArray();
            //var _shipObjects = EnemyShips.Values.ToArray();
            //FriendShips.
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
                case "TERRAN":
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

        //private void UpdateTheArrays(string shipName, List<GameObject> shortList, FriendOrFoe side, NearOrFar nearOrFar)
        //{
        //    string[] _nameParts = shipName.ToUpper().Split('_');
        //    string shipType = _nameParts[1];
        //    switch (shipType)
        //    {
        //        case "SCOUT":
        //            if (side == FriendOrFoe.friend)
        //                if (nearOrFar == NearOrFar.Near)
        //                    _friendScouts = shortList.ToArray();
        //                else _friendFarScouts = shortList.ToArray();
        //            else if (nearOrFar == NearOrFar.Near)
        //                _enemyScouts = shortList.ToArray();
        //            else _enemyFarScouts = shortList.ToArray();
        //            break;
        //        case "DESTROYER":
        //            if (side == FriendOrFoe.friend)
        //                if (nearOrFar == NearOrFar.Near)
        //                    _friendDestroyer = shortList.ToArray();
        //                else _friendFarDestroyer = shortList.ToArray();
        //            else if (nearOrFar == NearOrFar.Near)
        //                _enemyDestroyer = shortList.ToArray();
        //            else _enemyFarDestroyer = shortList.ToArray();
        //            break;
        //        case "CRUISER":
        //        case "LT-CRUISER":
        //        case "HVY-CRISER":
        //            if (side == FriendOrFoe.friend)
        //                if (nearOrFar == NearOrFar.Near)
        //                    _friendCapital = shortList.ToArray();
        //                else _friendFarCapital = shortList.ToArray();
        //            else if (nearOrFar == NearOrFar.Near)
        //                _enemyCapital = shortList.ToArray();
        //            else _enemyFarCapital = shortList.ToArray();
        //            break;
        //        //case "COLONY":
        //        //    return ShipType.Colony;
        //        //case "more ship types here":
        //        default:
        //            break;
        //    }
        //}

        public Transform GetShipTravelTarget(GameObject aShip)
        {
            return aShip.transform;
        }
        public void LoadFriendAndEnemyNames()
        {
            string[] _friendNameArray = new string[] { "FED_CRUISER_II", "FED_CRUISER_III", "FED_DESTROYER_II", "FED_DESTROYER_II",
                "FED_DESTROYER_I", "FED_SCOUT_II", "FED_SCOUT_IV" , "FED_COLONYSHIP_I" };
            FriendNameArray = _friendNameArray;
            string[] _enemyNameArray = new string[] {"KLING_DESTROYER_I", "KLING_DESTROYER_I", "KLING_CRUISER_II", "KLING_SCOUT_II", "KLING_COLONYSHIP_I","CARD_SCOUT_I",
                "ROM_CRUISER_III", "ROM_CRUISER_II", "ROM_SCOUT_III"}; //"KLING_DESTROYER_I",
            
            EnemyNameArray = _enemyNameArray;
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
                { "FED_DESTROYER_I", Fed_Destroyer_i }, { "FED_SCOUT_II", Fed_Scout_ii },
                { "FED_CRUISER_II", Fed_Cruiser_ii }, { "FED_DESTROYER_II", Fed_Destroyer_ii }, // { "FED_SCOUT_II", Fed_Scout_ii },
                { "FED_CRUISER_III", Fed_Cruiser_iii }, {"FED_SCOUT_IV", Fed_Scout_iv},//{ "FED_DESTROYER_III", Fed_Destroyer_iii }, { "FED_SCOUT_III", Fed_Scout_iii },
                { "FED_COLONYSHIP_I", Fed_Colonyship_i }, 
                { "KLING_DESTROYER_I", Kling_Destroyer_i},
                { "KLING_CRUISER_II", Kling_Cruiser_ii }, { "KLING_SCOUT_II", Kling_Scout_ii }, {"KLING_COLONYSHIP_I", Kling_Colonyship_i},
                { "CARD_SCOUT_I", Card_Scout_i },
                { "ROM_SCOUT_III", Rom_Scout_iii },
                { "ROM_CRUISER_II", Rom_Cruiser_ii }, { "ROM_CRUISER_III", Rom_Cruiser_iii }
            };
            if (PrefabShipDitionary == null) // do not load twice
                PrefabShipDitionary = tempPrefabDitionary;

            Dictionary<string, GameObject> systemPrefabDitionary = new Dictionary<string, GameObject>() // !! only try to load prefabs that exist
            {
                { "FED", FED_StarSystem },
                { "ROM", ROM_StarSystem },
                { "KLING", KLING_StarSystem },
                { "CARD", CARD_StarSystem },
                { "DOM", DOM_StarSystem },
                { "BORG", BORG_StarSystem },
                { "ACAMARIANS", ACAMARIANS_StarSystem },
                { "AKAALI", AKAALI_StarSystem },
                { "AKRITIRIANS", AKRITIRIANS_StarSystem },
                { "ALDEANS", ALDEANS_StarSystem },
                { "ALGOLIANS", ALGOLIANS_StarSystem },
                { "ALSAURIANS", ALSAURIANS_StarSystem },
                { "ANDORIANS", ANDORIANS_StarSystem },
                { "ANGOSIANS", ANGOSIANS_StarSystem },
                { "ANKARI", ANKARI_StarSystem },
                { "ANTEDEANS", ANTEDEANS_StarSystem },
                { "ANTICANS", ANTICANS_StarSystem },
                { "ARBAZAN", ARBAZAN_StarSystem },
                { "ARDANANS", ARDANANS_StarSystem },
                { "ARGRATHI", ARGRATHI_StarSystem },
                { "ARKARIANS", ARKARIANS_StarSystem },
                { "ATREANS", ATREANS_StarSystem },
                { "AXANAR", AXANAR_StarSystem },
                { "BAJORANS", BAJORANS_StarSystem },
                { "BAKU", BAKU_StarSystem },
                { "BANDI", BANDI_StarSystem },
                { "BANEANS", BANEANS_StarSystem },
                { "BARZANS", BARZANS_StarSystem },
                { "BENZITES", BENZITES_StarSystem },
                { "BETAZOIDS", BETAZOIDS_StarSystem },
                { "BILANAIANS", BILANAIANS_StarSystem },
                { "BOLIANS", BOLIANS_StarSystem },
                { "BOMAR", BOMAR_StarSystem },
                { "BOSLICS", BOSLICS_StarSystem },
                { "BOTHA", BOTHA_StarSystem },
                { "BREELLIANS", BREELLIANS_StarSystem },
                { "BREEN", BREEN_StarSystem },
                { "BREKKIANS", BREKKIANS_StarSystem },
                { "BYNARS", BYNARS_StarSystem },
                { "CAIRN", CAIRN_StarSystem },
                { "CALDONIANS", CALDONIANS_StarSystem },
                { "CAPELLANS", CAPELLANS_StarSystem },
                { "CHALNOTH", CHALNOTH_StarSystem },
                { "CORIDAN", CORIDAN_StarSystem },
                { "CORVALLENS", CORVALLENS_StarSystem },
                { "CYTHERIANS", CYTHERIANS_StarSystem },
                { "DELTANS", DELTANS_StarSystem },
                { "DENOBULANS", DENOBULANS_StarSystem },
                { "DEVORE", DEVORE_StarSystem },
                { "DOPTERIANS", DOPTERIANS_StarSystem },
                { "DOSI", DOSI_StarSystem },
                { "DRAI", DRAI_StarSystem },
                { "DREMANS", DREMANS_StarSystem },
                { "EDO", EDO_StarSystem },
                { "ELAURIANS", ELAURIANS_StarSystem },
                { "ELAYSIANS", ELAYSIANS_StarSystem },
                { "ENTHARANS", ENTHARANS_StarSystem },
                { "EVORA", EVORA_StarSystem },
                { "EXCALBIANS", EXCALBIANS_StarSystem },
                { "FERENGI", FERENGI_StarSystem },
                { "FLAXIANS", FLAXIANS_StarSystem },
                { "GORN", GORN_StarSystem },
                { "GRAZERITES", GRAZERITES_StarSystem },
                { "HAAKONIANS", HAAKONIANS_StarSystem },
                { "HALKANS", HALKANS_StarSystem },
                { "HAZARI", HAZARI_StarSystem },
                { "HEKARANS", HEKARANS_StarSystem },
                { "HIROGEN", HIROGEN_StarSystem },
                { "HORTA", HORTA_StarSystem },
                { "IYAARANS", IYAARANS_StarSystem },
                { "JNAII", JNAII_StarSystem },
                { "KAELON", KAELON_StarSystem },
                { "KAREMMA", KAREMMA_StarSystem },
                { "KAZON", KAZON_StarSystem },
                { "KELLERUN", KELLERUN_StarSystem },
                { "KESPRYTT", KESPRYTT_StarSystem },
                { "KLAESTRONIANS", KLAESTRONIANS_StarSystem },
                { "KRADIN", KRADIN_StarSystem },
                { "KREETASSANS", KREETASSANS_StarSystem },
                { "KRIOSIANS", KRIOSIANS_StarSystem },
                { "KTARIANS", KTARIANS_StarSystem },
                { "LEDOSIANS", LEDOSIANS_StarSystem },
                { "LISSEPIANS", LISSEPIANS_StarSystem },
                { "LOKIRRIM", LOKIRRIM_StarSystem },
                { "LURIANS", LURIANS_StarSystem },
                { "MALCORIANS", MALCORIANS_StarSystem },
                { "MALON", MALON_StarSystem },
                { "MAQUIS", MAQUIS_StarSystem },
                { "MARKALIANS", MARKALIANS_StarSystem },
                { "MERIDIANS", MERIDIANS_StarSystem },
                { "MINTAKANS", MINTAKANS_StarSystem },
                { "MIRADORN", MIRADORN_StarSystem },
                { "MIZARIANS", MIZARIANS_StarSystem },
                { "MOKRA", MOKRA_StarSystem },
                { "MONEANS", MONEANS_StarSystem },
                { "NAUSICAANS", NAUSICAANS_StarSystem },
                { "NECHANI", NECHANI_StarSystem },
                { "NEZU", NEZU_StarSystem },
                { "NORCADIANS", NORCADIANS_StarSystem },
                { "NUMIRI", NUMIRI_StarSystem },
                { "NUUBARI", NUUBARI_StarSystem },
                { "NYRIANS", NYRIANS_StarSystem },
                { "OCAMPA", OCAMPA_StarSystem },
                { "ORIONS", ORIONS_StarSystem },
                { "ORNARANS", ORNARANS_StarSystem },
                { "PAKLED", PAKLED_StarSystem },
                { "PARADANS", PARADANS_StarSystem },
                { "QUARREN", QUARREN_StarSystem },
                { "RAKHARI", RAKHARI_StarSystem },
                { "RAKOSANS", RAKOSANS_StarSystem },
                { "RAMATIANS", RAMATIANS_StarSystem },
                { "REMANS", REMANS_StarSystem },
                { "RIGELIANS", RIGELIANS_StarSystem },
                { "RISIANS", RISIANS_StarSystem },
                { "RUTIANS", RUTIANS_StarSystem },
                { "SELAY", SELAY_StarSystem },
                { "SHELIAK", SHELIAK_StarSystem },
                { "SIKARIANS", SIKARIANS_StarSystem },
                { "SKRREEA", SKRREEA_StarSystem },
                { "SONA", SONA_StarSystem },
                { "SULIBAN", SULIBAN_StarSystem },
                { "TAKARANS", TAKARANS_StarSystem },
                { "TAKARIANS", TAKARIANS_StarSystem },
                { "TAKTAK", TAKTAK_StarSystem },
                { "TALARIANS", TALARIANS_StarSystem },
                { "TALAXIANS", TALAXIANS_StarSystem },
                { "TALOSIANS", TALOSIANS_StarSystem },
                { "TAMARIANS", TAMARIANS_StarSystem },
                { "TANUGANS", TANUGANS_StarSystem },
                { "TELLARITES", TELLARITES_StarSystem },
                { "TEPLANS", TEPLANS_StarSystem },
                { "THOLIANS", THOLIANS_StarSystem },
                { "TILONIANS", TILONIANS_StarSystem },
                { "TLANI", TLANI_StarSystem },
                { "TRABE", TRABE_StarSystem },
                { "TRILL", TRILL_StarSystem },
                { "TROGORANS", TROGORANS_StarSystem },
                { "TZENKETHI", TZENKETHI_StarSystem },
                { "ULLIANS", ULLIANS_StarSystem },
                { "VAADWAUR", VAADWAUR_StarSystem },
                { "VENTAXIANS", VENTAXIANS_StarSystem },
                { "VHNORI", VHNORI_StarSystem },
                { "VIDIIANS", VIDIIANS_StarSystem },
                { "VISSIANS", VISSIANS_StarSystem },
                { "VORGONS", VORGONS_StarSystem },
                { "VORI", VORI_StarSystem },
                { "VULCANS", VULCANS_StarSystem },
                { "WADI", WADI_StarSystem },
                { "XANTHANS", XANTHANS_StarSystem },
                { "XEPOLITES", XEPOLITES_StarSystem },
                { "XINDI", XINDI_StarSystem },
                { "XYRILLIANS", XYRILLIANS_StarSystem },
                { "YADERANS", YADERANS_StarSystem },
                { "YRIDIANS", YRIDIANS_StarSystem },
                { "ZAHL", ZAHL_StarSystem },
                { "ZAKDORN", ZAKDORN_StarSystem },
                { "ZALKONIANS", ZALKONIANS_StarSystem },
                { "ZIBALIANS", ZIBALIANS_StarSystem },
                //{ "GALACTIC_CENTER", GALACTIC_Center }
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
        //public void LoadSystemData(string filename)
        //{
        //    #region Read SystemData.txt 

        //    Dictionary<string, string[]> _systemDataDictionary = new Dictionary<string, string[]>();
        //    var file = new FileStream(filename, FileMode.Open, FileAccess.Read);

        //    var _dataPoints = new List<string>();
        //    using (var reader = new StreamReader(file))
        //    {

        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            if (line == null)
        //                continue;
        //            _dataPoints.Add(line.Trim());

        //            if (line.Length > 0)
        //            {
        //                var coll = line.Split(separator);

        //               // _ = int.TryParse(coll[1], out int currentValueOne);
        //               // _ = int.TryParse(coll[2], out int currentValueTwo);
        //               // _ = int.TryParse(coll[3], out int currentValueThree);
        //               // _ = int.TryParse(coll[4], out int currentValueFour);
        //               // _ = int.TryParse(coll[5], out int currentValueFive);
        //               // _ = int.TryParse(coll[6], out int currentValueSix);
        //               // _ = int.TryParse(coll[7], out int currentValueSeven);
        //               // _ = int.TryParse(coll[8], out int currentValueEight);
        //               // _ = int.TryParse(coll[9], out int currentValueNine);
        //               // _ = int.TryParse(coll[10], out int currentValueTen);
        //               // _ = int.TryParse(coll[11], out int currentValueEleven);
        //               // _ = int.TryParse(coll[12], out int currentValueTweleve);
        //               // _ = int.TryParse(coll[13], out int currentValueThirteen);
        //               // _ = int.TryParse(coll[14], out int currentValueFourteen);
        //               // _ = int.TryParse(coll[15], out int currentValueFifteen);

        //                //string[] systemDataArray = new string[25]
        //                //{
        //                //    coll[0],
        //                //    coll[1],
        //                //    coll[2],
        //                //    coll[3],
        //                //    coll[4],
        //                //    coll[5],
        //                //    coll[6],
        //                //    coll[7],
        //                //    coll[8],
        //                //    coll[9],
        //                //    coll[10],
        //                //    coll[11],
        //                //    coll[12],
        //                //    coll[13],
        //                //    coll[14],
        //                //    coll[15],
        //                //    coll[16],
        //                //    coll[17],
        //                //    coll[18],
        //                //    coll[19],
        //                //    coll[20],
        //                //    coll[21],
        //                //    coll[22],
        //                //    coll[23],
        //                //    coll[24]
        //                //};

        //                _systemDataDictionary.Add(coll[5].ToString(), coll);
        //                //_shipInts.Clear();
        //            }
        //        }

        //        reader.Close();
        //        SystemDataDictionary = _systemDataDictionary;
        //        //StaticStuff staticStuffToLoad = new StaticStuff();
        //        //staticStuffToLoad.LoadStaticShipData(_shipDataDictionary);
        //    }
        //    #endregion
        //}
        #region Old Load Combat Data
        public void LoadCombatData() //(string filename) // List<sting>
        {
            //// ToDo: relocate to combat class
            ////GameObject[] localAnimationEmpties = new GameObject[12] { FriendScout_Y0_Z0,
            ////FriendDestroyer_Y0_Z1, FriendCapital_Y0_Z2, FriendColony_Y1_Z0, Friend_Y1_Z1, Friend_Y1_Z2,
            ////EnemyScout_Y0_Z0, EnemyDestroyer_Y0_Z1, EnemyCapital_Y0_Z2, EnemyColony_Y1_Z0, Enemy_Y1_Z1, Enemy_Y1_Z2 };
            ////var something = animationEmpties; // = localAnimationEmpties;

            ////Dictionary<string, GameObject> prefabDitionary = new Dictionary<string, GameObject>() // !! only try to load prefabs that exist
            ////{
            ////    { "FED_DESTROYER_I", Fed_Destroyer_i }, //{ "FED_SCOUT_I", Fed_Scout_i },
            ////    { "FED_CRUISER_II", Fed_Cruiser_ii }, { "FED_DESTROYER_II", Fed_Destroyer_ii }, // { "FED_SCOUT_II", Fed_Scout_ii },
            ////    { "FED_CRUISER_III", Fed_Cruiser_iii }, //{ "FED_DESTROYER_III", Fed_Destroyer_iii }, { "FED_SCOUT_III", Fed_Scout_iii },
            ////    { "KLING_DESTROYER_I", Kling_Destroyer_i},
            ////    { "KLING_CRUISER_II", Kling_Cruiser_ii }, { "KLING_SCOUT_II", Kling_Scout_ii },
            ////    { "CARD_SCOUT_I", Card_Scout_i },
            ////    { "ROM_SCOUT_III", Rom_Scout_iii },
            ////    { "ROM_CRUISER_II", Rom_Cruiser_ii }, { "ROM_CRUISER_III", Rom_Cruiser_iii }
            ////};
            ////#region Ships to load for game
            ////string[] _gameShipsNameArray = new string[] { "FED_CRUISER_II", "FED_CRUISER_III", "FED_DESTROYER_II", "FED_DESTROYER_II", "FED_DESTROYER_I",
            ////    "KLING_DESTROYER_I", "CARD_SCOUT_I", "KLING_CRUISER_II", "KLING_SCOUT_II", "ROM_CRUISER_III", "ROM_CRUISER_II", "ROM_SCOUT_III" };
            ////#endregion
            //#region Ships to load for Combat

            ////string[] _friendNameArray = new string[] { "FED_CRUISER_II", "FED_CRUISER_III", "FED_DESTROYER_II", "FED_DESTROYER_II", "FED_DESTROYER_I" };
            ////FriendNameArray = _friendNameArray;
            ////string[] _enemyNameArray = new string[] { "KLING_DESTROYER_I", "CARD_SCOUT_I", "KLING_CRUISER_II", "KLING_SCOUT_II",
            ////    "ROM_CRUISER_III", "ROM_CRUISER_II", "ROM_SCOUT_III" }; //"KLING_DESTROYER_I",
            ////EnemyNameArray = _enemyNameArray;
            //#endregion

            ////int yFactor = 3000;
            ////int zFactor = 3500;
            ////int xFactorFriend = -3500;

            //#region Grid roes for Friends
            //GameObject _scoutNearFriend = Instantiate(Friend_0, new Vector3(offsetFriendLeft, 0, 0), Quaternion.identity);
            //RotateFriend(_scoutNearFriend);
            //List<GameObject> emptyFriendScouts = new List<GameObject>() { _scoutNearFriend };
            //GameObject _scoutFarFriend = Instantiate(Friend_0, new Vector3(offsetFriendRight, 0, 0), Quaternion.identity);
            ////RotateFriend(_scoutFarFriend);
            //List<GameObject> emptyFriendFarScouts = new List<GameObject>() { _scoutFarFriend };
            //for (int i = 1; i < 21; i++)
            //{
            //    GameObject _tempStartScout = Instantiate(Friend_0, new Vector3(offsetFriendLeft, 0, zFactor * i), Quaternion.identity);
            //    RotateFriend(_tempStartScout);
            //    emptyFriendScouts.Add(_tempStartScout); // add to list of friend empty the next scout start points 
            //    GameObject _tempFarScout = Instantiate(Friend_0, new Vector3(offsetFriendRight, 0, zFactor * i), Quaternion.identity);
            //    //   RotateFriend(_tempFarScout);
            //    emptyFriendFarScouts.Add(_tempFarScout); // add to list of friend empty the next scout FAR points 
            //}
            //_friendScouts = emptyFriendScouts.ToArray();
            //_friendFarScouts = emptyFriendFarScouts.ToArray();

            //GameObject _capitalNearFriend = Instantiate(Friend_0, new Vector3(offsetFriendLeft, yFactor * 1, 0), Quaternion.identity);
            //RotateFriend(_capitalNearFriend);
            //List<GameObject> emptyFriendCapital = new List<GameObject>() { _capitalNearFriend };
            //GameObject _capitalFarFriend = Instantiate(Friend_0, new Vector3(offsetFriendRight, yFactor * 1, 0), Quaternion.identity);
            ////RotateFriend(_capitalFarFriend);
            //List<GameObject> emptyFriendFarCapital = new List<GameObject>() { _capitalFarFriend };
            //for (int i = 1; i < 21; i++)
            //{
            //    GameObject _tempStartCapital = Instantiate(Friend_0, new Vector3(offsetFriendLeft, yFactor * 1, zFactor * i), Quaternion.identity);
            //    RotateFriend(_tempStartCapital);
            //    emptyFriendCapital.Add(_tempStartCapital); // list of friend empty capital start points
            //    GameObject _tempFarCapital = Instantiate(Friend_0, new Vector3(offsetFriendRight, yFactor * 1, zFactor * i), Quaternion.identity);
            //    //RotateFriend(_tempFarCapital);
            //    emptyFriendFarCapital.Add(_tempFarCapital);
            //}
            //_friendCapital = emptyFriendCapital.ToArray();
            //_friendFarCapital = emptyFriendFarCapital.ToArray();

            //GameObject _destroyerNearFriend = Instantiate(Friend_0, new Vector3(offsetFriendLeft, yFactor * 2, 0), Quaternion.identity);
            //RotateFriend(_destroyerNearFriend);
            //List<GameObject> emptyFriendDestroyers = new List<GameObject>() { _destroyerNearFriend };
            //GameObject _destroyerFarFriend = Instantiate(Friend_0, new Vector3(offsetFriendRight, yFactor * 2, 0), Quaternion.identity);
            ////RotateFriend(_destroyerFarFriend);
            //List<GameObject> emptyFriendFarDestroyers = new List<GameObject>() { _destroyerFarFriend };
            //for (int i = 1; i < 21; i++)
            //{
            //    GameObject _tempStartDestroyers = Instantiate(Friend_0, new Vector3(offsetFriendLeft, yFactor * 2, zFactor * i), Quaternion.identity);
            //    RotateFriend(_tempStartDestroyers);
            //    emptyFriendDestroyers.Add(_tempStartDestroyers); // list of friend empty destroyer start point
            //    GameObject _tempFarDestroyers = Instantiate(Friend_0, new Vector3(offsetFriendRight, yFactor * 2, zFactor * i), Quaternion.identity);
            //    //RotateFriend(_tempFarDestroyers);
            //    emptyFriendFarDestroyers.Add(_tempFarDestroyers); // list of friend empty destroyer start point
            //}
            //_friendDestroyer = emptyFriendDestroyers.ToArray();
            //_friendFarDestroyer = emptyFriendFarDestroyers.ToArray();
            //#endregion

            //#region Grid roes for Enemies

            //GameObject _scoutNearEnemy = Instantiate(Enemy_0, new Vector3(offsetEnemyRight, yFactor * 0, 1500), Quaternion.identity);
            //RotateEnemy(_scoutNearEnemy);
            //List<GameObject> emptyEnemyScouts = new List<GameObject>() { _scoutNearEnemy };
            //GameObject _scoutFarEnemy = Instantiate(Enemy_0, new Vector3(offsetEnemyLeft, yFactor * 0, 1500), Quaternion.identity);
            ////RotateEnemy(_scoutFarEnemy);
            //List<GameObject> emptyEnemyFarScouts = new List<GameObject>() { _scoutFarEnemy };
            //for (int i = 1; i < 21; i++)
            //{
            //    GameObject _tempNearScout = Instantiate(Enemy_0, new Vector3(offsetEnemyRight, 0, (zFactor * i + 1500)), Quaternion.identity);
            //    RotateEnemy(_tempNearScout);
            //    emptyEnemyScouts.Add(_tempNearScout);
            //    GameObject _tempFarScout = Instantiate(Enemy_0, new Vector3(offsetEnemyLeft, 0, (zFactor * i + 1500)), Quaternion.identity);
            //    //RotateEnemy(_tempFarScout);
            //    emptyEnemyFarScouts.Add(_tempFarScout);
            //}
            //_enemyScouts = emptyEnemyScouts.ToArray();
            //_enemyFarScouts = emptyEnemyFarScouts.ToArray();

            //GameObject _capitalNearEnemy = Instantiate(Enemy_0, new Vector3(offsetEnemyRight, yFactor * 1, 1500), Quaternion.identity);
            //RotateEnemy(_capitalNearEnemy);
            //List<GameObject> emptyEnemyCapital = new List<GameObject>() { _capitalNearEnemy };
            //GameObject _capitalFarEnemy = Instantiate(Enemy_0, new Vector3(offsetEnemyLeft, yFactor * 1, 1500), Quaternion.identity);
            ////RotateEnemy(_capitalFarEnemy);
            //List<GameObject> emptyEnemyFarCapital = new List<GameObject>() { _capitalFarEnemy };
            //for (int i = 1; i < 21; i++)
            //{
            //    GameObject _tempNearCapital = Instantiate(Enemy_0, new Vector3(offsetEnemyRight, yFactor * 1, (zFactor * i + 1500)), Quaternion.identity);
            //    RotateEnemy(_tempNearCapital);
            //    emptyEnemyCapital.Add(_tempNearCapital);
            //    GameObject _tempFarCapital = Instantiate(Enemy_0, new Vector3(offsetEnemyLeft, yFactor * 1, (zFactor * i + 1500)), Quaternion.identity);
            //    //RotateEnemy(_tempFarCapital);
            //    emptyEnemyFarCapital.Add(_tempFarCapital);

            //}
            //_enemyCapital = emptyEnemyCapital.ToArray();
            //_enemyFarCapital = emptyEnemyFarCapital.ToArray();

            //GameObject _destroyerNearEnemy = Instantiate(Enemy_0, new Vector3(offsetEnemyRight, yFactor * 2, 1500), Quaternion.identity);
            //RotateEnemy(_destroyerNearEnemy);
            //List<GameObject> emptyEnemyDestroyers = new List<GameObject>() { _destroyerNearEnemy };
            //GameObject _destroyerFarEnemy = Instantiate(Enemy_0, new Vector3(offsetEnemyLeft, yFactor * 2, 1500), Quaternion.identity);
            ////RotateEnemy(_destroyerFarEnemy);
            //List<GameObject> emptyEnemyFarDestroyers = new List<GameObject>() { _destroyerFarEnemy };
            //for (int i = 1; i < 21; i++)
            //{
            //    GameObject _tempNearDestroyers = Instantiate(Enemy_0, new Vector3(offsetEnemyRight, yFactor * 2, (zFactor * i + 1500)), Quaternion.identity);
            //    RotateEnemy(_tempNearDestroyers);
            //    emptyEnemyDestroyers.Add(_tempNearDestroyers);
            //    GameObject _tempFarDestroyers = Instantiate(Enemy_0, new Vector3(offsetEnemyLeft, yFactor * 2, (zFactor * i + 1500)), Quaternion.identity);
            //    // RotateEnemy(_tempFarDestroyers);
            //    emptyEnemyFarDestroyers.Add(_tempFarDestroyers);

            //}
            //_enemyDestroyer = emptyEnemyDestroyers.ToArray();
            //_enemyFarDestroyer = emptyEnemyFarDestroyers.ToArray();

            //#endregion

            //// Do ship layers
            //string readFriendName = FriendNameArray[0].ToUpper();
            //string[] _collFriend = readFriendName.Split('_');
            //SetShipLayer(_collFriend[0], true);

            //string readEnemyName = EnemyNameArray[0].ToUpper();
            //string[] _collEnemy = readEnemyName.Split('_');
            //SetShipLayer(_collEnemy[0], false);

            //#region Instantiate Prefab Friend Ships
            ////instantiate prefab ships using friendNameArray to prefab Dictionary onto as many empties in grids 
            //Dictionary<int, GameObject> _friendsLocal = new Dictionary<int, GameObject>();
            //var cameraTargets = new List<GameObject>();
            ////var friendNearTargets = new List<GameObject>();
            //Dictionary<GameObject, GameObject[]> localShipTargetDictionary = new Dictionary<GameObject, GameObject[]>();

            //for (int i = 0; i < FriendNameArray.Count(); i++)
            //{
            //    GameObject[] resetFriendArray = GetRoeByShipType(FriendNameArray[i], FriendOrFoe.friend, NearOrFar.Near); //use the current first empty from the correct side and roe by ship type
            //    GameObject _tempPrefabFriend = (GameObject)Instantiate(PrefabDitionary[FriendNameArray[i]], resetFriendArray[0].transform.Position, resetFriendArray[0].transform.rotation);
            //    GameObject newEmptyCameraTarget = (GameObject)Instantiate(resetFriendArray[0], resetFriendArray[0].transform.Position, resetFriendArray[0].transform.rotation);
            //    GameObject[] resetFriendFarArray = GetRoeByShipType(FriendNameArray[i], FriendOrFoe.friend, NearOrFar.Far);
            //    GameObject newEmptyFriendFarTarget = (GameObject)Instantiate(resetFriendFarArray[0], resetFriendFarArray[0].transform.Position, resetFriendFarArray[0].transform.rotation);
            //    GameObject animationNearFTarget = (GameObject)Instantiate(new GameObject(), resetFriendArray[0].transform.Position, resetFriendArray[0].transform.rotation);
            //    GameObject animationFarFTarget = (GameObject)Instantiate(new GameObject(), resetFriendFarArray[0].transform.Position, resetFriendFarArray[0].transform.rotation);
            //    localShipTargetDictionary.Add(_tempPrefabFriend, new GameObject[] { animationNearFTarget, animationFarFTarget });

            //    newEmptyCameraTarget.transform.SetParent(resetFriendArray[0].transform, true);
            //    cameraTargets.Add(newEmptyCameraTarget);
            //    _tempPrefabFriend.transform.localScale = new Vector3(transform.localScale.x * shipScale, transform.localScale.y * shipScale, transform.localScale.z * shipScale);
            //    _tempPrefabFriend.transform.SetParent(resetFriendArray[0].transform, true);
            //    _friendsLocal.Add(i, _tempPrefabFriend);
            //    GameObject animationEmtpy = GetAnimatorEmpty(_tempPrefabFriend, FriendOrFoe.friend);
            //    resetFriendArray[0].transform.SetParent(animationEmtpy.transform, true);

            //    List<GameObject> resetingAList = resetFriendArray.ToList();
            //    List<GameObject> resetingFarList = resetFriendFarArray.ToList();
            //    resetingAList.Remove(resetingAList[0]);
            //    resetingFarList.Remove(resetingFarList[0]);
            //    UpdateTheArrays(FriendNameArray[i], resetingAList, FriendOrFoe.friend, NearOrFar.Near); // rebuild Array Lists
            //    UpdateTheArrays(FriendNameArray[i], resetingFarList, FriendOrFoe.friend, NearOrFar.Far); // rebuild Array Lists

            //    Ship.SetLayerRecursively(animationEmtpy, friendShipLayer);

            //    if (ShipDataDictionary.TryGetValue(_tempPrefabFriend.name.ToUpper(), out int[] _result))
            //    {
            //        _tempPrefabFriend.GetComponent<Ship>()._shieldsMaxHealth = _result[0];
            //        _tempPrefabFriend.GetComponent<Ship>()._hullMaxHealth = _result[1];
            //        _tempPrefabFriend.GetComponent<Ship>()._torpedoDamage = _result[2];
            //        _tempPrefabFriend.GetComponent<Ship>()._beamDamage = _result[3];
            //        _tempPrefabFriend.GetComponent<Ship>()._cost = _result[4];
            //    }
            //}
            //FriendShips = _friendsLocal;
            ////ship.SetFriendTargets(friendTargetDictionary);

            //#endregion

            //#region Instantiate Prefab Enemy Ships
            //Dictionary<int, GameObject> _enemysLocal = new Dictionary<int, GameObject>();
            ////var enemyNearTargets = new List<GameObject>();

            //for (int i = 0; i < EnemyNameArray.Count(); i++)
            //{
            //    GameObject[] resetEnemyArray = GetRoeByShipType(EnemyNameArray[i], FriendOrFoe.enemy, NearOrFar.Near);
            //    GameObject _tempPrefabEnemy = (GameObject)Instantiate(PrefabDitionary[EnemyNameArray[i]], resetEnemyArray[0].transform.Position, resetEnemyArray[0].transform.rotation);
            //    GameObject anEmptyCameraTarget = (GameObject)Instantiate(resetEnemyArray[0], resetEnemyArray[0].transform.Position, resetEnemyArray[0].transform.rotation);
            //    GameObject[] resetEnemyFarArray = GetRoeByShipType(EnemyNameArray[i], FriendOrFoe.enemy, NearOrFar.Far);
            //    GameObject newEmptyEnemyFarTarget = (GameObject)Instantiate(resetEnemyFarArray[0], resetEnemyFarArray[0].transform.Position, resetEnemyFarArray[0].transform.rotation);
            //    GameObject animationNearETarget = (GameObject)Instantiate(new GameObject(), resetEnemyArray[0].transform.Position, resetEnemyArray[0].transform.rotation);
            //    GameObject animationFarETarget = (GameObject)Instantiate(new GameObject(), resetEnemyFarArray[0].transform.Position, resetEnemyFarArray[0].transform.rotation);
            //    localShipTargetDictionary.Add(_tempPrefabEnemy, new GameObject[] { animationNearETarget, animationFarETarget });

            //    anEmptyCameraTarget.transform.SetParent(resetEnemyArray[0].transform, true);
            //    cameraTargets.Add(anEmptyCameraTarget);
            //    _tempPrefabEnemy.transform.localScale = new Vector3(transform.localScale.x * shipScale, transform.localScale.y * shipScale, transform.localScale.z * shipScale);
            //    _tempPrefabEnemy.transform.SetParent(resetEnemyArray[0].transform, true);
            //    _enemysLocal.Add(i, _tempPrefabEnemy);
            //    GameObject animationEmtpy = GetAnimatorEmpty(_tempPrefabEnemy, FriendOrFoe.enemy);
            //    resetEnemyArray[0].transform.SetParent(animationEmtpy.transform, true);

            //    List<GameObject> resetingList = resetEnemyArray.ToList();
            //    List<GameObject> resetingFarList = resetEnemyFarArray.ToList();
            //    resetingList.Remove(resetingList[0]);
            //    resetingFarList.Remove(resetingFarList[0]);
            //    UpdateTheArrays(EnemyNameArray[i], resetingList, FriendOrFoe.enemy, NearOrFar.Near);
            //    UpdateTheArrays(EnemyNameArray[i], resetingFarList, FriendOrFoe.enemy, NearOrFar.Far);

            //    Ship.SetLayerRecursively(animationEmtpy, enemyShipLayer);

            //    if (ShipDataDictionary.TryGetValue(_tempPrefabEnemy.name.ToUpper(), out int[] _result))
            //    {
            //        _tempPrefabEnemy.GetComponent<Ship>()._shieldsMaxHealth = _result[0];
            //        _tempPrefabEnemy.GetComponent<Ship>()._hullMaxHealth = _result[1];
            //        _tempPrefabEnemy.GetComponent<Ship>()._torpedoDamage = _result[2];
            //        _tempPrefabEnemy.GetComponent<Ship>()._beamDamage = _result[3];
            //        _tempPrefabEnemy.GetComponent<Ship>()._cost = _result[4];
            //    }
            //}
            //EnemyShips = _enemysLocal;
            //_shipTargetDictionary = localShipTargetDictionary;

            //#endregion

            //cameraMultiTarget.SetTargets(cameraTargets.ToArray());

            //friends = FriendNameArray.Count();
            //enemies = EnemyNameArray.Count();

            ////StaticStuff.LoadStaticEnemyDictionary(EnemyShips);   
        }
        #endregion
        public Dictionary<GameObject, GameObject[]> GetShipTravelTargets()
        {
            return _shipTargetDictionary;
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