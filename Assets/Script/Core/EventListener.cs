using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class EventListener : MonoBehaviour
{
    void Start()
    {
        // Subscribe to the OnSpecialEventReached event
        TimeManager.instance.OnSpecialEventReached += HandleSpecialEvent;
    }

    // Method to handle special events
    private void HandleSpecialEvent(TrekEventSO specialEvent)
    {
        if (specialEvent != null)
        {
            Debug.Log("Special event reached: " + specialEvent.eventName + " on stardate " + 
                specialEvent.stardate + " TrekEventType: " + specialEvent.trekEventType + 
                " parameter: " + specialEvent.eventParameter);
            // Add your logic to handle the special event here
            switch (specialEvent.trekEventType)
            {
                case TrekEventType.RaceDiscovered:
                    {
                        //RaceDsicovered.Diplomacy();
                        break;
                    }
                case TrekEventType.TechDiscovered:
                    {
                        break;
                    }
                case TrekEventType.Invasion:
                    {                   
                        break;
                    }
                case TrekEventType.GiftReceived:
                    {
                        break;
                    }
                case TrekEventType.DisasterEvent:
                    {
                        break;
                    }
                case TrekEventType.CombatEvent:
                    {
                        break;
                    }
                case TrekEventType.RemoveTempTargets:
                    {
                        RemoveTempTargets();
                        break;
                    }
                default:
                    break;
            }
        }
    }
    void RemoveTempTargets()
    {
        //foreach(var gameObj in GameObject.FindGameObjectsWithTag("DestroyTemp"))
        //{
        //    Destroy(gameObj);
        //}
    }
    // OnDestroy is called when the GameObject is destroyed
    private void OnDestroy()
    {
        // Unsubscribe from the OnSpecialEventReached event
        TimeManager.instance.OnSpecialEventReached -= HandleSpecialEvent;
    }
}
