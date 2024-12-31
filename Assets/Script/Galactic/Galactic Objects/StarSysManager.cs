using FischlWorks_FogWar;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using Unity.VisualScripting;

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
                //Debugger.Break();
                GameObject starSystemNewGameOb = (GameObject)Instantiate(sysPrefab, new Vector3(0, 0, 0),
                     Quaternion.identity);

                starSystemNewGameOb.layer = 4; // water layer (also used by fog of war for obsticles with shows to line of sight
                starSystemNewGameOb.transform.Translate(new Vector3(sysData.GetPosition().x,
                    sysData.GetPosition().y, sysData.GetPosition().z));
                starSystemNewGameOb.transform.SetParent(galaxyCenter.transform, true);
                starSystemNewGameOb.transform.localScale = new Vector3(1, 1, 1);
                StarSysController starSysController = starSystemNewGameOb.GetComponentInChildren<StarSysController>();
                Transform fogObsticleTransform = starSystemNewGameOb.transform.Find("FogObstacle");
                fogObsticleTransform.SetParent(galaxyCenter.transform, false);
                fogObsticleTransform.Translate(new Vector3(sysData.GetPosition().x, -55f, sysData.GetPosition().z));
                starSystemNewGameOb.name = sysData.GetSysName();

                sysData.SysGameObject = starSystemNewGameOb;

                TextMeshProUGUI[] TheText = starSystemNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (var OneTmp in TheText)
                {
                    //Debugger.Break();
                    OneTmp.enabled = true;
                    if (OneTmp != null && OneTmp.name == "SysName")
                    {
                        if (!GameController.Instance.AreWeLocalPlayer(sysData.CurrentOwner)) // != CivManager.current.LocalPlayerCivEnum)
                        {
                            OneTmp.text = "UNKNOWN";
                        }
                        else
                        {
                            OneTmp.text = sysData.GetSysName();
                           //var sysThingy = sysData
                        }
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
                //StarSysController starSysController = starSystemNewGameOb.GetComponentInChildren<StarSysController>();
                starSysController.name = sysData.GetSysName();
                //starSysController.StarSysUIController.Sys
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
                if (civSO.CivInt < 7) 
                {
                    StarSysSO starSysSO = GetStarSObyInt(civSO.CivInt);
                    sysData.PowerStations = GetSystemFacilities(starSysSO.PowerStations, PowerPlantPrefab, civSO.CivInt, sysData);
                    sysData.Factories = GetSystemFacilities(starSysSO.Factories, FactoryPrefab, civSO.CivInt, sysData);
                    sysData.Shipyards = GetSystemFacilities(starSysSO.Shipyards, ShipyardPrefab, civSO.CivInt, sysData);
                    sysData.ShieldGenerators = GetSystemFacilities(starSysSO.ShieldGenerators, ShieldGeneratorPrefab, civSO.CivInt, sysData);
                    sysData.OrbitalBatteries = GetSystemFacilities(starSysSO.OrbitalBatteries, OrbitalBatteryPrefab, civSO.CivInt, sysData);
                    sysData.ResearchCenters = GetSystemFacilities(starSysSO.ResearchCenters, ResearchCenterPrefab, civSO.CivInt, sysData);
                    SetParentForFacilities(starSystemNewGameOb, sysData);
                    if (GameController.Instance.AreWeLocalPlayer(sysData.CurrentOwner))
                        NewSystemUI(starSysController);
                }

                //***** This is temporary so we can test a multi-starsystem civ
                //******* before diplomacy will alow civs/systems to join another civ
                //if (systemCount == 8)
                //{
                //    CivManager.current.nowCivsCanJoinTheFederation = true;
                //}
            }
        }
        private void SetParentForFacilities(GameObject parent, StarSysData starSysData)
        {
            foreach (var go in starSysData.PowerStations)
            {
                go.transform.SetParent(parent.transform, false);
            }
            foreach (var go in starSysData.Factories)
            {
                go.transform.SetParent(parent.transform, false);
            }
            foreach (var go in starSysData.Shipyards)
            {
                go.transform.SetParent(parent.transform, false);
            }
            foreach (var go in starSysData.ShieldGenerators)
            {
                go.transform.SetParent(parent.transform, false);
            }
            foreach (var go in starSysData.OrbitalBatteries)
            {
                go.transform.SetParent(parent.transform, false);
            }
            foreach (var go in starSysData.ResearchCenters)
            {
                go.transform.SetParent(parent.transform, false);
            }
        }

        private List<GameObject> GetSystemFacilities(int numOf, GameObject prefab, int civInt, StarSysData sysData)
        {
            List<GameObject> list = new List<GameObject>();
            switch (prefab.name)
            {
                // "SysName" in each system ribbon is set in GalaxyMenuUIController without a facility game object needed
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
                        powerPlantData.PowerOutput = powerPlantSO.PowerOutput;  
                        powerPlantData.PowerPlantSprite = powerPlantSO.PowerPlantSprite;
                        powerPlantData.Description = powerPlantSO.Description;
                        sysData.PowerPlantData = powerPlantData;
                        
                        List<PowerPlantData> powerPlantDatas = new List<PowerPlantData>();
                        for (int i = 0; i < numOf; i++)
                        {
                            powerPlantDatas.Add(powerPlantData);
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            GetPowerPlantText(newFacilityGO, powerPlantData, numOf);
                            newFacilityGO.SetActive(false);
                            
                            powerPlantData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }                        
                        break;
                    }
                case "FactoryData":
                    {
                        //Debugger.Break();
                        FactoryData factoryData = new FactoryData("null");
                        var factorySO = GetFactorySObyCivInt(civInt);
                        factoryData.CivInt = factorySO.CivInt;
                        factoryData.TechLevel = factorySO.TechLevel;
                        factoryData.FacilitiesEnumType = factorySO.FacilitiesEnumType;
                        factoryData.Name = factorySO.Name;
                        factoryData.StartStarDate = factorySO.StartStarDate;
                        factoryData.PowerLoad = factorySO.PowerLoad;
                        factoryData.BuildDuration = factorySO.BuildDuration;
                        factoryData.On = 1;
                        factoryData.PowerPlantSprite = factorySO.FactorySprite;
                        factoryData.Description = factorySO.Description;
                        sysData.FactoryData = factoryData;
                        List<FactoryData> fDatas = new List<FactoryData>();
                        for (int i = 0; i < numOf; i++)
                            fDatas.Add(factoryData);
                        for (int i = 0; i < fDatas.Count; i++)
                        {
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            TextMeshProUGUI On = newFacilityGO.AddComponent<TextMeshProUGUI>();
                            On.text = factoryData.On.ToString(); // 0 = off, 1 = on. is set to 1 above
                            GetFactoryText(newFacilityGO, factoryData, numOf);
                            newFacilityGO.SetActive(false);
                            factoryData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }
                        break;
                    }
                case "ShipyardData":
                    {
                        ShipyardData sData = new ShipyardData("null");
                        var sSO = GetShipyardSObyCivInt(civInt);
                        sData.CivInt = sSO.CivInt;
                        sData.TechLevel = sSO.TechLevel;
                        sData.FacilitiesEnumType = sSO.FacilitiesEnumType;
                        sData.Name = sSO.Name;
                        sData.StartStarDate = sSO.StartStarDate;
                        sData.BuildDuration = sSO.BuildDuration;
                        sData.PowerLoad = sSO.PowerLoad;
                        sData.On = 1;
                        sData.ShipyardSprite = sSO.ShipyardSprite;
                        sData.Description = sSO.Description;
                        sysData.ShipyardData = sData;
                        List<ShipyardData> syDatas = new List<ShipyardData>();
                        for (int i = 0; i < numOf; i++)
                            syDatas.Add(sData);
                        for (int i = 0; i < syDatas.Count; i++)
                        {
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            TextMeshProUGUI On = newFacilityGO.AddComponent<TextMeshProUGUI>();
                            On.text = sData.On.ToString();
                            GetShipyardText(newFacilityGO, sData, numOf);
                            newFacilityGO.SetActive(false);
                            sData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }
                        break;
                    }
                case "ShieldGeneratorData":
                    {
                        ShieldGeneratorData sgData = new ShieldGeneratorData("null");
                        var sgSO = GetShieldGeneratorSObyCivInt(civInt);
                        sgData.CivInt = sgSO.CivInt;
                        sgData.TechLevel = sgSO.TechLevel;
                        sgData.FacilitiesEnumType = sgSO.FacilitiesEnumType;
                        sgData.Name = sgSO.Name;
                        sgData.StartStarDate = sgSO.StartStarDate;
                        sgData.BuildDuration = sgSO.BuildDuration;
                        sgData.PowerLoad = sgSO.PowerLoad;
                        sgData.On = 1;
                        sgData.ShieldGeneratorSprite = sgSO.ShieldGeneratorSprite;
                        sgData.Description = sgSO.Description;
                        sysData.ShieldGeneratorData = sgData;
                        List<ShieldGeneratorData> sGDatas = new List<ShieldGeneratorData>();
                        for (int i = 0; i < numOf; i++)
                            sGDatas.Add(sgData);
                        for (int i = 0; i < sGDatas.Count; i++)
                        {
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            TextMeshProUGUI On = newFacilityGO.AddComponent<TextMeshProUGUI>();
                            On.text = sgData.On.ToString();
                            GetShieldGText(newFacilityGO, sgData, numOf);
                            newFacilityGO.SetActive(false);
                            sgData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }
                        break;
                    }
                case "OrbitalBatteryData":
                    {
                        OrbitalBatteryData obData = new OrbitalBatteryData("null");
                        var sgSO = GetOrbitalBatterySObyCivInt(civInt);
                        obData.CivInt = sgSO.CivInt;
                        obData.TechLevel = sgSO.TechLevel;
                        obData.FacilitiesEnumType = sgSO.FacilitiesEnumType;
                        obData.Name = sgSO.Name;
                        obData.StartStarDate = sgSO.StartStarDate;
                        obData.BuildDuration = sgSO.BuildDuration;
                        obData.PowerLoad = sgSO.PowerLoad;
                        obData.On = 1;
                        obData.OrbitalBatterySprite = sgSO.OrbitalBatterySprite;
                        obData.Description = sgSO.Description;
                        sysData.OrbitalBatteryData = obData;
                        List<OrbitalBatteryData> oBDatas = new List<OrbitalBatteryData>();
                        for (int i = 0; i < numOf; i++)
                            oBDatas.Add(obData);
                        for (int i = 0; i < oBDatas.Count; i++)
                        {
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            TextMeshProUGUI On = newFacilityGO.AddComponent<TextMeshProUGUI>();
                            On.text = obData.On.ToString();
                            GetOBText(newFacilityGO, obData, numOf);
                            newFacilityGO.SetActive(false);
                            obData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }
                        break;
                    }
                case "ResearchCenterData":
                    {
                        ResearchCenterData researchData = new ResearchCenterData("null");
                        var sgSO = GetResearchCenterSObyCivInt(civInt);
                        researchData.CivInt = sgSO.CivInt;
                        researchData.TechLevel = sgSO.TechLevel;
                        researchData.FacilitiesEnumType = sgSO.FacilitiesEnumType;
                        researchData.Name = sgSO.Name;
                        researchData.StartStarDate = sgSO.StartStarDate;
                        researchData.BuildDuration = sgSO.BuildDuration;
                        researchData.PowerLoad = sgSO.PowerLoad;
                        researchData.On = 1;
                        researchData.ResearchCenterSprite = sgSO.ResearchCenterSprite;
                        researchData.Description = sgSO.Description;
                        sysData.ResearchCenterData = researchData;
                        List<ResearchCenterData> reDatas = new List<ResearchCenterData>();
                        for (int i = 0; i < numOf; i++)
                            reDatas.Add(researchData);
                        for (int i = 0; i < reDatas.Count; i++)
                        {
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            TextMeshProUGUI On = newFacilityGO.AddComponent<TextMeshProUGUI>();
                            On.text = researchData.On.ToString();
                            GetResearchCenterText(newFacilityGO, researchData, numOf);
                            newFacilityGO.SetActive(false);
                            researchData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                        }
                        break;
                    }
                default:
                    break;
            }

            return list;
        }

        private void GetPowerPlantText(GameObject go, PowerPlantData plantData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp.name == "NameText (TMP)")
                    OneTmp.text = plantData.Name;
                else if (OneTmp.name == "NumTotalUnits (TMP)")
                    OneTmp.text = numOf.ToString();
                else if (OneTmp.name == "NumTotalEOut (TMP)")
                    OneTmp.text = plantData.PowerOutput.ToString();
                else if (OneTmp.name == "DescriptionText (TMP)")
                    OneTmp.text = plantData.Description;
                // image here

            }
        }
        private void GetFactoryText(GameObject go, FactoryData factoryData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp.name == "NameFactory")
                    OneTmp.text = factoryData.Name;
                else if (OneTmp.name == "NumFactoryRatio")
                    OneTmp.text = numOf.ToString();
                else if (OneTmp.name == "FactoryLoad")
                    OneTmp.text = factoryData.PowerLoad.ToString();
                else if (OneTmp.name == "DescriptionFactory")
                    OneTmp.text = factoryData.Description;
                    
                // image here

            }
        }

        private void GetShipyardText(GameObject go, ShipyardData factoryData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp.name == "NameShipyard")
                    OneTmp.text = factoryData.Name;
                else if (OneTmp.name == "NumShipyardRatio")
                    OneTmp.text = numOf.ToString();
                else if (OneTmp.name == "ShipyardLoad")
                    OneTmp.text = factoryData.PowerLoad.ToString();
                else if (OneTmp.name == "DescriptionShipyard")
                    OneTmp.text = factoryData.Description;
                // image here

            }
        }
        private void GetShieldGText(GameObject go, ShieldGeneratorData shieldData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp.name == "NameShieldG")
                    OneTmp.text = shieldData.Name;
                else if (OneTmp.name == "NumShieldGRatio")
                    OneTmp.text = numOf.ToString();
                else if (OneTmp.name == "ShieldGLoad")
                    OneTmp.text = shieldData.PowerLoad.ToString();
                else if (OneTmp.name == "DescriptionShieldG")
                    OneTmp.text = shieldData.Description;
                else if (OneTmp.name == "NumOn")
                    OneTmp.text = shieldData.Description;
                // image here

            }
        }
        private void GetOBText(GameObject go, OrbitalBatteryData oBData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp.name == "NameOB")
                    OneTmp.text = oBData.Name;
                else if (OneTmp.name == "NumOBRatio")
                    OneTmp.text = numOf.ToString();
                else if (OneTmp.name == "OBLoad")
                    OneTmp.text = oBData.PowerLoad.ToString();
                else if (OneTmp.name == "DescriptionOB")
                    OneTmp.text = oBData.Description;
                // image here

            }
        }
        private void GetResearchCenterText(GameObject go, ResearchCenterData oBData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTmp in TheText)
            {
                OneTmp.enabled = true;
                if (OneTmp.name == "NameResearchCenter")
                    OneTmp.text = oBData.Name;
                else if (OneTmp.name == "NumResearchCenterRatio")
                    OneTmp.text = numOf.ToString();
                else if (OneTmp.name == "ResearchCenterLoad")
                    OneTmp.text = oBData.PowerLoad.ToString();
                else if (OneTmp.name == "DescriptionResearchCenter")
                    OneTmp.text = oBData.Description;
                // image here

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
            
            if (sysController.StarSysData.CurrentOwner == GameController.Instance.GameData.LocalPlayerCivEnum)
            {
                GameObject starSysUI = (GameObject)Instantiate(sysUIPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
                starSysUI.layer = 5;
                sysController.StarSysUIController = starSysUI; 
                starSysUI.transform.SetParent(contentFolderParent.transform, false); // load into Queue
            }
            
        }

    }
    
}

