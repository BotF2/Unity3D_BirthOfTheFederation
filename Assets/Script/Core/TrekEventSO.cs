using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public enum TrekEventType { RaceDiscovered, TechDiscovered, Invasion, GiftReceived, DisasterEvent }


[CreateAssetMenu(fileName = "New Trek Event", menuName = "Star Trek/Trek Event")]
public class TrekEventSO : ScriptableObject
{
    public string eventName;
    public int stardate; // stardate of the event

    public TrekEventType trekEventType;

    public string eventParameter;
}
