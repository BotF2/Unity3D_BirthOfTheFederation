using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        public Canvas canvasYourStarSysUI;
        public static event Action<TrekRandomEventSO> TrekEventDisasters;
        [SerializeField]
        private List<PowerPlantData> powerPlantDatasList;
        [SerializeField]
        private List<FactoryData> factoryDatasList;
        [SerializeField]
        private List<FactoryData> sysFactoryBuildQueueList;
        [SerializeField]
        private List<ShipyardData> shipyardDatasList;
        [SerializeField]
        private List<ShieldGeneratorData> shieildGeneratorDatasList;
        [SerializeField]
        private List<OrbitalBatteryData> orbitalBatteryDatasList;
        [SerializeField]
        private List<ResearchCenterData> researchCenterDatasList;
        [SerializeField]
        private bool building = false;

        public StarSysController(string name)
        {
            StarSysData = new StarSysData(name);
        }
        private void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            canvasToolTip.worldCamera = galaxyEventCamera;
            TimeManager.Instance.OnRandomSpecialEvent += DoDisaster;
            OnOffSysFacilityEvents.current.FacilityOnClick += FacilityOnClick;// subscribe methode to the event += () => Debug.Log("Action Invoked!");
        }
        private void Update()
        {
            if (building)
            {

            }
        }
        // ****** ToDo: need to know when a new facility has completed its build
        // ********* call for BuildCompeted(newGO, int powerloaddelta);
        public void FactoryBuildTimer(StarSysFacilities starSysFacilities)
        {
            TechLevel ourTechLevel = this.StarSysData.CurrentCivController.CivData.TechLevel;
            //ToD use tech level to set features of system production, defence....
            building = true;
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
        public void BuildClick(StarSysController sysCon, string name) // open build list UI
        {
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
