using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using FischlWorks_FogWar;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Rendering.VirtualTexturing;

namespace Assets.Core
{
    /// <summary>
    /// Instantiates the star system (a StarSysController and a StarSysData) using StarSysSO
    ///     /// <summary>
    /// This is a type of galactic object that is a 'StarSystem' class (Manager/Controller/Data and can have a habitable 'planet') 
    /// with a real star or a nebula or a complex as in the Borg Unicomplex)
    /// Other galactic objects not described by StarSys (will have their own classes (ToDo: Managers/Controllers/Data) for stations (one class),
    /// and blackholes/wormholes (one class.)
    /// </summary> 
    /// </summary>
    public class StarSysManager : MonoBehaviour
    {
        public static StarSysManager Instance;
        [SerializeField]
        private List<StarSysSO> starSysSOList; // get StarSysSO for civ by int
        [SerializeField]
        private GameObject sysPrefab;
        public List<StarSysController> ManagersStarSysControllerList;
        [SerializeField]
        private GameObject galaxyImage;
        [SerializeField]
        private Sprite unknowSystem;
        [SerializeField]
        private GameObject galaxyCenter;
        private Camera galaxyEventCamera;
        private Canvas systemUICanvas;
        private int starSystemCounter = 0;
        //private int systemCount = -1; // Used only in testing multiple systems in Federation
        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        public void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
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
                SysData.SystemType = starSysSO.StarType;
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
                    FleetManager.Instance.FleetDataFromSO(civSO, SysData.GetPosition());
                if (SysData.SysName != "null")
                    starSysDatas.Add(SysData);
            }
            starSysDatas.Remove(starSysDatas[0]); // pull out the null
            //GameManager.Instance.GameData.LoadGalacticDestinations(starSysDatas);
        }
        public void InstantiateSystem(StarSysData sysData, CivSO civSO)
        {


            if (MainMenuUIController.Instance.MainMenuData.SelectedGalaxyType == GalaxyMapType.RANDOM)
            { // do something random with sysData.position
            }
            else if (MainMenuUIController.Instance.MainMenuData.SelectedGalaxyType == GalaxyMapType.RING)
            {
                // do something in a ring with sysData.position
            }
            else
            {
                GameObject starSystemNewGameOb = (GameObject)Instantiate(sysPrefab, new Vector3(0, 0, 0),
                     Quaternion.identity);
                starSystemNewGameOb.layer = 4; // water layer (also used by fog of war for obsticles with shows to line of sight
                starSystemNewGameOb.transform.Translate(new Vector3(sysData.GetPosition().x,
                    sysData.GetPosition().y, sysData.GetPosition().z));

                starSystemNewGameOb.transform.SetParent(galaxyCenter.transform, true);
                starSystemNewGameOb.transform.localScale = new Vector3(1, 1, 1);
                Transform fogObsticleTransform = starSystemNewGameOb.transform.Find("FogObstacle");
                fogObsticleTransform.SetParent(galaxyCenter.transform, false);
                fogObsticleTransform.Translate(new Vector3(sysData.GetPosition().x, -55f, sysData.GetPosition().z));
                starSystemNewGameOb.name = sysData.GetSysName();

                sysData.SysGameObject = starSystemNewGameOb;

                TextMeshProUGUI[] TheText = starSystemNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (var OneTmp in TheText)
                {
                    OneTmp.enabled = true;
                    if (OneTmp != null && OneTmp.name == "SysName (TMP)")
                    {
                        if (!GameController.Instance.AreWeLocalPlayer(sysData.CurrentOwner)) // != CivManager.Instance.LocalPlayerCivEnum)
                        {
                            OneTmp.text = "UNKNOWN";
                        }
                        else
                            OneTmp.text = sysData.GetSysName();
                    }
                    else if (OneTmp != null && OneTmp.name == "SysDescription (TMP)")
                        OneTmp.text = sysData.Description;

                }
                var Renderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();
                foreach (var oneRenderer in Renderers)
                {
                    if (oneRenderer != null)
                    {
                        if (oneRenderer.name == "OwnerInsignia")
                        {
                            oneRenderer.sprite = civSO.Insignia;
                            //oneRenderer.sprite.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
                        } // ToDo: random map with random sprites on nebula, wormholes
                        else if (oneRenderer.name == "StarSprite")
                                oneRenderer.sprite = sysData.StarSprit;
                    }
                }

                MapLineFixed ourDropLine = starSystemNewGameOb.GetComponentInChildren<MapLineFixed>();

                ourDropLine.GetLineRenderer();

                Vector3 galaxyPlanePoint = new Vector3(starSystemNewGameOb.transform.position.x,
                    galaxyImage.transform.position.y, starSystemNewGameOb.transform.position.z);
                Vector3[] points = { starSystemNewGameOb.transform.position, galaxyPlanePoint };
                ourDropLine.SetUpLine(points);
                StarSysController starSysConroller = starSystemNewGameOb.GetComponentInChildren<StarSysController>();
                starSysConroller.name = sysData.GetSysName();
                starSysConroller.StarSysData = sysData;
                foreach (var civCon in CivManager.Instance.CivControllersInGame)
                {
                    if (civCon.CivData.CivEnum == starSysConroller.StarSysData.GetFirstOwner())
                        starSysConroller.StarSysData.CurrentCivController = civCon;
                }
                starSystemNewGameOb.SetActive(true);
                ManagersStarSysControllerList.Add(starSysConroller);
                //systemCount++;
                List<StarSysController> listStarSysCon = new List<StarSysController> { starSysConroller };
                CivManager.Instance.AddSystemToOwnSystemListAndHomeSys(listStarSysCon);
                var canvases = starSystemNewGameOb.GetComponentsInChildren<Canvas>();
                starSystemCounter++;
                if (starSystemCounter == CivManager.Instance.CivControllersInGame.Count)
                {
                    csFogWar.Instance.RunFogOfWar(); // star systems are in place so time to scan for the fog
                    
                }


                //***** This is temporary so we can test a multi-starsystem civ
                //******* before diplomacy will alow civs/systems to join another civ
                //if (systemCount == 8)
                //{
                //    CivManager.Instance.nowCivsCanJoinTheFederation = true;
                //}
            }
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


            foreach (var sysCon in ManagersStarSysControllerList)
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
            foreach (var sysCon in ManagersStarSysControllerList) 
            {
              if(sysCon.StarSysData.GetFirstOwner() == civCurrent)
                    sysCon.StarSysData.CurrentOwner = civNew;
            }
        }
        void RemoveStarSysConrollerFromAllControllers(StarSysController starSysController)
        {
            ManagersStarSysControllerList.Remove(starSysController);
        }
        void AddFleetConrollerFromAllControllers(StarSysController starSysController)
        {
            ManagersStarSysControllerList.Add(starSysController);
        }
    }
}

