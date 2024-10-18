using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System;

public enum DiplomacyStatusEnum
{
    TotalWar = 0,
    ColdWar = 20,
    Hostile = 40,
    Neutral = 50,
    Friendly = 60,
    Allied = 80,
    Unified = 100
}
public enum CivTraitsEnum
{
    Scientific,
    Materialistic,
    Fanatic,
    Xenophobia,
    Indifferent,
    Compassion,
    Honorable,
    Ruthless,
    Null
}
public enum WarLike
{
    FireAllWeapons = 0, // intended for 'will come out shooting' on contact without stopping for diplomacy, as in the Borg
    WarLike, // inclined to wars like civs, the Klingons but do not bypass diplomacy UI
    Hostile,
    Neutral,
    Friendly,
    PeaceLoving // will give 'ground' to keep the peace. appeasement
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
        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum) ||
            GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
            DiplomacyUIManager.Instance.LoadDiplomacyUI(diplomacyController);
        else DoDiplomacyForAI(civPartyOne, civPartyTwo, hitGO);
        
    }
    private void DoDiplomacyForAI(CivController civOne, CivController civTwo, GameObject weHitGO)
    {
        //Do some diplomacy without a UI by/for either civ
    }
    public void DoDiplomacy(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        if (ManagersDiplomacyControllerList.Count == 0) 
        {
            //first contact of game
            InstantiateDipolmacy(civPartyOne, civPartyTwo, hitGO);
        }
        foreach (var diplomacyController in ManagersDiplomacyControllerList)
        {
            if (diplomacyController != null)
            {
                if (diplomacyController.DiplomacyData.CivOne == civPartyOne && diplomacyController.DiplomacyData.CivTwo == civPartyTwo || diplomacyController.DiplomacyData.CivTwo == civPartyOne && diplomacyController.DiplomacyData.CivOne == civPartyTwo)
                {
                    diplomacyController.NextDiplomaticContact(diplomacyController);
                    break;
                }
                else
                {
                    // This is frist contact
                    InstantiateDipolmacy(civPartyOne, civPartyTwo, hitGO);
                    break;
                }

            }
            else
            {
                // This is frist contact
                InstantiateDipolmacy(civPartyOne, civPartyTwo, hitGO);
            }
            break;
        }
    }
}
    

