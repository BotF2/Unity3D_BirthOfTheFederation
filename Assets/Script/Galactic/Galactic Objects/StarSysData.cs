using Assets.Core;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a type of galactic object that is a 'StarSystem' class (Manager/Controller/Data and can have a habitable 'planet') 
/// with a real star or a nebula or a complex as in the Borg Unicomplex)
/// Other galactic objects not described by StarSys (will have their own classes (ToDo: Managers/Controllers/Data) for stations (one class),
/// and blackholes/wormholes (one class.)
/// </summary>
public class StarSysData
{
    private int starSysInt;
    private Vector3 position;
    public GameObject SysGameObject;
    private string sysName;
    public string SysName { get { return sysName; } }
    private CivEnum firstOwner;
    public CivEnum CurrentOwner;
    public CivController CurrentCivController;
    public GalaxyObjectType SystemType;
    //public TechLevel is a civ level value, not a system data value.
    public int TechUnits; // ResearchCenters centers provide tech output units that determins progress to a civ level TechLevel enum.
    public Sprite StarSprit;
    public List<GameObject> PowerStations;
    public List<GameObject> Factories;
    public List<GameObject> FactoryBuildQueue;
    public List<GameObject> ResearchCenters;
    public List<GameObject> Shipyards;
    public List<ShipData> ShipyardQueue;
    public List<GameObject> PowerUnits;
    public List<GameObject> ShieldGenerators;
    public List<GameObject> OrbitalBatteries;
    public int TotalSysPowerLoad = 0;
    public PowerPlantData PowerPlantData;
    public FactoryData FactoryData;
    public ShipyardData ShipyardData;
    public ShieldGeneratorData ShieldGeneratorData;
    public OrbitalBatteryData OrbitalBatteryData;
    public ResearchCenterData ResearchCenterData;
    


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
    //    civCon = CivManager.current.GetCivControllerByEnum(civEnum);

    //    return civCon;
    //}
}
