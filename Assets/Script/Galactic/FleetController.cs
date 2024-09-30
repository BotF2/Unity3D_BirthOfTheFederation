using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;
using JetBrains.Annotations;
using FischlWorks_FogWar;

namespace Assets.Core
{    /// <summary>
     /// Controlling fleet movement and interactions while the matching FeetData class
     /// holds key info on status and for save game
     /// </summary>
    public enum FleetState { FleetCombat, FleetDipolmacy, FleetInSystem, FleetsInRendezvous, FleetStationary, FleetAtWarp}
    public class FleetController : MonoBehaviour
    {
        //Fields
        private FleetData fleetData;
        public FleetData FleetData { get { return fleetData; } set { fleetData = value; } }
        public string Name;
        public FleetState FleetState;
        public bool IsArrived = false;

        private float warpFudgeFactor = 10f;
  
        private Rigidbody rb;
        public DropLineMovable DropLine;
 
        public Canvas OurSelectedMarkerCanvas;

        [SerializeField]
        private List<string> shipDropdownOptions;
        private Camera galaxyEventCamera;
        public Canvas FleetUICanvas { get; private set; }
        public Canvas CanvasToolTip;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;

            OurSelectedMarkerCanvas.gameObject.SetActive(false);
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;
            FleetData.CurrentWarpFactor = 9.8f;
            foreach (var shipCon in this.FleetData.ShipsList)
            {
                if (shipCon.ShipData.maxWarpFactor < this.FleetData.MaxWarpFactor)
                { this.FleetData.MaxWarpFactor = shipCon.ShipData.maxWarpFactor;}
            }
            Name = FleetData.CivShortName + " Fleet " + FleetData.Name;
            FleetState = FleetState.FleetStationary;
        }
        void Update()
        {

            switch (FleetState)
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
                    FleetState = FleetState.FleetAtWarp;
                    MoveToDesitinationGO();
                }
            }
        }

        public Rigidbody GetRigidbody() { return rb; }

        private void OnMouseDown() 
        {
            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                GameObject hitObject = hit.collider.gameObject;
                // What a fleet controller does with a hit

                if (this.FleetData.CivEnum == CivManager.Instance.LocalPlayerCivEnum)
                {
                    if (FleetUIManager.Instance.MouseClickSetsDestination == false)
                    {
                        FleetUIManager.Instance.LoadFleetUI(hitObject);
                    }
                }
                else if (FleetUIManager.Instance.MouseClickSetsDestination == true && this != FleetUIManager.Instance.controller)
                {
                    FleetUIManager.Instance.SetAsDestination(hitObject);
                    this.OurSelectedMarkerCanvas.gameObject.SetActive(true);
                    //MousePointerChanger.Instance.ResetCursor();
                    //MousePointerChanger.Instance.HaveGalaxyMapCursor = false;
                }             
            }
        }

        void OnTriggerEnter(Collider collider) // Not using OnCollisionEnter....
        {
            FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
            if (fleetController != null) // it is a FleetController and not a StarSystem or other f                                                                                                                                                      leetController
            {
                OnFleetEncounteredFleet(fleetController, collider.gameObject);
                Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
            }
            StarSysController starSysController = collider.gameObject.GetComponent<StarSysController>();
            if (starSysController != null)
            {
                OnFleetEncounteredStarSys(starSysController, collider.gameObject);
            }
            PlayerDefinedTargetController playerTargetController = collider.gameObject.GetComponent<PlayerDefinedTargetController>();
            if (playerTargetController != null)
            {
                OnFleetEncounteredPlayerDefinedTarget(playerTargetController);
            }
        }
        public void OnFleetEncounteredFleet(FleetController fleetController, GameObject hitGO)
        {
            if (fleetController.FleetData.OurCivController != this.FleetData.OurCivController)
                this.FleetData.OurCivController.Diplomacy(this.fleetData.OurCivController, fleetController.fleetData.OurCivController, hitGO);
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
                //FleetState = FleetState.FleetInSystem;
            }
            //FleetManager.Instance.
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) ?first contatact > what kind of hail?
            //4) ?combat
            //5) ?move ships in and out of fleets
        }
        public void OnFleetEncounteredStarSys(StarSysController starSysController, GameObject hitGO)
        {
            //????StarSysManager.Instance.
            FleetManager.Instance.GetFleetGroupInSystemForShipTransfer(starSysController);
            this.FleetData.OurCivController.Diplomacy(this.fleetData.OurCivController, starSysController.StarSysData.CurrentCivController, hitGO);
            // is it our fleet or not? Diplomacy or manage fleets or keep going?
            if (starSysController.gameObject == this.FleetData.Destination)
            {
                /// use fleet enum state
                //this.FleetData.Destination = null;
                //this.FleetData.war
                //FleetState = FleetState.FleetInSystem;
            }
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) ?first contatact > what kind of hail, diplomacy vs uninhabited ?colonize vs terraform a rock vs do fleet busness in our system
            //4) ?combat vs diplomacy and or traid...
        }
        public void OnFleetEncounteredPlayerDefinedTarget(PlayerDefinedTargetController playerTargetController)
        {
            //????PlayerDefinedTargetManager.Instance.
            //FleetManager.Instance.
            //1) you get the FleetController of the new fleet GO
            //2) ?build a deep space starbase vs a partol point for travel
        }

        public void SetWarpSpeed(float newSpeed)
        {
            if (newSpeed <= this.FleetData.MaxWarpFactor)
            {
                FleetData.CurrentWarpFactor = newSpeed;
                if (newSpeed > 0)
                    this.FleetState = FleetState.FleetAtWarp;
            }
            if (newSpeed == 0f)
                this.FleetState= FleetState.FleetStationary;

        }

        void MoveToDesitinationGO()
        {
            Vector3 direction = (this.FleetData.Destination.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, this.FleetData.Destination.transform.position);
            if (this.FleetData.CurrentWarpFactor > this.FleetData.MaxWarpFactor)
            {
                this.FleetData.CurrentWarpFactor = this.FleetData.MaxWarpFactor;
            }
            Vector3 nextPosition = Vector3.MoveTowards(rb.position, FleetData.Destination.transform.position,
            FleetData.CurrentWarpFactor * warpFudgeFactor * Time.fixedDeltaTime);
            rb.MovePosition(nextPosition); // kinematic with physics movement
            rb.velocity = Vector3.zero;
            // OnArrivedAtDestination();
            
            // update dropline
            Vector3 galaxyPlanePoint = new Vector3(rb.position.x, -60f, rb.position.z);
            Vector3[] points = { rb.position, galaxyPlanePoint };
            DropLine.SetUpLine(points);
        }
        void OnArrivedAtDestination()
        {
            if (IsArrived == false)
            {
                IsArrived = true;
                // Logic to handle what happens when the fleet arrives at the destination
                Debug.Log("Arrived at destination: " + this.FleetData.Destination.name);
                // Example: Stop the fleet, update UI, trigger events, etc.
            }
        }
        public void AddToShipList(ShipController shipController)
        {
            foreach (var ShipData in this.FleetData.GetShipList())
                FleetData.AddToShipList(shipController);
            UpdateMaxWarp();
        }
        public void RemoveFromShipList(ShipController shipController)
        {
            this.FleetData.RemoveFromShipList(shipController);
            UpdateMaxWarp();
        }
        public void UpdateMaxWarp()
        {
            float maxWarp = 9.8f;
            foreach (var shipController in fleetData.ShipsList)
            {
               if (shipController.ShipData.maxWarpFactor < maxWarp)
                {
                    maxWarp = shipController.ShipData.maxWarpFactor;
                }
            }
            fleetData.MaxWarpFactor = maxWarp;
        }
    }
}
