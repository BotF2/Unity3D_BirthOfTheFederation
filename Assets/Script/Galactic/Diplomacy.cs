using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diplomacy : MonoBehaviour
{
    private CivEnum us;
    private CivEnum them;
    public string raceName;
    public string raceDescription;
    [Header("Events")]
    public TrekRandomEventSO onRaceDiscovered;

    void Awake()
    {
        
    }

    //private void OnRaceDiscovered(string raceName, string raceDescrition)
    //{
    //    onRaceDiscovered.Raise(this, this);
    //}

    void Update()
    {
        
    }
}
