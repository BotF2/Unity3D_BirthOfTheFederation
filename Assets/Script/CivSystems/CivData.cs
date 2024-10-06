using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Core
{

    public class CivData // has list of civsInGame, it's starSytems Data
    {
        public int CivInt;
        public CivEnum CivEnum;
        public string CivShortName;
        public string CivLongName;
        public string CivHomeSystem;
        public string TraitOne;
        public string TraitTwo;
        public Sprite CivImage;
        public Sprite Insignia;
        public int Population;
        public int Credits;
        public int TechPoints;
        public TechLevel CivTechLevel; // all cis have tech points, tech level is more a playable race game state thing????
        public bool Playable;
        public bool PlayedByAI = true;
        public CivEnum LocalPlayerCivEnum;
        public bool HasWarp;
        public string Decription;
        public List<StarSysController> StarSysOwned;
        public List<CivController> CivControllersWeKnow;
        public List<CivEnum> CivEnumsWeKnow;
        //public float TaxRate; // universal or variable by civ/sys??
        //public float GrowthRate; // universal or variable by civ/sys??
        public float IntelPoints;
        
        public void AddToCivControllersWeKnow(CivController civControllerWeFound)
        {
            CivControllersWeKnow.Add(civControllerWeFound);
            CivEnumsWeKnow.Add(civControllerWeFound.CivData.CivEnum);
        }
    }
}

