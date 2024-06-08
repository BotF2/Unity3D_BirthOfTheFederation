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
        [SerializeField]
        private List<StarSysSO> starSysSOList; // get StarSysSO for civ by int
        [SerializeField]
        private GameObject sysPrefab;
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

                SysData = new StarSysData(starSysSO);               
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
                    FleetManager.instance.FleetDataFromSO(civSO, SysData.GetPosition()); 
            }
            StarSysDataList = starSysDatas;
            StarSysDataList.Remove(StarSysDataList[0]);
            GameManager.Instance.LoadGalacticDestinations(StarSysDataList);


        }
        public void InstantiateSystem(StarSysData sysData, CivSO civSO)
        { 
            GameObject starSystemNewGameOb = (GameObject)Instantiate(sysPrefab, new Vector3(0,0,0),
                 Quaternion.identity);
            starSystemNewGameOb.transform.Translate(new Vector3(sysData.GetPosition().x, sysData.GetPosition().y, sysData.GetPosition().z));

            starSystemNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            starSystemNewGameOb.transform.localScale = new Vector3(1, 1, 1);
            starSystemNewGameOb.name = sysData.GetSysName();
            //starSystemNewGameOb.
            sysData.SysTransform = starSystemNewGameOb.transform;
            var ImageRenderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();

            TextMeshProUGUI[] TheText = starSystemNewGameOb.GetComponentsInChildren<TextMeshProUGUI>(); 
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp != null && OneTmp.name == "SysName (TMP)")
                    OneTmp.text = sysData.GetSysName();
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
            controller.name = sysData.GetSysName();
            controller.StarSysData = sysData;

            starSystemNewGameOb.SetActive(true);
            StarSysControllerList.Add(controller);
            GameManager.Instance.LoadDestinationDropdown();
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

                if (sysData.GetSysName().Equals(name))
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
                if (starSysController.StarSysData.CurrentOwner == civEnum)
                {
                    controller = starSysController;
                }
            }
            return controller;
        }
    }
}

