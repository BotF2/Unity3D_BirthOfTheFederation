using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Core
{
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager instance;

        public List<FleetSO> fleetSOList;// all possible fleetSO(s), one list for each civ

        public GameObject fleetPrefab;

        public Dictionary<CivEnum, List<FleetData>> FleetDictionary; //all the fleets of that civ

        public GameObject galaxyImage;

        public GameObject galaxyCenter;

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
        public void FirstFleetData(CivSO civSO, Vector3 position) // first fleet
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
                fleetData.MaxWarpFactor = fleetSO.WarpFactor;
                fleetData.DefaultWarpFactor = fleetSO.DefaultWarpFactor;
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
                    // not fully tested until we get more fleets
                    //fleetData.Name = "998";
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
                    // if (listFleetData[j].Name != "998")
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

            fleetNewGameOb.name = fleetData.CivOwnerEnum.ToString() + "Fleet" + fleetData.Name; // game object Name
            
            var line = fleetNewGameOb.GetComponent<LineRenderer>();
            Vector3[] linePoints = new Vector3[] { fleetNewGameOb.transform.position, 
                new Vector3(fleetData.Position.x, galaxyImage.transform.position.y, fleetData.Position.z) };
            line.SetPositions(linePoints);
          //  fleetNewGameOb.GetComponent<FleetController>().lineRenderer = line;
            TextMeshProUGUI[] TheText = fleetNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;

                if (OneTmp != null && OneTmp.name == "Text FleetName (TMP)")
                    OneTmp.text = fleetData.CivShortName + " - Fleet " + fleetData.Name;
                else if (OneTmp != null && OneTmp.name == "Text Description (TMP)")
                    OneTmp.text = fleetData.Description;
            }

            var Renderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();
            foreach (var oneRenderer in Renderers)
            {
                if (oneRenderer != null)
                {
                    if (oneRenderer.name == "OwnerInsignia")
                    {
                        oneRenderer.sprite = fleetData.Insignia;
                        //oneRenderer.sprite.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
                    }

                }
            }
            DropLineMovable ourDropLine = fleetNewGameOb.GetComponent<DropLineMovable>();

            ourDropLine.GetLineRenderer();
            ourDropLine.transform.SetParent(fleetNewGameOb.transform, true);
            Vector3 galaxyPlanePoint = new Vector3(fleetNewGameOb.transform.position.x,
                galaxyImage.transform.position.y, fleetNewGameOb.transform.position.z);
            Vector3[] points = { fleetNewGameOb.transform.position, galaxyPlanePoint };
            ourDropLine.SetUpLine(points);

            var controller = fleetNewGameOb.GetComponentInChildren<FleetController>();
            
            controller.fleetData = fleetData;
            //fleetNameText.text = fleetData.Name;
            //fleetDescriptionText.text = fleetData.Description;

            // Find the child GameObject by Name
            Transform canvasTransFleetUI = fleetNewGameOb.transform.Find("CanvasFleetUI");
            // Check if the child GameObject exists
            if (canvasTransFleetUI != null)
            {
                controller.canvasFleetUIButton = canvasTransFleetUI.gameObject;
            }
            controller.canvasFleetUIButton.SetActive(false);
            Transform canvasTransButton = fleetNewGameOb.transform.Find("CanvasFeetUIButton");
            // Check if the child GameObject exists
            if (canvasTransButton != null)
            {
                canvasTransButton.SetParent(fleetNewGameOb.transform, true);
            }
            controller.fleetData.deltaYofGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

            fleetNewGameOb.SetActive(true);
            
        }

        public FleetData resultFleetData;

        //public FleetData GetFleetDataByName(string fleetName)
        //{

        //    FleetData result = null;

        //    foreach (var fleet in fleetDataList)
        //    {

        //        if (fleet.Name.Equals(fleetName))
        //        {
        //            result = fleet;
        //        }
        //    }
        //    return result;

        //}
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

        //public void GetFleetByName(string fleetName)
        //{
        //    resultFleetData = GetFleetDataByName(fleetName);
        //}

    }
}