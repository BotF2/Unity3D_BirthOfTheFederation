using FischlWorks_FogWar;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.UI;




namespace Assets.Core
{
    /// <summary>
    /// Instantiates the fleets (a FleetController and a FleetData) using FleetSO
    /// </summary>
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager Instance;
        [SerializeField]
        private Canvas parentCanavas;
        [SerializeField]
        private csFogWar fogWar;
        [SerializeField]
        private List<FleetSO> fleetSOList;// all possible fleetSO(s)
        [SerializeField]
        private GameObject fleetPrefab;
        [SerializeField]
        private GameObject fleetUIPrefab;
        [SerializeField] 
        private GameObject contentFolderParent;
        [SerializeField]
        private GameObject shipManagerMenuPrefab;
        [SerializeField]
        private Material fogPlaneMaterial;
        [SerializeField]
        private GameObject galaxyImage;
        [SerializeField]
        private GameObject galaxyCenter;
        public List<FleetController> FleetControllerList;
        public List<GameObject> FleetGOList = new List<GameObject>(); // all fleetGO GOs made
        [SerializeField]
        private GameObject fleetGroupPrefab;
        [SerializeField]
        private Sprite unknownfleet;
        [SerializeField]
        private GameObject canvasShipManager;
        [SerializeField]
        private List<int> destinationIntsInUse = new List<int>() { 0 };
        private Dictionary<CivEnum, List<int>> fleetNumsInUse  = new Dictionary<CivEnum, List<int>>();
        public List<FleetController> FleetConrollersInGame = new List<FleetController>();

        private List<CivEnum> localPlayerCanSeeMyInsigniaList = new List<CivEnum>();


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
        public void Start()
        {
            for (int i = 0; i < CivManager.Instance.CivSOListAllPossible.Count; i++)
            {
                fleetNumsInUse.Add(CivManager.Instance.CivSOListAllPossible[i].CivEnum, new List<int>());
            }
        }
        public void CleanUpDictinaryForFleetNums()
        {
            var civs = CivManager.Instance.CivSOListAllPossible;
   
            var civsEnumsInGame = CivManager.Instance.CivEnumsInGame;
            for (int i = 0; i < civs.Count; i++)
            {
                if (!civsEnumsInGame.Contains(civs[i].CivEnum))
                {
                    fleetNumsInUse.Remove(civs[i].CivEnum);
                }
            }
        }
        public GameObject BuildShipInSystemWithFleet( StarSysController sysCon, bool inSystem, CivEnum civEnum)
        {
            // system builds a ship and we need a fleet in the system
            GameObject fleetGO = new GameObject();

            FleetSO fleetSO = GetFleetSObyInt((int)sysCon.StarSysData.CurrentOwnerCivEnum);
            FleetData fleetData = new FleetData(fleetSO);
            if (fleetSO != null)
            {
                fleetGO = InstantiateFleet(sysCon, fleetData, sysCon.StarSysData.GetPosition(), inSystem);
            }
            else fleetGO.name = "killMe";
            return fleetGO;
        }
        public void BuildFirstFleets(StarSysController sysCon, bool inSystem)
        {
            // first path here is sent on loading the game for civs with warp, first fleets from Systems/Civs with warp
            FleetSO fleetSO = GetFleetSObyInt((int)sysCon.StarSysData.CurrentOwnerCivEnum);
            var position = sysCon.StarSysData.GetPosition();

            // *** This is an option for more fleets/ships with larger galaxy
            //switch (GameManager.current.GalaxySize)
            //{
            //    case GalaxySize.SMALL:
            //        BuildFirstFleets(xyzBump, pairEnumList, position);
            //        break;
            //    case GalaxySize.MEDIUM:
            //        BuildFirstFleets(xyzBump +1, pairEnumList, position);
            //        break;
            //    case GalaxySize.LARGE:
            //        BuildFirstFleets(xyzBump +2, pairEnumList, position);
            //        break;
            //    default:
            //        BuildFirstFleets(xyzBump, pairEnumList, position);
            //        break;
            //

            CivData thisCivData = CivManager.Instance.GetCivDataByCivEnum(fleetSO.CivOwnerEnum); // new CivData();

            FleetData fleetData = new FleetData(fleetSO); // FleetData is not MonoBehavior so new is OK
            fleetData.CurrentWarpFactor = 3f;
            fleetData.CivLongName = thisCivData.CivLongName; //.CivLongName;
            fleetData.CivShortName = thisCivData.CivShortName;
            GameObject aFleet = InstantiateFleet(sysCon, fleetData, position, inSystem);  
            if (aFleet.name == "killMe")
            {
                Destroy(aFleet);
            }
        }
        
