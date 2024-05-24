using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking.Types;


namespace Assets.Core
{

    public class FleetController : MonoBehaviour
    { 
        //Fields
        public FleetData fleetData;
        public string Name;
        private ShipData shipData1;
        private ShipData shipData2;
        public List<ShipData> shipList;
        public List<FleetController> FleetConsWeHave;
        public List<StarSysController> StarSystemsWeHave;
        private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
        public Transform Destination;
        private float WarpFactor = 9;
        private float fudgeFactor = 0.05f; // so we see warp factor as in Star Trek but move in game space
        private float dropOutOfWarpDistance = 0.5f;
        private float maxWarpFactor;
        public Rigidbody rb;
        public GameObject destinationDropdownGO;
        public GameObject fleetDropdownGO;

        public LineRenderer lineRenderer;
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

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;
            shipData1 = gameObject.AddComponent<ShipData>();
            shipData1.ShipName = "USS Trump";
            shipData2 = gameObject.AddComponent<ShipData>();
            shipData2.ShipName = "USS John McCain";
            shipList = new List<ShipData>() { shipData1, shipData2 };           
            Name = fleetData.CivShortName + " Fleet " + fleetData.Name;
        }

        private void FixedUpdate()
        {
            //MoveToDestinationGO; 
            //ToDo **** need physics movement such that fleet pass around colliders that are not the destination
            // pending time manager timing, Marc is working on this.
            
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
        }
        public void OnFleetEncounteredFleet(FleetController fleetController)
        {
            //FleetManager.instance.
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) you will need to apply different logics depending of the answer
        }
        public void OnFleetEncounteredStarSys(StarSysController starSysController)
        {
            //FleetManager.instance.
            //1) you get the FleetController of the new fleet GO
            //2) you ask your factionOwner (CivManager) if you already know the faction of the new fleet
            //3) you will need to apply different logics depending of the answer
        }
        void MoveToDesitinationGO()
        {

        }
        void AddToShipList(ShipData ship)
        {
            shipList.Add(ship);
            deltaShipList = true;
        }
        void RemoveFromShipList(ShipData ship)
        {
            shipList.Remove(ship);
            deltaShipList = true;
        }

        public void UpdateWarpFactor(int delta)
        {
            fleetData.MaxWarpFactor += delta;
        }
        public void LoadFleetUI()
        {
            //FleetUIManager.instance.LoadFleetUI(fleetData.CivShortName + " Fleet " + fleetData.FleetName);
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
        }

        public void AddFleetController(FleetController controller)
        {
            FleetConsWeHave.Add(controller);
        }
        public void RemoveFleetController(FleetController controller)
        {
            FleetConsWeHave.Remove(controller);
        }
    }

}
