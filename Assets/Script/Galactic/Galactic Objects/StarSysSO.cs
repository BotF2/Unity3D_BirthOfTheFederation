using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Galaxy/StarSysSO")]
public class StarSysSO : ScriptableObject
{
    public int StarSysInt;
    //public int _x;
    //public int _y;
    //public int _z;
    public Vector3 Position;
    public string SysName;
    public CivEnum FirstOwner;
    public CivEnum CurrentOwner;
    public GalaxyObjectType StarType;
    public Sprite StarSprit;
    public int Population;
    public int PopulationLimit;
    public int Farms;
    public int PowerStations;
    public int Factories;  
    public int ResearchCenters;
    public int Shipyards;
    public int ShieldGenerators;
    public int OrbitalBatteries;
    public string Description;
    private string v;
    public Sprite powerPlantSprite;
    public Sprite factorySprite;
    public Sprite shipyardSprite;
    public Sprite shieldSprite;
    public Sprite orbitalSprite;
    public Sprite researchCenterSprite;
}
