using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    public string shipName;
    public float maxWarpFactor;

    void Start()
    {
        //little code change :D
        Debug.Log("asdf");
    }
    public ShipData(string name)
    {
        shipName = name;
    }
    public ShipData()
    {

    }
}
