using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
        public List<CivData> civDataInGameList = new List<CivData> { new CivData()};
        public List<CivController> allController;
        public Dictionary<CivEnum,List<CivController>> CivsThatACivKnows = new Dictionary<CivEnum,List<CivController>>();
        public CivData localPlayer;
        public CivData resultInGameCivData;
       

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else
            { 
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            //ToDo: early random minor races set before menu selects size and tech
            ShipManager.instance.SendEarlyCivSOListForFistShips(civSOListSmall); 
        }
        public CivData CreateLocalPlayer()
        {
            localPlayer = GetCivDataByName("FEDERATION");
            return localPlayer;
            
        }
        public void CivDataFromCivSO(CivData civData, CivSO civSO)
        {
            civData.CivInt = civSO.CivInt;
            civData.CivShortName = civSO.CivShortName;
        }

        public void CreateNewGameBySelections(int sizeGame, int gameTechLevel)
        {
            

            if (sizeGame == 0)
            {
                CivDataFromSO(civSOListSmall);
                CreateCivEnumList(civSOListSmall);

            }
            if (sizeGame == 1)
            {
                CivDataFromSO(civSOListMedium);
                CreateCivEnumList(civSOListMedium);

            }
            if (sizeGame == 2)
            {
                CivDataFromSO(civSOListLarge);
                CreateCivEnumList(civSOListLarge);

            }
            ShipManager.instance.FirstShipDateByTechlevel(gameTechLevel);
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
                civData.CivTechLevel = civSO.CivTechLevel;
                if (civSO.CivInt <= 5 || civSO.CivInt == 158)
                    civData.Playable = true;
                else civData.Playable = false;
                civData.HasWarp = civSO.HasWarp;
                civData.Decription = civSO.Decription;
                civData.StarSysOwned = civSO.StarSysOwned;
                //civData.TaxRate = civSO.TaxRate;
                //civData.GrowthRate = civSO.GrowthRate;
                civData.IntelPoints = civSO.IntelPoints;
                CivsThatACivKnows.Add(civData.CivEnum, civData.ContactList);
                civDataInGameList.Add(civData);
            }
            civDataInGameList.Remove(civDataInGameList[0]); // remove the null entered by field
            StarSysManager.instance.SysDataFromSO(civSOList);
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
        public void OnNewGameButtonClicked(int gameSize, int gameTechLevel)
        {
            CreateNewGameBySelections(gameSize, gameTechLevel);
            
        }

        public void GetCivByName(string civname)
        {
            resultInGameCivData = GetCivDataByName(civname);

        }
    }
}
