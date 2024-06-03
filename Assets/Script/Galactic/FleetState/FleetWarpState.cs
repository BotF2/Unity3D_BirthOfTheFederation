using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FleetWarpState : FleetBaseState
{
    public float currentWarpFactor;
    
    public void WarpFactor(float newWarpFactor)
    {
        currentWarpFactor = newWarpFactor;
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

    //public override void OnTriggerEnter(FleetController fleetController)
    //{
    //    throw new System.NotImplementedException();
    //}
}
