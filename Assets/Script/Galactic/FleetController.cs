using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;

namespace Assets.Core
{
    public enum FleetState { FleetCombat, FleetDipolmacy, FleetInSystem, FleetsInRendezvous, FleetStationary, FleetAtWarp}
    public class FleetController : MonoBehaviour
    {
        //Fields
        private FleetData fleetData;
        public FleetData FleetData { get { return fleetData; } set { fleetData = value; } }
        public string Name;
        public List<ShipController> ShipControllerList;
        //public List<FleetController> FleetContollersWeHave;
        //List<StarSysController> StarSystemsWeHave;
        //List<PlayerDefinedTargetController> PlayerDefinedTargetControllersWeHave;
        private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
        public FleetState fleetState;
        public bool isArrived =false;
        [SerializeField]
        private float maxWarpFactor = 9.8f;
        private float fudgeFactor = 1f;
        private float dropOutOfWarpDistance = 0.5f; // stop, but should be destination collider?
        private Rigidbody rb;
        public DropLineMovable dropLine;
        GameObject fleetDropdownGO;
        public GameObject destinationDropdownGO; // UI dropdown
        public TMP_Dropdown destinationDropdown;
        public string SelectedDestination; // save destination name for FleetUI, start null
        public GameObject shipDropdownGO;
        public TMP_Dropdown shipDropdown;
        public List<string> shipDropdownOptions;

        [SerializeField]
        private TMP_Text ourDestination;
        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas FleetUICanvas;
        [SerializeField]
        private Canvas CanvasToolTip;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;
            FleetData.CurrentWarpFactor = 0f;
            Name = FleetData.CivShortName + " Fleet " + FleetData.Name;
            fleetState = FleetState.FleetStationary;

        }
        void Update()
        {
            switch (fleetState) 
            {
                case FleetState.FleetInSystem:
                    {
                        // add to system fleet list
                        AllStop();
                        break;
                    }
                case FleetState.FleetCombat:
                    {
                        AllStop();
                        break;
                    }
                case FleetState.FleetDipolmacy:
                    {
                        //AllStop();
                        break;
                    }
                case FleetState.FleetStationary:
                    {
                        break; 
                    }
                case FleetState.FleetAtWarp:
                    { 
                        break;
                    }
                case FleetState.FleetsInRendezvous:
                    {
                        //if(this.FleetData.Destination == )
                        AllStop();
                        break;
                    }
            }
        }
        private void AllStop()
        {
            this.FleetData.Destination = null;
            this.FleetData.CurrentWarpFactor = 0f;
        }
        private void FixedUpdate()
        {
            if (FleetData.CivEnum != CivEnum.ZZUNINHABITED10)
            {
                if (FleetData.Destination != null && FleetData.CurrentWarpFactor > 0f)
                {
                    fleetState = FleetState.FleetAtWarp;
                    MoveToDesitinationGO();
                }
            }
        }

        public Rigidbody GetRigidbody() { return rb; }

