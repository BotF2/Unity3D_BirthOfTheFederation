using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;

public class ShipController : MonoBehaviour
{
    private ShipData shipData;
    public ShipData ShipData { get { return shipData; } set { shipData = value; } }
    public string Name;
    //private bool deltaShipList = false; //??? do I need this or the shipdropdown listener
    //Rigidbody rb;

    //[SerializeField]
    //private TMP_Text dropdownText;
    //[SerializeField]
    //private TMP_Text andMoreShipText;
    //[SerializeField]
    //private TMP_Text evenMoreText;
    ////private Camera combatEventCamera;
    //[SerializeField]
    //private Canvas CombatUICanvas;
    //[SerializeField]
    //private Canvas CanvasToolTip;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //combatEventCamera = GameObject.FindGameObjectWithTag("Combat Camera").GetComponent<Camera>();
        //var CanvasGO = GameObject.Find("CombatShipUI");
        //CombatUICanvas = CanvasGO.GetComponent<Canvas>();
        //CombatUICanvas.worldCamera = combatEventCamera;
        //CanvasToolTip.worldCamera = combatEventCamera;
        //Name = shipData.CivEnum.ToString() + " Fleet " + shipData.ShipName;

    }
    private void FixedUpdate()
    {
        // might use this
    }
    private void OnMouseDown()
    {
        //string goName;
        //Ray ray = combatEventCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    GameObject hitObject = hit.collider.gameObject;
        //    goName = hitObject.name;
        //}
        //CombatUIManager.instance.LoadShipUI(gameObject);
    }
    void OnTriggerEnter(Collider collider)
    {
        ShipController shipController = collider.gameObject.GetComponent<ShipController>();
        if (shipController != null) // it is a shipController 
        {
            OnShipEncounteredShip(shipController);
            Debug.Log("fleet Controller collided with " + shipController.gameObject.name);
        }
        //OtherController otherController = collider.gameObject.GetComponent<OtherController>();
        //if (otherSysController != null)
        //{
        //    OnShipEncounteredOther(otherController);
        //}
    }
    public void OnShipEncounteredShip(ShipController shipController)
    {
        //1) you get the ShipController of the ship GO we hit
        //2) you ask your factionOwner (CivManager) 
    }
    //public void OnShipEncounteredOther(OtherController starSysController)
    //{
    //    //1) you get the OtheerController of the GO

    //}
}
