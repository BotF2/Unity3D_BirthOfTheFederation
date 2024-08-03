using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System;

public class StarSysController : MonoBehaviour
{
    //Fields
    private StarSysData starSysData;
    public StarSysData StarSysData { get { return starSysData; } set { starSysData = value; } }
    private Camera galaxyEventCamera;
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
        var CanvasGO = GameObject.Find("CanvasStarSysUI");
        canvasStarSysUI = CanvasGO.GetComponent<Canvas>();
        canvasStarSysUI.worldCamera = galaxyEventCamera;
        TimeManager.instance.onRandomSpecialEvent = DoDisaster;  
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
          
            if (hitObject == gameObject)
            {
                StarSysUIManager.instance.LoadStarSysUI(gameObject);
            }
        }
    }
    public void OnEnable()
    {
        TimeManager.instance.onRandomSpecialEvent += DoDisaster;
    }
    public void OnDisable()
    {
        TimeManager.instance.onRandomSpecialEvent -= DoDisaster;
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
