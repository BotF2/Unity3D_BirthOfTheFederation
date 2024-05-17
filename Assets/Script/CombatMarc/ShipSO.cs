using UnityEngine;
using Assets.Core;

[CreateAssetMenu(fileName = "ShipSO", menuName = "ShipSO", order = 1)]
public class ShipSO : ScriptableObject
{
    public string ShipName;
    public CivEnum CivEnum;
    public TechLevel Tech;
    public ShipType Class;
    public int ShieldMaxHealth;
    public int HullMaxHealth;
    public int TorpedoDamage;
    public int BeamDamage;
    public int Cost;
    public GameObject Mesh;

}
