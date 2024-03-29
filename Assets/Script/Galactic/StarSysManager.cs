using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

namespace Assets.Core
{
    public class StarSysManager : MonoBehaviour
    {
        public static StarSysManager instance;

        public List<StarSysSO> starSysSOList;

        public GameObject sysPrefab;

        public List<StarSysData> StarSysDataList = new List<StarSysData>() { new StarSysData()};

        private List<StarSysData> starSysDatas = new List<StarSysData>() { new StarSysData()};

        public GameObject galaxyImage;

        public GameObject galaxyCenter;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        public void CreateGameSystems(List<CivSO> civSOList)
        {
            //List<StarSysData> starSysDatas = new List<StarSysData>();
            //starSysDataList = starSysDatas;
            foreach (var civSO in civSOList)
            {
                StarSysSO starSysSO = GetStarSObyInt(civSO.CivInt);
                StarSysData SysData = new StarSysData();
                SysData.Position = starSysSO.Position;
                SysData.SysName = starSysSO.SysName;
                SysData.FirstOwner = starSysSO.FirstOwner;
                SysData.CurrentOwner = starSysSO.FirstOwner;
                SysData.StarType = starSysSO.StarType;
                SysData.StarSprit = starSysSO.StarSprit;
                SysData.Population = starSysSO.Population;
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

                //if (!starSysDataList.Contains(SysData))
                starSysDatas.Add(SysData);
                InstantiateSystemButton(SysData, civSO);
            }
            StarSysDataList = StarSysDataList;
            FleetManager.instance.CreatGameStarterFleets(civSOList);
            SolarSystemView view = new SolarSystemView();
        }
        public void InstantiateSystemButton(StarSysData sysData, CivSO civSO)
        { 
            GameObject starSystemNewGameOb = (GameObject)Instantiate(sysPrefab, new Vector3(0,0,0),
                 Quaternion.identity);
            starSystemNewGameOb.transform.Translate(new Vector3(sysData.Position.x, sysData.Position.y, sysData.Position.z));
            //starSystemNewGameOb.transform.Translate(new Vector3(sysData.Position.x, sysData.Position.z, sysData.Position.y));
            starSystemNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            starSystemNewGameOb.transform.localScale = new Vector3(5, 5, 5);
            starSystemNewGameOb.name = sysData.SysName;
            var ImageRenderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();

            var TMPs = starSystemNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TMPs)
            {
                if (OneTmp != null && OneTmp.name == "StarName (TMP)")
                    OneTmp.text = sysData.SysName;
                else if (OneTmp != null && OneTmp.name == "Owner (TMP)")
                    OneTmp.text = sysData.FirstOwner.ToString();
            }
            var Renderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();
            foreach (var oneRenderer in Renderers)
            {
                if (oneRenderer != null)
                {
                    if (oneRenderer.name == "CivRaceSprite")
                    {
                        oneRenderer.sprite = civSO.CivImage; // ok
                    }


                    else if (oneRenderer.name == "OwnerInsignia")
                    {
                        oneRenderer.sprite = civSO.Insignia;
                        //oneRenderer.sprite.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
                    }
                    else if (oneRenderer.name == "ImageStar")
                        oneRenderer.sprite = sysData.StarSprit;
                }
            }
             DropLineFixed ourDropLine = starSystemNewGameOb.GetComponent<DropLineFixed>();
            
            ourDropLine.GetLineRenderer();

            Vector3 galaxyPlanePoint = new Vector3(starSystemNewGameOb.transform.position.x,
                galaxyImage.transform.position.y, starSystemNewGameOb.transform.position.z);
            Vector3[] points = {starSystemNewGameOb.transform.position, galaxyPlanePoint};
            ourDropLine.SetUpLine(points);   

            starSystemNewGameOb.SetActive(true);
            //Undo.MoveGameObjectToScene(starSystemNewGameOb, GalaxyScene)


            //view.NumbersOfSystemID(NumbersForSystem);
            //ourGalaxy.PopulateCanonSystem();
        }
        public StarSysData resultInGameStarSysData;

        public StarSysSO GetStarSObyInt(int sysInt)
        {

            StarSysSO result = null;


            foreach (var starSO in starSysSOList)
            {

                if (starSO.StarSysInt.Equals(sysInt))
                {
                    result = starSO;
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
        //public void OnNewGameButtonClicked(int gameSize)
        //{
        //    CreateNewGame(gameSize);

        //}

        //public void GetStarSysByName(string sysName)
        //{
        //    resultInGameStarSysData = GetStarSysDataByName(sysName);

        //}
    }
}

