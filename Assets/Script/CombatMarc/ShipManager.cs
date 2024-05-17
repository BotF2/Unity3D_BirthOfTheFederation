using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;
    public ShipData shipData;
    public GameObject ShipPrefab;
    public List<ShipSO> ShipSOList;// all possible ShipSO(s), one list for each civ

    public Dictionary<CivEnum, List<ShipData>> ShipDictionary; //all the fleets of that civ

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        shipData = gameObject.AddComponent<ShipData>();
        shipData.shipName = "999";
        List <ShipData> list = new List<ShipData>() { shipData };
        ShipDictionary = new Dictionary<CivEnum, List<ShipData>>() { { CivEnum.ZZUNINHABITED9, list } };
    }
}
