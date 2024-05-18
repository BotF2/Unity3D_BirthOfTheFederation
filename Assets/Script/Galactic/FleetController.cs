using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting;


namespace Assets.Core
{

    public class FleetController : MonoBehaviour
    { 
        //Fields
        public FleetData fleetData;
        private ShipData shipData1;
        private ShipData shipData2;
        public List<StarSysData> systemsList;
        public List<ShipData> shipList;
        private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
        public Transform Destination;
        private float WarpFactor = 9;
        private float fudgeFactor = 0.05f; // so we see warp factor as in Star Trek but move in game space
        private float dropOutOfWarpDistance = 0.5f;
        private float maxWarpFactor;
       // public bool warpTravel = false;// do we need this warp factor >0
        public Rigidbody rb;
        public GameObject destinationDropdownGO;
        public GameObject shipDropdownGO;
        //public GameObject fleetDropLine;
        public LineRenderer lineRenderer;
        [SerializeField]
        private TMP_Text dropdownSysText;
        [SerializeField]
        private TMP_Text dropdownShipText;
        [SerializeField]
        private TMP_Text ourDestination;

        public string Name;

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
            shipData1.shipName = "USS Trump";

            shipData2 = gameObject.AddComponent<ShipData>();
            shipData2.shipName = "USS John McCain";
            shipList = new List<ShipData>() { shipData1, shipData2 };
            
            Name = fleetData.CivShortName + " Fleet " + fleetData.Name;
        }

        public void DropdownItemSelected(TMP_Dropdown dropdown)
        {
            int index = dropdown.value;
            if (dropdown.name == "Dropdown Systems")
            {
                dropdownSysText.text = dropdown.options[index].text;
                var sys = systemsList[index];
                Destination = sys.SysTransform;
            }
            else if(dropdown.name == "Dropdown Ships")
            {
                dropdownShipText.text = dropdown.options[index].text;
                var ship = shipList[index]; // Can we or should we do stuff here??

            }
        }

        private void FixedUpdate()
        {
            //ToDo **** need physics movement such that fleet pass around colliders that are not the destination
            //if (WarpFactor > 0 && Destination != null)
            //{
            //    Vector3 destinationPosition = Destination.transform.position;
            //    Vector3 currentPosition = transform.position;
            //    float distance = Vector3.Distance(destinationPosition, currentPosition);
            //    if (distance > dropOutOfWarpDistance)
            //    {
            //        Vector3 travelVector = destinationPosition - transform.position;
            //        travelVector.Normalize();
            //        rb.MovePosition(currentPosition + (travelVector * WarpFactor * fudgeFactor * Time.deltaTime));
            //    }
            //    Vector3[] linePoints = new Vector3[] { transform.position,
            //    new Vector3(transform.position.x, -6f, transform.position.z) };
            //    lineRenderer.SetPositions(linePoints);
            //}
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
        public void DropdownSystems(int index)
        {
            foreach (var system in systemsList) 
            {
            
            }
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


        void OnTriggerEnter(Collider collider)
        {

            FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
            if (fleetController != null)
            {
                // fleet controller to get civ sysList we know
                Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
            }
            
        }

    }

}
