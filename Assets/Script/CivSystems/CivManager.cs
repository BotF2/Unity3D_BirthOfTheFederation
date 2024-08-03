using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Core
{

    public class CivManager : MonoBehaviour
    {
        public static CivManager instance;

        public List<CivSO> civSOListSmall;
        public List<CivSO> civSOListMedium;
        public List<CivSO> civSOListLarge;
        public List<CivSO> civSOListAllPossible;
        public List<CivEnum> CivSOInGame;
        public List<CivData> civDataInGameList = new List<CivData> { new CivData() };
        public List<CivController> CivControllersInGame;
        public Dictionary<CivController, List<CivController>> CivsThatACivKnows = new Dictionary<CivController, List<CivController>>();
        public CivData localPlayer;
        public bool isSinglePlayer;
        //public bool nowCivsCanJoinTheFederation = true; // for use with testing a muliple system Federation
        private int HoldCivSize = 0;
        [SerializeField]
        private GameObject civFolder;
        [SerializeField]
        private GameObject civPrefab;


        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            //ToDo: early random minor races set before menu selects size and tech
        }
        private void Update()
        {
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
            //    StarSysManager.instance.UpdateStarSystemOwner(CivEnum.ANDORIANS, CivEnum.FED);
            //    StarSysManager.instance.UpdateStarSystemOwner(CivEnum.VULCANS, CivEnum.FED);
            //    StarSysManager.instance.UpdateStarSystemOwner(CivEnum.TELLARITES, CivEnum.FED);
            //}
            //nowCivsCanJoinTheFederation = false;
        }
        public void SetSingleVsMulitplayer()
        {

        }

        public void CreateNewGameBySelections(int sizeGame, int gameTechLevel, int galaxyType, int localPlayerCivInt, bool isSingleVsMultiplayer)
        {
            GameManager.Instance._techLevelOnLoadGame = (TechLevel)gameTechLevel;
            GameManager.Instance._galaxySize = (GalaxySize)sizeGame;
            GameManager.Instance._galaxyType = (GalaxyType)galaxyType;
            isSinglePlayer = isSingleVsMultiplayer;
            // ToDo: assure that selected civ is in game, in game civs (Terrans)

            //localPlayer = GetCivDataByCivEnum((CivEnum)localPlayerCivInt);

            switch (sizeGame)
            { case 0:
                CivDataFromSO(civSOListSmall);
                CreateCivEnumList(civSOListSmall);
                //ShipManager.instance.SendEarlyCivSOListForFirstShips(civSOListSmall);
                break;
              case 1:

                CivDataFromSO(civSOListMedium);
                CreateCivEnumList(civSOListMedium);
                //ShipManager.instance.SendEarlyCivSOListForFirstShips(civSOListMedium);
                HoldCivSize = sizeGame; // used in Multisystems in Fedearation test code 
                break;
              case 2:
                CivDataFromSO(civSOListLarge);
                CreateCivEnumList(civSOListLarge);
                //ShipManager.instance.SendEarlyCivSOListForFirstShips(civSOListLarge);
                break;
              default:
                CivDataFromSO(civSOListSmall);
                CreateCivEnumList(civSOListSmall);
                //ShipManager.instance.SendEarlyCivSOListForFirstShips(civSOListSmall);
                break;
            }
        }
        public void CivDataFromSO(List<CivSO> civSOList)
        {
            foreach (var civSO in civSOList)
            {
                CivData civData = new CivData();
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
                civData.CivTechLevel = GameManager.Instance._techLevelOnLoadGame;
                civData.Playable = civSO.Playable;
                civData.HasWarp = civSO.HasWarp;
                civData.Decription = civSO.Decription;
                civData.IntelPoints = civSO.IntelPoints;
                civDataInGameList.Add(civData);
                InstantiateCivilizations(civData);

            }
            if (civDataInGameList[0].CivHomeSystem != null) { }
            else
                civDataInGameList.Remove(civDataInGameList[0]); // remove the null entered by field
            StarSysManager.instance.SysDataFromSO(civSOList);
        }
        private void InstantiateCivilizations(CivData civData)
        {
            GameObject civNewGameOb = (GameObject)Instantiate(civPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
            var civController = civNewGameOb.GetComponentInChildren<CivController>();
            civController.CivData = civData;
            civController.CivShortName = civData.CivShortName;
            CivControllersInGame.Add(civController);
            CivsThatACivKnows.Add(civController, new List<CivController> { civController });
            civNewGameOb.transform.SetParent(civFolder.transform, true);
            civNewGameOb.name = civData.CivShortName.ToString();
        }
        public void Diplomacy(CivController civPartyOne, CivController civPartyTwo)
        {
            if (!CivsThatACivKnows[civPartyOne].Contains(civPartyTwo))
            {
                FirstContact(civPartyOne, civPartyTwo);
            }
        }
        private void FirstContact(CivController civPartyOne, CivController civPartyTwo)
        {
            CivsThatACivKnows[civPartyOne].Add(civPartyTwo);
            CivsThatACivKnows[civPartyTwo].Add(civPartyOne);
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


            foreach (var civ in civDataInGameList)
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

            foreach (var civ in civDataInGameList)
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
            CivController aCiv = new CivController("placeholder");
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
