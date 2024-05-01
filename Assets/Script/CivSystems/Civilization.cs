using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using DG.Tweening.Core.Easing;
using UnityEngine.UI;
using GalaxyMap;

namespace Assets.Core
{
    public class Civilization : MonoBehaviour
    {
        //ToDo: add 2 Traits for a civ
        public GameObject civPrefab;
        public List<CivController> civControllers;
        public CivSO civSOFed;
        //public GameManager gameManager;
        public Dictionary<int, CivController> fleetsDictionary = new Dictionary<int, CivController>();
        public static Dictionary<CivEnum, CivData> civDataDictionary = new Dictionary<CivEnum, CivData>();
        //private Canvas _canvasGalactic;
        public int _civID;
        public CivEnum _civEnum;
        public Text _shortNameText;
        public Text _longNameText;
        public Text _descriptionText;
        public Image _artworkCivImage;
        //public StarSystemEnum _homeSystemEnum;
        //public static Civilization FED;
        //public static Civilization ROM;
        //public static Civilization KLING;
        //public static Civilization CARD;
        //public static Civilization DOM;
        //public static Civilization BORG;
        //public static Civilization TERRAN;
        //public Race race -- try to leave this off, too complicated
        //public StarSystemManager _homeSystem;
        public bool _weAreMajorCiv;
        //public Traits _traitOne;
        //public Traits _traitTwo;
        public Sprite _civImage;
        public Sprite _insignia;
        public List<FleetController> civFleetList;
        public List<float> _sysTradeAllocation = new List<float> { 100f, };
        //public RelationshipManager _relationshipManager;
        //public int[,] relationshipScoresArray; // index of player and other civ, holds int relationship score listSONames
        //public RelationshipInfo relationshipInfo; // has property of relationship score -100 to +100
        public static Dictionary<int, string[]> CivStringsDictionary;
        public static List<CivData> civsInGame = new List<CivData>(); // should this be in GameManager
        //public Dictionary<CivEnum, DiplomaticEnum> _relationshipDictionary = new Dictionary<CivEnum, DiplomaticEnum>() { { (CivEnum)111, (DiplomaticEnum)(-3) } }; // key is other civ (111 is Placeholder) and DiplomaticEnum -3 is unknown 
        public List<float> _relationshipScores = new List<float>();
        //public float _civPopulation; // credits per game time
        //public List<Bonus> _globalBonuses;
        //public readonly CivilizationMapData _mapData;
        //public readonly ResearchPool _research;
        //public readonly ResourcePool _resources;
        //public readonly List<SitRepEntry> _sitRepEntries;
        //public int _totalPopulation;
        //public readonly Meter _totalValue;
        public float _civTechPoints;
        public TechLevel _civTechLevel;
        public float _civTaxRate;
        public float _cviGrowthRate; // currently using public float techPopGrowthRate = 0.01f in CivData
        //public float _intel;
        public float _civCredits;
        //public readonly Treasury _treasury;
        //public int _maintenanceCostLastTurn;
        //public int _rankCredits;
        //public List<CivHistory> _civHist_List = new List<CivHistory>();
        public List<CivData> _contactList; //**** who we have met
        //public List<StarSystemEnum> _ownedSystemEnums;
        //public List<Ship> _listCombatShips;
        //public List<int> _IntelIDs;
        //public MapLocation? _homeColonyLocation;
        //public int _seatOfGovernmentId = -1;
        //public readonly Meter _totalIntelligenceAttackingAccumulated;
        //public readonly Meter _totalIntelligenceDefenseAccumulated;

        //public int _rankMaint;
        //public int _rankResearch;
        //public int _rankIntelAttack;
        //public readonly string newline = Environment.NewLine;
        //public readonly IPlayer localPlayer;
        //public readonly AppContext _appContext

