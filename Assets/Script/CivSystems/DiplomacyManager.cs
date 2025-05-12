using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DiplomacyStatusEnum // between two civs held in the DiplomacyData
{
    War = -20,
    ColdWar = 0,
    Hostile = 20,
    UnFriendly = 40,
    Neutral = 60,
    Friendly = 80,
    Allied = 100,
    Membership = 120
}
public enum NagotiationPoints
{
    DeclarWar,
    Sanctions,
    ThreatenAction,
    OfferTraid,
    OfferCulturalExchange,
    OfferTech,
    OfferAid,
    OfferAlliance
}
public enum SecretService
{
    Sabatoge,
    Disinformation,
    IntellectualTheft,
    GatherIntelligence,
    Combat
}
public enum DiplomaticEventEnum // Diplomacy AI uses to move relations                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
{
    War,
    DiscoveredSabatoge,
    DiscoveredDisinformation,
    DiscoveredIntellectualTheft,
    CulturalExchange,
    Trade,
    ShareTech,
    GiveAid,
    Alliance
}
public enum WarLikeEnum // held by the civ
{
    Warlike =-2,
    Aggressive = -1,
    Neutral = 0,
    Peaceful =1,
    Pacifist = 2
}
public enum XenophobiaEnum // held by the civ
{
    Xenophobia = -2,
    Intolerant = -1,
    Indifferent = 0,
    Sympathetic = 1,
    Compassion = 2
}
public enum RuthlessEnum
{
    Ruthless =-2,
    Callous =-1,
    Regulated =0,
    Ethical = 1,
    Honorable = 2
}
public enum GreedyEnum
{
    Greedy =-2,
    Materialistic =-1,
    Transactional =0,
    Egaliterian =1,
    Idealistic =2
}


public class DiplomacyManager : MonoBehaviour
{
    public static DiplomacyManager Instance;
    public List<DiplomacyController> DiplomacyControllerList = new List<DiplomacyController>();
    //[SerializeField]
    //private GameObject diplmacyPrefab;
    [SerializeField]
    private GameObject diplomacyUIPrefab;
    [SerializeField]
    private GameObject diplomacyUIGO;

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
    //public GameObject GetNewDiplomacyController(CivController civPartyOne, CivController civPartyTwo)
    //{
    //    DiplomacyData diplomacyData = new DiplomacyData();
    //    diplomacyData.CivOne = civPartyOne;
    //    diplomacyData.CivTwo = civPartyTwo;
    //    diplomacyData.PositionOfNonLocalPlayerHomeSys = civPartyOne.CivData.HomeStarSystemPosition;
    //    DiplomacyController diplomacyController = new DiplomacyController(diplomacyData);
    //    InstantiateDiplomacyUIGameObject(diplomacyController);
    //    //return diplomacyUIGO;
    //}
   #region from FleetManager getting new fleetCon and fleetUI

    //    //IEnumerable<StarSysController> ourCivSysCons =
    //    //from x in StarSysManager.Instance.StarSysControllerList
    //    //where (x.StarSysData.CurrentOwnerCivEnum == fleetData.CivEnum)
    //    //select x;
    //    //var ourSysCons = ourCivSysCons.ToList();

    //    //if (fleetData.CivEnum < CivEnum.ZZUNINHABITED1)
    //    //{
    //    //    GameObject fleetNewGameOb = (GameObject)Instantiate(fleetPrefab, new Vector3(0, 0, 0),
    //    //            Quaternion.identity);
    //    //    FleetGOList.Add(fleetNewGameOb);
    //    //    fleetNewGameOb.layer = 6; // galaxy layer

    //    //    var fleetController = fleetNewGameOb.GetComponentInChildren<FleetController>();
    //    //    fleetController.BackgroundGalaxyImage = galaxyImage;
    //    //    fleetController.FleetData = fleetData;
    //    //    Canvas[] canvasArray = fleetNewGameOb.GetComponentsInChildren<Canvas>();
    //    //    for (int j = 0; j < canvasArray.Length; j++)
    //    //    {
    //    //        if (canvasArray[j].name == "CanvasToolTip")
    //    //        {
    //    //            fleetController.CanvasToolTip = canvasArray[j];
    //    //        }
    //    //    }
    //    //    FleetControllerList.Add(fleetController); // add to list of all fleet controllers
    //    //    if (!inSystem)
    //    //    {
    //    //        var transGalaxyCenter = GalaxyCenter.gameObject.transform;
    //    //        var trans = sysCon.gameObject.transform;
    //    //        fleetNewGameOb.transform.SetParent(transGalaxyCenter, true); // parent is galaxy center, it is not in a star system
    //    //                                                                     // now put it near the home world and visible/seen on the galaxy map, in galaxy space. It is not 'hidden' in the system
    //    //        fleetNewGameOb.transform.Translate(new Vector3(trans.position.x + 20f, trans.position.y + 20f, trans.position.z));
    //    //        fleetData.Position = fleetNewGameOb.transform.position;
    //    //    }
    //    //    else // it is in the system shipyard so 'hidden' on the galaxy map inside the system
    //    //    {
    //    //        fleetNewGameOb.transform.SetParent(sysCon.gameObject.transform, false);
    //    //    }
    //    //    fleetNewGameOb.transform.localScale = new Vector3(0.7f, 0.7f, 1); // scale ship insignia here
    //    //    int fleetInt = GetNewFleetInt(fleetData.CivEnum);
    //    //    fleetNewGameOb.name = fleetData.CivShortName.ToString() + " Fleet " + fleetInt.ToString(); // name game object
    //    //    fleetData.Name = fleetNewGameOb.name;

