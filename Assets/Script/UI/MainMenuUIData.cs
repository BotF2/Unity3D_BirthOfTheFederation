using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using TMPro;

namespace Assets.Core
{
    public class MainMenuData
    {
        public GalaxyMapType SelectedGalaxyType;// { get; private set; }
        public GalaxySize SelectedGalaxySize; //{ get; private set; }
        public TechLevel SelectedTechLevel; //{ get; private set; }
        public List<CivEnum> InGamePlayableCivList = new List<CivEnum>();
    }
}

