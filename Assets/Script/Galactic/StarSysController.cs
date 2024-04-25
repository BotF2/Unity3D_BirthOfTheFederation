using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class StarSysController : MonoBehaviour
{
    //Fields
    public StarSysData starSysData;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas canvasSysButton;

    private void Start()
    {
        galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>() as Camera;
        canvasSysButton.worldCamera = galaxyEventCamera;
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
