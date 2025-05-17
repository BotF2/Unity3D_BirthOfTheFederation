using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

/// <summary>
/// This is a type of star system asset/object that can be built in the factory queue.
/// </summary>
[System.Serializable]
public class FactoryData // uses StarSysController and StarSysManager
{
    public int CivInt;
    public TechLevel TechLevel;
    public StarSysFacilities FacilitiesEnumType; // what type are we
    public string Name;
    public int StartStarDate; //start to build this factory in an existing factory queue
    public int BuildDuration;// duration to build can be reduced by number and output of factories
    public int PowerLoad;
    public Sprite FactorySprite;
    public string Description;
    private string v;
    public GameObject SysGameObject;
    public FactoryData(string v)
    {
        this.v = v;
        this.Name = v;
    }
}
