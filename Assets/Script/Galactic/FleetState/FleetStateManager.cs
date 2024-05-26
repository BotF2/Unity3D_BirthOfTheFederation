using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetStateManager : MonoBehaviour
{
    FleetBaseState currentState;
    public FleetInSystemState inSystemState = new FleetInSystemState();
    public FleetAllStopState allStopState = new FleetAllStopState();
    public FleetWarpState warpState = new FleetWarpState();
    public FleetCombatState combatState = new FleetCombatState();

    void Start()
    {
        currentState = allStopState;
        currentState.EnterState(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
        
    }
    void Update()
    {
        currentState.UpdateState(this); 
    }
    void SwitchState(FleetBaseState baseState)
    {
        currentState = baseState;
        baseState.EnterState(this);
    }
}
