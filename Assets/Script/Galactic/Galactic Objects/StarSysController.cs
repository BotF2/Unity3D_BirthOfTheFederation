using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;


namespace Assets.Core
{
    /// <summary>
    /// Controlling Star System interactions while the matching StarSystemData class
    /// holds key info on status and for save game
    /// </summary>
    public class StarSysController : MonoBehaviour
    {
        //Fields
        private StarSysData starSysData;
        public StarSysData StarSysData { get { return starSysData; } set { starSysData = value; } }
        [SerializeField]
        private GameObject starSysListUIGameObject; //The instantiated system UI for system list, a prefab clone, not a class but a game object
        // instantiated by StarSysManager from a prefab and added to StarSysController, NewSystemListUI()
        public GameObject StarSysListUIGameObject { get { return starSysListUIGameObject; } set { starSysListUIGameObject = value; } }
        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas canvasToolTip;
        public static event Action<TrekRandomEventSO> TrekEventDisasters;
        public GridLayoutGroup buildListGridLayoutGroup;
        public GridLayoutGroup shipListGridLayoutGroup;
        [SerializeField]
        private List<Transform> sysBuildQueueList;
        private int lastBuildQueueCount = 0;
        private Transform lastBuildingItem;
        private Transform buildingItem;
        private bool building = false;
        private bool starTimer = true;
        public Slider SliderBuildProgress;
        private float starDateOfCompletion = 1f;
        private int currentProgress =1;
        private int startDate = 1;
        public int TimeToBuild = 1;

        public StarSysController(string name)
        {
            StarSysData = new StarSysData(name);
            starDateOfCompletion = 0f;
        }
        private void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            canvasToolTip.worldCamera = galaxyEventCamera;
            TimeManager.Instance.OnRandomSpecialEvent += DoDisaster;
            OnOffSysFacilityEvents.current.FacilityOnClick += FacilityOnClick;// subscribe methode to the event += () => Debug.Log("Action Invoked!");
            starDateOfCompletion = 0f;
        }

        public void GridFactoryQueueUpdate()
        {
            lastBuildQueueCount = this.buildListGridLayoutGroup.transform.childCount;
            Debug.Log("Grid layout has changed!");
            // update syscontroller sysBuildQueue list to match buildListBridLayoutGroup.tranform children
            foreach (Transform child in buildListGridLayoutGroup.transform)
            {
                if (!sysBuildQueueList.Contains(child))
                    sysBuildQueueList.Add(child);
            }

            //Does sysBuildQueueList have extra items not in buildListGridLayoutGroup children?
            foreach (Transform child in buildListGridLayoutGroup.transform)
            {   
                if (!sysBuildQueueList.Contains(child))
                    sysBuildQueueList.Remove(child);
            }

            // Sort by Y position (top to bottom), then X position (left to right)
            sysBuildQueueList = sysBuildQueueList.OrderByDescending(t => t.localPosition.y)
                                    .ThenBy(t => t.localPosition.x)
                                    .ToList();
            if (sysBuildQueueList.Count > 0 && sysBuildQueueList[0] != null)
            {
                buildingItem = sysBuildQueueList[0];
                building = true;
                
                if (buildingItem != lastBuildingItem)
                {
                    TimeToBuild = GetBuildTimeDuration(buildingItem.gameObject.GetComponentInChildren<FactoryBuildableItem>().FacilityType);
                    lastBuildingItem = buildingItem;
                    starTimer = true;
                }
            }
            else { building = false; }
        }

