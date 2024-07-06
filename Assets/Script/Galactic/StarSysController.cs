using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class StarSysController : MonoBehaviour
{
    //Fields
    private StarSysData starSysData;
    public StarSysData StarSysData { get { return starSysData; } set { starSysData = value; } }
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas canvasToolTip;
    
    public StarSysController(string name)
    {
        StarSysData = new StarSysData(name);
    }
    private void Start()
    {
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
        canvasToolTip.worldCamera = galaxyEventCamera;
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
        //string goName;
        Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            //goName = hitObject.name;
            if (hitObject == gameObject)
            {
                StarSysUIManager.instance.LoadStarSysUI(gameObject);
                //PopulateShipDropdown();
                //stationaryState = new FleetStationaryState(hitObject);
                //warpState = new FleetWarpState(hitObject, this.FleetData.Destination, rb, this.FleetData.CurrentWarpFactor);
            }
        }

    }
}
