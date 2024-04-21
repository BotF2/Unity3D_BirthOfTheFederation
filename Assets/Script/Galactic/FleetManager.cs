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

        public List<FleetSO> fleetSOList;// all possible fleetSO(s), one for each civ

        public GameObject fleetPrefab;

        //public List<FleetData> fleetDataList = new List<FleetData> { new FleetData("999")}; // place holder FeetData named "999"

        public Dictionary<CivEnum, List<FleetData>> FleetDictionary;

        public GameObject galaxyImage;

        public GameObject galaxyCenter;

        //private string StarName;

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
                fleetData.WarpFactor = fleetSO.WarpFactor;
                fleetData.DefaultWarpFactor = fleetSO.DefaultWarpFactor;
                fleetData.CivLongName = civSO.CivLongName;
                fleetData.CivShortName = civSO.CivShortName;
                fleetData.Description = fleetSO.Description;
                fleetData.Destination = fleetSO.Destination;
                if (!FleetDictionary.ContainsKey(civSO.CivEnum))
                {
                    List<FleetData> listA = new List<FleetData>() { fleetData };
                    FleetDictionary.Add(civSO.CivEnum, listA);
                }
                else FleetDictionary[civSO.CivEnum].Add(fleetData);
                if (fleetData.Name != "999")
                {
                    // not fully tested until we get more fleets
                    fleetData.Name = "998";
                    GetFleetName(civSO.CivEnum);
                    InstantiateFleet(fleetData, position);
                }
            }

        }
        private void GetFleetName(Assets.Core.CivEnum civEnum)
        {
            var listFleetData = FleetDictionary[civEnum];
            List<int> ints = new List<int>();
            for (int j = 0 ; j < listFleetData.Count; j++)
            {
               // if (listFleetData[j].Name != "998")
                    ints.Add(int.Parse(listFleetData[j].Name));
            }
            for (int i = 0; i < listFleetData.Count; i++)
            {
                if (!ints.Contains(i +1))
                {
                    listFleetData[i].Name = (i+1).ToString();
                }
                else if (listFleetData[i].Name == "998")
                {
                    listFleetData[i].Name = (i+1).ToString();
                }
            }          
        }
        public void InstantiateFleet(FleetData fleetData, Vector3 position)
        {
            GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0,0,0),
                    Quaternion.identity);
            fleetNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 15f, fleetData.Position.y, fleetData.Position.z));
            fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            fleetNewGameOb.transform.localScale = new Vector3(1, 1, 1);

            fleetNewGameOb.name = fleetData.CivOwnerEnum.ToString() + "Fleet";

            var ImageRenderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();

            var TheText = fleetNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
            TheText[0].text = fleetData.CivShortName + " - Fleet " + fleetData.Name;
            //TheText[1].text = TheText[0].text;

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

            FleetController controller = fleetNewGameOb.GetComponentInChildren<FleetController>();
            controller.fleetData = fleetData;
            // Find the child GameObject by name
            Transform canvasTrans = fleetNewGameOb.transform.Find("Canvas FleetUI");
            // Check if the child GameObject exists
            if (canvasTrans != null)
            {
                controller.canvasFleetUI = canvasTrans.gameObject;
                //var theTMPs = canvasTrans.GetComponentsInChildren<TextMeshProUGUI>();
                //foreach (var OneTmp in theTMPs)
                //{
                //    if (OneTmp != null && OneTmp.name == "Text FleetName (TMP)")
                //        OneTmp.text = fleetData.Name;
                //    //else if (OneTmp != null && OneTmp.name == "Owner (TMP)")
                //    //    OneTmp.text = fleetData.Name.ToString();
                //}

                controller.canvasFleetUI.SetActive(false);
            }
            Transform canvasTransButton = fleetNewGameOb.transform.Find("CanvasButton");
            // Check if the child GameObject exists
            if (canvasTransButton != null)
            {
                canvasTransButton.SetParent(fleetNewGameOb.transform, true);
            }
            controller.fleetData.deltaYofGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

            fleetNewGameOb.SetActive(true);
            
        }
        private void TranslateFleetsToSystem(List<FleetData> fleetDatas)
        {
            foreach (var sysData in StarSysManager.instance.StarSysDataList)
            {
                foreach (var fleetData in fleetDatas)
                {
                    if (sysData.SysInt == fleetData.CivIndex)
                    {
                        //fleetData.Position = position;
                        break;
                    }
                    //    }              
                }

            }
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