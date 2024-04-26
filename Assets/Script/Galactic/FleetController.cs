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
        public GameObject canvasFleetUI;

        public GameObject buttonLoadFleetUI;

        public List<StarSysData> systemsList;
        public List<ShipData> shipList;
        public Transform targetTrans;
        public float warpSpeed = 0f;
        public bool warpTravel = false;

        public GameObject sysDropdownGO;
        public GameObject shipDropdownGO;
        [SerializeField]
        private TMP_Text dropdownSysText;
        [SerializeField]
        private TMP_Text dropdownShipText;
        private TMP_Text sysDestination;
       
        private Camera galaxyEventCamera;
        [SerializeField]
        private Canvas openFleetUIButtonCanvas;
       

        private void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
            openFleetUIButtonCanvas.worldCamera = galaxyEventCamera;

            systemsList = StarSysManager.instance.StarSysDataList;
            
            var sysDropdown = sysDropdownGO.GetComponent<TMP_Dropdown>();
            sysDropdown.options.Clear();
            List<string> sysList = new List<string>();
            // fill sysDropdown sys sysList
            foreach ( var item in systemsList )
            {
                sysDropdown.options.Add(new TMP_Dropdown.OptionData() { text= item.SysName});
            }
            DropdownItemSelected(sysDropdown);
            sysDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(sysDropdown); }) ;

            // placeholder ship list
            ShipData shipData1 = new ShipData();
            shipData1.shipName = "USS Trump";
            ShipData shipData2 = new ShipData();
            shipData2.shipName = "USS John McCain";
            shipList = new List<ShipData>() { shipData1, shipData2 };
            
            var shipDropdown = shipDropdownGO.GetComponent<TMP_Dropdown>();
            shipDropdown.options.Clear();
            
            // fill sysDropdown sys sysList
            foreach (var item in shipList)
            {
                shipDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.shipName });
            }
            DropdownItemSelected(shipDropdown);
            shipDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(shipDropdown); });
        }
        void DropdownItemSelected(TMP_Dropdown dropdown)
        {
            int index = dropdown.value;
            if (dropdown.name == "Dropdown Systems")
            {
                dropdownSysText.text = dropdown.options[index].text;
                var sys = systemsList[index];
                targetTrans = sys.SysTransform;
            }
            else if(dropdown.name == "Dropdown Ships")
            {
                dropdownShipText.text = dropdown.options[index].text;
                var ship = shipList[index]; // Can we or should we do stuff here??

            }
        }
        void Update()
        {
            if (fleetData != null)
            {
                fleetData.Position = transform.position;
            }
 
        }
        public void DropdownSystems(int index)
        {
            foreach (var system in systemsList) 
            {
            
            }

        }
        public void MoveFleet()
        {
            //if (moveFleet != null)
            //    {  moveFleet(); }
        }
        public void MoveTowardsTarget()
        {
            if (targetTrans != null)
            {
                // Calculate the direction towards the target
                Vector3 direction = (targetTrans.position - transform.position).normalized;

                // Move the object towards the target
                transform.position += direction * Time.deltaTime * warpSpeed;
            }
        }
        public void UpdateWarpFactor(int delta)
        {
            fleetData.WarpFactor += delta;
        }
        public void LoadFleetUI()
        {
             canvasFleetUI.SetActive(true);
        }
        public void UnLoadFleetUI()
        {
            canvasFleetUI.SetActive(false);
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
