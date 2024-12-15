using FischlWorks_FogWar;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Core
{
    /// <summary>
    /// Instantiates the star system (a StarSysController and a StarSysData) using StarSysSO.
    /// This is a type of galactic object that is a 'StarSystem' class (Manager/Controller/Data and can have a habitable 'planet') 
    /// with a real star or a nebula or a complex as in the Borg Unicomplex)
    /// Other galactic objects not described by StarSys (will have their own classes (ToDo: Managers/Controllers/Data) for stations (one class),
    /// and blackholes/wormholes (one class.) 
    /// </summary>
    public class StarSysManager : MonoBehaviour
    {
        public static StarSysManager Instance;
        [SerializeField]
        private List<StarSysSO> starSysSOList; // get StarSysSO for civ by int
        [SerializeField]
        private List<PowerPlantSO> powerPlantSOList; // get PowerPlantSO for civ by int
        [SerializeField]
        private List<FactorySO> factorySOList; // get factorySO for civ by int
        [SerializeField]
        private List<ShipyardSO> shipyardSOList; // get shipyardSO for civ by int
        [SerializeField]
        private List<ShieldGeneratorSO> shieldGeneratorSOList; // get shieldGeneratorSO for civ by int
        [SerializeField]
        private List<OrbitalBatterySO> orbitalBatterySOList; // get OrbitalBatterySO for civ by int
        [SerializeField]
        private List<ResearchCenterSO> researchCenterSOList; // get factorySO for civ by int
        [SerializeField]
        private GameObject sysPrefab;
        [SerializeField]
        private GameObject sysUIPrefab;
        public List<StarSysController> ManagersStarSysControllerList;
        public GameObject PowerPlantPrefab;
        public GameObject FactoryPrefab;
        public GameObject ShipyardPrefab;
        public GameObject ShieldGeneratorPrefab;
        public GameObject OrbitalBatteryPrefab;
        public GameObject ResearchCenterPrefab;
        [SerializeField]
        private GameObject contentFolderParent;
        [SerializeField]
        private GameObject galaxyImage;
        [SerializeField]
        private Sprite unknowSystem;
        [SerializeField]
        private GameObject galaxyCenter;
        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas yourStarSysUICanvas;
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
            yourStarSysUICanvas.worldCamera = galaxyEventCamera;
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
                SysData.Description = "description here";

                InstantiateSystem(SysData, civSO);
                if (civSO.HasWarp)
                    FleetManager.Instance.FleetDataFromSO(civSO, SysData.GetPosition());
                if (SysData.SysName != "null")
                    starSysDatas.Add(SysData);
            }
            starSysDatas.Remove(starSysDatas[0]); // pull out the null
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
                StarSysController starSysController = starSystemNewGameOb.GetComponentInChildren<StarSysController>();
                starSysController.name = sysData.GetSysName();
                starSysController.StarSysData = sysData;
                starSysController.canvasYourStarSysUI = yourStarSysUICanvas;
                foreach (var civCon in CivManager.Instance.CivControllersInGame)
                {
                    if (civCon.CivData.CivEnum == starSysController.StarSysData.GetFirstOwner())
                        starSysController.StarSysData.CurrentCivController = civCon;
                }
                starSystemNewGameOb.SetActive(true);
                ManagersStarSysControllerList.Add(starSysController);

                List<StarSysController> listStarSysCon = new List<StarSysController> { starSysController };
                CivManager.Instance.AddSystemToOwnSystemListAndHomeSys(listStarSysCon);
                var canvases = starSystemNewGameOb.GetComponentsInChildren<Canvas>();
                starSystemCounter++;
                if (starSystemCounter == CivManager.Instance.CivControllersInGame.Count)
                {
                    csFogWar.Instance.RunFogOfWar(); // star systems are in place so time to scan for the fog

                }
                if (civSO.CivInt < 7) // only for playable major civs
                {
                    StarSysSO starSysSO = GetStarSObyInt(civSO.CivInt);
                    sysData.PowerStations = GetSystemFacilities(starSysSO.PowerStations, PowerPlantPrefab, civSO.CivInt, sysData);
                    //sysData.Factories = GetSystemFacilities(starSysSO.Factories, FactoryPrefab, civSO.CivInt);
                    //sysData.ResearchCenters = GetSystemFacilities(starSysSO.ResearchCenters, ResearchCenterPrefab, civSO.CivInt);
                    //sysData.Shipyards = GetSystemFacilities(starSysSO.Shipyards, ShipyardPrefab, civSO.CivInt);
                    //sysData.ShieldGenerators = GetSystemFacilities(starSysSO.ShieldGenerators, ShieldGeneratorPrefab, civSO.CivInt);
                    //sysData.OrbitalBatteries = GetSystemFacilities(starSysSO.OrbitalBatteries, OrbitalBatteryPrefab, civSO.CivInt);
                    SetParentForFacilities(starSystemNewGameOb, sysData);
                }

                //***** This is temporary so we can test a multi-starsystem civ
                //******* before diplomacy will alow civs/systems to join another civ
                //if (systemCount == 8)
                //{
                //    CivManager.Instance.nowCivsCanJoinTheFederation = true;
                //}
            }
        }
        private void SetParentForFacilities(GameObject parent, StarSysData starSysData)
        {
            foreach (var go in starSysData.PowerStations)
            {
                go.transform.SetParent(parent.transform, false);
            }
            //foreach (var go in starSysData.Factories)
            //{
            //    go.transform.SetParent(parent.transform, false);
            //}
            //foreach (var go in starSysData.Shipyards)
            //{
            //    go.transform.SetParent(parent.transform, false);
            //}
            //foreach (var go in starSysData.ShieldGenerators)
            //{
            //    go.transform.SetParent(parent.transform, false);
            //}
            //foreach (var go in starSysData.OrbitalBatteries)
            //{
            //    go.transform.SetParent(parent.transform, false);
            //}
            //foreach (var go in starSysData.ResearchCenters)
            //{
            //    go.transform.SetParent(parent.transform, false);
            //}
        }
        private List<GameObject> GetSystemFacilities(int numOf, GameObject prefab, int civInt, StarSysData sysData)
        {
            List<GameObject> list = new List<GameObject>();
            switch (prefab.name)
            {
                case "PowerPlantData":
                    {
                        PowerPlantData powerPlantData = new PowerPlantData("null");
                        var powerPlantSO = GetPowrPlantSObyCivInt(civInt);
                        powerPlantData.CivInt = powerPlantSO.CivInt;
                        powerPlantData.TechLevel = powerPlantSO.TechLevel;
                        powerPlantData.FacilitiesEnumType = powerPlantSO.FacilitiesEnumType;
                        powerPlantData.Name = powerPlantSO.Name;                       
                        powerPlantData.StartStarDate = powerPlantSO.StartStarDate;
                        powerPlantData.BuildDuration = powerPlantSO.BuildDuration;
                        powerPlantData.PowerPlantSprite = powerPlantSO.PowerPlantSprite;
                        powerPlantData.Description = powerPlantSO.Description;
                        sysData.PlantData = powerPlantData;
                        
                        List<PowerPlantData> powerPlantDatas = new List<PowerPlantData>();
                        for (int i = 0; i < numOf; i++)
                        {
                            powerPlantDatas.Add(powerPlantData);
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            GetPowerPlantText(newFacilityGO, powerPlantData);
                            newFacilityGO.SetActive(true);
                            
                            powerPlantData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }                        
                        break;
                    }
                case "FactoryData":
                    {
                        //FactoryData factoryData = new FactoryData("null");
                        //var factorySO = GetFactorySObyCivInt(civInt);
                        //factoryData.CivInt = factorySO.CivInt;
                        //factoryData.TechLevel = factorySO.TechLevel;
                        //factoryData.FacilitiesEnumType = factorySO.FacilitiesEnumType;
                        //factoryData.Name = factorySO.Name;
                        //factoryData.StartStarDate = factorySO.StartStarDate;
                        //factoryData.BuildDuration = factorySO.BuildDuration;
                        //factoryData.PowerPlantSprite = factorySO.FactorySprite;
                        //factoryData.Description = factorySO.Description;
                        //List<FactoryData> pDatas = new List<FactoryData>();
                        //for (int i = 0; i < numOf; i++)
                        //    pDatas.Add(factoryData);
                        //for (int i = 0; i < pDatas.Count; i++)
                        //{
                        //    GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                        //        Quaternion.identity);
                        //    factoryData.SysGameObject = newFacilityGO;
                        //    list.Add(newFacilityGO);
                        //}
                        break;
                    }
                case "ShipyardData":
                    {
                        //ShipyardData sData = new ShipyardData("null");
                        //var sSO = GetShipyardSObyCivInt(civInt);
                        //sData.CivInt = sSO.CivInt;
                        //sData.TechLevel = sSO.TechLevel;
                        //sData.FacilitiesEnumType = sSO.FacilitiesEnumType;
                        //sData.Name = sSO.Name;
                        //sData.StartStarDate = sSO.StartStarDate;
                        //sData.BuildDuration = sSO.BuildDuration;
                        //sData.ShipyardSprite = sSO.ShipyardSprite;
                        //sData.Description = sSO.Description;
                        //List<ShipyardData> pDatas = new List<ShipyardData>();
                        //for (int i = 0; i < numOf; i++)
                        //    pDatas.Add(sData);
                        //for (int i = 0; i < pDatas.Count; i++)
                        //{
                        //    GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                        //        Quaternion.identity);
                        //    sData.SysGameObject = newFacilityGO;
                        //    list.Add(newFacilityGO);
                        //}
                        break;
                    }
                case "ShieldGeneratorData":
                    {
                        //ShieldGeneratorData sData = new ShieldGeneratorData("null");
                        //var sSO = GetShieldGeneratorSObyCivInt(civInt);
                        //sData.CivInt = sSO.CivInt;
                        //sData.TechLevel = sSO.TechLevel;
                        //sData.FacilitiesEnumType = sSO.FacilitiesEnumType;
                        //sData.Name = sSO.Name;
                        //sData.StartStarDate = sSO.StartStarDate;
                        //sData.BuildDuration = sSO.BuildDuration;
                        //sData.ShieldGeneratorSprite = sSO.ShieldGeneratorSprite;
                        //sData.Description = sSO.Description;
                        //List<ShieldGeneratorData> pDatas = new List<ShieldGeneratorData>();
                        //for (int i = 0; i < numOf; i++)
                        //    pDatas.Add(sData);
                        //for (int i = 0; i < pDatas.Count; i++)
                        //{
                        //    GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                        //        Quaternion.identity);
                        //    sData.SysGameObject = newFacilityGO;
                        //    list.Add(newFacilityGO);
                        //}
                        break;
                    }
                case "OrbitalBatteryData":
                    {
                    //    OrbitalBatteryData sData = new OrbitalBatteryData("null");
                    //    var sSO = GetOrbitalBatterySObyCivInt(civInt);
                    //    sData.CivInt = sSO.CivInt;
                    //    sData.TechLevel = sSO.TechLevel;
                    //    sData.FacilitiesEnumType = sSO.FacilitiesEnumType;
                    //    sData.Name = sSO.Name;
                    //    sData.StartStarDate = sSO.StartStarDate;
                    //    sData.BuildDuration = sSO.BuildDuration;
                    //    sData.OrbitalBatterySprite = sSO.OrbitalBatterySprite;
                    //    sData.Description = sSO.Description;
                    //    List<OrbitalBatteryData> pDatas = new List<OrbitalBatteryData>();
                    //    for (int i = 0; i < numOf; i++)
                    //        pDatas.Add(sData);
                    //    for (int i = 0; i < pDatas.Count; i++)
                    //    {
                    //        GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                    //            Quaternion.identity);
                    //        sData.SysGameObject = newFacilityGO;
                    //        list.Add(newFacilityGO);
                    //    }
                        break;
                    }
                case "ResearchCenterData":
                    {
                        //ResearchCenterData sData = new ResearchCenterData("null");
                        //var sSO = GetResearchCenterSObyCivInt(civInt);
                        //sData.CivInt = sSO.CivInt;
                        //sData.TechLevel = sSO.TechLevel;
                        //sData.FacilitiesEnumType = sSO.FacilitiesEnumType;
                        //sData.Name = sSO.Name;
                        //sData.StartStarDate = sSO.StartStarDate;
                        //sData.BuildDuration = sSO.BuildDuration;
                        //sData.ResearchCenterSprite = sSO.ResearchCenterSprite;
                        //sData.Description = sSO.Description;
                        //List<ResearchCenterData> pDatas = new List<ResearchCenterData>();
                        //for (int i = 0; i < numOf; i++)
                        //    pDatas.Add(sData);
                        //for (int i = 0; i < pDatas.Count; i++)
                        //{
                        //    GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                        //        Quaternion.identity);
                        //    sData.SysGameObject = newFacilityGO;
                        //    list.Add(newFacilityGO);
                        //}
                        break;
                    }
                default:
                    break;
            }

            return list;
        }

        private void GetFacilityDataFromSO(GameObject facilityGO, int civInt)
        {
            PowerPlantData powerPlantData = new PowerPlantData("null");
            List<PowerPlantData> powerPlantDatas = new List<PowerPlantData>();
            powerPlantDatas.Add(powerPlantData);
            foreach (var civSO in powerPlantDatas)
            {

            }
        }
        private void GetPowerPlantText(GameObject go, PowerPlantData plantData)
        {

            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp != null && OneTmp.name == "NameText (TMP)")
                {
                    OneTmp.text = plantData.Name;
                }
                //else if (OneTmp != null && OneTmp.name == "SysDescription (TMP)")
                //    OneTmp.text = plantData.Description;

            }
        }
        private StarSysSO GetStarSObyInt(int sysInt)
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
        private PowerPlantSO GetPowrPlantSObyCivInt(int  civInt)
        {
            PowerPlantSO result = null;

            foreach (var sO in powerPlantSOList)
            {
                if (sO.CivInt == civInt)
                {
                    result = sO;
                    break;
                }
            }
            return result;
        }
        private FactorySO GetFactorySObyCivInt(int civInt)
        {
            FactorySO result = null;

            foreach (var sO in factorySOList)
            {
                if (sO.CivInt == civInt)
                {
                    result = sO;
                    break;
                }
            }
            return result;
        }
        private ShipyardSO GetShipyardSObyCivInt(int civInt)
        {
            ShipyardSO result = null;

            foreach (var sO in shipyardSOList)
            {
                if (sO.CivInt == civInt)
                {
                    result = sO;
                    break;
                }
            }
            return result;
        }
        private ShieldGeneratorSO GetShieldGeneratorSObyCivInt(int civInt)
        {
            ShieldGeneratorSO result = null;

            foreach (var sO in shieldGeneratorSOList)
            {
                if (sO.CivInt == civInt)
                {
                    result = sO;
                    break;
                }
            }
            return result;
        }
        private OrbitalBatterySO GetOrbitalBatterySObyCivInt(int civInt)
        {
            OrbitalBatterySO result = null;

            foreach (var sO in orbitalBatterySOList)
            {
                if (sO.CivInt == civInt)
                {
                    result = sO;
                    break;
                }
            }
            return result;
        }
        private ResearchCenterSO GetResearchCenterSObyCivInt(int civInt)
        {
            ResearchCenterSO result = null;

            foreach (var sO in researchCenterSOList)
            {
                if (sO.CivInt == civInt)
                {
                    result = sO;
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
                if (sysCon.StarSysData.GetFirstOwner() == civCurrent)
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
        private void SetSysUIItems()
        {
        
        }
        public void NewSystemUI(StarSysController sysController)
        {
            //foreach (var sysController in StarSysManager.Instance.ManagersStarSysControllerList)
            //{
                if (sysController.StarSysData.CurrentOwner == GameController.Instance.GameData.LocalPlayerCivEnum)
                {
                    GameObject starSysUI = (GameObject)Instantiate(sysUIPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity);
                   // ManagersStarSysControllerList.Add(sysController);
                    sysController.StarSysUIController = starSysUI; // each system controller has its system UI
                    starSysUI.transform.SetParent(contentFolderParent.transform, false); // load Queue

                    GalaxyMenuUIController.Instance.UpdateSystemUI(sysController);
                }
            //}
        }

    }
    
}

