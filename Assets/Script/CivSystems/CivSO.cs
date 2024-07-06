using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Core
{
    [CreateAssetMenu(fileName = "CivSO", menuName = "CivSO")]
    public class CivSO : ScriptableObject
    {
        public int CivInt;
        public CivEnum CivEnum;
        public string CivShortName;
        public string CivLongName;
        public string CivHomeSystem; //best way???
        public string TraitOne;
        public string TraitTwo;
        public Sprite CivImage;
        public Sprite Insignia;
        public int Population;
        public int Credits;
        public int TechPoints;
        public TechLevel CivTechLevel; // ToDo we could define tech level by TechPoints???
        public bool Playable;
        public bool HasWarp;
        public string Decription;
        //public List<StarSysController> StarSysOwned;
        //public List<CivController> ContactList;
        //public float TaxRate; // universal or variable by civ/sys??
        //public float GrowthRate; // universal or variable by civ/sys??
        public float IntelPoints;
        //public List<CivData> ContactList = new List<CivData>();
    }
}


