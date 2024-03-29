using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;


namespace Assets.Core
{
    public class FleetManager : MonoBehaviour
    {
        public static FleetManager instance;

        public List<FleetSO> fleetSOList;

        public GameObject fleetPrefab;

        public List<FleetData> fleetDataList;

        public GameObject galaxyImage;

        public GameObject galaxyCenter;

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
        public void CreatGameStarterFleets(List<CivSO> listCivSO)
        {
            List<FleetData> fleetDatas = new List<FleetData>();
            fleetDataList = fleetDatas;
            foreach (var civSO in listCivSO)
            {
                FleetSO fleetSO = GetFleetSObyInt(civSO.CivInt);
                FleetData fleetData = new FleetData();
                fleetData.civIndex = fleetSO.CivIndex;
                fleetData.fleetName = fleetSO.Name;
                fleetData.description = fleetSO.description;
                fleetData.insign = fleetSO.Insignia;
                fleetData.civOwnerEnum = fleetSO.CivOwnerEnum;
                fleetData.location  = fleetSO.location;
                fleetData.ships = fleetSO.Ships;
                fleetData.warpFactor = fleetSO.warpFactor;
                fleetData.destination = fleetSO.destination;
                fleetData.origin = fleetSO.origin;
                fleetData.defaultWarp = 0;
                if (!fleetDataList.Contains(fleetData))
                    fleetDataList.Add(fleetData);
                InstantiateStarterFleets(fleetData, fleetSO);
            }
        }
        public void InstantiateStarterFleets(FleetData fleetData, FleetSO fleetSO)
        {
            GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
            fleetNewGameOb.transform.Translate(new Vector3(fleetSO.location.x, fleetSO.location.y, fleetSO.location.z ));
            //fleetNewGameOb.transform.Translate(new Vector3(sysData.Position.x, sysData.Position.z, sysData.Position.y));
            fleetNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            //fleetNewGameOb.transform.localScale = new Vector3(1, 1, 1);
            fleetNewGameOb.name = "Klingon Fart"; // fleetData.fleetName;
            var ImageRenderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();

            var TMPs = fleetNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TMPs)
            {
                if (OneTmp != null && OneTmp.name == "StarName (TMP)")
                    OneTmp.text = fleetData.fleetName;
                else if (OneTmp != null && OneTmp.name == "Owner (TMP)")
                    OneTmp.text = fleetData.fleetName.ToString();
            }
            var Renderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();
            foreach (var oneRenderer in Renderers)
            {
                if (oneRenderer != null)
                {
                    if (oneRenderer.name == "OwnerInsignia")
                    {
                        oneRenderer.sprite = fleetSO.Insignia;
                        //oneRenderer.sprite.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
                    }

                }
            }
            DropLineMovable ourDropLine = fleetNewGameOb.GetComponent<DropLineMovable>();

            ourDropLine.GetLineRenderer();

            Vector3 galaxyPlanePoint = new Vector3(fleetNewGameOb.transform.position.x,
                galaxyImage.transform.position.y, fleetNewGameOb.transform.position.z);
            Vector3[] points = { fleetNewGameOb.transform.position, galaxyPlanePoint };
            ourDropLine.SetUpLine(points);

            fleetNewGameOb.SetActive(true);
        }
        public FleetData resultFleetData;

        public FleetData GetFleetDataByName(string fleetName)
        {

            FleetData result = null;

            foreach (var fleet in fleetDataList)
            {

                if (fleet.fleetName.Equals(fleetName))
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