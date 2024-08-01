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
        //public TrekRandomEventSO trekEventSO;
        public UnityEvent response; // link method calls in editor by setting to gameobject

        private void Start()
        {
            TimeManager.instance.onRandomSpecialEvent = Instance_OnRandomEvent;
        }
        private void OnEnable()
        {
            if(TimeManager.instance != null)
            TimeManager.instance.onRandomSpecialEvent += Instance_OnRandomEvent;
        }

        private void Instance_OnRandomEvent(TrekRandomEventSO specialEvent)
        {
            //if (specialEvent != null)
            //{
            //    Debug.Log("Special event reached: " + specialEvent.eventName + " on stardateRate " +
            //        specialEvent.stardateRate + " TrekRandomEvents: " + specialEvent.trekEventType +
            //        " parameter: " + specialEvent.eventParameter);
            //    // Add your logic to handle the special event here
            //    switch (specialEvent.trekEventType)
            //    {
            //        case TrekRandomEvents.AsteroidHit:
            //            {
            //                //CALL METHOD FOR ASTEROID HIT HERE
            //                Debug.Log("******** Asteroid ***********"); ;
            //                break;
            //            }
            //        case TrekRandomEvents.Pandemic:
            //            {
            //                Debug.Log("********** PANDEMIC **********");
            //                break;
            //            }
            //        case TrekRandomEvents.SuperVolcano:
            //            {
            //                Debug.Log("********** SUPER VOLCANO **********");
            //                break;
            //            }
            //        case TrekRandomEvents.GamaRayBurst:
            //            {
            //                Debug.Log("********** GAMERAY BURST **********");
            //                break;
            //            }
            //        case TrekRandomEvents.SeismicEvent:
            //            {
            //                Debug.Log("********** SEISMEIC EVENT **********");
            //                break;
            //            }
            //        case TrekRandomEvents.Teribals:
            //            {
            //                break;
            //            }
            //        case TrekRandomEvents.RemoveTempTargets:
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
            TimeManager.instance.onRandomSpecialEvent -= Instance_OnRandomEvent;
        }
        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke();
        }
    }
    
}

