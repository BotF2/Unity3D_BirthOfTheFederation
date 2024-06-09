using UnityEngine;
using Assets.Core;
using Unity.VisualScripting;

public abstract class FleetBaseState 
{
    //public string Name;
    //protected StateMachine StateMachine;
    //public FleetBaseState(string name, StateMachine stateMachine)
    //{
    //    this.Name = name;
    //    this.StateMachine = stateMachine;
    //}
    public abstract void EnterState(FleetController fleetController);
    public abstract void ExitState(FleetController fleetController);
    public abstract void UpdateState(FleetController fleetController);

    public abstract void OnCollisionEnter(FleetController fleetController, Collision collsion);

    //public abstract void OnTriggerEnter(FleetController fleetController);
}
