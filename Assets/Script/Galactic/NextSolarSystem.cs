using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Core
{
    public class NextSolarSystem : MonoBehaviour 
    {
        public GameObject solarSystemView;

        public void ShowThisSolarSystemView(int buttonSystemID)
        {
            solarSystemView = GameObject.Find("solarSystemView");
            SolarSystemView view = solarSystemView.GetComponent<SolarSystemView>();
            view.ShowNextSolarSystemView(buttonSystemID);
            
        }
    }
}
