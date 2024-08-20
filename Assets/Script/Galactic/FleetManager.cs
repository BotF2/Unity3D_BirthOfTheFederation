using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assets.Core;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.UIElements;


namespace Assets.Core
{
    /// <summary>
    /// Instantiates the fleets (a FleetController and a FleetData) using FleetSO
    /// </summary>
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager Instance;
        [SerializeField]
        private List<FleetSO> fleetSOList;// all possible fleetSO(s)
        [SerializeField]
        private GameObject fleetPrefab;
        [SerializeField]
        private GameObject galaxyImage;
        [SerializeField]
        private GameObject galaxyCenter;
        public List<FleetController> ManagersFleetControllerList;
        public List<GameObject> FleetGOList = new List<GameObject>(); // all fleetGO GOs made
        [SerializeField]
        private GameObject fleetGroupPrefab;
       
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
        private void Update()
        { 

        }

        public void FleetDataFromSO(CivSO civSO, Vector3 position)
        {
            // sent from StarSystem for civs with warp, first fleets from Systems/Civs with warp
            FleetSO fleetSO = GetFleetSObyInt(civSO.CivInt);
            int xyzBump = 1;
            if (fleetSO != null)
            {
                BuildFirstFleets(xyzBump, civSO, position);
                // *** This is an option for more fleets/ships with larger galaxy
                //switch (GameManager.Instance.GalaxySize)
                //{
                //    case GalaxySize.SMALL:
                //        BuildFirstFleets(xyzBump, civSO, position);
                //        break;
                //    case GalaxySize.MEDIUM:
                //        BuildFirstFleets(xyzBump +1, civSO, position);
                //        break;
                //    case GalaxySize.LARGE:
                //        BuildFirstFleets(xyzBump +2, civSO, position);
                //        break;
                //    default:
                //        BuildFirstFleets(xyzBump, civSO, position);
                //        break;
                //}
            }           
        }
        private void BuildFirstFleets(int myInt, CivSO civSO, Vector3 position)
        {
            FleetSO fleetSO = GetFleetSObyInt(civSO.CivInt);
            for (int i = 0; i < myInt; i++)
            {
                FleetData fleetData = new FleetData(fleetSO); // FleetData is not MonoBehavior so new is OK
                fleetData.CivIndex = fleetSO.CivIndex;
                fleetData.Insignia = fleetSO.Insignia;
                fleetData.CivEnum = fleetSO.CivOwnerEnum;
                fleetData.Position = position + new Vector3(i * 40, 0, 0);
                fleetData.CurrentWarpFactor = 0f;
                fleetData.CivLongName = civSO.CivLongName;
                fleetData.CivShortName = civSO.CivShortName;
                fleetData.Name = (myInt -i).ToString();
                InstantiateFleet(fleetData, position);
            }
        }
        public void InstantiateFleet(FleetData fleetData, Vector3 position)
        {
            if (fleetData.CivEnum != CivEnum.ZZUNINHABITED10)
            {
                GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity);
                FleetGOList.Add(fleetNewGameOb);
                
                var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
                fleetController.FleetData = fleetData;
                fleetController.Name = fleetData.Name;
                fleetController.FleetState = FleetState.FleetStationary;
                ManagersFleetControllerList.Add(fleetController);
                fleetNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 40f,  fleetData.Position.y + 10f, fleetData.Position.z));
                fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
                fleetNewGameOb.transform.localScale = new Vector3(0.7f, 0.7f, 1); // scale ship insignia here
                fleetNewGameOb.name = fleetData.CivShortName.ToString() + " Fleet " + fleetData.Name; // name game object
                TextMeshProUGUI TheText = fleetNewGameOb.GetComponentInChildren<TextMeshProUGUI>();

                TheText.text = fleetNewGameOb.name;
                fleetData.Name = TheText.text;
                var Renderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();
                foreach (var oneRenderer in Renderers)
                {
                    if (oneRenderer != null)
                    {
                        if (oneRenderer.name == "Insignia")
                        {
                            oneRenderer.sprite = fleetData.Insignia;
                        }
                    }
                }
                DropLineMovable ourLineScript = fleetNewGameOb.GetComponentInChildren<DropLineMovable>();
                
                ourLineScript.GetLineRenderer();
                ourLineScript.transform.SetParent(fleetNewGameOb.transform, false);
                Vector3 galaxyPlanePoint = new Vector3(fleetNewGameOb.transform.position.x,
                    galaxyImage.transform.position.y, fleetNewGameOb.transform.position.z);
                Vector3[] points = { fleetNewGameOb.transform.position, galaxyPlanePoint };
                ourLineScript.SetUpLine(points);
                fleetController.FleetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;
                fleetController.DropLine = ourLineScript;
                fleetController.FleetData.ShipsList.Clear();
                foreach (var civCon in CivManager.Instance.CivControllersInGame)
                {
                    if (civCon.CivData.CivEnum == fleetData.CivEnum)
                        fleetData.OurCivController = civCon;
                }
                List<FleetController> list = new List<FleetController>() { fleetController}; 
                fleetController.FleetData.FleetGroupControllers = list;
                fleetNewGameOb.SetActive(true);
                
                ShipManager.Instance.BuildShipsOfFirstFleet(fleetNewGameOb);
                
                GameManager.Instance.LoadGalacticDestinations(fleetData, fleetNewGameOb);
            }
        }
        void RemoveFleetConrollerFromAllControllers(FleetController fleetController)
        {
            ManagersFleetControllerList.Remove(fleetController);
            foreach (FleetController fleetCon in ManagersFleetControllerList)
            {
                fleetCon.RemoveFleetController(fleetController);
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
            foreach (GameObject fleetGO in FleetGOList)
            {
                if (fleetGO.GetComponentInChildren<FleetController>() == fleetController)
                {
                    ourFleetGO = fleetGO;
                }
            }
            return ourFleetGO;
        }
        public FleetSO GetFleetSObyInt(int fleetInt)
        {

            FleetSO result = null;


            foreach (var fleetSO in fleetSOList)
            {

                if (fleetSO.CivIndex == fleetInt)
                {
                    result = fleetSO;
                    break;
                }
            }
            return result;

        }
        public static GameObject FindGameObjectInChildrenWithTag(GameObject parent, string tag)
        {
            Transform t = parent.transform;
            for (int i = 0; i< t.childCount; i++)
            {
                if(t.GetChild(i).gameObject.tag == tag)
                return t.GetChild(i).gameObject;
            }
            return null;
        }


    }
}