using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFleetWarpEvents : MonoBehaviour
{
    public static UpDownFleetWarpEvents current;

    public Action<FleetController, string> FleetOnWarpUpClick;

    private void Awake()
    {
        if (current != null) { Destroy(gameObject); }
        else
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        FleetOnWarpUpClick += DoFleetOnWarpUp;
    }
    public void DoFleetOnWarpUp(FleetController fleetCon, string name)
    {
        if (FleetOnWarpUpClick != null)
        {
            FleetOnWarpUpClick?.Invoke(fleetCon, name);
        }
    }
}
