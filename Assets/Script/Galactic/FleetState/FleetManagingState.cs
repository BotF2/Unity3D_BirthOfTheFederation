using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetManagingState : FleetBaseState
{
    private readonly GameObject _gameObject;
    public FleetManagingState(GameObject fleetGO)
    {
        _gameObject = fleetGO;
    }
    public FleetManagingState() { }
    public override void EnterState(FleetController fleetController)
    {

    }
    public override void UpdateState(FleetController fleetController)
    {

    }
    public override void OnCollisionEnter(FleetController fleetController, Collision collision)
    {

    }

    public override void ExitState(FleetController fleetController)
    {
        throw new System.NotImplementedException();
    }
}