using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Core
{
    [System.Serializable]
    public class CustomGameEvent : UnityEvent<Component, object>
    {
        
    }
    public class GameEventListener : MonoBehaviour
    {
        //public TrekEventSO trekEventSO;
        public UnityEvent response; // link method calls in editor by setting to gameobject

        private void Start()
        {
            TimeManager.instance.onSpecialEventReached = Instance_OnSpecialEventReached;
        }
        private void OnEnable()
        {
            if(TimeManager.instance != null)
            TimeManager.instance.onSpecialEventReached += Instance_OnSpecialEventReached;
        }

        private void Instance_OnSpecialEventReached(TrekEventSO specialEvent)
        {
            //if (specialEvent != null)
            //{
            //    Debug.Log("Special event reached: " + specialEvent.eventName + " on stardate " +
            //        specialEvent.stardate + " TrekEventType: " + specialEvent.trekEventType +
            //        " parameter: " + specialEvent.eventParameter);
            //    // Add your logic to handle the special event here
            //    switch (specialEvent.trekEventType)
            //    {
            //        case TrekEventType.AsteroidHit:
            //            {
            //                //CALL METHOD FOR ASTEROID HIT HERE
            //                Debug.Log("******** Asteroid ***********"); ;
            //                break;
            //            }
            //        case TrekEventType.Pandemic:
            //            {
            //                Debug.Log("********** PANDEMIC **********");
            //                break;
            //            }
            //        case TrekEventType.SuperVolcano:
            //            {
            //                Debug.Log("********** SUPER VOLCANO **********");
            //                break;
            //            }
            //        case TrekEventType.GamaRayBurst:
            //            {
            //                Debug.Log("********** GAMERAY BURST **********");
            //                break;
            //            }
            //        case TrekEventType.SeismicEvent:
            //            {
            //                Debug.Log("********** SEISMEIC EVENT **********");
            //                break;
            //            }
            //        case TrekEventType.Teribals:
            //            {
            //                break;
            //            }
            //        case TrekEventType.RemoveTempTargets:
            //            {
            //                Debug.Log("********** REMOVE TEMP TARGET **********");
            //                break;
            //            }
            //        default:
            //            break;
            //    }
            //}
        }

        private void OnDisable()
        {
            TimeManager.instance.onSpecialEventReached -= Instance_OnSpecialEventReached;
        }
        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke();
        }
    }
    
}

