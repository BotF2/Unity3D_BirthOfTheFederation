using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Core;
using System;
using Unity.VisualScripting;
using System.Diagnostics;
using UnityEngine.Rendering;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using UnityEngine.PlayerLoop;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class DiplomacyUIManager: MonoBehaviour
{
    public static DiplomacyUIManager Instance;
    private Camera galaxyEventCamera;
    [SerializeField]
    private Canvas parentCanvas;

    public GameObject diplomacyUIRoot;// GameObject controlles this active UI on/off

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        diplomacyUIRoot.SetActive(false);
        galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        parentCanvas.worldCamera = galaxyEventCamera;
    }

    public void LoadDiplomacyUI()
    {
        TimeManager.Instance.PauseTime();
        StarSysUIManager.Instance.CloseUnLoadStarSysUI();
        FleetUIManager.Instance.CloseUnLoadFleetUI();
        FleetSelectionUI.Instance.UnLoadShipManagerUI();
        diplomacyUIRoot.SetActive(true);
    }
    public void CloseUnLoadFleetUI()
    {
       diplomacyUIRoot.SetActive(false);
       TimeManager.Instance.ResumeTime();    
    }
}
