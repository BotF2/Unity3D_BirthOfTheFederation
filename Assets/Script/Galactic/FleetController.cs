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
        public List<StarSysData> SystemsList;
        public List<ShipController> ShipControllerList;
        public List<FleetController> FleetConsWeHave;
        List<StarSysController> StarSystemsWeHave;
        List<PlayerDefinedTargetController> PlayerDefinedTargetControllersWeHave;
        private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
        public Transform Destination;
        private float maxWarpFactor;
        private float currentWarpFactor = 0;
        private float fudgeFactor = 0.05f; // so we see warp factor as in Star Trek but move in game space
        Rigidbody rb;
        //GameObject fleetDropdownGO;
        public GameObject destinationDropdownGO;
        public GameObject sysDropdownGO;
        public TMP_Dropdown sysDropdown;
        public List<string> sysDropdownOptions;
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
        public FleetAllStopState allStopState = new FleetAllStopState();
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
            currentWarpFactor = 0f;
            Name = fleetData.CivShortName + " Fleet " + fleetData.Name;
            GameObject Target = new GameObject("MyGameObject");
            Transform TheTarget = Target.transform;
            TheTarget.position = new Vector3(0, 0, 0);
            Destination = Target.transform;

            SystemsList = StarSysManager.instance.StarSysDataList;
            currentState = allStopState;
            currentState.EnterState(this);
        }
        void Update()
        {
            currentState.UpdateState(this);
            ButtonInputs();
        }
        private void FixedUpdate()
        {
            if (Destination != null && currentWarpFactor > 0)
            {
                Vector3 nextPosition = Vector3.MoveTowards(rb.position, Destination.position, currentWarpFactor * Time.fixedDeltaTime);
                rb.MovePosition(nextPosition);
            }
        }
        public Rigidbody GetRigidbody() { return rb; }
        void ButtonInputs()
        {
            if (Input.GetKey("m"))
            {
                this.SwitchState(warpState);
            }
        }
            private void OnCollisionEnter(Collision collision)
        {
            currentState.OnCollisionEnter(this, collision);

        }
  

        public void SwitchState(FleetBaseState baseState)
        {
            currentState = baseState;
            baseState.EnterState(this);
        }
        void PopulateDestinationDropdown()
        {
            sysDropdownGO = GameObject.FindGameObjectWithTag("DropdownDestinationsGO");
            sysDropdown = sysDropdownGO.GetComponent<TMP_Dropdown>();
            if (sysDropdown == null || sysDropdownOptions == null)
            {
                return;
            }
            sysDropdown.options.Clear();
            // fill sysDropdown sys sysList
            foreach (var item in SystemsList)
            {
                sysDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.SysName });
            }
            //DropdownItemSelected(sysDropdown);
            //sysDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(sysDropdown); });

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
            DropdownItemSelected(shipDropdown);// not working, null ref
            shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
        }
        void DropdownItemSelected(TMP_Dropdown dropdown)
        {
            int index = dropdown.value;
            if (dropdown.name == "Dropdown _destination")
            {
                dropdownSysText.text = dropdown.options[index].text;
                var sys = SystemsList[index];
                Destination = sys.SysTransform;
            }
            else if (dropdown.name == "Dropdown Ships")
            {
                //dropdownShipText.text = dropdown.options[index].text;
                //var ship = ShipControllerList[index]; // Can we or should we do stuff here??

            }
        }
        private void OnMouseDown()
        {
            string goName;
            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                goName = hitObject.name;
            }
            FleetUIManager.instance.LoadFleetUI(gameObject);
            PopulateShipDropdown();
            PopulateDestinationDropdown();

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
            foreach (var ShipData in this.fleetData.GetShipList())
                fleetData.AddToShipList(shipController);
            deltaShipList = true;
        }
        void RemoveFromShipList(ShipController shipController)
        {
            this.fleetData.RemoveFromShipList(shipController);
            deltaShipList = true;
        }

        public void UpdateWarpFactor(float delta)
        {
            fleetData.CurrentWarpFactor += delta;
            this.currentWarpFactor += delta;
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
