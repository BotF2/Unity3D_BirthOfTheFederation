using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Assets.Core
{
    public class EventListener : MonoBehaviour
    {
    //    void Start()
    //    {
    //        // Subscribe to the OnSpecialEventReached event
    //        TimeManager.instance.OnSpecialEventReached += HandleSpecialEvent;
    //    }
    //    // OnDestroy is called when the GameObject is destroyed
    //    private void OnDestroy()
    //    {
    //        // Unsubscribe from the OnSpecialEventReached event
    //        TimeManager.instance.OnSpecialEventReached -= HandleSpecialEvent;
    //    }
    //    // Method to handle special events
    //    private void HandleSpecialEvent(TrekEventSO specialEvent)
    //    {
    //        if (specialEvent != null)
    //        {
    //            Debug.Log("Special event reached: " + specialEvent.eventName + " on stardate " +
    //                specialEvent.stardate + " TrekEventType: " + specialEvent.trekEventType +
    //                " parameter: " + specialEvent.eventParameter);
    //            // Add your logic to handle the special event here
    //            switch (specialEvent.trekEventType)
    //            {
    //                case TrekEventType.AsteroidHit:
    //                    {
    //                        //CALL METHOD FOR ASTEROID HIT HERE
    //                        Debug.Log("******** Asteroid ***********"); ;
    //                        break;
    //                    }
    //                case TrekEventType.Pandemic:
    //                    {
    //                        Debug.Log("********** PANDEMIC **********");
    //                        break;
    //                    }
    //                case TrekEventType.SuperVolcano:
    //                    {
    //                        Debug.Log("********** SUPER VOLCANO **********");
    //                        break;
    //                    }
    //                case TrekEventType.GamaRayBurst:
    //                    {
    //                        Debug.Log("********** GAMERAY BURST **********");
    //                        break;
    //                    }
    //                case TrekEventType.SeismicEvent:
    //                    {
    //                        Debug.Log("********** SEISMEIC EVENT **********");
    //                        break;
    //                    }
    //                case TrekEventType.Tribles:
    //                    {
    //                        break;
    //                    }
    //                case TrekEventType.RemoveTempTargets:
    //                    {
    //                        Debug.Log("********** REMOVE TEMP TARGET **********");
    //                        RemoveTempTargets();
    //                        break;
    //                    }
    //                default:
    //                    break;
    //            }
    //        }
    //    }
    //    void RemoveTempTargets()
    //    {
    //        //Debug.Log("TimeManager at 1011");
    //        //foreach (var gameObj in GameObject.FindGameObjectsWithTag("DestroyTemp"))
    //        //{
    //        //    Destroy(gameObj);
    //        //}
    //    }

    }
}
