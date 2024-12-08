using System;
using System.Collections.Generic;
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
        private GameObject starSysUIController;
        public GameObject StarSysUIController { get { return starSysUIController; } set { starSysUIController = value; } }
        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas canvasToolTip;
        public Canvas canvasYourStarSysUI;
        public static event Action<TrekRandomEventSO> TrekEventDisasters;
        private int stardate;
        public int PowerPerPlant = 10; // used by GalaxyMenuUIController
       

        public StarSysController(string name)
        {
            StarSysData = new StarSysData(name);
        }
        private void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            canvasToolTip.worldCamera = galaxyEventCamera;
            canvasYourStarSysUI.worldCamera = galaxyEventCamera;
            TimeManager.Instance.OnRandomSpecialEvent = DoDisaster;
        }
        private void Update()
        {
            stardate = TimeManager.Instance.currentStardate;
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
                    if (FleetUIController.Instance.MouseClickSetsDestination == false) // not while FleetUIController was looking for a destination
                    {
                        YourStarSysUIManager.Instance.LoadStarSysUI(gameObject);
                    }
                    else
                    {
                        NewDestination(hitObject); // one of our systems hit as destination
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
            //this.CanvasDestination.gameObject.SetActive(true);
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
        public void BuildClick() // open build list UI
        {
            // Do shit
            // StarSysManager.Instance.GetInstanceOfFacility();
        }
    }
}
