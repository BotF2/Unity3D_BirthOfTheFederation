
using UnityEngine;
using Assets.Core;
using TMPro;
using UnityEngine.InputSystem;


public class PlayerDefinedTargetController : MonoBehaviour
{
    public PlayerDefinedTargetData playerTargetData;
    public Sprite Insignia;
    public CivEnum CivOwnerEnum;
    public Vector3 Position;
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
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //    }
    //}
    //private void OnMouseDown()// not being hit for some reason??
    //{
    //    Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        GameObject hitObject = hit.collider.gameObject;

    //        //if (this.Fl == GameManager.Instance.GameData.LocalPlayerCivEnum)
    //        //{
    //        //    if (FleetUIManager.Instance.MouseClickSetsDestination == false)
    //        //    {
    //        //        FleetUIManager.Instance.LoadFleetUI(hitObject);
    //        //    }
    //        //}
    //        //else if (FleetUIManager.Instance.MouseClickSetsDestination == true && this != FleetUIManager.Instance.ourUIFleetController)
    //        //{
    //        //    FleetUIManager.Instance.SetAsDestination(hitObject);
    //        //    this.CanvasDestination.gameObject.SetActive(true);
    //        //    //MousePointerChanger.Instance.ResetCursor();
    //        //    //MousePointerChanger.Instance.HaveGalaxyMapCursor = false;
    //        //}
    //    }
    //}
    void OnTriggerEnter(Collider collider)
    {

        FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
        if (fleetController != null) // it is a FleetController
        {
            OnFleetEncounteredFleet(fleetController);
            Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
        }
    }
    public void OnFleetEncounteredFleet(FleetController fleetController)
    {
        //FleetManager.Instance.
        //1) you get the FleetController of the new fleet GO
        //2) you will need to apply different logics depending of the answer
    }
}
