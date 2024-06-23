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

    public class FleetController : MonoBehaviour
    {
        //Fields
        private FleetData fleetData;
        public FleetData FleetData { get { return fleetData; } set { fleetData = value; } }
        public string Name;
        public List<ShipController> ShipControllerList;
        public List<FleetController> FleetConsWeHave;
        List<StarSysController> StarSystemsWeHave;
        List<PlayerDefinedTargetController> PlayerDefinedTargetControllersWeHave;
        private bool deltaShipList = false; //??? do I need this or the shipdropdown listener

        Rigidbody rb;
        public DropLineMovable dropLine;
        GameObject fleetDropdownGO;
        public GameObject destinationDropdownGO; // UI dropdown
        public TMP_Dropdown destinationDropdown;
        public string SelectedDestination; // save destination name for FleetUI
        public GameObject shipDropdownGO;
        public TMP_Dropdown shipDropdown;
        public List<string> shipDropdownOptions;

        private LineRenderer lineRenderer;
        [SerializeField]
        private TMP_Text dropdownSysText;
        [SerializeField]
        private TMP_Text dropdownShipText;
        [SerializeField]
        private TMP_Text ourDestination;
        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas FleetUICanvas;
        [SerializeField]
        private Canvas CanvasToolTip;

        protected FleetBaseState currentState;
        public FleetInSystemState inSystemState = new FleetInSystemState();
        public FleetStationaryState stationaryState = new FleetStationaryState();
        public FleetWarpState warpState = new FleetWarpState();
        public FleetCombatState combatState = new FleetCombatState();
        public FleetDiplomacyState diplomacyState = new FleetDiplomacyState();
        public FleetManagingState managingState = new FleetManagingState();

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;
            FleetData.CurrentWarpFactor = 0f;
            Name = FleetData.CivShortName + " Fleet " + FleetData.Name;
            GameObject Target = new GameObject("MyGameObject");
            Transform TheTarget = Target.transform;
            TheTarget.position = new Vector3(0, 0, 0);
            FleetData.Destination = Target.transform;

            currentState = stationaryState;
            currentState.EnterState(this);
        }
        void Update()
        {
            currentState.UpdateState(this); // not working
        }
        private void FixedUpdate()
        {
            //currentState.UpdateState(this);
            if (FleetData.Destination != null && FleetData.CurrentWarpFactor > 0f)
            {
                currentState = warpState;
                Vector3 nextPosition = Vector3.MoveTowards(rb.position, FleetData.Destination.position,
                    FleetData.CurrentWarpFactor * FleetData.fudgeFactor * Time.fixedDeltaTime);
                rb.MovePosition(nextPosition);
                Vector3 galaxyPlanePoint = new Vector3(rb.position.x, -60f, rb.position.z);
                Vector3[] points = { rb.position, galaxyPlanePoint };
                dropLine.SetUpLine(points);
            }
        }

        public Rigidbody GetRigidbody() { return rb; }

        private void OnCollisionEnter(Collision collision)
        {
            currentState.OnCollisionEnter(this, collision);
            collision.gameObject.SetActive(true);
            var controllerTypeFleet = collision.gameObject.GetComponent<FleetController>();
            if (controllerTypeFleet != null)
            {
                // is it our fleet or not? Diplomacy or manage fleets or keep going?
            }
            var controllerTypeStarSys = collision.gameObject.GetComponent<StarSysController>();
            if (controllerTypeStarSys != null) 
            {
                // if not destination no change, keep going
                // if destination change to InSystem state, current warp factor = 0, destination = null
                if (controllerTypeStarSys.StarSysData.SysTransform == this.FleetData.Destination)
                {
                    this.FleetData.Destination = null;
                    this.FleetData.CurrentWarpFactor = 0;
                    currentState = inSystemState;
                }
            }
            var controllerTypePlayerTarget = collision.gameObject.GetComponent<PlayerDefinedTargetController>();
            if (controllerTypePlayerTarget != null)
            {
                // current warp factor = 0, destination = null, ?build something here? or patrol here?
            }
        }
  

        public void SwitchState(FleetBaseState baseState)
        {
            currentState.ExitState(this);
            currentState = baseState;
            baseState.EnterState(this);
        }
        void GetDestinationDropdown()
        {
            destinationDropdown = destinationDropdownGO.GetComponent<TMP_Dropdown>(); // fleetUI destination dropdown
            if (destinationDropdown == null) 
            {
                return;
            }
            //DropdownItemSelected(destinationDropdown);
            //destinationDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destinationDropdown); });

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
                    //stationaryState = new FleetStationaryState(hitObject);
                    //warpState = new FleetWarpState(hitObject, this.FleetData.Destination, rb, this.FleetData.CurrentWarpFactor);
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
        void MoveToDesitinationGO()
        {

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

        public void UpdateWarpFactor(float delta)
        {
            FleetData.CurrentWarpFactor += delta;
            FleetData.CurrentWarpFactor += delta;
        }
        public void AddFleetController(FleetController controller)
        {
            if (controller.FleetData.CivOwnerEnum == this.FleetData.CivOwnerEnum) 
                FleetConsWeHave.Add(controller);
        }
        public void RemoveFleetController(FleetController controller)
        {
            if (controller.FleetData.CivOwnerEnum == this.FleetData.CivOwnerEnum && FleetConsWeHave.Contains(controller))
                FleetConsWeHave.Remove(controller);
        }
    }

}
