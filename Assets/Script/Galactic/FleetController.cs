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
        private FleetData fleetData;
        public FleetData FleetData { get { return fleetData; } set { fleetData = value; } }
        public string Name;
        //private ShipData shipData1;
        //private ShipData shipData2;
        public List<ShipData> shipList;
        public List<FleetController> FleetConsWeHave;
        List<StarSysController> StarSystemsWeHave;
        List<PlayerDefinedTargetController> PlayerDefinedTargetControllersWeHave;
        private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
        public Transform Destination;
        private float maxWarpFactor;
        private float currentWarpFactor = 4;
        private float fudgeFactor = 0.05f; // so we see warp factor as in Star Trek but move in game space
        Rigidbody rb;
        public GameObject destinationDropdownGO;
        GameObject fleetDropdownGO;

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
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;

            // temp just to see list in UI
            //shipData1 = gameObject.AddComponent<ShipData>();
            //shipData1.ShipName = "USS Trump";
            //shipData2 = gameObject.AddComponent<ShipData>();
            //shipData2.ShipName = "USS John McCain";
            //fleetData.shipList = new List<ShipData>() { shipData1, shipData2 }; 
            
            Name = fleetData.CivShortName + " Fleet " + fleetData.Name;
            GameObject Target = new GameObject("MyGameObject");
            Transform TheTarget = Target.transform;
            TheTarget.position = new Vector3(0, 0, 0);
            Destination = Target.transform;
        }
        private void FixedUpdate()
        {
            if (Destination != null)
            {
                Vector3 nextPosition = Vector3.MoveTowards(rb.position, Destination.position, currentWarpFactor * Time.fixedDeltaTime);
                rb.MovePosition(nextPosition);
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

        public void UpdateWarpFactor(int delta)
        {
            fleetData.CurrentWarpFactor += delta;
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
