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
        if (_rb != null)
        {
            fleetController.SwitchState(fleetController.warpState);
            Vector3 nextPosition = Vector3.MoveTowards(_rb.position, _destination.position,
                _currentWarpFactor * fleetController.FleetData.fudgeFactor * Time.fixedDeltaTime);
            _rb.MovePosition(nextPosition);
            Vector3 galaxyPlanePoint = new Vector3(_rb.position.x, -60f, _rb.position.z);
            Vector3[] points = { _rb.position, galaxyPlanePoint };
            fleetController.dropLine.SetUpLine(points);
        }

        if (fleetController.FleetData.CurrentWarpFactor == 0 || fleetController.FleetData.Destination == null)
        {
            fleetController.SwitchState(fleetController.allStopState);
        }
    }
    public override void OnCollisionEnter(FleetController fleetController, Collision collision)
    {

    }

    //public override void OnTriggerEnter(FleetController fleetController)
    //{
    //    throw new System.NotImplementedException();
    //}
}
