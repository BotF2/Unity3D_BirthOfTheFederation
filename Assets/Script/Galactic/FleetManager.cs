using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Core
{
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager instance;

        public List<FleetSO> fleetSOList;

        public GameObject fleetPrefab;

        public List<FleetData> FleetDataList;

        private List<FleetData> fleetDataList = new List<FleetData>() { new FleetData()};

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
        }
        public void CreatGameStarterFleets(List<CivSO> listCivSO, List<StarSysData> starSysDatas)
        {
            foreach (var civSO in listCivSO)
            {
                if (civSO.HasWarp)
                {
                    FleetSO fleetSO = GetFleetSObyInt(civSO.CivInt);
                    if (fleetSO != null)
                    {
                        FleetData fleetData = new FleetData();
                        fleetData.CivIndex = fleetSO.CivIndex;
                        fleetData.Insignia = fleetSO.Insignia;
                        fleetData.CivOwnerEnum = fleetSO.CivOwnerEnum;

                        foreach (var sysData in starSysDatas)
                        {
                            if (sysData.SysInt == civSO.CivInt)
                            {
                                fleetData.Location = sysData.Position;
                                break;
                            }
                        }
                        fleetData.ShipsList = fleetSO.ShipsList;
                        fleetData.WarpFactor = fleetSO.WarpFactor;
                        fleetData.DefaultWarpFactor = fleetSO.DefaultWarpFactor;
                        fleetData.Name = "Fleet 1";
                        fleetData.Description = fleetSO.Description;
                        fleetData.Destination = fleetSO.Destination;
                        fleetDataList.Add(fleetData);
                        InstantiateThisStarterFleet(fleetData);
                    }
                }
            }
            fleetDataList.Remove(fleetDataList[0]);
            FleetDataList = fleetDataList;
            TranslateStarterFleetToSystem(FleetDataList);
        }
        public void InstantiateThisStarterFleet(FleetData fleetData)
        {
            GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
            fleetNewGameOb.transform.Translate(new Vector3(fleetData.Location.x +15f, fleetData.Location.y, fleetData.Location.z));
            fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            fleetNewGameOb.transform.localScale = new Vector3(1,1,1);

            fleetNewGameOb.name = fleetData.CivOwnerEnum.ToString() + "Fleet"; 
            var ImageRenderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();

             Text[] TheText = fleetNewGameOb.GetComponentsInChildren<Text>();
            foreach (var OneTmp in TheText)
            {
                //if (OneTmp != null && OneTmp.name == "StarName (TMP)")
                //    OneTmp.text = fleetData.Name;
                //else if (OneTmp != null && OneTmp.name == "Owner (TMP)")
                //    OneTmp.text = fleetData.Name.ToString();
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

            FleetController controller = fleetNewGameOb.GetComponent<FleetController>();
            controller.fleetData = fleetData;
            controller.fleetData.deltaYofGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

            fleetNewGameOb.SetActive(true);
        }
        private void TranslateStarterFleetToSystem(List<FleetData> fleetDatas)
        {
            foreach (var sysData in StarSysManager.instance.StarSysDataList)
            {
                foreach (var fleetData in fleetDatas)
                {
                    if (sysData.SysInt == fleetData.CivIndex)
                    {
                        fleetData.Location = sysData.Position;
                        break;
                    }
                }              
            }

        }
        public FleetData resultFleetData;

        public FleetData GetFleetDataByName(string fleetName)
        {

            FleetData result = null;

            foreach (var fleet in fleetDataList)
            {

                if (fleet.Name.Equals(fleetName))
                {
                    result = fleet;
                }
            }
            return result;

        }
        public FleetSO GetFleetSObyInt(int fleetInt)
        {

            FleetSO result = null;


            foreach (var fleetSO in fleetSOList)
            {

                if (fleetSO.CivIndex ==fleetInt)
                {
                    result = fleetSO;
                }
            }
            return result;

        }

        public void GetFleetByName(string fleetName)
        {
            resultFleetData = GetFleetDataByName(fleetName);
        }

    }
}