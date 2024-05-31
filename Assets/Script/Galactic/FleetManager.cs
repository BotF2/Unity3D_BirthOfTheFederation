using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
        private List<ShipController> shipsOfFirstFleets;

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
            GameObject fleetPlaceHolder = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
            fleetPlaceHolder.name = "999";
            var fleetController = fleetPlaceHolder.GetComponentInChildren<FleetController>();
            fleetController.Name = "999";
            FleetData placeHolderData = new FleetData("999");
            fleetController.FleetData = placeHolderData;
            fleetController.FleetData.CivOwnerEnum = CivEnum.ZZUNINHABITED10;
            fleetController.FleetData.Name =fleetController.Name;
            FleetGOList.Add(fleetPlaceHolder);
            AddFleetConrollerToAllControllers(fleetController);
            shipsOfFirstFleets = ShipManager.instance.GetShipControllersOfFirstFleet();
        }
        //public void SendShipControllerForFleet(List<ShipController> shipControllerList)
        //{
        //    shipsOfFirstFleets.AddRange(shipControllerList);
        //}

        public void UpdateFleetShipControllers(ShipController shipController)
        {

            shipsOfFirstFleets.Add(shipController);
            
            foreach (var fleetController in ManagersFleetControllerList)
            {
                //if (fleetController.FleetData.Name == "999")
                //{
                //    RemoveFleetConrollerFromAllControllers(fleetController);
                //    //Destroy(fleetController);
                //}
                if (fleetController.FleetData.CivOwnerEnum == shipController.ShipData.CivEnum)
                    shipController.gameObject.transform.SetParent(fleetController.gameObject.transform);  
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
                //if (shipsOfFirstFleets.Count > 0)
                //{
                //    foreach (var shipController in shipsOfFirstFleets)
                //    {
                //        if (shipController.ShipData != null)
                //        {
                //            if (shipController.ShipData.CivEnum == fleetData.CivOwnerEnum && !shipController.gameObject.name.Contains("First Ship"))
                //                fleetController.FleetData.ShipsList.Add(shipController);

                //        }
                //        else { Destroy(shipController.gameObject); }
                //    }
                //}
                fleetNewGameOb.SetActive(true);
                FleetGOList.Add(fleetNewGameOb);

                //foreach (var fleetControl in ManagersFleetControllerList)
                //{
                //    foreach (ShipController shipControllerList in shipsOfFirstFleets)
                //    {
                //        if (shipControllerList.ShipData != null && fleetControl.FleetData.CivOwnerEnum == shipControllerList.ShipData.CivEnum)
                //            shipControllerList.gameObject.transform.SetParent(fleetControl.gameObject.transform);
                //    }
                //}
                StarSysManager.instance.GetYourFirstStarSystem(fleetData.CivOwnerEnum);
            }
            else if(fleetData.CivOwnerEnum == CivEnum.ZZUNINHABITED10)
            {
                //RemoveFleetConrollerFromAllControllers(? controller)
            }

        }
        void AddFleetConrollerToAllControllers(FleetController fleetController)
        {
            ManagersFleetControllerList.Add(fleetController); // add for Manager
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