        public void Awake()
        {
            //FED = new CivData(0);
            //ROM = new CivData(1);
            //KLING = new CivData(2);
            //CARD = new CivData(3);
            //DOM = new CivData(4);
            //BORG = new CivData(5);
            //TERRAN = new CivData(300);
            GameObject tempObject = GameObject.Find("CanvasGalactic");
            if (tempObject != null)
            {
                //_canvasGalactic = tempObject.GetComponent<Canvas>();
            }
            #region Read Civilization Data.txt 
            char separator = ',';
            Dictionary<int, string[]> _civDataDictionary = new Dictionary<int, string[]>();
            var file = new FileStream(Environment.CurrentDirectory + "\\Assets\\" + "Civilizations.txt", FileMode.Open, FileAccess.Read);

            var _dataPoints = new List<string>();
            using (var reader = new StreamReader(file))
            {

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    if (line == null)
                        continue;
                    _dataPoints.Add(line.Trim());

                    if (line.Length > 0)
                    {
                        var civDataStringArray = line.Split(separator);

                        _civDataDictionary.Add(int.Parse(civDataStringArray[0]), civDataStringArray);
                    }
                }

                reader.Close();
                CivStringsDictionary = _civDataDictionary;

            }
            #endregion          
        }
        private void Start()
        {
            //if (CivData != null)
            //{
            //    _civID = CivData._civID;
            //    _civEnum = CivData._civEnum;

            //    _shortNameText.text = CivData._shortNameString;
            //    _longNameText.text = CivData._longNameString;
            //    _descriptionText.text = CivData._descriptionString;
            //    _artworkCivImage.sprite = CivData._civInsign;
            //    _homeSysEnum = CivData._homeSysEnum;
            //    starSysDataDictionary = CivData.starSystemsDictionary;
            //    fleetsDictionary = CivData.fleetsDictionary;
            //    _homeSysEnum = CivData._homeSysEnum;
            //}
        }
        void Update()
        {
            //if (CivData != null)
            //{
            //    starSysDataDictionary = CivData.starSystemsDictionary;
            //    fleetsDictionary = CivData.fleetsDictionary;
            //    _homeSysEnum = CivData._homeSysEnum;
            //}
        }
        public static CivData Instance { get; private set; } // Do we still need this???


        public void InitializeCivs(int systemInt)
        {
            GameObject civGO = Instantiate(civPrefab);
            InitializeCivController(civGO.GetComponent<CivController>(), civSOFed);

            civControllers.Add(civGO.GetComponent<CivController>());
            //CivData newCivData = ScriptableObject.CreateInstance<CivData>();
            // Civilization daCiv = new Civilization(systemInt);
            //var sysStrings = CivData.CivStringsDictionary[systemInt];
            //newCivData._civEnum = (CivEnum)systemInt;
            //if (systemInt <= 5) // update on adding more playable major civs
            //    newCivData._weAreMajorCiv = true;
            //else newCivData._weAreMajorCiv = false;
            //newCivData._shortNameString = sysStrings[2];
            //newCivData._longNameString = sysStrings[3];

            //GetImage(systemInt, 8, "Insignias/", newCivData); // 8 is insignia
            //string holdInsigniaName = CivStringsDictionary[systemInt][8];
            //string pathInsignia = "Insignias/" + holdInsigniaName.ToUpper();
            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane); // (nameInsginia);
            //go.Name = newCivData.Name + "PlaneForInsignia";
            //var rend = go.GetComponent<Renderer>();
            //rend.material.mainTexture = Resources.Load(pathInsignia) as Texture;
            //newCivData._civInsign = Sprite.InitializStarSystem((Texture2D)rend.material.mainTexture, new Rect(0, 0, rend.material.mainTexture.width, rend.material.mainTexture.height), new Vector2(0.5f, 0.5f));
            //newCivData._civInsign = Sprite.InitializSystem((Texture2D)rend.material.mainTexture, new Rect(0, 0, rend.material.mainTexture.width, rend.material.mainTexture.height), new Vector2(0.5f, 0.5f));
            //go.gameObject.SetActive(false);

