using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResearchCenterData
{
    public int CivInt;
    public TechLevel TechLevel;
    public StarSysFacilities FacilitiesEnumType; // what type are we
    public string Name;
    public int StartStarDate; //start to build this factory in an existing factory queue
    public int BuildDuration;// duration to build can be reduced by number and output of factories
    public int PowerLoad;
    public Sprite ResearchCenterSprite;
    public string Description;
    private string v;
    public GameObject SysGameObject;
    public ResearchCenterData(string v)
    {
        this.v = v;
        this.Name = v;
    }
}
