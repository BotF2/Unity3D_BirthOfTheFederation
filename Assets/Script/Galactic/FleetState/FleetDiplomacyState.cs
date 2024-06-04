using UnityEngine;
using Assets.Core;

public class FleetDiplomacyState : FleetBaseState
{
    private readonly GameObject _gameObject;
    public FleetDiplomacyState(GameObject fleetGO)
    {
        _gameObject = fleetGO;
    }
    public FleetDiplomacyState()
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

    public override void OnTriggerEnter(FleetController fleetController)
    {
        throw new System.NotImplementedException();
    }
}