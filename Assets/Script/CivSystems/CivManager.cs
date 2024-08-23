using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Core
{
    /// <summary>
    /// Instantiates the Civilizations(factions) (a CivController and a CivData) using CivSO
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
        //private int smallGalaxyRandomCivs =5, mediumGalaxyRandomCivs =10, largeGalaxyRandomCivs =15;
        public List<CivEnum> CivSOInGame;
        public List<CivData> CivDataInGameList = new List<CivData> { new CivData() };
        public List<CivController> CivControllersInGame;
        //public Dictionary<CivController, List<CivController>> CivsThatACivKnows = new Dictionary<CivController, List<CivController>>();
        public CivData LocalPlayer;
        public bool isSinglePlayer;
        public List<CivEnum> InGamePlayableCivs;
        //public bool nowCivsCanJoinTheFederation = true; // for use with testing a muliple star system Federation
        private int HoldCivSize = 0;// used in testing of a multiStarSystem civilization/faction
        [SerializeField]
        private GameObject civFolder;
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
            //***** This is temporary so we can test a multi-starsystem civ
            //******* before diplomacy will alow civs/systems to join another civ

            //if (nowCivsCanJoinTheFederation && HoldCivSize == 1)
            //{
            //    foreach (var civ in CivControllersInGame)
            //    {
            //        if(civ.CivData.CivEnum == CivEnum.ANDORIANS || civ.CivData.CivEnum == CivEnum.VULCANS || civ.CivData.CivEnum == CivEnum.TELLARITES)
            //        {
            //            civ.CivData.CivInt = CivControllersInGame[0].CivData.CivInt;
            //            civ.CivData.CivEnum = CivControllersInGame[0].CivData.CivEnum;
            //            civ.CivData.CivShortName = CivControllersInGame[0].CivData.CivShortName;
            //            civ.CivData.CivLongName = CivControllersInGame[0].CivData.CivLongName;
            //            civ.CivData.CivHomeSystem = CivControllersInGame[0].CivData.CivHomeSystem;
            //            civ.CivData.TraitOne = CivControllersInGame[0].CivData.TraitOne;
            //            civ.CivData.TraitTwo = CivControllersInGame[0].CivData.TraitTwo;
            //            civ.CivData.CivImage = CivControllersInGame[0].CivData.CivImage;
            //            civ.CivData.Insignia = CivControllersInGame[0].CivData.Insignia;
            //            civ.CivData.Playable = true;
            //            civ.CivData.PlayedByAI = true;
            //            civ.CivData.HasWarp =true;
            //            civ.CivData.Decription = "temp civ member of Federation";
            //        }
            //    }
            //    StarSysManager.Instance.UpdateStarSystemOwner(CivEnum.ANDORIANS, CivEnum.FED);
            //    StarSysManager.Instance.UpdateStarSystemOwner(CivEnum.VULCANS, CivEnum.FED);
            //    StarSysManager.Instance.UpdateStarSystemOwner(CivEnum.TELLARITES, CivEnum.FED);
            //}
            //nowCivsCanJoinTheFederation = false;
            #endregion
        }
        public void SetSingleVsMulitplayer()
        {

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
                //allCivSOsInGame = civSOListAllPossible;
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
    public void CreateNewGameBySelections( int sizeGame, int gameTechLevel, int galaxyType, int localPlayerCivInt, bool isSingleVsMultiplayer)
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
                civData.TraitOne = civSO.TraitOne;
                civData.TraitTwo = civSO.TraitTwo;
                civData.CivImage = civSO.CivImage;
                civData.Insignia = civSO.Insignia;
                civData.Population = civSO.Population;
                civData.Credits = civSO.Credits;
                civData.TechPoints = civSO.TechPoints;
                civData.CivTechLevel = MainMenuUIController.Instance.MainMenuData.SelectedTechLevel;
                civData.Playable = civSO.Playable;
                civData.HasWarp = civSO.HasWarp;
                civData.Decription = civSO.Decription;
                civData.IntelPoints = civSO.IntelPoints;
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
            civController.CivData.CivsWeKnow = new List<CivController>() { civController};
            civNewGameOb.transform.SetParent(civFolder.transform, true);
            civNewGameOb.name = civData.CivShortName.ToString();
        }
        public void Diplomacy(CivController civPartyOne, CivController civPartyTwo)
        {
            if (!civPartyOne.CivData.CivsWeKnow.Contains(civPartyTwo))
            {
                FirstContact(civPartyOne, civPartyTwo);
            }
        }
        private void FirstContact(CivController civPartyOne, CivController civPartyTwo)
        {
            civPartyOne.CivData.CivsWeKnow.Add(civPartyTwo);
            civPartyTwo.CivData.CivsWeKnow.Add(civPartyOne);
            // ToDo: Update the system name or the fleet insignia;
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
            foreach(var civ in CivControllersInGame)
            {
                if(civEnum == civ.CivData.CivEnum)
                {
                   aCiv = civ;
                }
            }
            return aCiv;
        }
        public void AddSystemToOwnedCivSystemList(StarSysController controller)
        {
            for (int i = 0; i < CivControllersInGame.Count; i++)
            {
                if ((CivControllersInGame[i]).CivData.CivHomeSystem == controller.StarSysData.SysName)
                {
                    CivControllersInGame[i].CivData.StarSysOwned.Add(controller);
                }
            }
        }
    }
}
