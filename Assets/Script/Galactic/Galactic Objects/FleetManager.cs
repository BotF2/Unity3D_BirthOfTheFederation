using FischlWorks_FogWar;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;



namespace Assets.Core
{
    /// <summary>
    /// Instantiates the fleets (a FleetController and a FleetData) using FleetSO
    /// </summary>
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager Instance;
        [SerializeField]
        private csFogWar fogWar;
        [SerializeField]
        private List<FleetSO> fleetSOList;// all possible fleetSO(s)
        [SerializeField]
        private GameObject fleetPrefab;
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
        //[SerializeField]
        //private GalaxyMapOurEvent galaxyMapOurEvent;
        [SerializeField]
        private List<int> intsInUse = new List<int>() { 0 };


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

        public void FleetDataFromSO(StarSysController sysCon, bool inSystem)
        {
            // first path here is sent on loading the game for civs with warp, first fleets from Systems/Civs with warp
            FleetSO fleetSO = GetFleetSObyInt((int)sysCon.StarSysData.CurrentOwner);
            int xyzBump = 1;
            if (fleetSO != null)
            {
                BuildFleets(xyzBump, fleetSO, sysCon.StarSysData.GetPosition(), inSystem);
                // *** This is an option for more fleets/ships with larger galaxy
                //switch (GameManager.current.GalaxySize)
                //{
                //    case GalaxySize.SMALL:
                //        BuildFleets(xyzBump, civSO, position);
                //        break;
                //    case GalaxySize.MEDIUM:
                //        BuildFleets(xyzBump +1, civSO, position);
                //        break;
                //    case GalaxySize.LARGE:
                //        BuildFleets(xyzBump +2, civSO, position);
                //        break;
                //    default:
                //        BuildFleets(xyzBump, civSO, position);
                //        break;
                //}
            }
        }
        public void BuildFleets(int numFleets, FleetSO fleetSO, Vector3 position, bool inSystem)
        {
            CivData thisCivData = CivManager.Instance.GetCivDataByCivEnum(fleetSO.CivOwnerEnum); // new CivData();

            //FleetSO fleetSO = GetFleetSObyInt(civInt);

            for (int i = 0; i < numFleets; i++)
            {
                FleetData fleetData = new FleetData(fleetSO); // FleetData is not MonoBehavior so new is OK

                //if (!inSystem)
                //{
                //    fleetData.Position = position + new Vector3(i * 40, 0, 0);
                //}
                fleetData.CurrentWarpFactor = 9f;
                fleetData.CivLongName = thisCivData.CivLongName; //.CivLongName;
                fleetData.CivShortName = thisCivData.CivShortName;
                //fleetData.Name = (numFleets - i).ToString();
                InstantiateFleet(fleetData, position, inSystem);
            }
            
        }
        
        public void InstantiateFleet(FleetData fleetData, Vector3 position, bool inSystem)
        {
            GameObject fleetNewGameOb = new GameObject();
    
            IEnumerable<StarSysController> ourCivSysCons =
                from x in StarSysManager.Instance.StarSysControllerList
                where (x.StarSysData.CurrentOwner == fleetData.CivEnum)
                select x;
            var ourSysCons = ourCivSysCons.ToList();
            if (fleetData.CivEnum != CivEnum.ZZUNINHABITED1)
            {
                fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity);
                FleetGOList.Add(fleetNewGameOb);
                fleetNewGameOb.layer = 6; // galaxy layer
                var fleetNumbers = fleetNewGameOb.GetComponent<FleetNumbers>();
                var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
                fleetController.BackgroundGalaxyImage = galaxyImage;
                fleetController.FleetData = fleetData;
                //fleetController.Name = fleetData.Name;
                fleetController.FleetState = FleetState.FleetStationary;
                //fleetController.GalaxyMapDestinationEvent = galaxyMapOurEvent;
                FleetControllerList.Add(fleetController);
                if (!inSystem)
                {
                    fleetNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 40f, fleetData.Position.y + 10f, fleetData.Position.z));
                }
                else
                {
                    for (int i = 0; i < ourSysCons.Count; i++)
                    {
                        if (ourSysCons[i].StarSysData.GetPosition() == position)
                        {
                            ourSysCons[i].StarSysData.FleetsInSystem.Add(fleetNewGameOb);
                            fleetNewGameOb.transform.SetParent(ourSysCons[i].gameObject.transform);
                        }
                    }
                }
                fleetNewGameOb.transform.localScale = new Vector3(0.7f, 0.7f, 1); // scale ship insignia here
                fleetNewGameOb.name = fleetData.CivShortName.ToString() + " Fleet " + fleetNumbers.GetNewFleetInt().ToString(); // name game object
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
                            if (!GameController.Instance.AreWeLocalPlayer(fleetController.FleetData.CivEnum))
                            {
                                Renderers[i].gameObject.SetActive(false);
                            }
                        }
                        if (Renderers[i].name == "InsigniaUnknown" && GameController.Instance.AreWeLocalPlayer(fleetController.FleetData.CivEnum))
                        {
                            Renderers[i].gameObject.SetActive(false);
                        }
                    }
                }
                // The line from Fleet to underlying galaxy image and to destination
                MapLineMovable[] ourLineToGalaxyImageScript = fleetNewGameOb.GetComponentsInChildren<MapLineMovable>();
                for (int i = 0;i < ourLineToGalaxyImageScript.Length;i++)
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
                        fleetData.OurCivController = civCon;
                }
                List<FleetController> list = new List<FleetController>() { fleetController };
                fleetController.FleetData.FleetGroupControllers = list;
                fleetNewGameOb.SetActive(true);
                if (!inSystem) // all first fleets are not in system
                    ShipManager.Instance.BuildShipsOfFirstFleet(fleetNewGameOb);
            }
            //if (inSystem)
            //{
            //    for (int i = 0; i < ourSysCons.Count; i++)
            //    {
            //        if (ourSysCons[i].StarSysData.GetPosition() == position)
            //        {
            //            ourSysCons[i].StarSysData.FleetsInSystem.Add(fleetNewGameOb);
            //        }
            //    }
            //}
        }

        void RemoveFleetConrollerFromAllControllers(FleetController fleetController)
        {
            FleetControllerList.Remove(fleetController);
        }
        void AddFleetConrollerFromAllControllers(FleetController fleetController)
        {
            FleetControllerList.Add(fleetController);
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
            if (intsInUse.Contains(destinationInt))
            {
                destinationInt++;
                if (!intsInUse.Contains(destinationInt))
                    return destinationInt;
            }
            else
            {
                intsInUse.Add(destinationInt);

            }
            return destinationInt;

        }
        public void RemoveFleet(GameObject go, int asDestinationInt)
        {
            intsInUse.Remove(asDestinationInt);
            FleetControllerList.Remove(go.GetComponent<FleetController>());
            go.IsDestroyed();
        }

    }
}