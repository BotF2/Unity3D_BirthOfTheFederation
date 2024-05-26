using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


namespace Assets.Core
{
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager instance;

        public List<FleetSO> fleetSOList;// all possible fleetSO(s), one list for each civ
        public GameObject fleetPrefab;
        public GameObject galaxyImage;
        public GameObject galaxyCenter;
        public List<FleetController> ManagersFleetControllerList;
        public List<GameObject> FleetGOList = new List<GameObject>(); // all fleetGO GOs made
        public Dictionary<CivEnum, List<FleetData>> FleetDictionary; //all the fleetGO datas of that civ
        //public TextMeshProUGUI fleetNameText;
        //public TextMeshProUGUI fleetDescriptionText;


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
            var data = new FleetData("999");
            List<FleetData> list = new List<FleetData>() {data};
            FleetDictionary = new Dictionary<CivEnum, List<FleetData>>() { { CivEnum.ZZUNINHABITED9, list } };
        }
        public void FleetDataFromSO(CivSO civSO, Vector3 position) // first fleetGO
        {

            FleetSO fleetSO = GetFleetSObyInt(civSO.CivInt);
            if (fleetSO != null)
            {
                FleetData fleetData = new FleetData();
                fleetData.CivIndex = fleetSO.CivIndex;
                fleetData.Insignia = fleetSO.Insignia;
                fleetData.CivOwnerEnum = fleetSO.CivOwnerEnum;
                fleetData.Position = position;
                fleetData.ShipsList = fleetSO.ShipsList;
                fleetData.MaxWarpFactor = fleetSO.maxWarpFactor;
                fleetData.CurrentWarpFactor = fleetSO.currentWarpFactor;
                fleetData.CivLongName = civSO.CivLongName;
                fleetData.CivShortName = civSO.CivShortName;
                fleetData.Description = fleetSO.Description;
                fleetData.Name = "998";
                if (!FleetDictionary.ContainsKey(civSO.CivEnum))
                {
                    List<FleetData> listA = new List<FleetData>() { fleetData };
                    FleetDictionary.Add(civSO.CivEnum, listA);
                }
                else FleetDictionary[civSO.CivEnum].Add(fleetData);
                if (fleetData.Name != "999")
                {
                    GetFleetName(civSO.CivEnum);
                    InstantiateFleet(fleetData, position);
                }
            }
            
        }
        private void GetFleetName(Assets.Core.CivEnum civEnum)
        {
            var listFleetData = FleetDictionary[civEnum];
            List<int> ints = new List<int>() { 999};

            if (listFleetData != null)
            {
                for (int j = 0; j < listFleetData.Count; j++) // build the list of int names
                {
                    ints.Add(int.Parse(listFleetData[j].Name));
                }
                for (int i = 0; i < listFleetData.Count; i++)
                {
                    if (!ints.Contains(i + 1))
                    {
                        listFleetData[i].Name = (i + 1).ToString();
                    }
                    else if (listFleetData[i].Name == "998")
                    {
                        listFleetData[i].Name = (i + 1).ToString();
                    }
                }
            }         
        }
        public void InstantiateFleet(FleetData fleetData, Vector3 position)
        {
            
            GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0,0,0),
                    Quaternion.identity);
            fleetNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 40f, fleetData.Position.y, fleetData.Position.z + 10f));
            fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            fleetNewGameOb.transform.localScale = new Vector3(1, 1, 1);

            fleetNewGameOb.name = fleetData.CivOwnerEnum.ToString() + " Fleet " + fleetData.Name; // game object FleetName
            //var canvas = fleetNewGameOb.GetComponent<Canvas>();
            //var fleetGO = FindGameObjectInChildrenWithTag(fleetNewGameOb,"Fleet");
            //fleetGO.transform.SetParent(canvas.transform, true);
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

            var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
            
            fleetController.fleetData = fleetData;

            fleetController.fleetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

            fleetNewGameOb.SetActive(true);
            FleetGOList.Add(fleetNewGameOb);
            AddFleetConrollerToAllControllers(fleetController);
            StarSysManager.instance.GetYourFirstStarSystem(fleetData.CivOwnerEnum);
   
        }
        void AddFleetConrollerToAllControllers(FleetController fleetController)
        {
            ManagersFleetControllerList.Add(fleetController);
            foreach (FleetController fleetCon in ManagersFleetControllerList)
            {
                fleetCon.AddFleetController(fleetController);
            }
        }
        void RemoveFleetConrollerToAllControllers(FleetController fleetController)
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