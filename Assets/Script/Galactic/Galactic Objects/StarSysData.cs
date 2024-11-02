using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class StarSysData
{
    /// <summary>
    /// This is a type of galactic object that is a 'StarSystem' class (Manager/Controller/Data and can have a habitable 'planet') 
    /// with a real star or a nebula or a complex as in the Borg Unicomplex)
    /// Other galactic objects not described by StarSys (will have their own classes (ToDo: Managers/Controllers/Data) for stations (one class),
    /// and blackholes/wormholes (one class.)
    /// </summary>
    private int starSysInt;
    private Vector3 position;
    public GameObject SysGameObject;
    private string sysName;
    public string SysName { get { return sysName; } }
    private CivEnum firstOwner;
    public CivEnum CurrentOwner;
    public CivController CurrentCivController;
    public GalaxyObjectType SystemType;
    public Sprite StarSprit;
    public int Population;
    public int PopulationLimit; 
    public int Farms;
    public int PowerStations;
    public int Factories;
    public int Research;
    public int food;
    public int power;
    public int production;
    public int tech;
    public string Description;
    private string v;

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
    //public CivController GetCivController(CivEnum civEnum)
    //{
    //    CivController civCon = new CivController("null");
    //    civCon = CivManager.Instance.GetCivControllerByEnum(civEnum);

    //    return civCon;
    //}
}
