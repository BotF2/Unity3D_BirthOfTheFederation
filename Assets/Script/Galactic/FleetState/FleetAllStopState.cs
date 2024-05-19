using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetAllStopState : FleetBaseState
{
    public override void EnterState(FleetStateManager fleet)
    {
        Debug.Log("all stop");
    }
    public override void UpdateState(FleetStateManager fleet)
    {

    }
    public override void OnCollisionEnter(FleetStateManager fleet, Collision collision)
    {

    }
}
