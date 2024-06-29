using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class StarSysData
{
    private int starSysInt;
    private Vector3 position;
    public GameObject SysGameObject;
    private string sysName;
    public string SysName { get { return sysName; } }
    private CivEnum firstOwner;
    public CivEnum CurrentOwner;
    public StarSystemType StarType;
    public Sprite StarSprit;
    public string Description;
    public int Population;
    private string v;

    public StarSysData()
    {

    }
    public StarSysData(StarSysSO starSysSO)
    {
        starSysInt = starSysSO.StarSysInt;
        position = new Vector3(starSysSO.Position.x, starSysSO.Position.y, starSysSO.Position.z);
        sysName = starSysSO.SysName;
        firstOwner = starSysSO.FirstOwner;
    }
    public StarSysData(string v)
    {
        this.v = v;
        this.sysName = v;
    }
    public int GetStarSysInt()
    {
        return this.starSysInt;
    }
    public Vector3 GetPosition()
    {
        return this.position;
    }
    public string GetSysName() { return this.sysName; }
    public CivEnum GetFirstOwner() { return this.firstOwner; }
}
