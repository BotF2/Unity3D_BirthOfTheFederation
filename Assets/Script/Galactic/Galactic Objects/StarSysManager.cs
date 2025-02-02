using FischlWorks_FogWar;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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
        private List<GameObject> buildListPrefabs;
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
        private GameObject sysPanel;
        [SerializeField]
        private ThemeSO localPlayerTheme;
        [SerializeField]
        private GameObject galaxyImage;
        [SerializeField]
        private GameObject canvasBuildList;
        [SerializeField]
        private Sprite unknowSystem;
        [SerializeField]
        private GameObject galaxyCenter;
        private Camera galaxyEventCamera;
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
        }
        public void SysDataFromSO(List<CivSO> civSOList)
        {
            StarSysData SysData = new StarSysData("null");
            List<StarSysData> starSysDatas = new List<StarSysData>();
            starSysDatas.Add(SysData);
            for (int i = 0; i < civSOList.Count; i++) 
            {
                StarSysSO starSysSO = GetStarSObyInt(civSOList[i].CivInt);
                SysData = new StarSysData(starSysSO);
                SysData.CurrentOwner = starSysSO.FirstOwner;
                SysData.SystemType = starSysSO.StarType;
                SysData.StarSprit = starSysSO.StarSprit;
                SysData.Description = "description here";

                InstantiateSystem(SysData, civSOList[i]);
                if (civSOList[i].HasWarp)
                    FleetManager.Instance.FleetDataFromSO(civSOList[i], SysData.GetPosition());
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
                StarSysController starSysController = starSystemNewGameOb.GetComponentInChildren<StarSysController>();
                Transform fogObsticleTransform = starSystemNewGameOb.transform.Find("FogObstacle");
                fogObsticleTransform.SetParent(galaxyCenter.transform, false);
                fogObsticleTransform.Translate(new Vector3(sysData.GetPosition().x, -55f, sysData.GetPosition().z));
                starSystemNewGameOb.name = sysData.GetSysName();

                sysData.SysGameObject = starSystemNewGameOb;

                TextMeshProUGUI[] TheText = starSystemNewGameOb.GetComponentsInChildren<TextMeshProUGUI>();
                for (int i = 0; i < TheText.Length; i++)
                {
                    TheText[i].enabled = true;
                    if (TheText[i] != null && TheText[i].name == "SysName")
                    {
                        if (!GameController.Instance.AreWeLocalPlayer(sysData.CurrentOwner)) // != CivManager.current.LocalPlayerCivEnum)
                        {
                            TheText[i].text = "UNKNOWN";
                        }
                        else
                        {
                            TheText[i].text = sysData.GetSysName();
                           //var sysThingy = sysData
                        }
                    }
                    else if (TheText[i] != null && TheText[i].name == "SysDescription (TMP)")
                        TheText[i].text = sysData.Description;

                }
                var Renderers = starSystemNewGameOb.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0;i < Renderers.Length; i++)
                {
                    if (Renderers[i] != null)
                    {
                        if (Renderers[i].name == "OwnerInsignia")
                        {
                            Renderers[i].sprite = civSO.Insignia;
                        } // ToDo: random map with random sprites on nebula, wormholes
                        else if (Renderers[i].name == "StarSprite")
                            Renderers[i].sprite = sysData.StarSprit;
                    }
                }

                MapLineFixed ourDropLine = starSystemNewGameOb.GetComponentInChildren<MapLineFixed>();

                ourDropLine.GetLineRenderer();

                Vector3 galaxyPlanePoint = new Vector3(starSystemNewGameOb.transform.position.x,
                    galaxyImage.transform.position.y, starSystemNewGameOb.transform.position.z);
                Vector3[] points = { starSystemNewGameOb.transform.position, galaxyPlanePoint };
                ourDropLine.SetUpLine(points);

                starSysController.name = sysData.GetSysName();
                starSysController.StarSysData = sysData;
                CivController[] controllers = CivManager.Instance.CivControllersInGame.ToArray();
                for (int i = 0; controllers.Length > 0; i++)
                {
                    if (controllers[i].CivData.CivEnum == starSysController.StarSysData.GetFirstOwner())
                    { 
                        starSysController.StarSysData.CurrentCivController = controllers[i];
                        break;
                    }
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
                if (GameController.Instance.AreWeLocalPlayer(sysData.CurrentOwner)) 
                {
                    StarSysSO starSysSO = GetStarSObyInt(civSO.CivInt);
                    sysData.PowerStations = GetSystemFacilities(starSysSO.PowerStations, PowerPlantPrefab, civSO.CivInt, sysData);
                    sysData.Factories = GetSystemFacilities(starSysSO.Factories, FactoryPrefab, civSO.CivInt, sysData);
                    sysData.Shipyards = GetSystemFacilities(starSysSO.Shipyards, ShipyardPrefab, civSO.CivInt, sysData);
                    sysData.ShieldGenerators = GetSystemFacilities(starSysSO.ShieldGenerators, ShieldGeneratorPrefab, civSO.CivInt, sysData);
                    sysData.OrbitalBatteries = GetSystemFacilities(starSysSO.OrbitalBatteries, OrbitalBatteryPrefab, civSO.CivInt, sysData);
                    sysData.ResearchCenters = GetSystemFacilities(starSysSO.ResearchCenters, ResearchCenterPrefab, civSO.CivInt, sysData);
                    SetParentForFacilities(starSystemNewGameOb, sysData);
                    NewSystemListUI(starSysController);
                    localPlayerTheme = ThemeManager.Instance.GetLocalPlayerTheme();
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
                // "SysName" not done here. See in each system ribbon is set in GalaxyMenuUIController without a facility game object needed
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
                        FactoryData factoryData = new FactoryData("null");
                        var factorySO = GetFactorySObyCivInt(civInt);
                        factoryData.CivInt = factorySO.CivInt;
                        factoryData.TechLevel = factorySO.TechLevel;
                        factoryData.FacilitiesEnumType = factorySO.FacilitiesEnumType;
                        factoryData.Name = factorySO.Name;
                        factoryData.StartStarDate = factorySO.StartStarDate;
                        factoryData.PowerLoad = factorySO.PowerLoad;
                        factoryData.BuildDuration = factorySO.BuildDuration;
                        factoryData.FactorySprite = factorySO.FactorySprite;
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
                            On.text = "1"; // 0 = off, 1 = on.
                            GetFactoryText(newFacilityGO, factoryData, numOf);
                            newFacilityGO.SetActive(false);
                            factoryData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                            sysData.TotalSysPowerLoad += factoryData.PowerLoad;
                        }
                        break;
                    }
                case "ShipyardData":
                    {
                        ShipyardData syData = new ShipyardData("null");
                        var sSO = GetShipyardSObyCivInt(civInt);
                        syData.CivInt = sSO.CivInt;
                        syData.TechLevel = sSO.TechLevel;
                        syData.FacilitiesEnumType = sSO.FacilitiesEnumType;
                        syData.Name = sSO.Name;
                        syData.StartStarDate = sSO.StartStarDate;
                        syData.BuildDuration = sSO.BuildDuration;
                        syData.PowerLoad = sSO.PowerLoad;
                        syData.ShipyardSprite = sSO.ShipyardSprite;
                        syData.Description = sSO.Description;
                        sysData.ShipyardData = syData;
                        List<ShipyardData> syDatas = new List<ShipyardData>();
                        for (int i = 0; i < numOf; i++)
                            syDatas.Add(syData);
                        for (int i = 0; i < syDatas.Count; i++)
                        {
                            GameObject newFacilityGO = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                            newFacilityGO.layer = 5;
                            TextMeshProUGUI On = newFacilityGO.AddComponent<TextMeshProUGUI>();
                            On.text = "1";
                            GetShipyardText(newFacilityGO, syData, numOf);
                            newFacilityGO.SetActive(false);
                            syData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                            sysData.TotalSysPowerLoad += syData.PowerLoad;
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
                            On.text = "1";
                            GetShieldGText(newFacilityGO, sgData, numOf);
                            newFacilityGO.SetActive(false);
                            sgData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                            sysData.TotalSysPowerLoad += sgData.PowerLoad;
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
                            On.text = "1";
                            GetOBText(newFacilityGO, obData, numOf);
                            newFacilityGO.SetActive(false);
                            obData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                            sysData.TotalSysPowerLoad += obData.PowerLoad;
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
                            On.text = "1";
                            GetResearchCenterText(newFacilityGO, researchData, numOf);
                            newFacilityGO.SetActive(false);
                            researchData.SysGameObject = newFacilityGO;
                            list.Add(newFacilityGO);
                            sysData.TotalSysPowerLoad += researchData.PowerLoad;
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
            for (int i = 0;i < TheText.Length;i++) 
            {
                TheText[i].enabled = true;
                if (TheText[i].name == "NameText (TMP)")
                    TheText[i].text = plantData.Name;
                else if (TheText[i].name == "NumTotalUnits (TMP)")
                    TheText[i].text = numOf.ToString();
                else if (TheText[i].name == "NumTotalEOut (TMP)")
                    TheText[i].text = plantData.PowerOutput.ToString();
                else if (TheText[i].name == "DescriptionText (TMP)")
                    TheText[i].text = plantData.Description;
                //Doing the system power load in SysData/ GalaxyMenuUIController //else if (OneTmp.name == "NumP Load")

                // image here

            }
        }
        private void GetFactoryText(GameObject go, FactoryData factoryData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i].name == "NameFactory")
                    TheText[i].text = factoryData.Name;
                else if (TheText[i].name == "NumFactoryRatio")
                    TheText[i].text = numOf.ToString();
                else if (TheText[i].name == "FactoryLoad")
                    TheText[i].text = factoryData.PowerLoad.ToString();
                else if (TheText[i].name == "DescriptionFactory")
                    TheText[i].text = factoryData.Description;
                    
                // image here

            }
        }

        private void GetShipyardText(GameObject go, ShipyardData factoryData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i].name == "NameShipyard")
                    TheText[i].text = factoryData.Name;
                else if (TheText[i].name == "NumShipyardRatio")
                    TheText[i].text = numOf.ToString();
                else if (TheText[i].name == "ShipyardLoad")
                    TheText[i].text = factoryData.PowerLoad.ToString();
                else if (TheText[i].name == "DescriptionShipyard")
                    TheText[i].text = factoryData.Description;
                // image here

            }
        }
        private void GetShieldGText(GameObject go, ShieldGeneratorData shieldData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i].name == "NameShieldG")
                    TheText[i].text = shieldData.Name;
                else if (TheText[i].name == "NumShieldGRatio")
                    TheText[i].text = numOf.ToString();
                else if (TheText[i].name == "ShieldGLoad")
                    TheText[i].text = shieldData.PowerLoad.ToString();
                else if (TheText[i].name == "DescriptionShieldG")
                    TheText[i].text = shieldData.Description;
                // image here

            }
        }
        private void GetOBText(GameObject go, OrbitalBatteryData oBData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i].name == "NameOB")
                    TheText[i].text = oBData.Name;
                else if (TheText[i].name == "NumOBRatio")
                    TheText[i].text = numOf.ToString();
                else if (TheText[i].name == "OBLoad")
                    TheText[i].text = oBData.PowerLoad.ToString();
                else if (TheText[i].name == "DescriptionOB")
                    TheText[i].text = oBData.Description;
                // image here

            }
        }
        private void GetResearchCenterText(GameObject go, ResearchCenterData resData, int numOf)
        {
            TextMeshProUGUI[] TheText = go.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i].name == "NameResearchCenter")
                    TheText[i].text = resData.Name;
                else if (TheText[i].name == "NumResearchCenterRatio")
                    TheText[i].text = numOf.ToString();
                else if (TheText[i].name == "ResearchCenterLoad")
                    TheText[i].text = resData.PowerLoad.ToString();
                else if (TheText[i].name == "DescriptionResearchCenter")
                    TheText[i].text = resData.Description;
                // image here

            }
        }
        private StarSysSO GetStarSObyInt(int sysInt)
        {
            StarSysSO result = null;
            for (int i = 0; i< starSysSOList.Count; i++)
            {
                if (starSysSOList[i].StarSysInt == sysInt)
                {
                    result = starSysSOList[i];
                    break;
                }
            }
            return result;

        }
        private PowerPlantSO GetPowrPlantSObyCivInt(int  civInt)
        {
            PowerPlantSO result = null;
            for(int i = 0;i< powerPlantSOList.Count;i++)
            {
                if (powerPlantSOList[i].CivInt == civInt)
                {
                    result = powerPlantSOList[i];
                    break;
                }
            }
            return result;
        }
        private FactorySO GetFactorySObyCivInt(int civInt)
        {
            FactorySO result = null;
            for (int i = 0; i < factorySOList.Count; i++)
            {
                if (factorySOList[i].CivInt == civInt)
                {
                    result = factorySOList[i];
                    break;
                }
            }
            return result;
        }
        private ShipyardSO GetShipyardSObyCivInt(int civInt)
        {
            ShipyardSO result = null;
            for (int i = 0;i< shipyardSOList.Count;i++)
            {
                if (shipyardSOList[i].CivInt == civInt)
                {
                    result = shipyardSOList[i];
                    break;
                }
            }
            return result;
        }
        private ShieldGeneratorSO GetShieldGeneratorSObyCivInt(int civInt)
        {
            ShieldGeneratorSO result = null;
            for (int i = 0;i< shieldGeneratorSOList.Count;i++)
            {
                if (shieldGeneratorSOList[i].CivInt == civInt)
                {
                    result = shieldGeneratorSOList[i];
                    break;
                }
            }
            return result;
        }
        private OrbitalBatterySO GetOrbitalBatterySObyCivInt(int civInt)
        {
            OrbitalBatterySO result = null;
            for (int i = 0;i< orbitalBatterySOList.Count;i++)
            {
                if (orbitalBatterySOList[i].CivInt == civInt)
                {
                    result = orbitalBatterySOList[i];
                    break;
                }
            }
            return result;
        }
        private ResearchCenterSO GetResearchCenterSObyCivInt(int civInt)
        {
            ResearchCenterSO result = null;
            for (int i = 0; i< researchCenterSOList.Count; i++)
            {
                if (researchCenterSOList[i].CivInt == civInt)
                {
                    result = researchCenterSOList[i];
                    break;
                }
            }
            return result;
        }
        public StarSysData GetStarSysDataByName(string name)
        {

            StarSysData result = null;
            for (int i = 0;i< ManagersStarSysControllerList.Count; i++)
            {

                if (ManagersStarSysControllerList[i].StarSysData.GetSysName().Equals(name))
                {
                    result = ManagersStarSysControllerList[i].StarSysData;
                    break;
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
        public void NewSystemListUI(StarSysController sysController)
        {
            if (sysController.StarSysData.CurrentOwner == GameController.Instance.GameData.LocalPlayerCivEnum)
            {
                GameObject thisStarSysUIGameObject = (GameObject)Instantiate(sysUIPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
                thisStarSysUIGameObject.layer = 5;
                sysController.StarSysListUIGameObject = thisStarSysUIGameObject; 
                thisStarSysUIGameObject.transform.SetParent(contentFolderParent.transform, false); // load into List of systems
            }
            
        }
        public void InstantiateSysBuildListUI(StarSysController sysCon)
        {
            int i = 0;
            switch (sysCon.StarSysData.CurrentCivController.CivData.CivEnum)
            {
                case CivEnum.FED:
                    i = 0;
                    break;
                case CivEnum.ROM:
                    i = 1;
                    break;
                case CivEnum.KLING:
                    i = 2;
                    break;
                case CivEnum.CARD:
                    i = 3;
                    break;
                case CivEnum.DOM:
                    i = 4;
                    break;
                case CivEnum.BORG:
                    i = 5;
                    break;
                case CivEnum.TERRAN:
                    i = 0;
                    break;
                default:
                    break;

            }
            GameObject sysBuildListUI = (GameObject)Instantiate(buildListPrefabs[i], new Vector3(0, 0, 0),
                Quaternion.identity);
            MenuManager.Instance.SetBuildMenu(sysBuildListUI);
            sysBuildListUI.layer = 5; //UI layer
            canvasBuildList.SetActive(true);
            // *********** get the FactoryBuildableItems codes, set StarSysController/Data for them so the can send image endDrags back.
            sysBuildListUI.transform.SetParent(canvasBuildList.transform, false);
            

        }
    }   
}

