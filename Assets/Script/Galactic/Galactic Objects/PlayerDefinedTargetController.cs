
using Assets.Core;
using TMPro;
using UnityEngine;


public class PlayerDefinedTargetController : MonoBehaviour
{
    public PlayerDefinedTargetData PlayerTargetData;
    public Sprite Insignia;
    public CivEnum CivOwnerEnum;
    public Vector3 Position;
    private Vector3 lastMousePosition;
    [SerializeField]
    private float mouseSpeed = 2f;
    private Rigidbody rb;
    public MapLineMovable DropLine;
    private TMP_Text ourDestination;
    public Camera galaxyEventCamera;
    //public InputAction actionPlayerTargetDestination;  
    public GameObject galaxyBackgroundImage;
    [SerializeField]
    private Canvas CanvasToolTip;
    public string Name;

    void Start()
    {
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void Update()
    {
        DragWithLeftMouse();
    }
    private void DragWithLeftMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            if (FleetUIManager.Instance.MouseClickSetsDestination)
            {
                Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.tag != "GalaxyImage" &&
                        GameController.Instance.AreWeLocalPlayer(this.PlayerTargetData.CivOwnerEnum))
                    {
                        if (FleetUIManager.Instance.MouseClickSetsDestination == true) // while FleetUIManager was looking for a destination
                        {
                            NewDestination(hitObject); // target hit as destination
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.Space))
        {
            Vector3 delta = (Input.mousePosition - lastMousePosition) / mouseSpeed;
            MoveTarget(delta.x, delta.y);
            lastMousePosition = Input.mousePosition;
            rb.MovePosition(transform.position); // kinematic with physics movement
            rb.velocity = Vector3.zero;
            // update drop line
            Vector3 galaxyPlanePoint = new Vector3(rb.position.x, -60f, rb.position.z);
            Vector3[] points = { rb.position, galaxyPlanePoint };
            DropLine.SetUpLine(points);
        }
    }
    private void MoveTarget(float xInput, float zInput)
    {
        float zMove = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * zInput + Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
        float xMove = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * zInput + Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
        transform.position = transform.position + new Vector3(xMove, 0, zMove);
    }
    private void NewDestination(GameObject hitObject)
    {
        bool isFleet = false;
        FleetUIManager.Instance.SetAsDestination(hitObject, isFleet);
        //this.CanvasDestination.gameObject.SetActive(true);
    }
    ///
    /// Doing trigger collider in the fleetController for now, sort by type of starSysController on hit object
    /// 

    //void OnTriggerEnter(Collider collider)
    //{

    //    FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
    //    if (fleetController != null) // it is a FleetController
    //    {
    //        OnEncounteredByFleet(fleetController);
    //    }
    //}
    //public void OnEncounteredByFleet(FleetController fleetController)
    //{
    //    if (fleetController.FleetData.Destination == this)
    //    {
    //        fleetController.FleetData.Destination = null;
    //    }
    //}
}
