using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class PlayerDefinedTargetData
{
    public Sprite Insignia;
    public CivEnum CivOwnerEnum;
    public Vector3 Position;
    public string CivShortName;
    public string Name;
    public string Description;

    public PlayerDefinedTargetData(string name)
    {
        Name = name;
    }
    public PlayerDefinedTargetData()
    {

    }
}