            //GetImage(systemInt, 7, "Civilizations/", newCivData); // 7 is Civ
            //string holdCivName = CivStringsDictionary[systemInt][7];
            //string pathCiv = "Civilizations/" + holdCivName.ToLower();
            //GameObject buildImage = GameObject.CreatePrimitive(PrimitiveType.Plane);
            //var rendTwo = buildImage.GetComponent<Renderer>();
            //rendTwo.material.mainTexture = Resources.Load(pathCiv) as Texture;
            //newCivData._civImage = Sprite.InitializStarSystem((Texture2D)rendTwo.material.mainTexture, new Rect(0, 0, rendTwo.material.mainTexture.width, rendTwo.material.mainTexture.height), new Vector2(0.5f, 0.5f));
            //newCivData._civImage = Sprite.InitializSystem((Texture2D)rendTwo.material.mainTexture, new Rect(0, 0, rendTwo.material.mainTexture.width, rendTwo.material.mainTexture.height), new Vector2(0.5f, 0.5f));
            //buildImage.gameObject.SetActive(false);
            //// newCivData._civPopulation = int.Parse(sysStrings[9]);
            //newCivData._civCredits = int.Parse(sysStrings[10]);
            //newCivData._civTechPoints = int.Parse(sysStrings[11]);
            ////newCivData._civTechLevel = TechLevel.EARLY; ToDo: set this by enough tech points to get new ship images
            //newCivData._homeSysEnum = (StarSystemEnum)systemInt;
            //// newCivData._homeSystem._ownerCiv = newCivData;
            //List<CivData> civsWeKnow = new List<CivData>() { newCivData }; // instantiate list with knowing our self
            //newCivData._contactList = civsWeKnow;
            //List<StarSystemEnum> ownedSystemStarterList = new List<StarSystemEnum>() { newCivData._homeSysEnum };
            //newCivData._ownedSysEnums = ownedSystemStarterList;
            ////newCivData._relationshipDictionary = new Dictionary<int, int>();
            //civsInGame.Add(newCivData);
            //for (int i = 0; i < systemInt; i++)
        }

        public void InitializeCivController(CivController civController, CivSO civSO)
        {

            civController.civData.CivShortName = civSO.CivShortName;
        }


        //newCivData._relationshipScores ;
        //if (!newCivData.deltaRelation.ContainsKey(systemInt))
        //    newCivData.deltaRelation.Add(systemInt, 0);
        //civDataDictionary.Add(newCivData._civEnum, newCivData);
        //return newCivData;
        //}
        //CivData newCivData = ScriptableObject.CreateInstance<CivData>();
        //UnityEditor.AssetDatabase.CreateAsset(newCivData, "Assets/SO.asset");

        //{
        //    deltaRelation = new Dictionary<int, int>() { {0,0} }; // initialize deltaRelation Dictionary;         
        //}
        // helper method when you want to work with relationships.
        //public int GetRelationshipInt(Civilization otherCivilization)
        //{
        //    return RelationshipManager.GetRelationshipScore(this, otherCivilization);
        //}
        //public RelationshipInfo GetRelationshipInfoWith(Civilization otherCivilization)
        //{
        //    return RelationshipManager.GetRelationshipInfo(this, otherCivilization);
        //}
        // ... other faction code
        public void LoadDictionaryOfCivs(int[] ints) // only call once on loading galaxy 
        {
            for (int i = 0; i < ints.Length; i++)
            {
                //CivData aCiv = Civilization.CreateCivs(ints[i]);
                //StarSystemManager.starSysDataDictionary.LoadSystemOwner(aCiv, aCiv._homeSysEnum); // StarSystemSO exists now so go back to set Civ for owner of system
                //CivilizationDictionary.Add((CivEnum)aCiv._civID, aCiv);
                //List<float> ourRelationScores = new List<float>();
                //for (int j = 0; j < ints.Length; j++)
                //{
                //    ourRelationScores.Add(-100F);
                //}
                //aCiv._relationshipScores = ourRelationScores;
            }
            //numStars = gameManager._galaxyStarCount.Length;
        }
        public void LoadRelationshipDictionaryOfCivs(int[] intsArray)
        {
            for (int i = 0; i < intsArray.Length; i++)
            {
                // civDataDictionary[(CivEnum)intsArray[i]]._relationshipDictionary.Add((CivEnum)intsArray[i], (DiplomaticEnum)(-3));
            }
        }
        public void UpdateCivContactListOnStartCivSelection(TechLevel ourStartTechLevel)
        {
            switch (ourStartTechLevel)
            {
                case TechLevel.EARLY:
                    {
                        // LoadOurStartingMinor();
                    }
                    break;
                case TechLevel.DEVELOPED:
                    {
                        LoadOurStartingMinor();
                    }
                    break;
                case TechLevel.ADVANCED:
                    {
                        LoadOurStartingMinor();
                        foreach (CivData aCiv in civsInGame)
                        {
                            if (aCiv.CivEnum == CivEnum.FED || aCiv.CivEnum == CivEnum.ROM ||
                                aCiv.CivEnum == CivEnum.KLING || aCiv.CivEnum == CivEnum.CARD ||
                                aCiv.CivEnum == CivEnum.DOM || aCiv.CivEnum == CivEnum.BORG || aCiv.CivEnum == CivEnum.TERRAN)
                            {
                                foreach (CivData thisCiv in civsInGame)
                                {
                                    //if (!thisCiv._contactList.Contains(aCiv))
                                    //    thisCiv._contactList.Add(aCiv);
                                }
                            }

                        }
                    }
                    break;
                case TechLevel.SUPREME:
                    {
                        LoadOurStartingMinor();
                        foreach (CivData aCiv in civsInGame)
                        { 
                            //aCiv._contactList = civsInGame; bring this back
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void LoadOurStartingMinor()
        {
            //foreach (CivData aCiv in CivData.civsInGame)
            //{
            //    switch (aCiv.CivEnum)
            //    {
            //        case CivEnum.FED:
            //            {
            //                CivData minorCiv = CivFromEnum(CivEnum.VULCANS);
            //                if (minorCiv != null)
            //                {
            //                    aCiv._contactList.Add(minorCiv); //Vulcans #146);  
            //                    minorCiv._contactList.Add(aCiv);
            //                }
            //            }
            //            break;
            //        case CivEnum.ROM:
            //            {
            //                CivData minorCiv = CivFromEnum(CivEnum.ZAKDORN);
            //                if (minorCiv != null)
            //                {
            //                    aCiv._contactList.Add(minorCiv); //Zackdorn #155);
            //                    minorCiv._contactList.Add(aCiv);
            //                }
            //            }
            //            break;

            //        case CivEnum.KLING:
            //            {
            //                CivData minorCiv = CivFromEnum(CivEnum.KRIOSIANS);
            //                if (minorCiv != null)
            //                {
            //                    aCiv._contactList.Add(minorCiv); //Kriosians);
            //                    minorCiv._contactList.Add(aCiv);
            //                }
            //            }
            //            break;
            //        case CivEnum.CARD:
            //            {
            //                CivData minorCiv = CivFromEnum(CivEnum.BAJORANS);
            //                if (minorCiv != null)
            //                {
            //                    aCiv._contactList.Add(minorCiv); //Bajorans #23);
            //                    minorCiv._contactList.Add(aCiv);
            //                }
            //            }
            //            break;
            //        case CivEnum.DOM:
            //            {
            //                CivData minorCiv = CivFromEnum(CivEnum.DOSI);
            //                if (minorCiv != null)
            //                {
            //                    aCiv._contactList.Add(minorCiv); //Dosi #50);
            //                    minorCiv._contactList.Add(aCiv);
            //                }
            //            }
            //            break;
            //        case CivEnum.BORG:
            //            {
            //                CivData minorCiv = CivFromEnum(CivEnum.NECHANI);
            //                if (minorCiv != null)
            //                {
            //                    aCiv._contactList.Add(minorCiv); //Nechani #96);
            //                    minorCiv._contactList.Add(aCiv);
            //                }
            //            }
            //            break;
            //        default:
            //            break;
            //    }
        }

        //public CivData CivFromEnum(CivEnum civEnum)
        //{

        //    if (CivData.civsInGame.Count > 0)
        //        foreach (CivData aCivData in CivData.civsInGame)
        //        {
        //            if (aCivData.CivEnum == civEnum)
        //                return aCivData;
        //        }
        //    return null;
        //}
        //public CivEnum CivEnumFromName(string Name)
        //{
        //    var file = new FileStream(Name, FileMode.Open, FileAccess.Read);

        //    var _dataPoints = new List<string>();
        //    using (var reader = new StreamReader(file))
        //    {

        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            if (line == null)
        //                continue;
        //            _dataPoints.Add(line.Trim());
        //            if (line.Length > 0)
        //            {
        //                var coll = line.Split("-");
        //                if (Enum.TryParse<CivEnum>(coll[0], out CivEnum civEnum))
        //                {
        //                    return civEnum;
        //                }

        //            }
        //        }
        //        return CivEnum.UNINHABITED;
        //    }
        //}

        //public CivData(int sysCivInt)
        //{
        //    this._civID = sysCivInt;
        //    //this.CivEnum
        //}
    }
}


