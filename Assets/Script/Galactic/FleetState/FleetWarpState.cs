using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetWarpState : FleetBaseState
{
    public float currentWarpFactor;
    private readonly GameObject _gameObject;
    //private readonly Vector3 _destinationPosition;
    private Rigidbody _rb;
    private Transform _destination;
    private float maxWarpFactor;


    public FleetWarpState(GameObject fleetGO, Transform destinationTransfrom)
    {
        _gameObject = fleetGO;
        _destination = destinationTransfrom;
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
        fleetController.UpdateWarpFactor(0f);
        //_destination = fleetController.Destination;
        //_rb = fleetController.GetRigidbody();
    }
    public override void ExitState(FleetController fleetController)
    {
        _destination = null;
        _rb = null;
    }

    public override void UpdateState(FleetController fleetController)
    {
        
        //if (_destination != null)
        //{
        //    Vector3 nextPosition = Vector3.MoveTowards(_rb.position, _destination.position, currentWarpFactor * Time.fixedDeltaTime);
        //    _rb.MovePosition(nextPosition);
        //}
        //_gameObject.transform.position = 
        //    Vector3.MoveTowards(_gameObject.transform.position, _destinationPosition, currentWarpFactor);
        //if (Vector3.Distance(_gameObject.transform.position, _destinationPosition) > 0.1f ) // ToDo: hit collider of...
        //{
        //    if(_gameObject.GetComponent<FleetController>() != null)
        //        _gameObject.GetComponent<FleetController>().SwitchState(new FleetAllStopState(_gameObject));
        //}
    }
    public override void OnCollisionEnter(FleetController fleetController, Collision collision)
    {

    }

    //public override void OnTriggerEnter(FleetController fleetController)
    //{
    //    throw new System.NotImplementedException();
    //}
}
