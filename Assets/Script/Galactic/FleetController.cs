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
    public enum FleetState { FleetCombat, FleetDipolmacy, FleetInSystem, FleetsTogether, FleetStationary, FleetWarp}
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
        private FleetState fleetState;
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
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;
            FleetData.CurrentWarpFactor = 0f;
            Name = FleetData.CivShortName + " Fleet " + FleetData.Name;
            GameObject Target = new GameObject("FirstDestination4FleetController");
            Target.tag = "DestroyTemp";
            Transform TheTarget = Target.transform;
            TheTarget.position = new Vector3(0, 0, 0);
            FleetData.Destination = Target;

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
                        AllStop();
                        break;
                    }
                case FleetState.FleetStationary:
                    {
                        AllStop();
                        break; 
                    }
                case FleetState.FleetWarp:
                    { 
                        break;
                    }
                case FleetState.FleetsTogether:
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
             
            if (FleetData.Destination != null && FleetData.CurrentWarpFactor > 0f)
            {
                fleetState = FleetState.FleetWarp;
                MoveToDesitinationGO();
            }
        }

        public Rigidbody GetRigidbody() { return rb; }

        private void OnCollisionEnter(Collision collision)
        {
            //currentState.OnCollisionEnter(this, collision);
            collision.gameObject.SetActive(true);
            var controllerFleet = collision.gameObject.GetComponent<FleetController>();
            if (controllerFleet != null)
            {
                CivManager.instance.Diplomacy(this.fleetData.OurCivController, controllerFleet.fleetData.OurCivController);
                // is it our fleet or not? Diplomacy or manage fleets or keep going?
                if (controllerFleet.gameObject == this.FleetData.Destination)
                {
                    /// use fleet enum state
                    //this.FleetData.Destination = null;
                    //this.FleetData.war
                    //fleetState = FleetState.FleetInSystem;
                }

            }
            var controllerStarSys = collision.gameObject.GetComponent<StarSysController>();
            if (controllerStarSys != null) 
            {
                CivManager.instance.Diplomacy(this.fleetData.OurCivController,
                    CivManager.instance.GetCivControllerByEnum(controllerStarSys.StarSysData.CurrentOwner));
                // if not destination no change, keep going
                if (controllerStarSys.StarSysData.SysGameObject == this.FleetData.Destination)
                {
                    /// use fleet enum state
                    this.FleetData.Destination = null;
                    this.FleetData.CurrentWarpFactor = 0;
                    fleetState = FleetState.FleetInSystem;
                }
            }
            var controllerPlayerTarget = collision.gameObject.GetComponent<PlayerDefinedTargetController>();
            if (controllerPlayerTarget != null)
            {
                // current warp factor = 0, destination = null, ?build something here? or patrol here?
            }
        }
  
        void PopulateShipDropdown()
        {
            shipDropdownGO = GameObject.FindGameObjectWithTag("DropdownShipsGO");
            shipDropdown = shipDropdownGO.GetComponent<TMP_Dropdown>();
            if (shipDropdown == null || shipDropdownOptions == null)
            {
                return;
            }

            shipDropdown.options.Clear();
            for (int i = 0; i < ShipControllerList.Count; i++)
            {
                if (ShipControllerList[i] == null)
                {
                    ShipControllerList.RemoveAt(i);
                }
            }

            foreach (var item in ShipControllerList)
            {
                shipDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.ShipData.ShipName.Replace("(CLONE)", string.Empty) });
            }
        }

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
                    PopulateShipDropdown();
                }
            }

        }
        void OnTriggerEnter(Collider collider)
        {
            FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
            if (fleetController != null) // it is a FleetController and not a StarSystem or other fleetController
            {
                OnFleetEncounteredFleet(fleetController);
                Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
            }
            StarSysController starSysController = collider.gameObject.GetComponent<StarSysController>();
            if (starSysController != null)             {
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
            //FleetManager.instance.
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) ?first contatact > what kind of hail?
            //4) ?combat
            //5) ?move ships in and out of fleets
        }
        public void OnFleetEncounteredStarSys(StarSysController starSysController)
        {
            //FleetManager.instance.
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) ?first contatact > what kind of hail, diplomacy vs uninhabited ?colonize vs terraform a rock vs do fleet busness in our system
            //4) ?combat vs diplomacy and or traid...
        }
        public void OnFleetEncounteredPlayerDefinedTarget(PlayerDefinedTargetController playerTargetController)
        {
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
        public void AddFleetController(FleetController controller)
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
