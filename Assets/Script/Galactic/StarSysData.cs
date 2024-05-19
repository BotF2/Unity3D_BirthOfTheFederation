using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class StarSysData
{
    public int SysInt;
    public Vector3 Position;
    public Transform SysTransform;
    public string SysName;
    public CivEnum FirstOwner;
    public CivEnum CurrentOwner;
    public StarSystemType StarType;
    public Sprite StarSprit;
    public string Description;
    public int Population;
    private string v;

    public StarSysData()
    {

    }
    public StarSysData(string v)
    {
        this.v = v;
        this.SysName = v;
    }
}
