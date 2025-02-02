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
    [SerializeField]
    private Sprite powerPlant;
    //[SerializeField]
    //private Sprite powerPlantBGroud;
    [SerializeField]
    private Sprite factory;
    //[SerializeField]
    //private Sprite factoryBGround;
    [SerializeField]
    private Sprite shipyard;
    //[SerializeField]
    //private Sprite shipyardBGround;
    [SerializeField]
    private Sprite shield;
    //[SerializeField]
    //private Sprite shieldBGround;
    [SerializeField]
    private Sprite orbital;
    //[SerializeField]
    //private Sprite orbitalBGround;
    [SerializeField]
    private Sprite researchCenter;
    //[SerializeField]
    //private Sprite researchCenterBGround;
}
