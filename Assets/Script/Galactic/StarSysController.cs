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
    public static event Action<TrekEventSO> trekEventDisasters;
    //public TrekEventSO trekEventSO;


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
        TimeManager.instance.onSpecialEventReached = DoDisaster;  
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
        TimeManager.instance.onSpecialEventReached += DoDisaster;
    }
    public void OnDisable()
    {
        TimeManager.instance.onSpecialEventReached -= DoDisaster;
    }
    private void DoDisaster(TrekEventSO specialEvent)
    {
        if (specialEvent != null)
        {
            Debug.Log("Special event reached StarSystemController: " + specialEvent.eventName + " on stardate " +
                specialEvent.stardate + " TrekEventType: " + specialEvent.trekEventType +
                " parameter: " + specialEvent.eventParameter);
            // Add your logic to handle the special event here
            switch (specialEvent.trekEventType)
            {
                case TrekEventType.AsteroidHit:
                    {
                        // ToDo: Do Disaster code for each disaster 
                        Debug.Log("******** Asteroid ***********"); ;
                        break;
                    }
                case TrekEventType.Pandemic:
                    {
                        Debug.Log("********** PANDEMIC **********");
                        break;
                    }
                case TrekEventType.SuperVolcano:
                    {
                        Debug.Log("********** SUPER VOLCANO **********");
                        break;
                    }
                case TrekEventType.GamaRayBurst:
                    {
                        Debug.Log("********** GAMERAY BURST **********");
                        break;
                    }
                case TrekEventType.SeismicEvent:
                    {
                        Debug.Log("********** SEISMEIC EVENT **********");
                        break;
                    }
                case TrekEventType.Teribals:
                    {
                        Debug.Log("********** TERIBAL TROUBLE **********");
                        break;
                    }
                case TrekEventType.RemoveTempTargets:
                    {
                        Debug.Log("********** REMOVE TEMP TARGET **********");
                        break;
                    }
                default:
                    break;
            }
        }
    }

}
