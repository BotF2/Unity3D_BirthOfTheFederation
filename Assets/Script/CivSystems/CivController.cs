using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Core
{
    /// <summary>
    /// Controlling a Civilization(faction) while the matching CivData class
    /// holds key info on status and for save game
    /// </summary>
    public class CivController : MonoBehaviour
    {
        //Fields
        public CivData CivData;
        public string CivShortName;
        public static event Action<TrekStardateEventSO> TrekEventStardate;
        private DiplomacyManager diplomacyManager;

        public void Start()
        {
            TimeManager.Instance.OnStardateSpecialEvent = DoStardateEvent;
            diplomacyManager = DiplomacyManager.Instance;
        }
        public void UpdateCredits()
        {
            CivData.Credits += 50;
        }
        //public void Diplomacy(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
        //{
        //    if (!civPartyOne.CivData.CivControllersWeKnow.Contains(civPartyTwo))
        //    {
        //        FirstContact(civPartyOne, civPartyTwo, hitGO);
        //    }
        //}
        //private void FirstContact(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
        //{
        //    civPartyOne.CivData.AddToCivControllersWeKnow(civPartyTwo);
        //    civPartyTwo.CivData.AddToCivControllersWeKnow(civPartyOne);
        //    ResetSprites(civPartyTwo, hitGO);
        //    ResetNames(civPartyTwo, hitGO);
        //    // ToDo: Update the system name and/or the fleet name/insignia;

        //}
        public void ResetSprites(CivController civPartyTwo, GameObject hitGO)
        {
            var gOs = hitGO.GetComponentsInChildren<RectTransform>(true);
            foreach (var gO in gOs)
            {
                gO.gameObject.SetActive(true);
            }
            var Renderers = hitGO.GetComponentsInChildren<SpriteRenderer>();
            foreach (var oneRenderer in Renderers)
            {
                if (oneRenderer != null)
                {
                    if(oneRenderer.name == "InsigniaUnknown")
                        oneRenderer.gameObject.SetActive(false);
                    if (oneRenderer.name == "StarSprite")
                        oneRenderer.sprite = hitGO.GetComponent<StarSysController>().StarSysData.StarSprit;
                }
            }
        }
        public void ResetNames(CivController civPartyTwo, GameObject hitGO)
        {
            TextMeshProUGUI[] TheText = hitGO.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var OneTMPtest in TheText)
            {
                OneTMPtest.enabled = true;
                if (OneTMPtest != null && OneTMPtest.name == "SysName (TMP)")
                {
                    if (hitGO.GetComponent<StarSysController>().StarSysData.CurrentOwner != CivManager.Instance.LocalPlayerCivEnum)

                     OneTMPtest.text = hitGO.GetComponent<StarSysController>().StarSysData.SysName;
                }
                else if (OneTMPtest != null && OneTMPtest.name == "SysDescription (TMP)")
                    OneTMPtest.text = hitGO.GetComponent<StarSysController>().StarSysData.Description;

            }
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
                            Debug.Log("******** FedLocalPalyerToggle Established ***********"); ;
                            break;
                        }
                    case TrekStardateEvents.RomulanNeutralZoneEst:
                        {
                            Debug.Log("********** RomLocalPlayerToggle Neutral Zone established **********");
                            break;
                        }
                    case TrekStardateEvents.KhitomerRomulanAttack:
                        {
                            Debug.Log("********** Khitomer RomLocalPlayerToggle Attack **********");
                            break;
                        }
                    case TrekStardateEvents.QandTheBorg:
                        {
                            Debug.Log("********** Q and BorgLocalPlayerToggle **********");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