        public GameObject InstantiateFleet(StarSysController sysCon, FleetData fleetData, Vector3 position, bool inSystem)
        {
            GameObject fleetNewGameOb = new GameObject();
   
            IEnumerable<StarSysController> ourCivSysCons =
                from x in StarSysManager.Instance.StarSysControllerList
                where (x.StarSysData.CurrentOwnerCivEnum == fleetData.CivEnum)
                select x;
            var ourSysCons = ourCivSysCons.ToList();
                
            if (fleetData.CivEnum != CivEnum.ZZUNINHABITED1)
            {
                fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity);
                FleetGOList.Add(fleetNewGameOb);
                fleetNewGameOb.layer = 6; // galaxy layer

                var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
                //currentActiveFleetCon = fleetController;
                fleetController.BackgroundGalaxyImage = galaxyImage;
                fleetController.FleetData = fleetData;
                //fleetController.FleetState = FleetState.FleetStationary;

                FleetControllerList.Add(fleetController); // add to list of all fleet controllers
                if (!inSystem)
                {
                    var transGalaxyCenter = galaxyCenter.gameObject.transform;
                    var trans = sysCon.gameObject.transform;
                    fleetNewGameOb.transform.SetParent(transGalaxyCenter, true); // parent is galaxy center, it is not in a star system
                                                                                    // now put it near the home world and visible/seen on the galaxy map, in galaxy space. It is not 'hidden' in the system
                    fleetNewGameOb.transform.Translate(new Vector3(trans.position.x + 20f, trans.position.y + 20f, trans.position.z));
                    fleetData.Position = fleetNewGameOb.transform.position;
                }
                else // it is in the system so 'hidden' on the galaxy map inside the system
                {
                    fleetNewGameOb.transform.SetParent(sysCon.gameObject.transform, false);
                }
                fleetNewGameOb.transform.localScale = new Vector3(0.7f, 0.7f, 1); // scale ship insignia here
                int fleetInt = GetNewFleetInt(fleetData.CivEnum);
                fleetNewGameOb.name = fleetData.CivShortName.ToString() + " Fleet " + fleetInt.ToString(); // name game object
                fleetData.Name = fleetNewGameOb.name;

                fleetController.FleetData.FleetInt = fleetInt;
                fleetController.Name = fleetData.Name;
                FleetConrollersInGame.Add(fleetController);
                fleetController.FleetData.CurrentWarpFactor = 0f;
                TextMeshProUGUI TheText = fleetNewGameOb.GetComponentInChildren<TextMeshProUGUI>();

                if (GameController.Instance.AreWeLocalPlayer(fleetData.CivEnum))
                {
                    var ourFogRevealerFleet = new csFogWar.FogRevealer(fleetNewGameOb.transform, 200, true);
                    fogWar.AddFogRevealer(ourFogRevealerFleet);
                }
                else
                {
                    fleetNewGameOb.AddComponent<csFogVisibilityAgent>();
                    var ourFogVisibilityAgent = fleetNewGameOb.GetComponent<csFogVisibilityAgent>();
                    ourFogVisibilityAgent.FogWar = fogWar;
                    ourFogVisibilityAgent.enabled = true;
                }

                TheText.text = fleetNewGameOb.name;
                fleetData.Name = TheText.text;
                var Renderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < Renderers.Length; i++)
                {
                    if (Renderers[i] != null)
                    {
                        if (Renderers[i].name == "InsigniaSprite")
                        {
                            Renderers[i].sprite = fleetController.FleetData.Insignia;
                            if (!GameController.Instance.AreWeLocalPlayer(fleetController.FleetData.CivEnum) && !localPlayerCanSeeMyInsigniaList.Contains(fleetData.CivEnum))
                            {
                                Renderers[i].gameObject.SetActive(false);
                            }
                            else Renderers[i].gameObject.SetActive(true);
                        }
                        if (Renderers[i].name == "InsigniaUnknown" && (GameController.Instance.AreWeLocalPlayer(fleetController.FleetData.CivEnum) || localPlayerCanSeeMyInsigniaList.Contains(fleetData.CivEnum)))
                        {
                            Renderers[i].gameObject.SetActive(false);
                        }
                    }
                }
                // The line from Fleet to underlying galaxy image and to destination
                MapLineMovable[] ourLineToGalaxyImageScript = fleetNewGameOb.GetComponentsInChildren<MapLineMovable>();
                for (int i = 0; i < ourLineToGalaxyImageScript.Length; i++)
                {
                    if (ourLineToGalaxyImageScript[i].name == "DropLine")
                    {
                        ourLineToGalaxyImageScript[i].GetLineRenderer();
                        ourLineToGalaxyImageScript[i].lineRenderer.startColor = Color.red;
                        ourLineToGalaxyImageScript[i].lineRenderer.endColor = Color.red;
                        ourLineToGalaxyImageScript[i].transform.SetParent(fleetNewGameOb.transform, false);
                        Vector3 galaxyPlanePoint = new Vector3(fleetNewGameOb.transform.position.x,
                            galaxyImage.transform.position.y, fleetNewGameOb.transform.position.z);
                        Vector3[] points = { fleetNewGameOb.transform.position, galaxyPlanePoint };
                        ourLineToGalaxyImageScript[i].SetUpLine(points);
                        fleetController.DropLine = ourLineToGalaxyImageScript[i];
                    }
                    // eles if "DestinationLine" is done in FleetController
                }

