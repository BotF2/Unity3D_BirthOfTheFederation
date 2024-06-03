using UnityEngine;
using Assets.Core;

public abstract class FleetBaseState 
{
    public abstract void EnterState(FleetController fleetController);
    public abstract void ExitState(FleetController fleetController);
    public abstract void UpdateState(FleetController fleetController);

    public abstract void OnCollisionEnter(FleetController fleetController, Collision collsion);
    //public abstract void OnTriggerEnter(FleetController fleetController);
}
