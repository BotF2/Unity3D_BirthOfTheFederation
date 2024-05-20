using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace Assets.Core
{

    public class CivManager : MonoBehaviour
    {
        public static CivManager instance;

        public List<CivSO> civSOListSmall;
        public List<CivSO> civSOListMedium;
        public List<CivSO> civSOListLarge;
        public List<CivData> civDataInGameList = new List<CivData> { new CivData()};
        public List<CivData> contactList = new List<CivData>() { new CivData()};
        public List<CivController> civControllerList;
        public CivData localPlayer;
        public CivData resultInGameCivData;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else
            { 
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        public CivData CreateLocalPlayer()
        {
            localPlayer = GetCivDataByName("FEDERATION");

            //localPlayer = Instantiate(civilizationPrefab).GetComponent<CivData>();
            //CivDataFromCivSO(localPlayer, civSOListSmall[0]); // local player first in list ****
            //civilizationPrefab.GetComponent<CivData>(); 
            return localPlayer;
            
        }
        public void CivDataFromCivSO(CivData civData, CivSO civSO)
        {
            civData.CivInt = civSO.CivInt;
            civData.CivShortName = civSO.CivShortName;
        }

        public void CreateNewGameBySize(int sizeGame)
        {
            if (sizeGame == 0)
            {
                CivDataFromSO(civSOListSmall);
                //FleetManager.CreateNewGameFleets(1);
            }
            if (sizeGame == 1)
            {
                CivDataFromSO(civSOListMedium);
                //FleetManager.CreateNewGameFleets(2);
            }
            if (sizeGame == 2)
            {
                CivDataFromSO(civSOListLarge);
                //FleetManager.CreateNewGameFleets(3);
            }
        }

        public void CivDataFromSO(List<CivSO> civSOList)
        {
            foreach (var civSO in civSOList)
            {
                CivData civData = new CivData();
                civData.CivInt = civSO.CivInt;
                civData.CivEnum = civSO.CivEnum;
                civData.CivLongName = civSO.CivLongName;
                civData.CivShortName = civSO.CivShortName;
                civData.TraitOne = civSO.TraitOne;
                civData.TraitTwo = civSO.TraitTwo;
                civData.CivImage = civSO.CivImage;
                civData.Insignia = civSO.Insignia;
                civData.Population = civSO.Population;
                civData.Credits = civSO.Credits;
                civData.TechPoints = civSO.TechPoints;
                civData.CivTechLevel = civSO.CivTechLevel;
                if (civSO.CivInt <= 5 || civSO.CivInt == 158)
                    civData.Playable = true;
                else civData.Playable = false;
                civData.HasWarp = civSO.HasWarp;
                civData.Decription = civSO.Decription;
                civData.StarSysOwned = civSO.StarSysOwned;
                //civData.TaxRate = civSO.TaxRate;
                //civData.GrowthRate = civSO.GrowthRate;
                civData.IntelPoints = civSO.IntelPoints;
                civData.ContactList = contactList;
                civData.ContactList.Add(civData); // add civ as contact for itself
                civData.ContactList.Remove(civData.ContactList[0]); // remove the null from above field
                civDataInGameList.Add(civData);
                InstantiateCiv(civData);
            }
            civDataInGameList.Remove(civDataInGameList[0]); // remove the null entered above
            StarSysManager.instance.SysDataFromSO(civSOList);
        }
        public void InstantiateCiv(CivData civData) //??????? is Civ a game object
        {
            //GameObject civNewGameOb = (GameObject)Instantiate(civPrefab, new Vector3(0, 0, 0),
            //     Quaternion.identity);
            //civNewGameOb.transform.Translate(new Vector3(sysData.Position.x, sysData.Position.y, sysData.Position.z));

            //civNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            //civNewGameOb.transform.localScale = new Vector3(1, 1, 1);
            //civNewGameOb.name = sysData.SysName;
            //sysData.SysTransform = civNewGameOb.transform;
            //var ImageRenderers = civNewGameOb.GetComponentsInChildren<SpriteRenderer>();

            //TextMeshProUGUI[] TheText = civNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
            //foreach (var OneTmp in TheText)
            //{
            //    OneTmp.enabled = true;
            //    if (OneTmp != null && OneTmp.name == "SysName (TMP)")
            //        OneTmp.text = sysData.SysName;
            //    else if (OneTmp != null && OneTmp.name == "SysDescription (TMP)")
            //        OneTmp.text = sysData.Description;

            //}
            //var Renderers = civNewGameOb.GetComponentsInChildren<SpriteRenderer>();
            //foreach (var oneRenderer in Renderers)
            //{
            //    if (oneRenderer != null)
            //    {
            //        //if (oneRenderer.FleetName == "CivRaceSprite")
            //        //{
            //        //    oneRenderer.sprite = civSO.CivImage; // ok
            //        //}

            //        if (oneRenderer.name == "OwnerInsignia")
            //        {
            //            oneRenderer.sprite = civSO.Insignia;
            //            //oneRenderer.sprite.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            //        }
            //        else if (oneRenderer.name == "StarSprite")
            //            oneRenderer.sprite = sysData.StarSprit;
            //    }
            //}
            //DropLineFixed ourDropLine = civNewGameOb.GetComponentInChildren<DropLineFixed>();

            //ourDropLine.GetLineRenderer();

            //Vector3 galaxyPlanePoint = new Vector3(civNewGameOb.transform.position.x,
            //    galaxyImage.transform.position.y, civNewGameOb.transform.position.z);
            //Vector3[] points = { civNewGameOb.transform.position, galaxyPlanePoint };
            //ourDropLine.SetUpLine(points);
            //StarSysController controller = civNewGameOb.GetComponentInChildren<StarSysController>();
            //controller.starSysData = sysData;
            ////Transform canvasTrans = civNewGameOb.transform.Find("CanvasSysButton");
            ////// Check if the child GameObject exists
            ////if (canvasTrans != null)
            ////{
            ////    // Is there a UI game object we need to turn on and off
            ////    //controller.c = canvasTrans.gameObject;
            ////    //controller.canvasFleetUIbutton.SetActive(false);
            ////}
            ////Transform canvasTransButton = civNewGameOb.transform.Find("Canvas Load FleetUI");
            ////// Check if the child GameObject exists
            ////if (canvasTransButton != null)
            ////{
            ////    canvasTransButton.SetParent(civNewGameOb.transform, true);
            ////}

            //civNewGameOb.SetActive(true);
            //StarSysControllerList.Add(controller);
            ////civControllerList.Add(civController);
        }
        

        public CivData GetCivDataByName(string shortName)
        {

            CivData result = null;


            foreach (var civ in civDataInGameList)
            {

                if (civ.CivShortName.Equals(shortName))
                {
                    result = civ;
                }


            }
            return result;

        }
        public void OnNewGameButtonClicked(int gameSize)
        {
            CreateNewGameBySize(gameSize);

        }

        public void GetCivByName(string civname)
        {
            resultInGameCivData = GetCivDataByName(civname);

        }
    }
}
