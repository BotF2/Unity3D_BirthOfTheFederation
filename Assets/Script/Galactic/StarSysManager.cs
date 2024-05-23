using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Rendering.VirtualTexturing;

namespace Assets.Core
{
    public class StarSysManager : MonoBehaviour
    {
        public static StarSysManager instance;

        public List<StarSysSO> starSysSOList;
        public GameObject sysPrefab;
        public List<StarSysData> StarSysDataList;
        public List<StarSysController> StarSysControllerList;
        private List<StarSysData> starSysDatas = new List<StarSysData>(); // { new StarSysData("Not Selected") };
        public GameObject galaxyImage;
        public GameObject galaxyCenter;
        public StarSysData resultInGameStarSysData;
        private Camera galaxyEventCamera;
        private Canvas systemUICanvas; //ToDo system ui
        public StarSysController starSystemPrefabPlaceHolder;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        public void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
            // *** ToDo system UI
            //var CanvasGO = GameObject.Find("CanvasSytemUI"); 
            //systemUICanvas = CanvasGO.GetComponent<Canvas>();
            //systemUICanvas.worldCamera = galaxyEventCamera;
        }
        public void SysDataFromSO(List<CivSO> civSOList)
        {
            StarSysData SysData = new StarSysData("Not Selected");
            starSysDatas.Add(SysData);
            foreach (var civSO in civSOList)
            {
                StarSysSO starSysSO = GetStarSObyInt(civSO.CivInt);

                //StarSysData 
                SysData = new StarSysData("Not Selected");               
                SysData.SysInt = civSO.CivInt;
                SysData.Position = new Vector3(starSysSO.Position.x, starSysSO.Position.y, starSysSO.Position.z);
                SysData.SysName = starSysSO.SysName;
                SysData.FirstOwner = starSysSO.FirstOwner;
                SysData.CurrentOwner = starSysSO.FirstOwner;
                SysData.StarType = starSysSO.StarType;
                SysData.StarSprit = starSysSO.StarSprit;
                SysData.Population = starSysSO.Population;
                SysData.Description = "description here";
                 #region more stuff
                //public float _sysCredits;
                //public float _sysTaxRate; Set it at civ level
                //public float _sysPopLimit;
                //public float _currentSysPop;
                //public float _systemFactoryLimit; Do it all with pop limit??
                //public float _currentSysFactories;
                //public float _production;
                //private int _maintenanceCostLastTurn;
                //private int _rankCredits;
                //public List<CivHistory> _civHist_List = new List<CivHistory>();
                // public bool _homeColony;
                //public string _text;
                //public GameObject _systemSphere;
                //public List<GameObject> _fleetsInSystem;
                #endregion
                
                starSysDatas.Add(SysData);
                InstantiateSystem(SysData, civSO);
                if (civSO.HasWarp) 
                    FleetManager.instance.FleetDataFromSO(civSO, SysData.Position);
            }
            StarSysDataList = starSysDatas;
            FleetUIManager.instance.LoadDestinations(StarSysDataList);

        }
        public void InstantiateSystem(StarSysData sysData, CivSO civSO)
        { 
            GameObject starSystemNewGameOb = (GameObject)Instantiate(sysPrefab, new Vector3(0,0,0),
                 Quaternion.identity);
            starSystemNewGameOb.transform.Translate(new Vector3(sysData.Position.x, sysData.Position.y, sysData.Position.z));

            starSystemNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            starSystemNewGameOb.transform.localScale = new Vector3(1, 1, 1);
            starSystemNewGameOb.name = sysData.SysName;
            sysData.SysTransform = starSystemNewGameOb.transform;
            var ImageRenderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();

            TextMeshProUGUI[] TheText = starSystemNewGameOb.GetComponentsInChildren<TextMeshProUGUI>(); 
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp != null && OneTmp.name == "SysName (TMP)")
                    OneTmp.text = sysData.SysName;
                else if (OneTmp != null && OneTmp.name == "SysDescription (TMP)")
                    OneTmp.text = sysData.Description;
   
            }
            var Renderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();
            foreach (var oneRenderer in Renderers)
            {
                if (oneRenderer != null)
                {
                    //if (oneRenderer.FleetName == "CivRaceSprite")
                    //{
                    //    oneRenderer.sprite = civSO.CivImage; // ok
                    //}

                    if (oneRenderer.name == "OwnerInsignia")
                    {
                        oneRenderer.sprite = civSO.Insignia;
                        //oneRenderer.sprite.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
                    }
                    else if (oneRenderer.name == "StarSprite")
                        oneRenderer.sprite = sysData.StarSprit;
                }
            }
             DropLineFixed ourDropLine = starSystemNewGameOb.GetComponentInChildren<DropLineFixed>();
            
            ourDropLine.GetLineRenderer();

            Vector3 galaxyPlanePoint = new Vector3(starSystemNewGameOb.transform.position.x,
                galaxyImage.transform.position.y, starSystemNewGameOb.transform.position.z);
            Vector3[] points = {starSystemNewGameOb.transform.position, galaxyPlanePoint};
            ourDropLine.SetUpLine(points);
            StarSysController controller = starSystemNewGameOb.GetComponentInChildren<StarSysController>();
            controller.starSysData = sysData;
            //Transform canvasTrans = starSystemNewGameOb.transform.Find("CanvasSysButton");
            //// Check if the child GameObject exists
            //if (canvasTrans != null)
            //{
            //    // Is there a UI game object we need to turn on and off
            //    //controller.c = canvasTrans.gameObject;
            //    //controller.canvasFleetUIbutton.SetActive(false);
            //}
            //Transform canvasTransButton = starSystemNewGameOb.transform.Find("Canvas Load FleetUI");
            //// Check if the child GameObject exists
            //if (canvasTransButton != null)
            //{
            //    canvasTransButton.SetParent(starSystemNewGameOb.transform, true);
            //}

            starSystemNewGameOb.SetActive(true);
            StarSysControllerList.Add(controller);  
        }

        public StarSysSO GetStarSObyInt(int sysInt)
        {

            StarSysSO result = null;


            foreach (var starSO in starSysSOList)
            {

                if (starSO.StarSysInt == sysInt)
                {
                    result = starSO;
                    break;
                }


            }
            return result;

        }
        public StarSysData GetStarSysDataByName(string name)
        {

            StarSysData result = null;


            foreach (var sysData in StarSysDataList)
            {

                if (sysData.SysName.Equals(name))
                {
                    result = sysData;
                }


            }
            return result;

        }
        public StarSysController GetYourFirstStarSystem(CivEnum civEnum)
        {
            StarSysController controller = starSystemPrefabPlaceHolder;
            foreach (var starSysController in StarSysControllerList)
            {
                if (starSysController.starSysData.CurrentOwner == civEnum)
                {
                    controller = starSysController;
                }
            }
            return controller;
        }
    }
}