    //    //    fleetController.FleetData.FleetInt = fleetInt;
    //    //    fleetController.Name = fleetData.Name;
    //    //    FleetConrollersInGame.Add(fleetController);
    //    //    fleetController.FleetData.CurrentWarpFactor = 0f;
    //    //    TextMeshProUGUI TheText = fleetNewGameOb.GetComponentInChildren<TextMeshProUGUI>();

    //    //    if (GameController.Instance.AreWeLocalPlayer(fleetData.CivEnum))
    //    //    {
    //    //        var ourFogRevealerFleet = new csFogWar.FogRevealer(fleetNewGameOb.transform, 200, true);
    //    //        fogWar.AddFogRevealer(ourFogRevealerFleet);
    //    //    }
    //    //    else
    //    //    {
    //    //        fleetNewGameOb.AddComponent<csFogVisibilityAgent>();
    //    //        var ourFogVisibilityAgent = fleetNewGameOb.GetComponent<csFogVisibilityAgent>();
    //    //        ourFogVisibilityAgent.FogWar = fogWar;
    //    //        ourFogVisibilityAgent.enabled = true;
    //    //    }

    //    //    TheText.text = fleetNewGameOb.name;
    //    //    fleetData.Name = TheText.text;
    //    //    var Renderers = fleetNewGameOb.GetComponentsInChildren<SpriteRenderer>();
    //    //    for (int i = 0; i < Renderers.Length; i++)
    //    //    {
    //    //        if (Renderers[i] != null)
    //    //        {
    //    //            if (Renderers[i].name == "InsigniaSprite")
    //    //            {
    //    //                Renderers[i].sprite = fleetController.FleetData.Insignia;
    //    //                if (!GameController.Instance.AreWeLocalPlayer(fleetController.FleetData.CivEnum) && !localPlayerCanSeeMyInsigniaList.Contains(fleetData.CivEnum))
    //    //                {
    //    //                    Renderers[i].gameObject.SetActive(false);
    //    //                }
    //    //                else Renderers[i].gameObject.SetActive(true);
    //    //            }
    //    //            if (Renderers[i].name == "InsigniaUnknown" && (GameController.Instance.AreWeLocalPlayer(fleetController.FleetData.CivEnum) || localPlayerCanSeeMyInsigniaList.Contains(fleetData.CivEnum)))
    //    //            {
    //    //                Renderers[i].gameObject.SetActive(false);
    //    //            }
    //    //        }
    //    //    }
    //    //    // The line from Fleet to underlying galaxy image and to destination
    //    //    MapLineMovable[] ourLineToGalaxyImageScript = fleetNewGameOb.GetComponentsInChildren<MapLineMovable>();
    //    //    for (int i = 0; i < ourLineToGalaxyImageScript.Length; i++)
    //    //    {
    //    //        if (ourLineToGalaxyImageScript[i].name == "DropLine")
    //    //        {
    //    //            ourLineToGalaxyImageScript[i].GetLineRenderer();
    //    //            ourLineToGalaxyImageScript[i].lineRenderer.startColor = Color.red;
    //    //            ourLineToGalaxyImageScript[i].lineRenderer.endColor = Color.red;
    //    //            ourLineToGalaxyImageScript[i].transform.SetParent(fleetNewGameOb.transform, false);
    //    //            Vector3 galaxyPlanePoint = new Vector3(fleetNewGameOb.transform.position.x,
    //    //                galaxyImage.transform.position.y, fleetNewGameOb.transform.position.z);
    //    //            Vector3[] points = { fleetNewGameOb.transform.position, galaxyPlanePoint };
    //    //            ourLineToGalaxyImageScript[i].SetUpLine(points);
    //    //            fleetController.DropLine = ourLineToGalaxyImageScript[i];
    //    //        }

