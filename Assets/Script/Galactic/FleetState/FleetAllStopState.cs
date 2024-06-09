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

    }
    public override void ExitState(FleetController fleetController)
    {

    }
    public override void UpdateState(FleetController fleetController)
    {
        if (fleetController.FleetData.CurrentWarpFactor > 0 && fleetController.FleetData.Destination != null)
        {
            fleetController.SwitchState(fleetController.warpState);
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
