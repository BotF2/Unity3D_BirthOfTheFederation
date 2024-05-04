using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;


namespace Assets.Core
{

    public class FleetController : MonoBehaviour
    { 
        //Fields
        public FleetData fleetData;
        public GameObject fleetUIRoot;
        //public GameObject canvasFleetUIButton;
        public GameObject buttonLoadFleetUI;
        //[SerializeField]
        //private Slider warpSlider;
        //[SerializeField]
        //private TextMeshProUGUI warpSliderText;
        //[SerializeField]
        //private float maxSliderValue = 100.0f;
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
        public GameObject fleetDropLine;
        public LineRenderer lineRenderer;
        [SerializeField]
        private TMP_Text dropdownSysText;
        [SerializeField]
        private TMP_Text dropdownShipText;
        [SerializeField]
        private TMP_Text sysDestination;

        public string Name;

        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas openFleetUIButtonCanvas;
        //private GameObject FirstCocusItem;
        //[SerializeField]
        //private Canvas rootUICanvas;
        //private Stack<Canvas> stack = new Stack<Canvas>();


        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
            var buttons = GetComponentsInChildren<Button>();
            foreach (var item in buttons)
            {
                if (item.name == "Button Load FleetUI")
                    buttonLoadFleetUI = item.gameObject;
            }
            //canvasFleetUIButton = GetComponentInChildren<Canvas>().gameObject;
            //warpSlider = GetComponentInChildren<Slider>();
            //warpSlider.onValueChanged.AddListener((v) => { warpSliderText.text = v.ToString("0.00"); });
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
            openFleetUIButtonCanvas.worldCamera = galaxyEventCamera;
            //lineRenderer = FleetManager.instance.Get
            // ToDo while FleetUIManager will have a list of star system as destination the FleetController will need to 
            // send a list of fleets it can see as a destination
            //systemsList = StarSysManager.instance.StarSysDataList;
            
            //var destDropdown = destinationDropdownGO.GetComponent<TMP_Dropdown>();
            //destDropdown.options.Clear();
            //List<string> sysList = new List<string>();
            //// fill destDropdown sys sysList
            //foreach ( var item in systemsList )
            //{
            //    destDropdown.options.Add(new TMP_Dropdown.OptionData() { text= item.SysName});
            //}
            //DropdownItemSelected(destDropdown);
            //destDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(destDropdown); }) ;

            // placeholder ship list
            ShipData shipData1 = new ShipData();
            shipData1.shipName = "USS Trump";
            ShipData shipData2 = new ShipData();
            shipData2.shipName = "USS John McCain";
            shipList = new List<ShipData>() { shipData1, shipData2 };
            
            //var shipDropdown = shipDropdownGO.GetComponent<TMP_Dropdown>();
            //shipDropdown.options.Clear();
            
            //// fill destDropdown sys sysList
            //foreach (var item in shipList)
            //{
            //    shipDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.shipName });
            //}
            //DropdownItemSelected(shipDropdown);
            //shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
            Name = fleetData.CivShortName + " Fleet " + fleetData.Name;
        }

        void DropdownItemSelected(TMP_Dropdown dropdown)
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
        void Update()
        {
            if (shipList != null && deltaShipList) // && check for change in the ship list or )
            {
                maxWarpFactor = 10;
                for (int i = 0; i < shipList.Count; i++)
                {
                    if (shipList[i].maxWarpFactor < maxWarpFactor)
                        maxWarpFactor = shipList[i].maxWarpFactor;
                }
                deltaShipList = false;
            }
 
        }
        private void FixedUpdate()
        {
            if (WarpFactor > 0 && Destination != null)
            {
                Vector3 destinationPosition = Destination.transform.position;
                Vector3 currentPosition = transform.position;
                float distance = Vector3.Distance(destinationPosition, currentPosition);
                if (distance > dropOutOfWarpDistance)
                {
                    Vector3 travelVector = destinationPosition - transform.position;
                    travelVector.Normalize();
                    rb.MovePosition(currentPosition + (travelVector * WarpFactor * fudgeFactor * Time.deltaTime));
                }
                Vector3[] linePoints = new Vector3[] { transform.position,
                new Vector3(transform.position.x, -6f, transform.position.z) };
                lineRenderer.SetPositions(linePoints);
            }
        }
        //public void DeltaFleetWarpFactor(float warpFactor)
        //{
        //    WarpFactor = warpFactor;
        //}
        //public void WarpSliderChange(float value)
        //{
        //    float localValue = value* maxSliderValue;
        //    warpSliderText.text = localValue.ToString("0.00");
        //    WarpFactor = value;
        //}
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
            FleetUIManager.instance.LoadFleetUI(this);
                //fleetData.CivOwnerEnum.ToString() + " Fleet " + Name, destinationDropdownGO, shipList);
        }
        //public void UnLoadFleetUI()
        //{
        //    fleetUIRoot.SetActive(false);
        //}

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
