using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetAllStopState : FleetBaseState
{
    public override void EnterState(FleetStateManager fleetStateMan)
    {
        Debug.Log("all stop");
    }
    public override void UpdateState(FleetStateManager fleetStateMan)
    {

    }
    public override void OnCollisionEnter(FleetStateManager fleetStateMan, Collision collision)
    {

    }
}
