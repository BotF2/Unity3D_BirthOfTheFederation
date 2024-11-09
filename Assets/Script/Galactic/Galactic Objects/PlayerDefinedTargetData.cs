using Assets.Core;
using UnityEngine;

public class PlayerDefinedTargetData
{
    public Sprite Insignia;
    public CivEnum CivOwnerEnum;
    public Vector3 Position;
    public string CivShortName;
    public GalaxyObjectType GalaxyObjectType = GalaxyObjectType.TargetDestination;
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

