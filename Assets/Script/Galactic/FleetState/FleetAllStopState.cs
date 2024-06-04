using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetAllStopState : FleetBaseState
{
    private readonly GameObject _gameObject;

    public FleetAllStopState(GameObject fleetGO)
    {
        _gameObject = fleetGO;
    }

    public FleetAllStopState()
    {
    }

    public override void EnterState(FleetController fleetController)
    {
        _gameObject.GetComponent<FleetController>().FleetData.CurrentWarpFactor = 0;
    }
    public override void ExitState(FleetController fleetController)
    {

    }
    public override void UpdateState(FleetController fleetController)
    {

    }
    public override void OnCollisionEnter(FleetController fleetController, Collision collision)
    {

    }

    //public override void OnTriggerEnter(FleetController fleetController)
    //{
    //    throw new System.NotImplementedException();
    //}
}
