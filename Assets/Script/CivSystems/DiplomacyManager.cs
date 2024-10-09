using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System;

public enum DiplomacyStatusEnum
{
    TotalWar = 0,
    ColdWar = 200,
    Neutral = 400,
    Friendly = 600,
    Allied = 800,
    Unified = 1000,
}
public class DiplomacyManager : MonoBehaviour
{
    public static DiplomacyManager Instance;
    public List<DiplomacyController> ManagersDiplomacyControllerList;
    public GameObject diplomacyPrefab;
    public GameObject diplomacyUIGO;


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
    public void InstantiateDipolmacy(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        GameObject DiplomacyNewGameOb = (GameObject)Instantiate(diplomacyPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
        var diplomacyController = DiplomacyNewGameOb.GetComponent<DiplomacyController>();
        DiplomacyData ourDiplomacyData = new DiplomacyData();
        diplomacyController.DiplomacyData = ourDiplomacyData;
        diplomacyController.DiplomacyData.CivOne = civPartyOne;
        diplomacyController.DiplomacyData.CivTwo = civPartyTwo;
        ManagersDiplomacyControllerList.Add(diplomacyController);
        diplomacyController.FirstContact(civPartyOne, civPartyTwo, hitGO);
        DiplomacyUIManager.Instance.LoadDiplomacyUI();
        
    }
    public void DoDiplomacy(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        foreach (var diplomacyController in ManagersDiplomacyControllerList)
        {
            DiplomacyData ourDiplomacyData = new DiplomacyData();
            if (diplomacyController.DiplomacyData.CivOne == civPartyOne && diplomacyController.DiplomacyData.CivTwo == civPartyTwo || diplomacyController.DiplomacyData.CivTwo == civPartyOne && diplomacyController.DiplomacyData.CivOne == civPartyTwo)
            {
                diplomacyController.DiplomaticContact(civPartyOne, civPartyTwo);
                break;
            }
            else

            break;
            //if (!civPartyOne.CivData.CivControllersWeKnow.Contains(civPartyTwo))
            //{
            //    FirstContact(civPartyOne, civPartyTwo);
            //}
        }
        InstantiateDipolmacy(civPartyOne, civPartyTwo, hitGO);
    }
}
    