                fleetController.FleetData.ShipsList.Clear();
                foreach (var civCon in CivManager.Instance.CivControllersInGame)
                {
                    if (civCon.CivData.CivEnum == fleetData.CivEnum)
                        fleetData.CivController = civCon;
                }
                List<FleetController> list = new List<FleetController>() { fleetController };
                fleetController.FleetData.FleetGroupControllers = list;
                fleetController.UpdateMaxWarp();
                fleetNewGameOb.SetActive(true);
                if (!inSystem) // all first fleets are not in system
                    ShipManager.Instance.BuildShipsOfFirstFleet(fleetNewGameOb);
                InstantiateFleetUIGameObject(fleetController);
            }
            else fleetNewGameOb.name = "killMe";
            
            return fleetNewGameOb;
        }
        private void InstantiateFleetUIGameObject(FleetController fleetCon)
        {
            if (fleetCon.FleetData.CivEnum == GameController.Instance.GameData.LocalPlayerCivEnum)
            {
                if (fleetCon.FleetUIGameObject == null)
                {
                    GameObject thisFleetUIGameObject = (GameObject)Instantiate(fleetUIPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity); //parentCanavas.transform);
                    //RectTransform rt = thisFleetUIGameObject.GetComponent<RectTransform>();
                    //rt.anchoredPosition = Vector2.zero; // Set position if needed
                    thisFleetUIGameObject.SetActive(true);
                    thisFleetUIGameObject.layer = 5;
                    fleetCon.FleetUIGameObject = thisFleetUIGameObject;
                    thisFleetUIGameObject.transform.SetParent(contentFolderParent.transform, false); // load into List of fleets

                }
            }
        }
        public void InstantiateFleetsShipManagerUI(FleetController fleetCon)
        {
            GameObject shipManagerUIInstance = (GameObject)Instantiate(shipManagerMenuPrefab, new Vector3(0, -70, 0),
                Quaternion.identity);
            GalaxyMenuUIController.Instance.SetActiveManageFleetsShipMenu(shipManagerUIInstance);
            shipManagerUIInstance.layer = 5; //UI layer
            canvasShipManager.SetActive(true);

            // getting the ships data so they can send images for drag drop
            shipManagerUIInstance.transform.SetParent(canvasShipManager.transform, false);
            ShipInFleetItem[] ships = shipManagerUIInstance.GetComponentsInChildren<ShipInFleetItem>();


            for (int m = 0; m < ships.Length; m++)
            {
                ships[m].FleetController = fleetCon;
                if (ships[m].ShipType == ShipType.Scout)
                {
                    // To Do Find subtypes, unlike facilities with prefabs the ship data is in the SOs;
                }
                else if (ships[m].ShipType == ShipType.Destroyer) { }
                else if (ships[m].ShipType == ShipType.Transport) { }
                else if (ships[m].ShipType == ShipType.Cruiser) { }
                else if (ships[m].ShipType == ShipType.LtCruiser) { }
                else if (ships[m].ShipType == ShipType.HvyCruiser) { }
            }
        }
        void RemoveFleetConrollerFromAllControllers(FleetController fleetController)
        {
            FleetControllerList.Remove(fleetController);
        }
        void AddFleetConrollerFromAllControllers(FleetController fleetController)
        {
            FleetControllerList.Add(fleetController);
        }
        public void FleetToFleetManagement(FleetController fleetConA, FleetController fleetConB)
        {
            List<ShipController> shipListA = fleetConA.FleetData.GetShipList();
            List<ShipController> shipListB = fleetConB.FleetData.GetShipList();
            // we already know the civ of fleetConA == the civ of fleetConB
            if (GameController.Instance.AreWeLocalPlayer(fleetConA.FleetData.CivEnum))
            {
                //call up UI for civ
            }
            else
            {
                //call up AI for civ fleet management
            }

        }
        public void GetFleetGroupInSystemForShipTransfer(StarSysController starSysCon)
        {
            //GameObject fleetGroupNewGO = (GameObject)Instantiate(fleetGroupPrefab, new Vector3(0, 0, 0),
            //        Quaternion.identity);
        }
        public GameObject FindFleetGO(FleetController fleetController)
        {
            GameObject ourFleetGO = fleetPrefab;
            for (int i = 0; i< FleetGOList.Count; i++)
            {
                if (FleetGOList[i].GetComponentInChildren<FleetController>() == fleetController)
                {
                    ourFleetGO = FleetGOList[i];
                    break;
                }
            }
            return ourFleetGO;
        }
        public FleetSO GetFleetSObyInt(int fleetInt)
        {
            FleetSO result = null;
            for (int i = 0;i< fleetSOList.Count; i++)
            //foreach (var fleetSO in fleetSOList)
            {

                if (fleetSOList[i].CivIndex == fleetInt)
                {
                    result = fleetSOList[i];
                    break;
                }
            }
            return result;

        }
        public static GameObject FindGameObjectInChildrenWithTag(GameObject parent, string tag)
        {
            Transform t = parent.transform;
            for (int i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).gameObject.tag == tag)
                    return t.GetChild(i).gameObject;
            }
            return null;
        }
        private int GetUniqueIntAsDestination(int destinationInt)
        {
            if (destinationIntsInUse.Contains(destinationInt))
            {
                destinationInt++;
                if (!destinationIntsInUse.Contains(destinationInt))
                    return destinationInt;
            }
            else
            {
                destinationIntsInUse.Add(destinationInt);

            }
            return destinationInt;

        }
        public void RemoveFleet(GameObject go, int asDestinationInt)
        {
            destinationIntsInUse.Remove(asDestinationInt);
            FleetControllerList.Remove(go.GetComponent<FleetController>());
            go.IsDestroyed();
        }
        public int GetNewFleetInt(CivEnum civEnum)
        {
            List<int> ourFleetNumsInUse = fleetNumsInUse[civEnum];
            int numToReturn = 1;
            if (ourFleetNumsInUse.Count == 0)
            {
                ourFleetNumsInUse.Add(numToReturn);
                return numToReturn;
            }
            else
            {
                for (int i = 1; i < ourFleetNumsInUse.Count + 1; i++)
                {
                    if (!ourFleetNumsInUse.Contains(numToReturn))
                    {
                        numToReturn = i;
                        break;
                    }
                    else
                    {
                        numToReturn = i + 1;
                    }
                }
                ourFleetNumsInUse.Add(numToReturn);
                ourFleetNumsInUse.Sort();
            }
            return numToReturn;
        }
        public void RemoveFleetInt(CivEnum civEnum, int fleetInt)
        {
            fleetNumsInUse[civEnum].Remove(fleetInt);
        }
        public void ExposeAllFleetInsigniaSprites(CivEnum civEnum)
        {
            localPlayerCanSeeMyInsigniaList.Add(civEnum);
            foreach (var fleetController in FleetControllerList)
            {
                if (fleetController.FleetData.CivEnum == civEnum)
                {
                    Transform[] transforms = fleetController.gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform t in transforms)
                    {
                        if (t.name == "InsigniaHolder")
                        {
                            t.GetChild(0).gameObject.SetActive(true);// activate the child of holder so the sprite renderer can be found
                            break;
                        }
                    }
                    var Renderers = fleetController.gameObject.GetComponentsInChildren<SpriteRenderer>();
                    for (int i = 0; i < Renderers.Length; i++)
                    {
                        if (Renderers[i] != null)
                        {
                            if (Renderers[i].name == "InsigniaSprite")
                            {
                                Renderers[i].gameObject.SetActive(true);
                                var fog = fleetController.gameObject.GetComponent<csFogVisibilityAgent>();
                                if (fog != null)
                                    fog.spriteRenderers.Add(Renderers[i]);
                            }
                            else if (Renderers[i].name == "InsigniaUnknown")
                                Renderers[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}