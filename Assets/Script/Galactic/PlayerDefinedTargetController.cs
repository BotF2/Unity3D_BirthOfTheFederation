using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;

public class PlayerDefinedTargetController : MonoBehaviour
{
    public PlayerDefinedTargetData playerTargetData;
    public Sprite Insignia;
    public CivEnum CivOwnerEnum;
    public Vector3 Position;
    private TMP_Text ourDestination;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas CanvasToolTip;
    public List<PlayerDefinedTargetController> playerConsWeHave;

    void Start()
    {
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
    }
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
    public void AddPlayerTargetController(PlayerDefinedTargetController controller)
    {
        playerConsWeHave.Add(controller);
    }
    public void RemovePlayerTargetController(PlayerDefinedTargetController controller)
    {
        playerConsWeHave.Remove(controller);
    }
}
