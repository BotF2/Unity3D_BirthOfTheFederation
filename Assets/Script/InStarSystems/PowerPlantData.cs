using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerPlantData // uses StarSysController and StarSysManager
{
    public int CivInt;
    public TechLevel TechLevel;
    public StarSysFacilities FacilitiesEnumType;
    public string Name;
    public int StartStarDate; //start to build in factory queue
    public int BuildDuration;// duration to build can be reduced by number and output of factories
    public int PowerOutput;
    public Sprite PowerPlantSprite;
    public string Description;
    public bool On;
    private string v;
    public GameObject SysGameObject;

    public PowerPlantData(string v)
    {
        this.v = v;
        this.Name = v;
    }
}
