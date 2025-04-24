using Assets.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetGroupController : MonoBehaviour
{
    //***** Will try to make the Fleet Group a form of Fleet before creating a the group that saves original fleets inside its code.
    private List<FleetController> memberFleets;
    public List<FleetController> MemberFleets { get { return memberFleets; } set { memberFleets = value; } }
    public string Name;

    public FleetState fleetState;
    public bool isArrived = false;
    [SerializeField]
    private float maxWarpFactor = 9.8f;
    private float fudgeFactor = 1f;
    private float dropOutOfWarpDistance = 0.5f; // stop, but should be destination collider?
    private Rigidbody rb;
    public MapLineMovable dropLine;
    GameObject fleetGroupDropdownGO;
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
        //rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        //GalaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //var CanvasGO = GameObject.Find("CanvasFleetUI");
        //DiplomacyUICanvas = CanvasGO.GetComponent<canvas>();
        //DiplomacyUICanvas.worldCamera = GalaxyEventCamera;
        //CanvasToolTip.worldCamera = GalaxyEventCamera;
        //FleetData.CurrentWarpFactor = 0f;
        //TextComponent = FleetData.CivShortName + " Fleet " + FleetData.TextComponent;
        //FleetState = FleetState.FleetStationary;

    }
    void Update()
    {
        //switch (FleetState)
        //{
        //    case FleetState.FleetInSystem:
        //        {
        //            // add to system fleet list
        //            AllStop();
        //            break;
        //        }
        //    case FleetState.FleetCombat:
        //        {
        //            AllStop();
        //            break;
        //        }
        //    case FleetState.FleetDipolmacy:
        //        {
        //            //AllStop();
        //            break;
        //        }
        //    case FleetState.FleetStationary:
        //        {
        //            break;
        //        }
        //    case FleetState.FleetAtWarp:
        //        {
        //            break;
        //        }
        //    case FleetState.FleetsInRendezvous:
        //        {
        //            //if(this.FleetData.Destination == )
        //            AllStop();
        //            break;
        //        }
        //}
    }
    private void AllStop()
    {
        //this.FleetData.Destination = null;
        //this.FleetData.CurrentWarpFactor = 0f;
    }
    private void FixedUpdate()
    {
        //if (FleetData.CivEnum != CivEnum.ZZUNINHABITED1)
        //{
        //    if (FleetData.Destination != null && FleetData.CurrentWarpFactor > 0f)
        //    {
        //        FleetState = FleetState.FleetAtWarp;
        //        MoveToDesitinationGO();
        //    }
        //}
    }

    public Rigidbody GetRigidbody() { return rb; }

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
                //this.LoadAFleetUI(gameObject);
            }
        }

    }
    void OnTriggerEnter(Collider collider) // Not using OnCollisionEnter....
    {
        FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
        if (fleetController != null) // it is a FleetController and not a StarSystem or other fleetController
        {
            OnFleetEncounteredFleet(fleetController);
            Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
        }
        StarSysController starSysController = collider.gameObject.GetComponent<StarSysController>();
        if (starSysController != null)
        {
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
        //CivManager.current.Diplomacy(this.fleetData.CivController, fleetController.fleetData.CivController);
        //// is it our fleet or not? Diplomacy or manage fleets or keep going?
        //if (fleetController.gameObject == this.FleetData.Destination)
        //{
        //    /// use fleet enum state
        //    //this.FleetData.Destination = null;
        //    //this.FleetData.war
        //    //FleetState = FleetState.FleetInSystem;
        //}
        //FleetManager.current.
        //1) player get the FleetController of the new fleet GO
        //2) player ask your factionOwner (CivManager) if player already know the faction of the new fleet
        //3) ?first contatact > what kind of hail?
        //4) ?combat
        //5) ?move ships in and out of fleets
    }
    public void OnFleetEncounteredStarSys(StarSysController starSysController)
    {
        //????StarSysManager.current.
        //FleetManager.current.GetFleetGroupInSystemForShipTransfer(StarSysController);
        //CivManager.current.Diplomacy(this.fleetData.CivController, StarSysController.StarSysData.CurrentCivController);
        //// is it our fleet or not? Diplomacy or manage fleets or keep going?
        //if (StarSysController.gameObject == this.FleetData.Destination)
        //{
        //    /// use fleet enum state
        //    //this.FleetData.Destination = null;
        //    //this.FleetData.war
        //    //FleetState = FleetState.FleetInSystem;
        //}
        //1) player get the FleetController of the new fleet GO
        //2) player ask your factionOwner (CivManager) if player already know the faction of the new fleet
        //3) ?first contatact > what kind of hail, diplomacy vs uninhabited ?colonize vs terraform a rock vs do fleet busness in our system
        //4) ?combat vs diplomacy and or traid...
    }
    public void OnFleetEncounteredPlayerDefinedTarget(PlayerDefinedTargetController playerTargetController)
    {
        //????PlayerDefinedTargetManager.current.
        //FleetManager.current.
        //1) player get the FleetController of the new fleet GO
        //2) ?build a deep space starbase vs a partol point for travel

    }
    public void SetWarpSpeed(float newSpeed)
    {
        if (newSpeed <= maxWarpFactor)
        {
            //FleetData.CurrentWarpFactor = newSpeed;
        }
    }
    public void SetDestination(GameObject newDestination)
    {
        //this.FleetData.Destination = newDestination;
    }
    void MoveToDesitinationGO()
    {
        //Vector3 direction = (this.FleetData.Destination.transform.position - transform.position).normalized;
        //float distance = Vector3.Distance(transform.position, this.FleetData.Destination.transform.position);

        //if (distance > dropOutOfWarpDistance)
        //{
        //    Vector3 nextPosition = Vector3.MoveTowards(rb.position, FleetData.Destination.transform.position,
        //        FleetData.CurrentWarpFactor * warpFudgeFactor * Time.fixedDeltaTime);
        //    rb.MovePosition(nextPosition); // kinematic with physics movement
        //}
        //else
        //{
        //    rb.velocity = Vector3.zero;
        //    OnArrivedAtDestination();
        //}
        //Vector3 galaxyPlanePoint = new Vector3(rb.position.x, -60f, rb.position.z);
        //Vector3[] points = { rb.position, galaxyPlanePoint };
        //DropLine.SetUpLine(points);
    }
    void OnArrivedAtDestination()
    {
        //if (IsArrived == false)
        //{
        //    IsArrived = true;
        //    // Logic to handle what happens when the fleet arrives at the destination
        //    Debug.Log("Arrived at destination: " + this.FleetData.Destination.name);
        //    // Example: Stop the fleet, update UI, trigger events, etc.
        //}
    }
    void AddToShipList(ShipController shipController)
    {
        //foreach (var ShipData in this.FleetData.GetShipList())
        //    FleetData.AddToShipList(shipController);
        //deltaShipList = true;
    }
    void RemoveFromShipList(ShipController shipController)
    {
        //this.FleetData.RemoveFromShipList(shipController);
        //deltaShipList = true;
    }

    //public void UpdateWarpFactor(float delta)
    //{
    //    FleetData.CurrentWarpFactor += delta;
    //    FleetData.CurrentWarpFactor += delta;
    //}
    //public void AddFleetController(FleetController FleetController) // do we need this?
    //{
    //    if (!FleetManager.current.FleetControllerList.Contains(FleetController))
    //        FleetManager.current.FleetControllerList.Add(FleetController);
    //}
    //public void RemoveFleetController(FleetController FleetController)
    //{
    //    if (FleetManager.current.FleetControllerList.Contains(FleetController))
    //        FleetManager.current.FleetControllerList.Remove(FleetController);
    //}
}


