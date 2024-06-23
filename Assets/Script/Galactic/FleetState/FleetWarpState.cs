using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetWarpState : FleetBaseState
{
    public float currentWarpFactor;
    private readonly GameObject _gameObject;
    private Rigidbody _rb;
    private Transform _destination;
    private float _currentWarpFactor;
    private float maxWarpFactor;


    public FleetWarpState(GameObject fleetGO) //, Transform destinationTransfrom, Rigidbody rb, float warpFactor)
    {
        _gameObject = fleetGO;
        //_destination = destinationTransfrom;
        //_rb = rb;
        //_currentWarpFactor = warpFactor;
    }

    public FleetWarpState()
    {
    }

    public void WarpFactor(float newWarpFactor)
    {
        currentWarpFactor = newWarpFactor;
    }
    public override void EnterState(FleetController fleetController)
    {
        //_destination = fleetController.FleetData.Destination;
        //_rb = fleetController.GetRigidbody();
        //maxWarpFactor = fleetController.FleetData.GetMaxWarpFactor();
    }
    public override void ExitState(FleetController fleetController)
    {
        _destination = null;
        _rb = null;
        maxWarpFactor = 9.8f;
    }

    public override void UpdateState(FleetController fleetController)
    {

        if (fleetController.FleetData.CurrentWarpFactor == 0 || fleetController.FleetData.Destination == null)
        {
            fleetController.SwitchState(fleetController.stationaryState);
        }
    }
    public override void OnCollisionEnter(FleetController fleetController, Collision collision)
    {

    }

}
