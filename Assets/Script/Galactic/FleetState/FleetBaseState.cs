using UnityEngine;

public abstract class FleetBaseState 
{
    public abstract void EnterState(FleetStateManager fleet);

    public abstract void UpdateState(FleetStateManager fleet);

    public abstract void OnCollisionEnter(FleetStateManager fleet, Collision collsion);

}
