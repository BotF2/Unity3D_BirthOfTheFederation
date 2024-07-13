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
            fleetController.FleetData.CivEnum = CivEnum.ZZUNINHABITED10;
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
                if (fleetController.FleetData.CivEnum == shipController.ShipData.CivEnum)
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
                if (GameManager.Instance._galaxySize == GalaxySize.SMALL)
                {
                    FleetData fleetData = new FleetData(fleetSO);
                    fleetData.CivIndex = fleetSO.CivIndex;
                    fleetData.Insignia = fleetSO.Insignia;
                    fleetData.CivEnum = fleetSO.CivOwnerEnum;
                    fleetData.Position = position;
                    fleetData.CurrentWarpFactor = 0f;
                    fleetData.CivLongName = civSO.CivLongName;
                    fleetData.CivShortName = civSO.CivShortName;
                    fleetData.Name = "1";
                    InstantiateFleet(fleetData, position);
                }
                else if (GameManager.Instance._galaxySize == GalaxySize.MEDIUM)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        FleetData fleetData = new FleetData(fleetSO);
                        fleetData.CivIndex = fleetSO.CivIndex;
                        fleetData.Insignia = fleetSO.Insignia;
                        fleetData.CivEnum = fleetSO.CivOwnerEnum;
                        fleetData.Position = position + new Vector3(i * 40, 0, 0);
                        fleetData.CurrentWarpFactor = 0f;
                        fleetData.CivLongName = civSO.CivLongName;
                        fleetData.CivShortName = civSO.CivShortName;
                        fleetData.Name = (i+1).ToString();
                        InstantiateFleet(fleetData, position);
                    }
                }
            }           
        }
        public void InstantiateFleet(FleetData fleetData, Vector3 position)
        {
            if (fleetData.CivEnum != CivEnum.ZZUNINHABITED10)
            {
                GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity);
                var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
                fleetController.FleetData = fleetData;
                fleetController.Name = fleetData.Name;
                AddFleetConrollerToAllControllers(fleetController);
                fleetNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 40f, fleetData.Position.y, fleetData.Position.z + 10f));
                fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
                fleetNewGameOb.transform.localScale = new Vector3(1, 1, 1);

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
                fleetController.dropLine = ourLineScript;
                foreach (var civCon in CivManager.instance.allCivControllersList) 
                {
                    if (civCon.CivShortName == fleetData.CivShortName) 
                        fleetData.OurCivController = civCon;
                }
                fleetNewGameOb.SetActive(true);
                FleetGOList.Add(fleetNewGameOb);
               // StarSysManager.instance.GetYourFirstStarSystem(fleetData.CivOwnerEnum);

                GameManager.Instance.LoadGalacticDestinations(fleetData, fleetNewGameOb);
            }
        }
        void AddFleetConrollerToAllControllers(FleetController fleetController)
        {
            foreach (var fleetCon in ManagersFleetControllerList)
            {
                //int howMany = 0;
                List<FleetController> list = new List<FleetController>() { ManagersFleetControllerList[0] };
                if (fleetCon.FleetData.CivEnum == fleetController.FleetData.CivEnum)
                {
                    list.Add(fleetCon);
                    foreach (var item in list)
                    {
                        if (item.FleetData.CivEnum != fleetController.FleetData.CivEnum)
                            list.Remove(item);
                    }
                }
                list.Last().FleetData.Name = (Int32.Parse(fleetController.FleetData.Name)+1).ToString();
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