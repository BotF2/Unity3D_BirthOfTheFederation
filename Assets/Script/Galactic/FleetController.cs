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
    public class FleetController : MonoBehaviour// IPointerEnterHandler, IPointerExitHandler
    {
        //Fields
        [SerializeField]
        //private Camera uiCamera;
        public FleetData fleetData;
        public GameObject canvasFleetUI;
        public GameObject tooltipFleet;
        public GameObject buttonLoadFleetUI;
        public GameObject tooltipBackground;
        public List<StarSysData> systemsList;
        public List<ShipData> shipList;
        public Transform targetTrans;
        public float warpSpeed = 0f;
        public bool warpTravel = false;
        private float timeToWait = 0.5f;
        //public event Action openFleetUI; // need to close any open fleetUI to make room for the current!!
        //public event Action moveFleet; 
        public GameObject sysDropdownGO;
        public GameObject shipDropdownGO;
        [SerializeField]
        private TMP_Text dropdownSysText;
        [SerializeField]
        private TMP_Text dropdownShipText;
        private TMP_Text sysDestination;
        [SerializeField]
        private TMP_Text tooltipText;
        //[SerializeField]
        private RectTransform backgroundRecTrans;

        private void Start()
        {
            //uiCamera = Camera.main;
            //tooltipText.text = "Testing Tooltip";
            //backgroundRecTrans = tooltipBackground.transform.GetComponent<RectTransform>();
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
            tooltipBackground.SetActive(false);
            
            //ShowToolTipFleetName("FLEETNAME");
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
        //private void OnMouseEnter()
        //{
        //    ShowToolTipFleetName(fleetData.Name);
        //}
        //private void OnMouseExit()
        //{
        //    HideToolTipFleetName();
        //}
        void Update()
        {
            if (fleetData != null)
            {
                fleetData.Position = transform.position;
            }
            //Vector2 localPoint;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
            //transform.localPosition = localPoint;
        }
        public void DropdownSystems(int index)
        {
            foreach (var system in systemsList) 
            {
            
            }
            //switch (index) 
            //{ default:break; }
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
        //private void ShowToolTipFleetName(string tooltipString)
        //{
        //    tooltipFleet.SetActive(true);
        //    tooltipText.text = tooltipString;
        //    float textPaddingSize = 0.5f;
        //    Vector2 background = new Vector2(tooltipText.preferredWidth + (textPaddingSize * 2f),
        //        tooltipText.preferredHeight + (textPaddingSize * 2f));
        //    backgroundRecTrans.sizeDelta = background;
        //}
        //private void HideToolTipFleetName()
        //{
        //    tooltipFleet.SetActive(false);
        //    tooltipText.text = string.Empty;
        //}
        private void OnEnable()
        {
            //canvasFleetUI.SetActive(true);
            if (fleetData != null)
                fleetData.Position = transform.position;
            
        }
        private void OnDisable()
        {
           
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

        //void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(StartTimer());
        //}

        //void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        //{
        //    StopAllCoroutines();
        //    HoverTipManager.OnMouseLoseFocus();
        //}
        //private IEnumerator StartTimer()
        //{
        //    yield return new WaitForSeconds(timeToWait);
        //    tooltipBackground.SetActive(true);
        //}
    }

}
