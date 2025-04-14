using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

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
        public int intName = 1;
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
        [SerializeField]
        private GameObject fleetListUIGameObject; //The instantiated fleet UI for this fleet controller, a prefab clone, not a class but a game object
        // instantiated by FleetManager from a prefab and added to FleetController.
        public GameObject FleetListUIGameObject { get { return fleetListUIGameObject; } set { fleetListUIGameObject = value; } }
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var CanvasGO = GameObject.Find("CanvasGalaxyMenuRibbon");
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
                // What a fleet FleetController does with a hit
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
            bool weAreLocalPlayer = GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum);

            bool isOurDestination = false;
            if (this.FleetData.Destination == collider.gameObject)
            {
                isOurDestination = true;
            }
            if (isOurDestination)
            {
                if (weAreLocalPlayer)
                {
                    FleetUIController.Instance.ClickCancelDestinationButton(); // stop
                    FleetUIController.Instance.CloseUnLoadFleetUI();
                }
            }
            if (collider.gameObject.GetComponent<FleetController>() != null)  // Ensure it's another fleet
            { // *** check ID of the OnTriggerEnter so only one of the two fleet colliding objects reports the collision
                if (gameObject.GetInstanceID() < collider.gameObject.GetInstanceID()) 
                {
                    var fleetConB = collider.gameObject.GetComponent<FleetController>();
                    if (this.FleetData.CivEnum != fleetConB.FleetData.CivEnum)
                    {
                        if (isOurDestination)
                            OnArrivedAtDestination();//? should we do other stuff here for FleetController at fleet destination?
                        EncounterManager.Instance.RegisterEncounter(this, collider.gameObject.GetComponent<FleetController>());
                        if (weAreLocalPlayer)
                            EncounterUnknownFleetGetNameAndSprite(collider.gameObject); // setactive ships sprite and name
                    }
                    else if (!isOurDestination)
                    {
                        // no encounter, just a fleet passing by
                    }
                }
            }
            else if (collider.gameObject.GetComponent<StarSysController>() != null) // only the fleetController reporst a collition for now, not the systems
            {
                var sysCon = collider.gameObject.GetComponent<StarSysController>();

                if (this.FleetData.CivEnum != sysCon.StarSysData.CurrentOwnerCivEnum)
                {
                    if (isOurDestination)
                    {
                        OnArrivedAtDestination();//? should we do other stuff here for FleetController at system destination?
                        OnEnterStarSystem();
                    }
                    EncounterManager.Instance.RegisterEncounter(this, sysCon);
                    if (weAreLocalPlayer)
                        EncounterUnknownSystemShowName(collider.gameObject, sysCon.StarSysData.CurrentOwnerCivEnum);
                }
                else if (!isOurDestination)
                {
                    // no encounter, just a fleet passing by
                }

            }
            else if (collider.gameObject.GetComponent<PlayerDefinedTargetController>() != null)
            {
                if (isOurDestination && weAreLocalPlayer)
                    OnArrivedAtDestination();//? should we anounce FleetController at target destination?
                else if (isOurDestination)
                    OnArrivedAtDestination();//? AI at target destination?
                    
                // no encounter, just fleet stopping at a player defined target
                // EncounterManager.Instance.RegisterEncounter(this, collider.gameObject.GetComponent<PlayerDefinedTargetController>());
            }        
        }
        
        private void EncounterUnknownSystemShowName(GameObject hitGO, CivEnum civEnum)
        {  
            var sysData = hitGO.GetComponent<StarSysController>().StarSysData;

            TextMeshProUGUI[] TheText = hitGO.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i] != null && TheText[i].name == "SysName")
                {
                    if (GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum)) 
                    {
                        TheText[i].text = sysData.GetSysName();
                    }
                }
                else if (TheText[i] != null && TheText[i].name == "SysDescription (TMP)")
                    TheText[i].text = sysData.Description;

            }
        }
        private void EncounterUnknownFleetGetNameAndSprite(GameObject hitGO)
        {
            var fleetData = hitGO.GetComponent<FleetController>().FleetData;
            CivEnum civEnum = fleetData.CivEnum;

            TextMeshProUGUI[] TheText = hitGO.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < TheText.Length; i++)
            {
                TheText[i].enabled = true;
                if (TheText[i] != null && TheText[i].name == "Name (TMP)")
                {
                    if (GameController.Instance.AreWeLocalPlayer(this.FleetData.CivEnum))
                    {
                        TheText[i].text = fleetData.GetFleetName();
                    }
                }
                //else if (TheText[i] != null && TheText[i].name == "SysDescription (TMP)")
                //    TheText[i].text = fleetData.Description;

            }
            FleetManager.Instance.ExposeAllFleetInsigniaSprites(civEnum);
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
            //rb.linearVelocity = Vector3.zero;
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
                      //FleetUIController.current.ClickCancelDestinationButton(); 
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
        public void DestroyFleet(FleetData fleetData, GameObject fleetGO)
        {
            FleetManager.Instance.RemoveFleetInt(fleetData.CivEnum, fleetData.FleetInt);
            if(FleetManager.Instance.FleetControllerList.Contains(this))
            {
                FleetManager.Instance.FleetGOList.Remove(fleetGO);
                FleetManager.Instance.FleetControllerList.Remove(this);
                Destroy(fleetGO.gameObject);
            }
        }
        public void ShipManageClick(FleetController fleetCon) // open ship manager UI
        {
            FleetManager.Instance.InstantiateFleetsShipManagerUI(this);
            GalaxyMenuUIController.Instance.OpenMenu(Menu.ManageShipsMenu, null);
        }
    }
}