    //    //    }
    //    //    fleetController.FleetData.Destination = GalaxyCenter;
    //    //    fleetController.FleetData.ShipsList.Clear();
    //    //    foreach (var civCon in CivManager.Instance.CivControllersInGame)
    //    //    {
    //    //        if (civCon.CivData.CivEnum == fleetData.CivEnum)
    //    //            fleetData.CivController = civCon;
    //    //    }
    //    //    List<FleetController> list = new List<FleetController>() { fleetController };
    //    //    fleetController.FleetData.FleetGroupControllers = list;
    //    //    fleetController.UpdateMaxWarp();
    //    //    fleetNewGameOb.SetActive(true);
    //    //    if (!inSystem) // all first fleets are not in system
    //    //        ShipManager.Instance.BuildShipsOfFirstFleet(fleetNewGameOb);
    //    //   InstantiateDiplomacyUIGameObject(diplomacyController);
    //    //    return fleetNewGameOb;
    //    //}
    //    //else
    //    //{
    //    //    GameObject fleetNewGameOb = new GameObject();
    //    //    fleetNewGameOb.name = "killMe";
    //    //    return fleetNewGameOb;
    //    //}
        #endregion
    
    private void InstantiateDiplomacyUIGameObject(DiplomacyController diplomacyCon)
    {
        if (diplomacyCon.DiplomacyData.CivOne.CivData.CivEnum == GameController.Instance.GameData.LocalPlayerCivEnum
             || diplomacyCon.DiplomacyData.CivTwo.CivData.CivEnum == GameController.Instance.GameData.LocalPlayerCivEnum)
        {
            if (diplomacyCon.DiplomacyUIGameObject == null)
            {
                GameObject thisDiplomacyUIGameObject = (GameObject)Instantiate(diplomacyUIPrefab, new Vector3(0, 0, 0),
                Quaternion.identity); //parentCanavas.transform);
                                      //RectTransform rt = thisFleetUIGameObject.GetComponent<RectTransform>();
                                      //rt.anchoredPosition = Vector2.zero; // Set position if needed
                thisDiplomacyUIGameObject.SetActive(true);
                thisDiplomacyUIGameObject.layer = 5;
                diplomacyCon.DiplomacyUIGameObject = thisDiplomacyUIGameObject;
               // thisDiplomacyUIGameObject.transform.SetParent(contentFolderParent.transform, false); // load into List of fleets

            }
        }
    }
    public void SpaceCombatScene(FleetController fleetConA, FleetController fleetConB, StarSysController aNull)
    {
        SceneController.Instance.LoadCombatScene();
        GalaxyMenuUIController.Instance.CloseMenu(Menu.DiplomacyMenu);
        //SubMenuManager.Instance.CloseMenu(Menu.DiplomacyMenu);
        ShipManager.Instance.ShipsFromFleetsForCombat(); //shipType, fleetGOinSys, this);
        //CombatManager.Instance.InstatniateCombat(controller.DiplomacyData.CivOne.CivData.FleetControllers, controller.DiplomacyData.CivTwo.CivData.FleetControllers);
    }
    public void SpaceCombatScene(FleetController fleetConA, FleetController fleetConB)
    {
        SceneController.Instance.LoadCombatScene();
        GalaxyMenuUIController.Instance.CloseMenu(Menu.DiplomacyMenu);
        //SubMenuManager.Instance.CloseMenu(Menu.DiplomacyMenu);
        ShipManager.Instance.ShipsFromFleetsForCombat(); //shipType, fleetGOinSys, this);
        //CombatManager.Instance.InstatniateCombat(controller.DiplomacyData.CivOne.CivData.FleetControllers, controller.DiplomacyData.CivTwo.CivData.FleetControllers);
    }
    public void FirstContactGetNewDiplomacyContoller(CivController civPartyOne, CivController civPartyTwo)
    {
        // is frist contact diplomacy
        DiplomacyData diplomacyData = new DiplomacyData();
        diplomacyData.CivOne = civPartyOne;
        diplomacyData.CivTwo = civPartyTwo;
        diplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        diplomacyData.DiplomacyPointsOfCivs = (int)DiplomacyStatusEnum.Neutral;
        DiplomacyController diplomacyController = new DiplomacyController(diplomacyData);
        InstantiateDiplomacyUIGameObject(diplomacyController);
        GalaxyMenuUIController.Instance.SetUpDiplomacyUIData(diplomacyController);
        

        if (GameController.Instance.AreWeLocalPlayer(civPartyOne.CivData.CivEnum))
        {
            diplomacyData.PositionOfNonLocalPlayerHomeSys = civPartyTwo.CivData.HomeStarSystemPosition;
            DiplomacyUIController.Instance.LoadDiplomacyUI(diplomacyController);
        }
        else if (GameController.Instance.AreWeLocalPlayer(civPartyTwo.CivData.CivEnum))
        {
            diplomacyData.PositionOfNonLocalPlayerHomeSys = civPartyOne.CivData.HomeStarSystemPosition;
            DiplomacyUIController.Instance.LoadDiplomacyUI(diplomacyController);
        }
        else
        {
            DoDiplomacyForAI(diplomacyController);
        }
        #region testing auto combat
        // For Testing..... 
        //if ((diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.FED
        //    && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.VULCANS)
        //    || (diplomacyController.DiplomacyData.CivOne.CivData.CivEnum == CivEnum.VULCANS
        //    && diplomacyController.DiplomacyData.CivTwo.CivData.CivEnum == CivEnum.FED))
        //{
        //    diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.War;
        //    diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 0;
        //}
        //else
        //{

        //    diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral;
        //    diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = 60; //60 = netural on a -20 to 120 scale
        //}
        #endregion
        DiplomacyControllerList.Add(diplomacyController);
        diplomacyController.FirstContact(civPartyOne, civPartyTwo);
        diplomacyController.DiplomacyData.DiplomacyEnumOfCivs = CalculateDiplomaticStatusOnFirstContact(diplomacyController);
        diplomacyController.DiplomacyData.DiplomacyPointsOfCivs = (int)diplomacyController.DiplomacyData.DiplomacyEnumOfCivs;       
    }
    private void DoDiplomacyForAI(DiplomacyController diploCon) //, GameObject weHitGO)
    {
        //Do SpaceCombatScene or so some other diplomacy without a UI by/for either civ
    }
    public bool FoundADiplomacyController(CivController civPartyOne, CivController civPartyTwo) //, GameObject hitGO)
    {
        bool found = false;
        //List<DiplomacyController> placeholderControllers = new List<DiplomacyController>();
        for (int i = 0; i < DiplomacyControllerList.Count; i++)
        {
            if (DiplomacyControllerList[i] != null) 
            {
                if (DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyOne && DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyTwo
                    || DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyOne && DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyTwo)
                {
                    found = true;
                    break;
                }
            }
        }
        return found;
    }
    public void NextDiplomacyControllerActions(CivController civPartyOne, CivController civPartyTwo)
    {
        // get diplomacy controller and do something with it
    }
    public DiplomacyController ReturnADiplomacyController(CivController civPartyOne, CivController civPartyTwo)
    {
        DiplomacyController diplomacyController = null;
        for (int i = 0; i < DiplomacyControllerList.Count; i++)
        {
            if (DiplomacyControllerList[i] != null && ((DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyOne && DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyTwo)
                || (DiplomacyControllerList[i].DiplomacyData.CivOne == civPartyTwo && DiplomacyControllerList[i].DiplomacyData.CivTwo == civPartyOne)))
            {
                diplomacyController = DiplomacyControllerList[i];
                break;
            }
        }
        return diplomacyController;
    }
    public DiplomacyStatusEnum CalculateDiplomaticStatusOnFirstContact(DiplomacyController ourDiploCon)
    {
        CivController civOne = ourDiploCon.DiplomacyData.CivOne;
        CivController civTwo = ourDiploCon.DiplomacyData.CivTwo;
        DiplomacyStatusEnum diplomacyStatus = DiplomacyStatusEnum.Neutral;
        int warLike = Math.Abs((int)civOne.CivData.Warlike - (int)civTwo.CivData.Warlike);
        int xenophobia = Math.Abs((int)civOne.CivData.Xenophbia - (int)civTwo.CivData.Xenophbia);
        int ruthless = Math.Abs((int)civOne.CivData.Ruthelss - (int)civTwo.CivData.Ruthelss);
        int greedy = Math.Abs((int)civOne.CivData.Greedy - (int)civTwo.CivData.Greedy);
        int degreesOfSparation = warLike + xenophobia + ruthless + greedy;
        switch (degreesOfSparation)
        {
            case 0:
                diplomacyStatus = DiplomacyStatusEnum.Friendly;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                diplomacyStatus = DiplomacyStatusEnum.Neutral;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                diplomacyStatus = DiplomacyStatusEnum.UnFriendly;
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                diplomacyStatus = DiplomacyStatusEnum.Hostile;
                break;
            case 13:
            case 14:
            case 15:
            case 16:
                diplomacyStatus = DiplomacyStatusEnum.ColdWar;
                break;
            default:
                diplomacyStatus = DiplomacyStatusEnum.Neutral;
                break;
        }
        return diplomacyStatus;
    }
}


