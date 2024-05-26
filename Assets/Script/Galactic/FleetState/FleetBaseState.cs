using UnityEngine;

public abstract class FleetBaseState 
{
    public abstract void EnterState(FleetStateManager fleetStateMan);

    public abstract void UpdateState(FleetStateManager fleetStateMan);

    public abstract void OnCollisionEnter(FleetStateManager fleetStateMan, Collision collsion);

}
