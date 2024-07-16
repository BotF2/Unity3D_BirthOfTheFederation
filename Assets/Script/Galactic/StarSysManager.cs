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
        public List<StarSysController> StarSysControllerList;
        public GameObject galaxyImage;
        public GameObject galaxyCenter;
        private Camera galaxyEventCamera;
        private Canvas systemUICanvas; 
        private int systemCount = -1; // Used only in testing multiple systems in Federation

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
            var CanvasGO = GameObject.Find("CanvasStarSysUI");
            systemUICanvas = CanvasGO.GetComponent<Canvas>();
            systemUICanvas.worldCamera = galaxyEventCamera;
        }
        public void SysDataFromSO(List<CivSO> civSOList)
        {
            StarSysData SysData = new StarSysData("null");
            List<StarSysData> starSysDatas = new List<StarSysData>();
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
                SysData.PopulationLimit = starSysSO.PopulationLimit;
                SysData.Farms = starSysSO.Farms;
                SysData.PowerStations = starSysSO.PowerStations;
                SysData.power = starSysSO.PowerStations;
                SysData.Factories = starSysSO.Factories;
                SysData.production = starSysSO.Factories;
                SysData.Research = starSysSO.Research;
                SysData.tech = 0;
                //SysData.food;
                //SysData.power;
                //SysData.production;
                //SysData.tech;
                //SysData.Description;
                //SysData.v;
                //public List<GameObject> _fleetsInSystem;

                //starSysDatas.Add(SysData);
                InstantiateSystem(SysData, civSO);
                if (civSO.HasWarp)
                    FleetManager.instance.FleetDataFromSO(civSO, SysData.GetPosition());
                if (SysData.SysName != "null")
                    starSysDatas.Add(SysData);
            }
            starSysDatas.Remove(starSysDatas[0]); // pull out the null
            GameManager.Instance.LoadGalacticDestinations(starSysDatas);
        }
        public void InstantiateSystem(StarSysData sysData, CivSO civSO)
        { 
           
            GameObject starSystemNewGameOb = (GameObject)Instantiate(sysPrefab, new Vector3(0,0,0),
                 Quaternion.identity);
            starSystemNewGameOb.transform.Translate(new Vector3(sysData.GetPosition().x,
                sysData.GetPosition().y, sysData.GetPosition().z));

            starSystemNewGameOb.transform.SetParent(galaxyCenter.transform, true);
            starSystemNewGameOb.transform.localScale = new Vector3(1, 1, 1);
            starSystemNewGameOb.name = sysData.GetSysName();
            //starSystemNewGameOb.
            sysData.SysGameObject = starSystemNewGameOb;
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
                    //if (oneRenderer.CivName == "CivRaceSprite")
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
            systemCount++;
            CivManager.instance.AddSystemToOwnedCivSystemList(controller);
            //***** This is temporary so we can test a multi-starsystem civ
            //******* before diplomacy will alow civs/systems to join another civ
            //if (systemCount == 8)
            //{
            //    CivManager.instance.nowCivsCanJoinTheFederation = true;
            //}
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


            foreach (var sysCon in StarSysControllerList)
            {

                if (sysCon.StarSysData.GetSysName().Equals(name))
                {
                    result = sysCon.StarSysData;
                }
            }
            return result;

        }
        public void UpdateStarSystemOwner(CivEnum civCurrent, CivEnum civNew)
        {
            foreach (var sysCon in StarSysControllerList) 
            {
              if(sysCon.StarSysData.GetFirstOwner() == civCurrent)
                    sysCon.StarSysData.CurrentOwner = civNew;
            }
        }
    }
}

