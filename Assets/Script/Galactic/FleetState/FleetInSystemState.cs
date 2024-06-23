using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetInSystemState : FleetBaseState
{
    private readonly GameObject _gameObject;
    public FleetInSystemState(GameObject fleetGO)
    {
        _gameObject = fleetGO;
    }

    public FleetInSystemState()
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

    }
    public override void OnCollisionEnter(FleetController fleetController, Collision collision)
    {

    }


}
