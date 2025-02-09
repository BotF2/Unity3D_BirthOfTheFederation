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
       // public Canvas canvasYourStarSysUI;
        public static event Action<TrekRandomEventSO> TrekEventDisasters;
        public GridLayoutGroup buildListGridLayoutGroup;
        [SerializeField]
        private List<Transform> sysBuildQueueList;
        private int lastBuildQueueCount = 0;
        private Transform lastBuildingItem;
        private Transform buildingItem;
        private bool building = false;
        private bool starTimer = true;
        private float starDateOfCompletion = 0f; 
        [SerializeField]
        private float remainingTimeToBuild = 1;


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
                    remainingTimeToBuild = GetBuildTimeDuration(buildingItem.gameObject.GetComponentInChildren<FactoryBuildableItem>().FacilityType);
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
                       // building = false;
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
            if (building && remainingTimeToBuild > 0)
            {

                if (starTimer)
                {
                    starDateOfCompletion = TimeManager.Instance.CurrentStarDate() + remainingTimeToBuild;
                    starTimer = false;
                }
                else if (TimeManager.Instance.CurrentStarDate() >= starDateOfCompletion)
                {
                    building = false;
                    starTimer = true;
                    remainingTimeToBuild = 0f;
                    buildingItem = null;
                    switch (sysBuildQueueList[0].gameObject.GetComponentInChildren<FactoryBuildableItem>().FacilityType)
                    {
                        case StarSysFacilities.PowerPlanet:
                            this.StarSysData.PowerStations.Add(StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.PowerPlantPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData)[0]);
                            if (starSysListUIGameObject != null)
                            {
                                TextMeshProUGUI[] theTextItems = starSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                                for (int j = 0; j < theTextItems.Length; j++)
                                {
                                    theTextItems[j].enabled = true;
                                    if (theTextItems[j].name == "NumPUnits")
                                        theTextItems[j].text = this.StarSysData.PowerStations.Count.ToString();
                                    else if (theTextItems[j].name == "NumTotalEOut")
                                        theTextItems[j].text = ((this.StarSysData.PowerStations.Count) * (this.StarSysData.PowerPlantData.PowerOutput)).ToString();
                                }
                            }
                            break;
                    
                        case StarSysFacilities.Factory:
                            var factory = (StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.FactoryPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData)[0]);
                            this.StarSysData.Factories.Add(factory);
                            var textOnOff = factory.GetComponent<TextMeshProUGUI>();
                            textOnOff.enabled = true;
                            textOnOff.text = "0"; // off 
                            if (starSysListUIGameObject != null)
                            {
                                TextMeshProUGUI[] theTextItems = starSysListUIGameObject.GetComponentsInChildren<TextMeshProUGUI>();
                                for (int j = 0; j < theTextItems.Length; j++)
                                {
                                    theTextItems[j].enabled = true;
                                    if (theTextItems[j].name == "FactoryLoad")
                                        theTextItems[j].text = ((this.StarSysData.Factories.Count) * (this.StarSysData.FactoryData.PowerLoad)).ToString();
                                    else if (theTextItems[j].name == "NumFactoryRatio")
                                    {
                                        int num = 0;
                                        foreach (var item in this.StarSysData.Factories) 
                                        {
                                            TextMeshProUGUI TheText = item.GetComponent<TextMeshProUGUI>();
                                            if (TheText.text == "1") // 1 = on and 0 = off
                                                num++;
                                        }
                                        theTextItems[j].text = num.ToString() + "/" + (this.StarSysData.Factories.Count).ToString();
                                    }
                                }
                            }
                            break;
                        case StarSysFacilities.Shipyard:
                            this.StarSysData.Shipyards.Add(StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.ShipyardPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData)[0]);
                            break;
                        case StarSysFacilities.ShieldGenerator:
                            this.StarSysData.ShieldGenerators.Add(StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.ShieldGeneratorPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData)[0]);
                            break;
                        case StarSysFacilities.OrbitalBattery:
                            this.StarSysData.OrbitalBatteries.Add(StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.OrbitalBatteryPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData)[0]);
                            break;
                        case StarSysFacilities.ResearchCenter:
                            this.StarSysData.ResearchCenters.Add(StarSysManager.Instance.AddSystemFacilities(1, StarSysManager.Instance.ResearchCenterPrefab, (int)this.StarSysData.CurrentOwner, this.StarSysData)[0]);
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
            else if (remainingTimeToBuild < 0)
            {
                remainingTimeToBuild = 0;

            }
        }

 
        public float GetBuildTimeDuration(StarSysFacilities starSysFacilities)
        {
            float timeDuration = 1;
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
        private void RunCoundownClock(float duration)
        {

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
        public void BuildClick(StarSysController sysCon) // open build list UI
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
                            for (int i = 0; i < this.StarSysData.Factories.Count; i++)
                            {
                                if (StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    StarSysData.Factories[i].GetComponent<TextMeshProUGUI>().text = "1";
                                    this.StarSysData.TotalSysPowerLoad += StarSysData.FactoryData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateFactories(this, 1);
                                    break;
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
                                    GalaxyMenuUIController.Instance.UpdateFactories(this, -1);
                                    break;
                                }                      
                            }
                        }
                        break;
                    case "YardButtonOn":
                        {
                            for (int i = 0; i < this.StarSysData.Shipyards.Count; i++)
                            {
                                if (StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    StarSysData.Shipyards[i].GetComponent<TextMeshProUGUI>().text = "1";
                                    this.StarSysData.TotalSysPowerLoad += StarSysData.ShipyardData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateYards(this, 1);
                                    break;
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
                                    GalaxyMenuUIController.Instance.UpdateYards(this,  -1);
                                    break;
                                }
                            }
                        }
                        break;
                    case "ShieldButtonOn":
                        {
                            for (int i = 0; i < this.StarSysData.ShieldGenerators.Count; i++)
                            {
                                if (StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    StarSysData.ShieldGenerators[i].GetComponent<TextMeshProUGUI>().text = "1";
                                    this.StarSysData.TotalSysPowerLoad += StarSysData.ShieldGeneratorData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateShields(this, 1);
                                    break;
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
                                    GalaxyMenuUIController.Instance.UpdateShields(this, -1);
                                    break;
                                }
                            }
                        }
                        break;
                    case "OBButtonOn":
                        {
                            for (int i = 0; i < this.StarSysData.OrbitalBatteries.Count; i++)
                            {
                                if (StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    StarSysData.OrbitalBatteries[i].GetComponent<TextMeshProUGUI>().text = "1";
                                    this.StarSysData.TotalSysPowerLoad += StarSysData.OrbitalBatteryData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateOBs(this, 1);
                                    break;
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
                                    GalaxyMenuUIController.Instance.UpdateOBs(this, -1);
                                    break;
                                }
                            }
                        }
                        break;
                    case "ResearchButtonOn":
                        {
                            for (int i = 0; i < this.StarSysData.ResearchCenters.Count; i++)
                            {
                                if (StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text == "0")
                                {
                                    StarSysData.ResearchCenters[i].GetComponent<TextMeshProUGUI>().text = "1";
                                    this.StarSysData.TotalSysPowerLoad += StarSysData.ResearchCenterData.PowerLoad;
                                    GalaxyMenuUIController.Instance.UpdateResearchCenters(this, 1);
                                    break;
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
                                    GalaxyMenuUIController.Instance.UpdateResearchCenters(this, -1);
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
    }
}
