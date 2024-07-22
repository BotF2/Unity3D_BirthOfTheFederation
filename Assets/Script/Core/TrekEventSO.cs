using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum TrekEventType { RaceDiscovered, TechDiscovered, Invasion, GiftReceived, DisasterEvent, CombatEvent, RemoveTempTargets, }


[CreateAssetMenu(fileName = "New Trek Event", menuName = "Game Event/Trek Event")]
public class TrekEventSO : ScriptableObject
{
    public string eventName;
    public int stardate; // stardate of the event
    public List<GameEventListener> listeners = new List<GameEventListener>();
    public TrekEventType trekEventType;

    public string eventParameter;

    public void Raise(Component sender, Object data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }
    public void RegisterListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);  
        }
    }
    public void UnregisterListener(GameEventListener listener)
    {if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }

    }
}
