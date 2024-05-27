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
    public void UpdateOwner(CivEnum newOwner) 
    {
        starSysData.CurrentOwner = newOwner;
    }

}
