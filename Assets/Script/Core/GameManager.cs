using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using TMPro;


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
        TERRAN,
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
        KLAESTRON,
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
        ZZUNINHABITED11,
        ZZUNINHABITED12,
        ZZUNINHABITED13,
        ZZUNINHABITED14,
        ZZUNINHABITED15,
        ZZUNINHABITED16,
        ZZUNINHABITED17,
        ZZUNINHABITED18,
        ZZUNINHABITED19,
        ZZUNINHABITED20,
        ZZUNINHABITED21,
        ZZUNINHABITED22,
        ZZUNINHABITED23,
        ZZUNINHABITED24,
        ZZUNINHABITED25,
        ZZUNINHABITED26,
        ZZUNINHABITED27,
        ZZUNINHABITED28,
        ZZUNINHABITED29,
        ZZUNINHABITED30,
        ZZUNINHABITED31,
        ZZUNINHABITED32,
        ZZUNINHABITED33,
        ZZUNINHABITED34,
        ZZUNINHABITED35,
        ZZUNINHABITED36,
        ZZUNINHABITED37,
        ZZUNINHABITED38,
        ZZUNINHABITED39,
        ZZUNINHABITED40,
        ZZUNINHABITED41,
        ZZUNINHABITED42,
        ZZUNINHABITED43,
        ZZUNINHABITED44,
        ZZUNINHABITED45,
        ZZUNINHABITED46,
        ZZUNINHABITED47,
        ZZUNINHABITED48,
        ZZUNINHABITED49,
        ZZUNINHABITED50,
        ZZUNINHABITED51,
        ZZUNINHABITED52,
        ZZUNINHABITED53
        #endregion
    }
    public enum GalaxyMapType
    {
        CANON,
        RANDOM,
        RING,
        WHATEVER
    }
    public enum GalaxySize
    {
        SMALL,
        MEDIUM,
        LARGE,
        PONDEROUS
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
        public static GameManager Instance { get; private set; } // a static singleton, no other script can instatniate a GameManager, must us the singleton
        public TimeManager TimeManager;
        [SerializeField]
        private MainMenuUIController mainMenuUIController;
        // ?? will we need these again?
        //List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
        public bool _weAreFriend = false;
        public bool _warpingInIsOver = false; // WarpingInCompleted() called from E_Animator3 sets true and set false again in CombatCompleted state in BeginState
        public CivEnum LocalPlayer;
        public string NoDestination;
        private readonly List<string> destinationNames = new List<string>() { "No Destination Selected" };
        public List<string> DestinationNames { get { return destinationNames; } }
        public Dictionary<string, GameObject> DestinationDictionary = new Dictionary<string, GameObject>();
        public GalaxySize GalaxySize;
        public GalaxyMapType GalaxyType;
        public TechLevel TechLevelOnLoadGame;
        public Galaxy Galaxy; // = new Galaxy(GameManager.Instance, GalaxyMapType.ELLIPTICAL, 20);
        private SolarSystemView solarSystemView;
        /// <summary>
        /// Old combat tool for view of all ships in combat
        /// </summary>
        //public CameraMultiTarget cameraMultiTarget; 
        //private GameObject[] _cameraTargets; // = new GameObject [] { Friend_0, Enemy_0 };
        /// <summary>
        /// Old UI elements
        /// </summary>
        //public SinglePlayer _SinglePlayer;
        //public MultiPlayer _MultiPlayer;
        //public LoadGamePanel _LoadGamePanel;
        //public SaveGamePanel _SaveGamePanel;
        //public SettingsGamePanel _SettingsGamePanel;
        //public ExitQuit _ExitQuit;
        //public CreditsGamePanel _CreditsGamePanel;
        /// <summary>
        /// Old pre combat opening ship warpin animation and setup for start of combat
        /// </summary>
        //public GameObject animFriend1;
        //public GameObject animFriend2;
        //public GameObject animFriend3;
        //public GameObject animEnemy1;
        //public GameObject animEnemy2;
        //public GameObject animEnemy3;
        //public GameObject Friend_0; // prefab empty gameobject to clone instantiat into the grids
        //public GameObject Enemy_0;

        //public int yFactor = 3000; // old LoadCombatData combat, gap in grid between empties on y axis
        //public int zFactor = 3000;
        //public int offsetFriendLeft = -5500; // listSONames of x axis for friend grid left side (start here), world location
        //public int offsetFriendRight = 5800; // listSONames of x axis for friend grid right side, world location
        //public int offsetEnemyRight = 5500; // start here
        //public int offsetEnemyLeft = -5800;

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

        //public GameObject[] animationEmpties = new GameObject[12]; // Populated in Unity Hierarchy under Combat for animation empty objexts
        // { FriendScout_Y0_Z0, 
        //    FriendDestroyer_Y0_Z1, FriendCapitalShip_Y0_Z2, FriendColony_Y1_Z0, Friend_Y1_Z1, Friend_Y1_Z2, 
        //    EnemyScout_Y0_Z0, EnemyDestroyer_Y0_Z1, EnemyCapital_Y0_Z2, EnemyColony_Y1_Z0, Enemy_Y1_Z1, Enemy_Y1_Z2 };
        // Unity does not like c# lists
        #endregion

        //public static List<string> StartGameObjectNames = new List<string>();
        //public  string[] FriendNameArray; // For old Combat ****
        //public  string[] EnemyNameArray;

        //public int friends;
        //public int enemies;
        //public  List<GameObject> FriendShips = new List<GameObject>();  // updated to current combat
        //public  List<GameObject> EnemyShips = new List<GameObject>();

        //private int friendShipLayer;
        //private int enemyShipLayer;
        //private int _level;

        //public int Level
        //{
        //    //get { return _level; }
        //    //set { _level = value; }
        //}

        bool _isSwitchingState = false;

        public bool _statePassedLobbyInit = false;
        public bool _statePassedMain_Init = false;
        public bool _statePassedCombatMenu_Init = false;
        public bool _statePassedCombatInit = false; // COMBAT INIT
        public bool _statePassedCombatPlay = false;

        public void InitializeGameManagerWithMainMenuUIController() {
            mainMenuUIController = GameObject.Find("MainMenuUIController").GetComponent<MainMenuUIController>();
            GalaxySize = mainMenuUIController.SelectedGalaxySize;
            GalaxyType = mainMenuUIController.SelectedGalaxyType;
            TechLevelOnLoadGame = mainMenuUIController.SelectedTechLevel;
            LocalPlayer = mainMenuUIController.SelectedLocalCivEnum;
        }

        private void Awake()
        {
            Instance = this; // static reference to single GameManager
            InitializeGameManagerWithMainMenuUIController();
        }
        void Start()
        {
            NoDestination = DestinationNames[0];
        }
        public void LoadGalacticDestinations(List<StarSysData> starSysDataList)
        {
            foreach (var sysData in starSysDataList)
            {
                destinationNames.Add(sysData.SysName);
                if (!DestinationDictionary.ContainsKey(sysData.SysName))
                    DestinationDictionary.Add(sysData.SysName, sysData.SysGameObject);
            }
        }
        public void LoadGalacticDestinations(FleetData fleetData, GameObject fleetGO)
        {
            destinationNames.Add(fleetData.Name);
            if(!DestinationDictionary.ContainsKey(fleetData.Name))
                DestinationDictionary.Add(fleetData.Name, fleetGO);
        }
        public void RemoveFleetFromGalaxyDestiations(FleetData fleetData, GameObject fleetGO)
        {
            destinationNames.Remove(fleetData.Name);
            DestinationDictionary.Remove(fleetData.Name);
        }
        public void LoadPlayerGalacticDestinations(PlayerDefinedTargetData playerTargetData, GameObject playerTargetGO)
        {
            destinationNames.Add(playerTargetData.Name);
            DestinationDictionary.Add(playerTargetData.Name, playerTargetGO);
        }
        public void RemovePlayerTargetFromGalaxyDestiations(PlayerDefinedTargetData playerTargetData)
        {
            destinationNames.Remove(playerTargetData.Name);
            DestinationDictionary.Remove(playerTargetData.Name);
        }

        //  MARC CODE
        public GameObject UICamera;
        public GameObject GalaxyCamera;
        
        public void SetCameraTargets() // ToDo: re-implement in combat
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
        //public void ProvideFriendCombatShips(int numIndex, GameObject daObject)
        //{
        //    FriendShips.Add(daObject); // geting friend combat ship dictionary for combat
        //}
        //public void ProvideEnemyCombatShips(int numIndex, GameObject daObject)
        //{
        //    EnemyShips.Add(daObject);
        //}
        public void WarpingInCompleted() // ToDo: re-implement in combat
        {
            _warpingInIsOver = true;
        }
        public void SetShipLayer() // ToDo: re-implement in combat
        {
            //List<GameObject> allDaShipObjectInCombat = new List<GameObject>();
            //allDaShipObjectInCombat = FriendShips;
            //for (int i = 0; i < EnemyShips.Count; i++)
            //{
            //    allDaShipObjectInCombat.Add(EnemyShips[i]);
            //}
            
            //foreach (var shipGameObject in allDaShipObjectInCombat)
            //{
            //    var arrayOfName = shipGameObject.name.ToUpper().Split('_');
            //    shipGameObject.layer = SetShipLayer(arrayOfName[0]);
                
            //} 
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
    }
}