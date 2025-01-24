using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShieldGeneratorData
{
    public int CivInt;
    public TechLevel TechLevel;
    public StarSysFacilities FacilitiesEnumType; // what type are we
    public string Name;
    public int StartStarDate; //start to build this Shield Gen in an existing factory queue
    public int BuildDuration;// duration to build can be reduced by number and output of factories
    public int PowerLoad;
    public Sprite ShieldGeneratorSprite;
    public string Description;
    private string v;
    public GameObject SysGameObject;
    public ShieldGeneratorData(string v)
    {
        this.v = v;
        this.Name = v;
    }
}
