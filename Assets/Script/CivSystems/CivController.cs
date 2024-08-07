using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class CivController : MonoBehaviour
    {
        //Fields
        public CivData CivData;
        public string CivShortName;
        public static event Action<TrekStardateEventSO> TrekEventStardate;
        //public List<CivController> CivContollersWeHave;
        //private List<CivController> civsControllerList;

        public CivController(string name)
        {
            CivShortName = name;
        }

        public void Start()
        {
            TimeManager.Instance.OnStardateSpecialEvent = DoStardateEvent;
        }
        public void UpdateCredits()
        {
            CivData.Credits += 50;
        }

        public void DoStardateEvent(TrekStardateEventSO specialEvent)
        {
            if (specialEvent != null)
            {
                Debug.Log("Special event reached StarSystemController: " + specialEvent.eventName + " on oneInXChance " +
                    specialEvent.stardate + " TrekRandomEvents: " + specialEvent.trekEventType +
                    " parameter: " + specialEvent.eventParameter);
                // Add your logic to handle the special event here
                switch (specialEvent.trekEventType)
                {
                    case TrekStardateEvents.FederartionEst:
                        {
                            // ToDo: Do Disaster code for each disaster 
                            Debug.Log("******** Fed Established ***********"); ;
                            break;
                        }
                    case TrekStardateEvents.RomulanNeutralZoneEst:
                        {
                            Debug.Log("********** Rom Neutral Zone established **********");
                            break;
                        }
                    case TrekStardateEvents.KhitomerRomulanAttack:
                        {
                            Debug.Log("********** Khitomer Rom Attack **********");
                            break;
                        }
                    case TrekStardateEvents.QandTheBorg:
                        {
                            Debug.Log("********** Q and Borg **********");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
