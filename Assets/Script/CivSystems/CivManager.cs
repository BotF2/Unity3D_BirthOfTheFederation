using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    /// <summary>
    /// Instantiates the Civilizations(factions) (a CivController and a CivData) using CivSO
    /// See civ SOs listed in Unity CivManager SerializeFields
    /// Playable civs are: 0 FED, 1 ROM, 2 KLING, 3 CARD, 4 DOM, 5 BORG, 6 TERRAN
    /// FOR CANON MAP GETS THESE MINORS NEAR THE PLAYALBLES PER MAP SIZE AND + MORE RANDOMS PER MAP SIZE
    ///  SMALL map minor race near: FED = 146 VULCAN, ROM = 62 GORN, KLING = 131 THOLIANS, CARD = 24 BAJORANS, DOM = 73 KAREMMA, BORG = 142 VIDIANS, TERRAN = 54 EDO
    ///  MEDIUM map minors adds near: FED = 129 TELLARITES, ROM = 37 BREEN, KLING = 96 NAUSICAANS, CARD = 85 LURIANS, DOM = 147 WADI, BORG = 74 KAZON, TERRAN = 30 BETAZOIDS
    ///  LARGE map minors adds near: FED = 13 ANDORIAN, ROM = 155 ZAKDORN, KLING = 156 ZIBALIANS, CARD = 121 TAKARANS, DOM = 51 DOSI, BORG = 145 VORI, TERRAN = 47 DELTANS
    /// </summary>

    public class CivManager : MonoBehaviour
    {
        public static CivManager Instance;
        [SerializeField]
        private List<CivSO> civSOListAllPossible;
        private List<CivSO> allCivSOsInGame;
        [SerializeField]
        private List<CivSO> smallMapMinorNeighborsInGame;
        [SerializeField]
        private List<CivSO> mediumMapMinorNeighborsInGame;
        [SerializeField]
        private List<CivSO> largeMapMinorNeighborsInGame;
        private List<CivSO> randomMinorsInGame;

        public List<CivEnum> CivSOInGame;
        public List<CivData> CivDataInGameList = new List<CivData> { new CivData() };
        public List<CivController> CivControllersInGame;

        //public CivData LocalPlayerCivEnum;// This will be set by NetCode checking if NetworkObject belongs to the local player by comparing the NetworkObject.OwnerClientId with NetworkManager.Singleton.LocalClientId. 
        public bool isSinglePlayer;
        public List<CivEnum> InGamePlayableCivs;
        public CivController LocalPlayerCivContoller;

        //public bool nowCivsCanJoinTheFederation = true; // for use with testing a muliple star system Federation
        private int HoldCivSize = 0;// used in testing of a multiStarSystem civilization/faction
        [SerializeField]
        private GameObject civFolder; // hold civs
        [SerializeField]
        private GameObject civPrefab;
        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            //ToDo: early random minor races set before menu selects size and tech
        }
        private void Update()
        {
            #region temp multi-starsystem hack
            //***** This is temporary so we can test a multi-starsystem civCon
            //******* before diplomacy will alow civs/systems to join another civCon

            //if (nowCivsCanJoinTheFederation && HoldCivSize == 1)
            //{
            //    foreach (var civCon in CivControllersInGame)
            //    {
            //        if(civCon.CivData.CivEnum == CivEnum.ANDORIANS || civCon.CivData.CivEnum == CivEnum.VULCANS || civCon.CivData.CivEnum == CivEnum.TELLARITES)
            //        {
            //            civCon.CivData.CivInt = CivControllersInGame[0].CivData.CivInt;
            //            civCon.CivData.CivEnum = CivControllersInGame[0].CivData.CivEnum;
            //            civCon.CivData.CivShortName = CivControllersInGame[0].CivData.CivShortName;
            //            civCon.CivData.CivLongName = CivControllersInGame[0].CivData.CivLongName;
            //            civCon.CivData.CivHomeSystem = CivControllersInGame[0].CivData.CivHomeSystem;
            //            civCon.CivData.TraitOne = CivControllersInGame[0].CivData.TraitOne;
            //            civCon.CivData.TraitTwo = CivControllersInGame[0].CivData.TraitTwo;
            //            civCon.CivData.CivRaceSprite = CivControllersInGame[0].CivData.CivRaceSprite;
            //            civCon.CivData.InsigniaSprite = CivControllersInGame[0].CivData.InsigniaSprite;
            //            civCon.CivData.Playable = true;
            //            civCon.CivData.PlayedByAI = true;
            //            civCon.CivData.HasWarp =true;
            //            civCon.CivData.Decription = "temp civCon member of Federation";
            //        }
            //    }
            //    StarSysManager.current.UpdateStarSystemOwner(CivEnum.ANDORIANS, CivEnum.FED);
            //    StarSysManager.current.UpdateStarSystemOwner(CivEnum.VULCANS, CivEnum.FED);
            //    StarSysManager.current.UpdateStarSystemOwner(CivEnum.TELLARITES, CivEnum.FED);
            //}
            //nowCivsCanJoinTheFederation = false;
            #endregion
        }
        public void SetSingleVsMulitplayer()
        {

        }
        public void SetLocalPlayerCivController(CivController civ)
        {
            this.LocalPlayerCivContoller = civ;
        }
        public void UpdatePlayableCivGameList(List<CivEnum> listPlayableCivEnumForCivSOs, int galaxySize, GalaxyMapType galaxyType)
        {
            if (galaxyType == GalaxyMapType.CANON)
            {
                #region COMMENT OUT SELECTIVE CIS AND TURN ON ALL CIVS BELOW
                //// **********TURN OFF SELECTIVE CIVS HERE AND TURN ON ALL CIVS BELOW *******
                ///

                List<CivSO> _SOsInGame = new List<CivSO>();
                for (int i = 0; i < listPlayableCivEnumForCivSOs.Count; i++)
                {
                    if (listPlayableCivEnumForCivSOs[i] != CivEnum.ZZUNINHABITED1)
                    {
                        _SOsInGame.Add(civSOListAllPossible[i]); // add the playable
                        _SOsInGame.Add(smallMapMinorNeighborsInGame[i]); // add playable's minor races
                        if (galaxySize >= 1)
                            _SOsInGame.Add(mediumMapMinorNeighborsInGame[i]);
                        if (galaxySize == 2)
                            _SOsInGame.Add(largeMapMinorNeighborsInGame[i]);
                    }
                }
                SetRandomCanonCivsByGalaxySize(galaxySize, _SOsInGame);
                allCivSOsInGame = _SOsInGame;


                ////**** See all Civs -  ****
                // allCivSOsInGame = civSOListAllPossible;
                #endregion TURN ON ALL CIVs WITH LAST LINE ABOVE
            }
            else if (galaxyType == GalaxyMapType.RANDOM)
            {
                // do random map here
            }
            else if (galaxyType == GalaxyMapType.RING)
            {
                // do ring galaxy here
            }
            else if (galaxyType == GalaxyMapType.WHATEVER)
            {
                // do something else here
            }

        }
        private void SetRandomCanonCivsByGalaxySize(int galaxySize, List<CivSO> _SOsInGame)
        {
            civSOListAllPossible = civSOListAllPossible.OrderBy(i => Guid.NewGuid()).ToList();

            for (int i = 0; i < (50 * (1 + galaxySize)); i++)
            {
                for (int j = 0; j < civSOListAllPossible.Count; j++)
                {
                    int oneMoreCiv = j;
                    {
                        if (!_SOsInGame.Contains(civSOListAllPossible[i]))
                        {
                            _SOsInGame.Add(civSOListAllPossible[i]);
                            break;
                        }
                        else if (!_SOsInGame.Contains(civSOListAllPossible[i + 1]))
                        {
                            _SOsInGame.Add(civSOListAllPossible[i + 1]);
                            j++;
                            break;
                        }
                        else
                            j++;
                    }
                }
            }
        }
        public void CreateNewGameBySelections(int sizeGame, int gameTechLevel, int galaxyType, int localPlayerCivInt, bool isSingleVsMultiplayer)
        {
            MainMenuUIController.Instance.MainMenuData.SelectedGalaxySize = (GalaxySize)sizeGame;
            MainMenuUIController.Instance.MainMenuData.SelectedTechLevel = (TechLevel)gameTechLevel;
            MainMenuUIController.Instance.MainMenuData.SelectedGalaxyType = (GalaxyMapType)galaxyType;
            isSinglePlayer = isSingleVsMultiplayer;
            CivDataFromSO(allCivSOsInGame);
            CreateCivEnumList(allCivSOsInGame);
        }
        public void CivDataFromSO(List<CivSO> civSOList)
        {
            foreach (var civSO in civSOList)
            {
                CivData civData = new CivData(); // CivData is not MonoBehavior so new is OK
                civData.CivInt = civSO.CivInt;
                civData.CivEnum = civSO.CivEnum;
                civData.CivLongName = civSO.CivLongName;
                civData.CivShortName = civSO.CivShortName;
                civData.Warlike = (WarLikeEnum)civSO.WarLikeEnum; // a scale from most worklike 0 to neutral 3 and most peasful at 5
                civData.Xenophbia = civSO.XenophbiaEnum;
                civData.Ruthelss = civSO.RuthlessEnum;
                civData.Greedy = civSO.GreedyEnum;
                civData.CivRaceSprite = civSO.CivImage;
                civData.InsigniaSprite = civSO.Insignia;
                civData.Population = civSO.Population;
                civData.Credits = civSO.Credits;
                civData.TechPoints = civSO.TechPoints;
                civData.TechLevel = MainMenuUIController.Instance.MainMenuData.SelectedTechLevel;
                civData.Playable = civSO.Playable;
                civData.HasWarp = civSO.HasWarp;
                civData.Decription = civSO.Decription;
                civData.IntelPoints = civSO.IntelPoints;
                if ((int)civData.CivEnum <= 6) // playable races, major civilization
                {
                    civData.Population = 20;
                    civData.Credits = 300;
                    civData.TechPoints = 100; // set to tech level early at 100 points
                    civData.TechLevel = TechLevel.EARLY;
                }
                else if ((int)civData.CivEnum >= 159)// uninhabited system
                {
                    civData.Population = 0;
                    civData.Credits = 0;
                    civData.TechPoints = 0;
                }
                CivDataInGameList.Add(civData);
                InstantiateCivilizations(civData);

            }
            if (CivDataInGameList[0].CivHomeSystem != null) { }
            else
                CivDataInGameList.Remove(CivDataInGameList[0]); // remove the null entered by field
            StarSysManager.Instance.SysDataFromSO(civSOList);
        }
        private void InstantiateCivilizations(CivData civData)
        {
            GameObject civNewGameOb = (GameObject)Instantiate(civPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            var civController = civNewGameOb.GetComponentInChildren<CivController>();
            civController.CivData = civData;
            civController.CivShortName = civData.CivShortName;
            CivControllersInGame.Add(civController);
            civController.CivData.CivControllersWeKnow = new List<CivController>() { civController };
            civController.CivData.CivEnumsWeKnow = new List<CivEnum>() { civController.CivData.CivEnum };
            civNewGameOb.transform.SetParent(civFolder.transform, true);
            civNewGameOb.name = civData.CivShortName.ToString();
            //********** We are just assuming Fed is local player, need to do a check for 
            // NetCode, NetworkObject belongs to the local player
            if (GameController.Instance.AreWeLocalPlayer(civController.CivData.CivEnum)) // *** this is temp

            {
                SetLocalPlayerCivController(civController);
            }

        }

        void CreateStarSystemsWeOwnList(List<CivSO> list)
        {
            //for (int i = 0; i < .Count; i++)
            //{

            //}
        }
        void CreateCivEnumList(List<CivSO> listOfCivSO)
        {
            foreach (var civSO in listOfCivSO)
            {
                CivSOInGame.Add(civSO.CivEnum);
            }
        }
        public CivData GetCivDataByName(string shortName)
        {

            CivData result = null;


            foreach (var civ in CivDataInGameList)
            {
                if (civ.CivShortName.Equals(shortName))
                {
                    result = civ;
                }
            }
            return result;

        }
        public CivData GetCivDataByCivEnum(CivEnum civEnum)
        {
            CivData result = null;

            foreach (var civ in CivDataInGameList)
            {

                if (civ.CivEnum.Equals(civEnum))
                {
                    result = civ;
                }
            }
            return result;

        }
        public void OnNewGameButtonClicked(int gameSize, int gameTechLevel, int galaxyType, int selectedLocalCiv, bool isSingle)
        {
            CreateNewGameBySelections(gameSize, gameTechLevel, galaxyType, selectedLocalCiv, isSingle);
        }

        public CivController GetCivControllerByEnum(CivEnum civEnum)
        {
            GameObject aCivGO = (GameObject)Instantiate(civPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            CivController aCiv = aCivGO.GetComponent<CivController>();
            foreach (var civ in CivControllersInGame)
            {
                if (civEnum == civ.CivData.CivEnum)
                {
                    aCiv = civ;
                }
            }
            return aCiv;
        }
        public void AddSystemToOwnSystemListAndHomeSys(List<StarSysController> controllers)
        {
            for (int i = 0; i < CivControllersInGame.Count; i++)
            {
                if (CivControllersInGame[i].CivData.CivEnum == controllers[0].StarSysData.CurrentOwner)
                {
                    CivControllersInGame[i].CivData.StarSysOwned = controllers;
                    CivControllersInGame[i].CivData.CivHomeSystem = controllers[0].StarSysData.SysName;
                }
            }
        }
        public CivController GetLocalPlayerCivController()
        {
            CivController civController = null;
            foreach (var civCon in CivControllersInGame)
                if (civCon == CivManager.Instance.LocalPlayerCivContoller)
                    civController = civCon;
            return civController;
        }
    }
}
