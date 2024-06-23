using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetStationaryState : FleetBaseState
{
    private readonly GameObject _gameObject;

    public FleetStationaryState(GameObject fleetGO)
    {
        _gameObject = fleetGO;
    }

    public FleetStationaryState()
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

}
