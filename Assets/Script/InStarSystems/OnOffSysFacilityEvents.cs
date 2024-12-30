using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnOffSysFacilityEvents : MonoBehaviour
{
    public static OnOffSysFacilityEvents current;

    public Action<StarSysController,string> FacilityOnClick; 

    private void Awake()
    {
        if (current != null) { Destroy(gameObject); }
        else
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        FacilityOnClick += DoFacilityOn;
    }
    public void DoFacilityOn(StarSysController sysCon, string name)
    {
        if (FacilityOnClick != null)
        {
            FacilityOnClick?.Invoke(sysCon,name);
        }
    }
}


