using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetWarpState : FleetBaseState
{
    public float currentWarpFactor;
    private readonly GameObject _gameObject;
    private readonly Vector3 _destinationPosition;

    public FleetWarpState(GameObject fleetGO, Vector3 destinationPosition)
    {
        _gameObject = fleetGO;
        _destinationPosition = destinationPosition;
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

    }
    public override void ExitState(FleetController fleetController)
    {

    }
    public override void UpdateState(FleetController fleetController)
    {
        _gameObject.transform.position = 
            Vector3.MoveTowards(_gameObject.transform.position, _destinationPosition, currentWarpFactor);
        if (Vector3.Distance(_gameObject.transform.position, _destinationPosition) > 0.1f ) // ToDo: hit collider of...
        {
            if(_gameObject.GetComponent<FleetController>() != null)
                _gameObject.GetComponent<FleetController>().SwitchState(new FleetAllStopState(_gameObject));
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
