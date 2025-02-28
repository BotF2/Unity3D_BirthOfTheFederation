using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{    
    public enum FleetState { FleetCombat, FleetDipolmacy, FleetInSystem, FleetsInRendezvous, FleetStationary, FleetAtWarp }
    /// <summary>
    /// Controlling fleet movement and interactions while the matching FeetData class
    /// holds key info on status and for save game
    /// </summary>
    public class FleetController : MonoBehaviour
    {
        //Fields
        private FleetData fleetData;
        public FleetData FleetData { get { return fleetData; } set { fleetData = value; } }
        public string Name;
        private int intName = 1;
        public FleetState FleetState;
        private float warpFudgeFactor = 10f;
        private Rigidbody rb;
        public MapLineMovable DropLine;
        public MapLineMovable DestinationLine;
        public GameObject BackgroundGalaxyImage;
        [SerializeField]
        private List<string> shipDropdownOptions;
        private Camera galaxyEventCamera;
        public Canvas FleetUICanvas { get; private set; }
        public Canvas CanvasToolTip;
        public PlayerDefinedTargetController TargetController;
        private Vector3 vectorOffset;
        private float ourZCoordinate;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasFleetUI");
            FleetUICanvas = CanvasGO.GetComponent<Canvas>();
            FleetUICanvas.worldCamera = galaxyEventCamera;
            CanvasToolTip.worldCamera = galaxyEventCamera;
            FleetData.CurrentWarpFactor = 9.8f;
            for (int i = 0; i < FleetData.ShipsList.Count; i++)
            //foreach (var shipCon in this.FleetData.ShipsList)
            {
                if (FleetData.ShipsList[i].ShipData.maxWarpFactor < this.FleetData.MaxWarpFactor)
                { this.FleetData.MaxWarpFactor = FleetData.ShipsList[i].ShipData.maxWarpFactor; }
            }
            Name = FleetData.CivShortName + " Fleet " + intName.ToString();
            FleetState = FleetState.FleetStationary;
            DestinationLine = this.GetComponentInChildren<MapLineMovable>();
            DestinationLine.GetLineRenderer();
            DestinationLine.transform.SetParent(transform, false);
            
        }
        void Update()
        {

            switch (FleetState)
            {
                case FleetState.FleetInSystem:
                    {
                        // add to system fleet list
                        AllStop();
                        break;
                    }
                case FleetState.FleetCombat:
                    {
                        AllStop();
                        break;
                    }
                case FleetState.FleetDipolmacy:
                    {
                        //AllStop();
                        break;
                    }
                case FleetState.FleetStationary:
                    {
                        break;
                    }
                case FleetState.FleetAtWarp:
                    {
                        break;
                    }
                case FleetState.FleetsInRendezvous:
                    {
                        //if(this.FleetData.Destination == )
                        AllStop();
                        break;
                    }
            }
        }
        private void AllStop()
        {
            this.FleetData.Destination = null;
            this.FleetData.CurrentWarpFactor = 0f;
        }
        private void FixedUpdate()
        {
            if (FleetData.CivEnum != CivEnum.ZZUNINHABITED1) // move
            {
                if (FleetData.Destination != null && FleetData.CurrentWarpFactor > 0f)
                {
                    FleetState = FleetState.FleetAtWarp;
                    MoveToDesitinationGO();
                    DrawDestinationLine(FleetData.Destination.transform.position);
                }
            }
            else
            {
               //FleetData.Destination.;
            }

        }
        public Rigidbody GetRigidbody() { return rb; }

        private void OnMouseDown()
        {

            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                // What a fleet ourUIFleetController does with a hit
                /// ********** In multiplayer game ?? 

                if (FleetUIController.Instance.MouseClickSetsDestination == false) // the destination mouse pointer is off so open FleetUI for this FleetController
                {
                    if (GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum))
                        FleetUIController.Instance.LoadFleetUI(hitObject);
                }
                else if (FleetUIController.Instance.MouseClickSetsDestination == true && hitObject != this)
                {
                    NewDestination(hitObject);  // one of local player's objects as destination
                }
            }

        }
        private void OnMouseDrag()
        {
            if (this.TargetController != null)
            {
                this.TargetController.gameObject.transform.position = GetMouseWorldPosition() + vectorOffset;
            }
        }
        private Vector3 GetMouseWorldPosition()
        {
            // pixel coordinates (x,y)
            Vector3 mousePoint = Input.mousePosition;

            //z coordiante of game object on screen
            mousePoint.z = ourZCoordinate;

            return galaxyEventCamera.ScreenToWorldPoint(mousePoint);
        }
        private void OnSetDestination(GameObject destination, int destinationInt) // for the C# event system currently not used 
        {
            if (this == destination.GetComponent<FleetController>()) // are we the new destination?
            {
                // not implemented, looking for a good use case
            }
        }
        private void OnRemoveDestination(GameObject destination, int destinationInt) // for the C# event system
        {
            if (destination == this.FleetData.Destination)
            {
                // not implemented, looking for a good use case
            }
        }
        private void NewDestination(GameObject hitObject) // here is a destination
        {
            {
                DestinationLine.gameObject.SetActive(true);
                DestinationLine.lineRenderer.gameObject.SetActive(true);
                DestinationLine.lineRenderer.enabled = true;

                DestinationLine.lineRenderer.startColor = Color.blue;
                DestinationLine.lineRenderer.endColor = Color.red;
                // turn off cursor of destination
                bool isFleet = false;
                MousePointerChanger.Instance.ResetCursor(); // reset to default cursor because we just got the destination
                MousePointerChanger.Instance.HaveGalaxyMapCursor = false;

                if (hitObject.GetComponent<FleetController>() != null)
                {
                    //hitObject.GetComponent<FleetController>().CanvasDestination.gameObject.SetActive(true);
                    isFleet = true;
                }
                FleetUIController.Instance.SetAsDestination(hitObject, isFleet);
            }
        }

        void OnTriggerEnter(Collider collider) // Not using OnCollisionEnter....
        {
            FleetController hitFleetController = collider.gameObject.GetComponent<FleetController>();// the collider is on the hit gameObject this fleet hit
            if (hitFleetController != null) // it is a FleetController and not a StarSystem or other with collider                                                                                                                                                    leetController
            {
                OnFleetEncounteredFleet(collider.gameObject);
                Debug.Log(this.gameObject.name + " fleet Controller collided with " + collider.gameObject.name);
            }
            StarSysController hitStarSysController = collider.gameObject.GetComponent<StarSysController>();
            if (hitStarSysController != null)
            {
                OnFleetEncounteredStarSys(collider.gameObject); // hitStarSysController and its GO
            }
            PlayerDefinedTargetController playerTargetController = collider.gameObject.GetComponent<PlayerDefinedTargetController>();
            if (playerTargetController != null)
            {
                OnFleetEncounteredPlayerDefinedTarget(playerTargetController);
            }
        }
        public void OnFleetEncounteredFleet(GameObject hitGO)
        {
            CivController hitFleetCivController = hitGO.GetComponent<FleetController>().FleetData.OurCivController;
            DiplomacyController diplomacyController = DiplomacyManager.Instance.GetTheDiplomacyController(this.FleetData.OurCivController, hitFleetCivController);
            if (this.FleetData.Destination == hitGO)
            {
                if (this.FleetData.CivEnum != hitGO.GetComponent<FleetController>().FleetData.CivEnum)
                {
                    if (diplomacyController.areWePlaceholder)
                    {
                        // FistContactDiplomacy for both local palyer using the UI and for non local human players using their UI and for AI without a UI
                        DiplomacyManager.Instance.FistContactDiplomacy(this.FleetData.OurCivController, hitFleetCivController, hitGO);
                        this.FleetState = FleetState.FleetDipolmacy;
                    }
                    else if (!diplomacyController.areWePlaceholder && diplomacyController.DiplomacyData.DiplomacyEnumOfCivs == DiplomacyStatusEnum.TotalWar)
                    {
                        //**** Do Combat ****
                        //Do this in combat code, TimeManager.current.PauseTime();
                    }
                    // is this fleet we hit our destination
                    if (this.FleetData.Destination == hitGO.gameObject)
                    {
                        FleetUIController.Instance.ClickCancelDestinationButton();
                        FleetUIController.Instance.CloseUnLoadFleetUI();
                        //enuManager.Instance.OpenMenu
                        //YourStarSysUIManager.Instance.CloseUnLoadStarSysUI();
                        OnArrivedAtDestination();//? should we do other stuff here for FleetController at destination?
                    }
                }
                else if (GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum))
                {
                    // local player fleet hits another local player fleet, manage ships?
                    if (this.FleetData.ShipsList.Count >= this.FleetData.ShipsList.Count)
                    {
                        //this.FleetData.FleetGroupControllers.Add(thisFleetController);
                        //this.FleetState = FleetState.FleetsInRendezvous;
                        //ToDo: manage to fleets in conjoined for ship exchange and what to do with original fleets, two or more
                    }
                }
            }
            else if (!diplomacyController.areWePlaceholder && diplomacyController.DiplomacyData.DiplomacyEnumOfCivs == DiplomacyStatusEnum.TotalWar)
            {
                //**** Do Combat ****
            }

            /// I am thinking that checking the hitGO for reaching the Destination is redundant. If all controllers are checking the both of
            /// the controllers for the two in a hit are checked above so do not check again.
            //else if (thisFleetController.gameObject == hitGO.GetComponent<FleetController>().FleetData.Destination)
            //{
            //    FleetUIController.current.ClickCancelDestinationButton();
            //    FleetUIController.current.CloseUnLoadFirstContactUI();
            //    YourStarSysUIManager.current.CloseUnLoadStarSysUI();   
            //}

            // old checklist
            //FleetManager.current.
            //1) player get the FleetController of the new fleet GO
            //2) player ask your factionOwner (CivManager) if player already know the civ/faction of the new fleet
            //3) ?first contatact > what kind of hail?
            //4) ?combat
            //5) ?move ships in and out of fleets
        }
        public void OnFleetEncounteredStarSys(GameObject hitGO)
        {

            CivController hitSysCivController = hitGO.GetComponent<StarSysController>().StarSysData.CurrentCivController;
            CivEnum hitCivEnum = hitGO.GetComponent<StarSysController>().StarSysData.CurrentOwner;
            if (this.FleetData.Destination == hitGO)
            {
                int firstUninhabited = (int)CivEnum.ZZUNINHABITED1; // all lower than this are inhabited (including Borg UniComplex and inhabitable Nebulas)
                if (this.FleetData.CivEnum != hitCivEnum)
                {
                    DiplomacyController diplomacyController = DiplomacyManager.Instance.GetTheDiplomacyController(this.FleetData.OurCivController, hitSysCivController);
                    if (diplomacyController.areWePlaceholder && (int)hitSysCivController.CivData.CivEnum < firstUninhabited)
                    {   // First Contact
                        Destroy(diplomacyController.gameObject);
                        // ToDo: FirstContactDiplomacy for both local palyer using the UI and for non local human players using their UI and for AI without a UI
                        DiplomacyManager.Instance.FistContactDiplomacy(this.FleetData.OurCivController, hitSysCivController, hitGO);
                        this.FleetState = FleetState.FleetDipolmacy;

                    }
                    // If an uninhabited system
                    else if (diplomacyController.areWePlaceholder && (int)hitSysCivController.CivData.CivEnum >= firstUninhabited)
                    {
                        //React to Uninhabited system contact
                        Destroy(diplomacyController.gameObject);
                        if (GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum))
                            hitGO.GetComponent<StarSysController>().DoHabitalbeSystemUI(this);
                        foreach (ShipController shipController in this.FleetData.GetShipList())
                        {
                            if (shipController.ShipData.ShipType == ShipType.Transport)
                            {
                                // ToDo: Colonies Opption/ UI?
                            }
                        }
                    }
                    else if (!diplomacyController.areWePlaceholder && diplomacyController.DiplomacyData.DiplomacyEnumOfCivs == DiplomacyStatusEnum.TotalWar)
                    {
                        // is it do combat or do we get an option to blockaid or what???
                        //**** Do Combat **
                    }

                    // is this fleet we hit our destination
                    if (this.FleetData.Destination == hitGO.gameObject)
                    {
                        FleetUIController.Instance.ClickCancelDestinationButton();
                        FleetUIController.Instance.CloseUnLoadFleetUI();
                        OnArrivedAtDestination();//? should we do other stuff here for FleetController at destination?
                        OnEnterStarSystem();
                    }
                }
                else if (GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum)) // is our civ's system and we are local player
                {
                    // local player fleet hits another local player system, enter system
                    //**** OnEnterStarSystem();
                }
            }
            else
            {
                //For now do nothing, even if at war
            }


            //1) player get the FleetController of the new fleet GO
            //2) player ask your factionOwner (CivManager) if player already know the faction of the new fleet
            //3) ?first contatact > what kind of hail, diplomacy vs firstUninhabited ?colonize vs terraform a rock vs do fleet busness in our system
            //4) ?combat vs diplomacy and or traid...
        }
        public void OnFleetEncounteredPlayerDefinedTarget(PlayerDefinedTargetController playerTargetController)
        {
            if (this.FleetData.Destination == playerTargetController.gameObject)
            {
                FleetUIController.Instance.ClickCancelDestinationButton();
                Destroy(playerTargetController.gameObject);
                DestinationLine.lineRenderer.positionCount = 0;
                FleetUIController.Instance.CloseUnLoadFleetUI();
            }
            //????PlayerDefinedTargetManager.current.
            //FleetManager.current.
            //1) player get the FleetController of the new fleet GO
            //2) ?build a deep space starbase vs a partol point for travel
        }

        public void SetWarpSpeed(float newSpeed)
        {
            if (newSpeed <= this.FleetData.MaxWarpFactor)
            {
                FleetData.CurrentWarpFactor = newSpeed;
                if (newSpeed > 0)
                    this.FleetState = FleetState.FleetAtWarp;
            }
            if (newSpeed == 0f)
                this.FleetState = FleetState.FleetStationary;

        }

        void MoveToDesitinationGO()
        {
            Vector3 direction = (this.FleetData.Destination.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, this.FleetData.Destination.transform.position);
            if (this.FleetData.CurrentWarpFactor > this.FleetData.MaxWarpFactor)
            {
                this.FleetData.CurrentWarpFactor = this.FleetData.MaxWarpFactor;
            }
            Vector3 nextPosition = Vector3.MoveTowards(rb.position, FleetData.Destination.transform.position,
            FleetData.CurrentWarpFactor * warpFudgeFactor * Time.fixedDeltaTime);
            rb.MovePosition(nextPosition); // kinematic with physics movement
            rb.linearVelocity = Vector3.zero;
            // OnArrivedAtDestination();

            // update dropline
            Vector3 galaxyPlanePoint = new Vector3(rb.position.x, -60f, rb.position.z);
            Vector3[] points = { rb.position, galaxyPlanePoint };
            DropLine.SetUpLine(points);
            this.FleetState = FleetState.FleetAtWarp;
        }
        void DrawDestinationLine(Vector3 destinationPoint)
        {
            if (DestinationLine != null) { }
            else
            {
                DestinationLine = this.GetComponentInChildren<MapLineMovable>();
                DestinationLine.GetLineRenderer();
                DestinationLine.transform.SetParent(transform, false);
                DestinationLine.enabled = true;
            }

            Vector3[] points = { transform.position, destinationPoint };
            DestinationLine.gameObject.SetActive(true);
            DestinationLine.lineRenderer.startColor = Color.blue;
            DestinationLine.lineRenderer.endColor = Color.red;
            DestinationLine.SetUpLine(points);
        }
        void OnArrivedAtDestination()
        {
            // Logic to handle what happens when the fleet arrives at the destination
            ;           //FleetUIController.current.ClickCancelDestinationButton(); 
                        // Debug.Log("Arrived at destination: " + this.FleetData.Destination.name);
                        // Example: Stop the fleet, update UI, trigger events, etc.

        }
        void OnEnterStarSystem()
        {
            // do stuff
        }
        public void AddToShipList(ShipController shipController)
        {
            foreach (var ShipData in this.FleetData.GetShipList())
                FleetData.AddToShipList(shipController);
            UpdateMaxWarp();
        }
        public void RemoveFromShipList(ShipController shipController)
        {
            this.FleetData.RemoveFromShipList(shipController);
            UpdateMaxWarp();
        }
        public void UpdateMaxWarp()
        {
            float maxWarp = 9.8f;
            for (int i = 0; i<fleetData.ShipsList.Count; i++)
            {
                if (fleetData.ShipsList[i].ShipData.maxWarpFactor < maxWarp)
                {
                    maxWarp = fleetData.ShipsList[i].ShipData.maxWarpFactor;
                }
            }
            fleetData.MaxWarpFactor = maxWarp;
        }
    }
}
