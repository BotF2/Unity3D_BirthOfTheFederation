using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    public string ShipName;
    public CivEnum CivEnum;
    public TechLevel TechLevel;
    public ShipType ShipType;
    public Sprite ShipSprite;
    public float maxWarpFactor;
    //public float currentWarpFactor;
    public int ShieldMaxHealth;
    public int HullMaxHealth;
    public int TorpedoDamage;
    public int BeamDamage;
    public int Price;
    public GameObject Mesh;

    void Start()
    {
        //little code change :D
        Debug.Log("asdf");
    }
    public ShipData(string name)
    {
        ShipName = name;
    }
    public ShipData()
    {

    }
}
