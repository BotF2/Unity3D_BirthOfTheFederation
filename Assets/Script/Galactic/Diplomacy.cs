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
    public TrekEventSO onRaceDiscovered;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void OnRaceDiscovered(string raceName, string raceDescrition)
    {
        onRaceDiscovered.Raise(this, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
