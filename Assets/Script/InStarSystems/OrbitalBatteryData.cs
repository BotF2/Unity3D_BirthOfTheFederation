using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

[System.Serializable]
public class OrbitalBatteryData
{
    public int CivInt;
    public TechLevel TechLevel;
    public StarSysFacilities FacilitiesEnumType;
    public string Name;
    public int StartStarDate; //start to build in factory queue
    public int BuildDuration;// duration to build can be reduced by number and output of factories
    public int PowerLoad;
    public Sprite OrbitalBatterySprite;
    public string Description;
    private string v;
    public GameObject SysGameObject;

    public OrbitalBatteryData(string v)
    {
        this.v = v;
        this.Name = v;
    }
}
