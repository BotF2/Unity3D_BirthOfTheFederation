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


namespace Assets.Core
{
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager instance;

        public List<FleetSO> fleetSOList;// all possible fleetSO(s)
        public GameObject fleetPrefab;
        public GameObject galaxyImage;
        public GameObject galaxyCenter;
        public List<FleetController> ManagersFleetControllerList;
        public List<GameObject> FleetGOList = new List<GameObject>(); // all fleetGO GOs made
        private List<ShipController> shipsOfAllFirstFleets;
        private bool PlaceHolderIsDestroyed = false;


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
            GameObject fleetGOPlaceHolder = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
            fleetGOPlaceHolder.gameObject.tag = "FleetPlaceHolder";
            fleetGOPlaceHolder.name = "999";
            var fleetController = fleetGOPlaceHolder.GetComponentInChildren<FleetController>();
            fleetController.Name = "999";
            FleetData fleetPlaceHolderData = new FleetData("999");
            fleetController.FleetData = fleetPlaceHolderData;
            fleetController.FleetData.CivOwnerEnum = CivEnum.ZZUNINHABITED10;
            fleetController.FleetData.Name =fleetController.Name;
            FleetGOList.Add(fleetGOPlaceHolder);
            AddFleetConrollerToAllControllers(fleetController);
            shipsOfAllFirstFleets = ShipManager.instance.GetShipControllersOfFirstFleet();
        }

        public void UpdateFleetShipControllers(ShipController shipController) // one at a time, as ShipManager makes ships by Menu civ
        {
            shipsOfAllFirstFleets.Add(shipController);
            
            foreach (var fleetController in ManagersFleetControllerList)
            {
                if (fleetController.FleetData.CivOwnerEnum == shipController.ShipData.CivEnum)
                {
                    if (!fleetController.FleetData.ShipsList.Contains(shipController))
                    {
                        shipController.gameObject.transform.SetParent(fleetController.gameObject.transform);
                        fleetController.ShipControllerList.Add(shipController);
                        fleetController.FleetData.AddToShipList(shipController);
                    }
                }
            }
            if (!PlaceHolderIsDestroyed)
            {
                GameObject[] shipObjects = GameObject.FindGameObjectsWithTag("ShipPlaceHolder");
                for (int i = 0; i < shipObjects.Count(); i++)
                {
                    Destroy(shipObjects[i]);
                } 
                GameObject[] fleetObjects = GameObject.FindGameObjectsWithTag("FleetPlaceHolder");
                Destroy(fleetObjects[0]);
                PlaceHolderIsDestroyed = true;
            }
        }
        public void FleetDataFromSO(CivSO civSO, Vector3 position) // first fleetGO
        {
            FleetSO fleetSO = GetFleetSObyInt(civSO.CivInt);
            if (fleetSO != null)
            {
                FleetData fleetData = new FleetData(fleetSO);
                fleetData.CivIndex = fleetSO.CivIndex;
                fleetData.Insignia = fleetSO.Insignia;
                fleetData.CivOwnerEnum = fleetSO.CivOwnerEnum;
                fleetData.Position = position;
                fleetData.CurrentWarpFactor = fleetSO.CurrentWarpFactor;
                fleetData.CivLongName = civSO.CivLongName;
                fleetData.CivShortName = civSO.CivShortName;
                fleetData.Name = "998";
                InstantiateFleet(fleetData, position);
            }           
        }
        private void GetUniqueFleetName(CivEnum civEnum, FleetController newFleetController)
        {
            List<int> ints = new List<int>() { 999 };
            if (ManagersFleetControllerList != null)
            {
                var controllersByCivEnum = new List<FleetController>() { newFleetController };
                foreach (var controller in ManagersFleetControllerList)
                {
                    if (controller.FleetData.CivOwnerEnum == newFleetController.FleetData.CivOwnerEnum)
                    {
                        if (controller != newFleetController)
                            controllersByCivEnum.Add(controller);
                    }
                }
                for (int j = 0; j < controllersByCivEnum.Count; j++) // Build ints list
                {
                    ints.Add(int.Parse(controllersByCivEnum[j].Name));
                }
                for (int i = 0; i < controllersByCivEnum.Count; i++)
                {
                    if (controllersByCivEnum[i].Name == "998")
                    {
                        if (!ints.Contains(i + 1))
                        {
                            controllersByCivEnum[i].Name = (i + 1).ToString();
                            controllersByCivEnum[i].FleetData.Name = (i + 1).ToString();
                        }  
                    }
                }
            }
        }
        public void InstantiateFleet(FleetData fleetData, Vector3 position)
        {
            if (fleetData.CivOwnerEnum != CivEnum.ZZUNINHABITED10)
            {
                GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity);
                var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
                fleetController.FleetData = fleetData;
                fleetController.Name = fleetData.Name;
                AddFleetConrollerToAllControllers(fleetController);
                GetUniqueFleetName(fleetData.CivOwnerEnum, fleetController);
                fleetNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 40f, fleetData.Position.y, fleetData.Position.z + 10f));
                fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
                fleetNewGameOb.transform.localScale = new Vector3(1, 1, 1);

                fleetNewGameOb.name = fleetData.CivOwnerEnum.ToString() + " Fleet " + fleetData.Name; // game object FleetName
                TextMeshProUGUI TheText = fleetNewGameOb.GetComponentInChildren<TextMeshProUGUI>();

                TheText.text = fleetData.CivShortName + " - Fleet " + fleetData.Name;
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

                fleetNewGameOb.SetActive(true);
                FleetGOList.Add(fleetNewGameOb);
                StarSysManager.instance.GetYourFirstStarSystem(fleetData.CivOwnerEnum);

                GameManager.Instance.LoadGalacticDestinations(fleetData, fleetNewGameOb.transform);
            }
            else if(fleetData.CivOwnerEnum == CivEnum.ZZUNINHABITED10)
            {
                //RemoveFleetConrollerFromAllControllers(? controller)
            }

        }
        void AddFleetConrollerToAllControllers(FleetController fleetController)
        {
            ManagersFleetControllerList.Add(fleetController); // add add for Manager
            foreach (FleetController fleetCon in ManagersFleetControllerList)
            {
                fleetCon.AddFleetController(fleetController); // add for Controller
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