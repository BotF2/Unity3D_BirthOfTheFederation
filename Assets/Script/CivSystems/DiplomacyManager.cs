using Assets.Core;
using System.Collections.Generic;
using UnityEngine;


public enum DiplomacyStatusEnum // between two civs held in the DiplomacyData
{
    TotalWar = 0,
    ColdWar = 20,
    Hostile = 40,
    Neutral = 50,
    Friendly = 60,
    Allied = 80,
    Unified = 100
}
public enum CivTraitsEnum // held by the civ
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
public enum WarLikeEnum // held by the civ
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
    [SerializeField]
    private Canvas canvasTherStarSysUI;


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
    public void InstantiateDipolmacyController(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        // instantiation is frist contact
        GameObject DiplomacyNewGameOb = (GameObject)Instantiate(diplomacyPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
        var diplomacyController = DiplomacyNewGameOb.GetComponent<DiplomacyController>();
        diplomacyController.areWePlaceholder = false;
        DiplomacyData ourDiplomacyData = new DiplomacyData();
        diplomacyController.DiplomacyUICanvas = canvasTherStarSysUI;
        diplomacyController.DiplomacyData = ourDiplomacyData;
        diplomacyController.DiplomacyData.CivOne = civPartyOne;
        diplomacyController.DiplomacyData.CivTwo = civPartyTwo;
        // ToDo: use traits to set diplomacy relation out of the gate and for AI actions
        diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 50;
        ManagersDiplomacyControllerList.Add(diplomacyController);
        diplomacyController.FirstContact(civPartyOne, civPartyTwo, hitGO);
        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum) ||
            GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
            TheirSysDiplomacyUIManager.Instance.LoadTheirSysDiplomacyUI(diplomacyController);
        //else if //*********check for human non-local palyers needing to do diplomacy in their UI ()
        //{
        //    //do Remote human player diplomacy
        //}
        else DoDiplomacyForAI(civPartyOne, civPartyTwo, hitGO);

    }
    private DiplomacyController InstantiatePlaceHolder(CivController civPartyOne, CivController civPartyTwo)
    {
        GameObject DiplomacyNewGameOb = (GameObject)Instantiate(diplomacyPrefab, new Vector3(0, 0, 0),
        Quaternion.identity);
        var diplomacyController = DiplomacyNewGameOb.GetComponent<DiplomacyController>();
        diplomacyController.areWePlaceholder = true;
        return diplomacyController;
    }
    private void DoDiplomacyForAI(CivController civOne, CivController civTwo, GameObject weHitGO)
    {
        //Do some diplomacy without a UI by/for either civ
    }
    public void FistContactDiplomacy(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        if (!FoundADiplomacyController(civPartyOne, civPartyTwo, hitGO))
        {
            //first contact of game
            InstantiateDipolmacyController(civPartyOne, civPartyTwo, hitGO);
        }
    }
    private bool FoundADiplomacyController(CivController civPartyOne, CivController civPartyTwo, GameObject hitGO)
    {
        bool found = false;
        foreach (var diplomacyController in ManagersDiplomacyControllerList)
        {
            if (diplomacyController != null)
            {
                if (diplomacyController.DiplomacyData.CivOne == civPartyOne && diplomacyController.DiplomacyData.CivTwo == civPartyTwo || diplomacyController.DiplomacyData.CivTwo == civPartyOne && diplomacyController.DiplomacyData.CivOne == civPartyTwo)
                {
                    found = true;
                    break;
                }
            }
        }
        return found;
    }
    public DiplomacyController GetTheDiplomacyController(CivController civOne, CivController civTwo)
    {
        DiplomacyController diplomacyController = InstantiatePlaceHolder(civOne, civTwo);
        diplomacyController.areWePlaceholder = true;
        if (ManagersDiplomacyControllerList.Count == 0)
        {
            return diplomacyController;
        }
        else
        {
            foreach (var aDiplomacyController in ManagersDiplomacyControllerList)
            {
                if ((aDiplomacyController.DiplomacyData.CivOne == civOne && aDiplomacyController.DiplomacyData.CivTwo == civTwo) || (aDiplomacyController.DiplomacyData.CivOne == civTwo && aDiplomacyController.DiplomacyData.CivTwo == civOne))
                {
                    diplomacyController = aDiplomacyController;
                }
            }
        }
        return diplomacyController;


    }
}


