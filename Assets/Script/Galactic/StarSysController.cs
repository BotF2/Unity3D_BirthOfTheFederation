using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Core
{
    public class StarSysController : MonoBehaviour
    {
        /// <summary>
        /// Controlling Star System interactions while the matching StarSystemData class
        /// holds key info on status and for save game
        /// </summary>
        //Fields
        private StarSysData starSysData;
        public StarSysData StarSysData { get { return starSysData; } set { starSysData = value; } }
        private Camera galaxyEventCamera;
        public Canvas OurSelectedMarkerCanvas;
        [SerializeField]
        private Canvas canvasToolTip;
        [SerializeField]
        private Canvas canvasStarSysUI;
        public static event Action<TrekRandomEventSO> TrekEventDisasters;
        //public TrekRandomEventSO trekEventSO;


        public StarSysController(string name)
        {
            StarSysData = new StarSysData(name);
        }
        private void Start()
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            canvasToolTip.worldCamera = galaxyEventCamera;
            OurSelectedMarkerCanvas.gameObject.SetActive(false);
            var CanvasGO = GameObject.Find("CanvasStarSysUI");
            canvasStarSysUI = CanvasGO.GetComponent<Canvas>();
            canvasStarSysUI.worldCamera = galaxyEventCamera;
            TimeManager.Instance.OnRandomSpecialEvent = DoDisaster;
        }
        public void UpdatePopulation(int delatPopulation)
        {
            if (starSysData.Population + delatPopulation < 0)
                starSysData.Population = 0;
            else
                starSysData.Population += delatPopulation;// population delta code, starSysData += xyz things happen;
        }
        public void UpdateOwner(CivEnum newOwner) // system captured or colonized
        {
            starSysData.CurrentOwner = newOwner;
        }
        private void OnMouseDown()
        {

            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                // what a Star System Controller does with a hit
                if (this.StarSysData.CurrentOwner == CivManager.Instance.LocalPlayerCivEnum)
                {
                    if (FleetUIManager.Instance.MouseClickSetsDestination == false)
                    {
                        StarSysUIManager.Instance.LoadStarSysUI(gameObject);
                    }
                    else
                    {
                        SetNewDestination(hitObject);
                    }
                }
                else if (FleetUIManager.Instance.MouseClickSetsDestination == true)
                {
                    SetNewDestination(hitObject);
                }
            }
        }
        private void SetNewDestination(GameObject hitObject) 
        {
            FleetUIManager.Instance.TurnOffCurrentDestination();
            FleetUIManager.Instance.SetAsDestination(hitObject);
            this.OurSelectedMarkerCanvas.gameObject.SetActive(true);
        }

        public void OnEnable()
        {
            TimeManager.Instance.OnRandomSpecialEvent += DoDisaster;
        }
        public void OnDisable()
        {
            TimeManager.Instance.OnRandomSpecialEvent -= DoDisaster;
        }
        private void DoDisaster(TrekRandomEventSO specialEvent)
        {
            if (specialEvent != null)
            {
                Debug.Log("Special event reached StarSystemController: " + specialEvent.eventName + " on oneInXChance " +
                    specialEvent.oneInXChance + " TrekRandomEvents: " + specialEvent.trekEventType +
                    " parameter: " + specialEvent.eventParameter);
                // Add your logic to handle the special event here
                switch (specialEvent.trekEventType)
                {
                    case TrekRandomEvents.AsteroidHit:
                        {
                            // ToDo: Do Disaster code for each disaster 
                            Debug.Log("******** Asteroid ***********"); ;
                            break;
                        }
                    case TrekRandomEvents.Pandemic:
                        {
                            Debug.Log("********** PANDEMIC **********");
                            break;
                        }
                    case TrekRandomEvents.SuperVolcano:
                        {
                            Debug.Log("********** SUPER VOLCANO **********");
                            break;
                        }
                    case TrekRandomEvents.GamaRayBurst:
                        {
                            Debug.Log("********** GAMERAY BURST **********");
                            break;
                        }
                    case TrekRandomEvents.SeismicEvent:
                        {
                            Debug.Log("********** SEISMEIC EVENT **********");
                            break;
                        }
                    case TrekRandomEvents.Teribals:
                        {
                            Debug.Log("********** TERIBAL TROUBLE **********");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
