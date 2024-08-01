using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum TrekStardateEvents { QandTheBorg, FederartionEst, RomulanNeutralZoneEst, KhitomerRomulanAttack }

[CreateAssetMenu(menuName = "Game Event/Stardate Trek Event")]
public class TrekStardateEventSO : ScriptableObject
{
    public string eventName;
    public int stardate; // stardateRate of the event
    public TrekStardateEvents trekEventType;

    public string eventParameter;
}
