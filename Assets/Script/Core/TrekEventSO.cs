using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum TrekEventType { AsteroidHit, Pandemic, SuperVolcano, GamaRayBurst, SeismicEvent, Teribals, RemoveTempTargets }


[CreateAssetMenu(fileName = "New Trek Event", menuName = "Game Event/Trek Event")]
public class TrekEventSO : ScriptableObject
{
    public string eventName;
    public int stardate; // stardate of the event
    public TrekEventType trekEventType;

    public string eventParameter;

}