        private void Update()
        {
            // Did anything change in the build queue?
            if (GameController.Instance.AreWeLocalPlayer(this.StarSysData.CurrentOwner))
            {
                if (buildListGridLayoutGroup != null)
                {
                    if (lastBuildQueueCount != buildListGridLayoutGroup.transform.childCount)
                    {
                        GridFactoryQueueUpdate();
                    }
                    else
                    {
                        int counter = 0;
                        foreach (var item in buildListGridLayoutGroup.transform)
                        {
                            if (sysBuildQueueList[counter] != null && (Transform)item != sysBuildQueueList[counter])
                            {
                                GridFactoryQueueUpdate();
                                break;
                            }
                            else
                                counter++;
                        }
                    }
                }
            }
            // Are we building anything
            // 
            if (building && TimeToBuild > 0)
            {

                if (starTimer)
                {
                    //maxProgress = TimeToBuild;
                    startDate = TimeManager.Instance.CurrentStarDate();
                    starDateOfCompletion = TimeManager.Instance.CurrentStarDate() + TimeToBuild;
                    starTimer = false;
                }
                else if(TimeManager.Instance.CurrentStarDate() <= starDateOfCompletion)
                {
                   currentProgress = (int)(TimeManager.Instance.CurrentStarDate() - startDate);
                   SetBuildProgress((float)currentProgress / (float)TimeToBuild);
                }
                else if (TimeManager.Instance.CurrentStarDate() >= starDateOfCompletion)
                {
                    building = false;
                    SetBuildProgress(0);
                    starTimer = true;
                    TimeToBuild = 0;
                    buildingItem = null;
                    switch (sysBuildQueueList[0].gameObject.GetComponentInChildren<FactoryBuildableItem>().FacilityType)
                    {
                        case StarSysFacilities.PowerPlanet:
                            this.StarSysData.PowerStations.Add(StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.PowerPlantPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData, 0)[0]);
                            if (starSysListUIGameObject != null)
                            {
                                TextMeshProUGUI[] theTextItems = starSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                                for (int j = 0; j < theTextItems.Length; j++)
                                {
                                    theTextItems[j].enabled = true;
                                    if (theTextItems[j].name == "NumPUnits")
                                        theTextItems[j].text = this.StarSysData.PowerStations.Count.ToString();
                                    else if (theTextItems[j].name == "NumTotalEOut")
                                    {
                                        this.starSysData.TotalSysPowerOutput = (this.StarSysData.PowerStations.Count) * (this.StarSysData.PowerPlantData.PowerOutput);
                                        theTextItems[j].text = this.starSysData.TotalSysPowerOutput.ToString();
                                    }
                                }
                            }
                            GalaxyMenuUIController.Instance.UpdateSystemPowerLoad(this);
                            break;

                        case StarSysFacilities.Factory:
                            var factory = (StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.FactoryPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData, 0)[0]);
                            AddSysFacility(factory, "FactoryLoad", "NumFactoryRatio", StarSysFacilities.Factory);
                            break;
                        case StarSysFacilities.Shipyard:
                            var shipyard = StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.ShipyardPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData, 0)[0];
                            AddSysFacility(shipyard, "YardLoad", "NumYardsOnRatio", StarSysFacilities.Shipyard);
                            break;
                        case StarSysFacilities.ShieldGenerator:
                            var shieldGenerator = StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.ShieldGeneratorPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData, 0)[0];
                            AddSysFacility(shieldGenerator, "ShieldLoad", "NumShieldRatio", StarSysFacilities.ShieldGenerator);
                            break;
                        case StarSysFacilities.OrbitalBattery:
                            var orbitalBatterie = StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.OrbitalBatteryPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData, 0)[0];
                            AddSysFacility(orbitalBatterie, "OBLoad", "NumOBRatio", StarSysFacilities.OrbitalBattery);
                            break;
                        case StarSysFacilities.ResearchCenter:
                            var researchCenter = StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.ResearchCenterPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData, 0)[0];
                            AddSysFacility(researchCenter, "ResearchLoad", "NumResearchRatio", StarSysFacilities.ResearchCenter);

                            break;
                        default:
                            break;
                    }
                    var imageTransform = sysBuildQueueList[0];
                    imageTransform.SetParent(imageTransform.GetComponent<FactoryBuildableItem>().originalParent, false);
                    if (imageTransform.parent.childCount > 1)
                    {
                        imageTransform.gameObject.SetActive(false);
                    }
                    sysBuildQueueList.Remove(sysBuildQueueList[0]);
                }
            }
            else if (TimeToBuild < 0)
            {
                TimeToBuild = 0;

            }
        }

        private void AddSysFacility(GameObject faciltyGO, string loadName, string ratioName, StarSysFacilities facilityType )
        {
            int newFacilityLoad = 0;
            List<GameObject> facilities = new List<GameObject>();
            switch (facilityType)
            {
                case StarSysFacilities.Factory:
                    newFacilityLoad = StarSysData.FactoryData.PowerLoad;
                    this.StarSysData.Factories.Add(faciltyGO);
                    facilities = this.StarSysData.Factories;
                    break;
                case StarSysFacilities.Shipyard:
                    newFacilityLoad = StarSysData.ShipyardData.PowerLoad;
                    this.StarSysData.Shipyards.Add(faciltyGO);
                    facilities = this.StarSysData.Shipyards;
                    break;
                case StarSysFacilities.ShieldGenerator:
                    newFacilityLoad = StarSysData.ShieldGeneratorData.PowerLoad;
                    this.StarSysData.ShieldGenerators.Add(faciltyGO);
                    facilities = StarSysData.ShieldGenerators;
                    break;
                case StarSysFacilities.OrbitalBattery:
                    newFacilityLoad = StarSysData.OrbitalBatteryData.PowerLoad;
                    this.StarSysData.OrbitalBatteries.Add(faciltyGO);
                    facilities = StarSysData.OrbitalBatteries;
                    break;
                case StarSysFacilities.ResearchCenter:
                    newFacilityLoad = StarSysData.ResearchCenterData.PowerLoad;
                    this.StarSysData.ResearchCenters.Add(faciltyGO);
                    facilities = StarSysData.ResearchCenters;
                    break;
                default:
                    break;
            }

            if (starSysListUIGameObject != null)
            {
                TextMeshProUGUI[] theTextItems = starSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                bool allDone = false;
                for (int j = 0; j < theTextItems.Length; j++)
                {
                    theTextItems[j].enabled = true;
                    int load = 0;
                    if (theTextItems[j].name == loadName)
                    {
                        for (int k = 0; k < facilities.Count; k++)
                        {
                            if (facilities[k].GetComponent<TextMeshProUGUI>().text == "1")
                            {
                                load += newFacilityLoad;
                            }
                        }
                        theTextItems[j].text = load.ToString();
                    }
                    else if (theTextItems[j].name == ratioName)
                    {
                        int numOn = 0;
                        for (int i = 0; i < facilities.Count; i++)
                        {
                            TextMeshProUGUI TheText = facilities[i].GetComponent<TextMeshProUGUI>();
                            if (TheText.text == "1") // 1 = on and 0 = off
                                numOn++;
                        }
                        theTextItems[j].text = numOn.ToString() + "/" + (facilities.Count).ToString();
                        allDone = true;
                    }
                    else if (allDone)
                        break;
                }
            }
            GalaxyMenuUIController.Instance.UpdateSystemPowerLoad(this);
        }
 
        public int GetBuildTimeDuration(StarSysFacilities starSysFacilities)
        {
            int timeDuration = 1;
            TechLevel ourTechLevel = this.StarSysData.CurrentCivController.CivData.TechLevel;
            switch (starSysFacilities)
            {
                case StarSysFacilities.PowerPlanet:
                    timeDuration = this.StarSysData.PowerPlantData.BuildDuration;
                    break;
                case StarSysFacilities.Factory:
                    timeDuration = this.StarSysData.FactoryData.BuildDuration;
                    break;
                case StarSysFacilities.Shipyard:
                    timeDuration = this.StarSysData.ShipyardData.BuildDuration;
                    break;
                case StarSysFacilities.ShieldGenerator:
                    timeDuration = this.StarSysData.ShieldGeneratorData.BuildDuration;
                    break;
                case StarSysFacilities.OrbitalBattery:
                    timeDuration = this.StarSysData.OrbitalBatteryData.BuildDuration;
                    break;
                case StarSysFacilities.ResearchCenter:
                    timeDuration = this.StarSysData.ResearchCenterData.BuildDuration;
                    break;
                default:
                    break;
            }
            return timeDuration;
            //ToD use tech level to set features of system production, defence....
        }

        public void AddToFactoryQueue(GameObject facilityPrefab)
        {
            GameObject systemFacilityGO = (GameObject)Instantiate(facilityPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
            this.StarSysData.FactoryBuildQueue.Add(systemFacilityGO);
        }

        public void AddToShipyardQueue(StarSysController theSystem, ShipData shipData)
        {
            this.StarSysData.ShipyardQueue.Add(shipData);
        }
        public void DoHabitalbeSystemUI(FleetController discoveringFleetCon)
        {
            if (discoveringFleetCon != null)
            {
                HabitableSysUIController.Instance.LoadHabitableSysUI(this, discoveringFleetCon);
            } 
        }

        public void UpdateOwner(CivEnum newOwner) // system captured or colonized
        {
            starSysData.CurrentOwner = newOwner;
        }
        private void OnMouseDown()
        {
            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                // what a Star System Controller does with a hit
                if (hitObject.tag != "GalaxyImage" && GameController.Instance.AreWeLocalPlayer(this.StarSysData.CurrentOwner)) // this 'StarSystem' is a local player galaxy object hit
                {
                    if (FleetUIController.Instance.MouseClickSetsDestination == false) // not while our FleetUIController was looking for a destination
                    {
                        GameObject aNull = new GameObject();
                        MenuManager.Instance.OpenMenu(Menu.ASystemMenu, aNull); // get a single system UI on map system click
                        GalaxyMenuUIController.Instance.OpenASystemUI(this);
                    }
                    else
                    {
                        NewDestination(hitObject); // one of our systems hit 
                    }
                }
                else if (FleetUIController.Instance.MouseClickSetsDestination == true)
                {
                    NewDestination(hitObject);
                }
            }
        }
        private void NewDestination(GameObject hitObject)
        {
            bool isFleet = false;
            FleetUIController.Instance.SetAsDestination(hitObject, isFleet);

        }

        public void OnEnable()
        {
            TimeManager.Instance.OnRandomSpecialEvent += DoDisaster;
        }
        public void OnDisable()
        {
            TimeManager.Instance.OnRandomSpecialEvent -= DoDisaster;
        }
        private void DoDisaster(TrekRandomEventSO randomSpecialEvent)
        {
            if (randomSpecialEvent != null)
            {
                Debug.Log("Special event reached StarSystemController: " + randomSpecialEvent.eventName + " on oneInXChance " +
                    randomSpecialEvent.oneInXChance + " TrekRandomEvents: " + randomSpecialEvent.trekEventType +
                    " parameter: " + randomSpecialEvent.eventParameter);
                // Add your logic to handle the special event here
                switch (randomSpecialEvent.trekEventType)
                {
                    case TrekRandomEvents.AsteroidHit:
                        {
                            // ToDo: Do Disaster code for each disaster 
                            Debug.Log("******** Asteroid ***********"); ;
                            break;
                        }
                    case TrekRandomEvents.Pandemic:
                        {
                            Debug.Log("********** PANDEMIC **********");
                            break;
                        }
                    case TrekRandomEvents.SuperVolcano:
                        {
                            Debug.Log("********** SUPER VOLCANO **********");
                            break;
                        }
                    case TrekRandomEvents.GamaRayBurst:
                        {
                            Debug.Log("********** GAMERAY BURST **********");
                            break;
                        }

                    case TrekRandomEvents.SeismicEvent:
                        {
                            Debug.Log("********** SEISMEIC EVENT **********");
                            break;
                        }
                    case TrekRandomEvents.Teribals:
                        {
                            Debug.Log("********** TERIBAL TROUBLE **********");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        public void BuildClick(StarSysController sysCon) // open build and ship build list UI
        {
            StarSysManager.Instance.InstantiateSysBuildListUI(this);

            MenuManager.Instance.OpenMenu(Menu.BuildMenu, null);

        }
        public void ShipClick(StarSysController sysCon) // open build and ship build list UI
        {
            StarSysManager.Instance.InstantiateSysBuildListUI(this);

            MenuManager.Instance.OpenMenu(Menu.BuildMenu, null);

        }
        public void FacilityOnClick(StarSysController sysCon, string name)
        {
            if (this == sysCon)
            {
                switch (name)
                {
                    case "FactoryButtonOn":
                        {
                            // Do we have enough power to turn a factory on?
                            if (this.StarSysData.TotalSysPowerLoad + StarSysData.FactoryData.PowerLoad >
                                    this.StarSysData.TotalSysPowerOutput)
                            {
                                GalaxyMenuUIController.Instance.FlashPowerOverload();
                                break;
                            }
                            for (int i = 0; i < this.StarSysData.Factories.Count; i++)
                            {
                                if (StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    if (this.StarSysData.TotalSysPowerLoad + StarSysData.FactoryData.PowerLoad <=
                                        this.StarSysData.TotalSysPowerOutput)
                                    {
                                        this.StarSysData.TotalSysPowerLoad += StarSysData.FactoryData.PowerLoad;
                                        StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text = "1";
                                        GalaxyMenuUIController.Instance.UpdateFacilityUI(this, 1, "FactoryLoad", "NumFactoryRatio", StarSysFacilities.Factory);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "FactoryButtonOff":
                        {
                            for (int i = 0; i < this.StarSysData.Factories.Count; i++)
                            {
                                if (StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text == "1")
                                {
                                    StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text = "0";
                                    this.StarSysData.TotalSysPowerLoad -= StarSysData.FactoryData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateFacilityUI(this, -1, "FactoryLoad", "NumFactoryRatio", StarSysFacilities.Factory);
                                    break;
                                }                      
                            }
                        }
                        break;
                    case "YardButtonOn":
                        {
                            if (this.StarSysData.TotalSysPowerLoad + StarSysData.ShipyardData.PowerLoad >
                                    this.StarSysData.TotalSysPowerOutput)
                            {
                                GalaxyMenuUIController.Instance.FlashPowerOverload();
                                break;
                            }
                            for (int i = 0; i < this.StarSysData.Shipyards.Count; i++)
                            {
                                if (StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    if (this.StarSysData.TotalSysPowerLoad + StarSysData.ShipyardData.PowerLoad <=
                                        this.StarSysData.TotalSysPowerOutput)
                                    {
                                        StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text = "1";
                                        this.StarSysData.TotalSysPowerLoad += StarSysData.ShipyardData.PowerLoad;
                                        GalaxyMenuUIController.Instance.UpdateFacilityUI(this, 1, "YardLoad", "NumYardsOnRatio", StarSysFacilities.Shipyard);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "YardButtonOff":
                        {

                            for (int i = 0; i < this.StarSysData.Shipyards.Count; i++)
                            {
                                if (StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text == "1")
                                {
                                    StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text = "0";
                                    this.StarSysData.TotalSysPowerLoad -= StarSysData.ShipyardData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateFacilityUI(this, -1, "YardLoad", "NumYardsOnRatio", StarSysFacilities.Shipyard);
                                    break;
                                }
                            }
                        }
                        break;
                    case "ShieldButtonOn":
                        {
                            if (this.StarSysData.TotalSysPowerLoad + StarSysData.ShieldGeneratorData.PowerLoad >
                                    this.StarSysData.TotalSysPowerOutput)
                            {
                                GalaxyMenuUIController.Instance.FlashPowerOverload();
                                break;
                            }
                            for (int i = 0; i < this.StarSysData.ShieldGenerators.Count; i++)
                            {
                                if (StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    if (this.StarSysData.TotalSysPowerLoad + StarSysData.ShieldGeneratorData.PowerLoad <=
                                        this.StarSysData.TotalSysPowerOutput)
                                    {
                                        StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text = "1";
                                        this.StarSysData.TotalSysPowerLoad += StarSysData.ShieldGeneratorData.PowerLoad;
                                        GalaxyMenuUIController.Instance.UpdateFacilityUI(this, 1, "ShieldLoad", "NumShieldRatio", StarSysFacilities.ShieldGenerator);  
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "ShieldButtonOff":
                        {
                            for (int i = 0; i < this.StarSysData.ShieldGenerators.Count; i++)
                            {
                                if (StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text == "1")
                                {
                                    StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text = "0";
                                    this.StarSysData.TotalSysPowerLoad -= StarSysData.ShieldGeneratorData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateFacilityUI(this, -1, "ShieldLoad", "NumShieldRatio", StarSysFacilities.ShieldGenerator);
                                    break;
                                }
                            }
                        }
                        break;
                    case "OBButtonOn":
                        {
                            if (this.StarSysData.TotalSysPowerLoad + StarSysData.OrbitalBatteryData.PowerLoad >
                                       this.StarSysData.TotalSysPowerOutput)
                            {
                                GalaxyMenuUIController.Instance.FlashPowerOverload();
                                break;
                            }
                            for (int i = 0; i < this.StarSysData.OrbitalBatteries.Count; i++)
                            {
                                if (StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    if (this.StarSysData.TotalSysPowerLoad + StarSysData.OrbitalBatteryData.PowerLoad <=
                                        this.StarSysData.TotalSysPowerOutput)
                                    {
                                        StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text = "1";
                                        this.StarSysData.TotalSysPowerLoad += StarSysData.OrbitalBatteryData.PowerLoad;
                                        GalaxyMenuUIController.Instance.UpdateFacilityUI(this, 1, "OBLoad", "NumOBRatio", StarSysFacilities.OrbitalBattery);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "OBButtonOff":
                        {
                            for (int i = 0; i < this.StarSysData.OrbitalBatteries.Count; i++)
                            {
                                if (StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text == "1")
                                {
                                    StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text = "0";
                                    this.StarSysData.TotalSysPowerLoad -= StarSysData.OrbitalBatteryData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateFacilityUI(this, -1, "OBLoad", "NumOBRatio", StarSysFacilities.OrbitalBattery);
                                    break;
                                }
                            }
                        }
                        break;
                    case "ResearchButtonOn":
                        {
                            if (this.StarSysData.TotalSysPowerLoad + StarSysData.ResearchCenterData.PowerLoad >
                                     this.StarSysData.TotalSysPowerOutput)
                            {
                                GalaxyMenuUIController.Instance.FlashPowerOverload();
                                break;
                            }
                            for (int i = 0; i < this.StarSysData.ResearchCenters.Count; i++)
                            {
                                if (StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    if (this.StarSysData.TotalSysPowerLoad + StarSysData.ResearchCenterData.PowerLoad <=
                                        this.StarSysData.TotalSysPowerOutput)
                                    {
                                        StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text = "1";
                                        this.StarSysData.TotalSysPowerLoad += StarSysData.ResearchCenterData.PowerLoad;
                                        GalaxyMenuUIController.Instance.UpdateFacilityUI(this, 1, "ResearchLoad", "NumResearchRatio", StarSysFacilities.ResearchCenter);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "ResearchButtonOff":
                        {
                            for (int i = 0; i < this.StarSysData.ResearchCenters.Count; i++)
                            {
                                if (StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text == "1")
                                {
                                    StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text = "0";
                                    this.StarSysData.TotalSysPowerLoad -= StarSysData.ResearchCenterData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateFacilityUI(this, -1, "ResearchLoad", "NumResearchRatio", StarSysFacilities.ResearchCenter);
                                    break;
                                }          
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        private void OnDestroy()
        {
            TimeManager.Instance.OnRandomSpecialEvent -= DoDisaster;
            OnOffSysFacilityEvents.current.FacilityOnClick -= FacilityOnClick;
        }
        public void SetBuildProgress(float progress)
        {
            SliderBuildProgress.value = progress;
        }
    }

}