        private void OnMouseDown()
        {
            //string goName;
            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                //goName = hitObject.name;
                if (hitObject == gameObject)
                {
                    FleetUIManager.instance.LoadFleetUI(gameObject);
                }
            }

        }
        void OnTriggerEnter(Collider collider) // Not using OnCollisionEnter....
        {
            FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
            if (fleetController != null) // it is a FleetController and not a StarSystem or other fleetController
            {
                OnFleetEncounteredFleet(fleetController);
                Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
            }
            StarSysController starSysController = collider.gameObject.GetComponent<StarSysController>();
            if (starSysController != null)
            {
                OnFleetEncounteredStarSys(starSysController);
            }
            PlayerDefinedTargetController playerTargetController = collider.gameObject.GetComponent<PlayerDefinedTargetController>();
            if (playerTargetController != null)
            {
                OnFleetEncounteredPlayerDefinedTarget(playerTargetController);
            }
        }
        public void OnFleetEncounteredFleet(FleetController fleetController)
        {
            if (fleetController.FleetData.OurCivController != this.FleetData.OurCivController)
                CivManager.instance.Diplomacy(this.fleetData.OurCivController, fleetController.fleetData.OurCivController);
            else
            {
                if (this.FleetData.ShipsList.Count >= fleetController.FleetData.ShipsList.Count)
                {
                    this.FleetData.FleetGroupControllers.Add(fleetController);
                    //ToDo: manage to fleets in conjoined for ship exchange and what to do with original fleets, two or more
                }
            }

            // is it our fleet or not? Diplomacy or manage fleets or keep going?
            if (fleetController.gameObject == this.FleetData.Destination)
            {
                /// use fleet enum state
                //this.FleetData.Destination = null;
                //this.FleetData.war
                //fleetState = FleetState.FleetInSystem;
            }
            //FleetManager.instance.
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) ?first contatact > what kind of hail?
            //4) ?combat
            //5) ?move ships in and out of fleets
        }
        public void OnFleetEncounteredStarSys(StarSysController starSysController)
        {
            //????StarSysManager.instance.
            FleetManager.instance.GetFleetGroupInSystemForShipTransfer(starSysController);
            CivManager.instance.Diplomacy(this.fleetData.OurCivController, starSysController.StarSysData.CurrentCivController);
            // is it our fleet or not? Diplomacy or manage fleets or keep going?
            if (starSysController.gameObject == this.FleetData.Destination)
            {
                /// use fleet enum state
                //this.FleetData.Destination = null;
                //this.FleetData.war
                //fleetState = FleetState.FleetInSystem;
            }
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) ?first contatact > what kind of hail, diplomacy vs uninhabited ?colonize vs terraform a rock vs do fleet busness in our system
            //4) ?combat vs diplomacy and or traid...
        }
        public void OnFleetEncounteredPlayerDefinedTarget(PlayerDefinedTargetController playerTargetController)
        {
            //????PlayerDefinedTargetManager.instance.
            //FleetManager.instance.
            //1) you get the FleetController of the new fleet GO
            //2) ?build a deep space starbase vs a partol point for travel

        }
        public void SetWarpSpeed(float newSpeed)
        {
            if (newSpeed <= maxWarpFactor)
            {
                FleetData.CurrentWarpFactor = newSpeed;
            }
        }
        public void SetDestination(GameObject newDestination)
        {
            this.FleetData.Destination = newDestination;
        }
        void MoveToDesitinationGO()
        {
            Vector3 direction = (this.FleetData.Destination.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, this.FleetData.Destination.transform.position);

            if (distance > dropOutOfWarpDistance)
            {
                Vector3 nextPosition = Vector3.MoveTowards(rb.position, FleetData.Destination.transform.position,
                    FleetData.CurrentWarpFactor * fudgeFactor * Time.fixedDeltaTime);
                rb.MovePosition(nextPosition); // kinematic with physics movement
            }
            else
            {
                rb.velocity = Vector3.zero;
                OnArrivedAtDestination();
            }
            Vector3 galaxyPlanePoint = new Vector3(rb.position.x, -60f, rb.position.z);
            Vector3[] points = { rb.position, galaxyPlanePoint };
            dropLine.SetUpLine(points);
        }
        void OnArrivedAtDestination()
        {
            if (isArrived == false)
            {
                isArrived = true;
                // Logic to handle what happens when the fleet arrives at the destination
                Debug.Log("Arrived at destination: " + this.FleetData.Destination.name);
                // Example: Stop the fleet, update UI, trigger events, etc.
            }
        }
        void AddToShipList(ShipController shipController)
        {
            foreach (var ShipData in this.FleetData.GetShipList())
                FleetData.AddToShipList(shipController);
            deltaShipList = true;
        }
        void RemoveFromShipList(ShipController shipController)
        {
            this.FleetData.RemoveFromShipList(shipController);
            deltaShipList = true;
        }

        //public void UpdateWarpFactor(float delta)
        //{
        //    FleetData.CurrentWarpFactor += delta;
        //    FleetData.CurrentWarpFactor += delta;
        //}
        public void AddFleetController(FleetController controller) // do we need this?
        {
            if (!FleetManager.instance.ManagersFleetControllerList.Contains(controller)) 
                FleetManager.instance.ManagersFleetControllerList.Add(controller);
        }
        public void RemoveFleetController(FleetController controller)
        {
            if (FleetManager.instance.ManagersFleetControllerList.Contains(controller))
                FleetManager.instance.ManagersFleetControllerList.Remove(controller);
        }
    }

}
