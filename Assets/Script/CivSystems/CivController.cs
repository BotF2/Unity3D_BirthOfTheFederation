using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class CivController : MonoBehaviour
    {
        //Fields
        public CivData CivData;
        public string CivShortName;
        public List<CivController> CivContollersWeHave;
        private List<CivController> civsWeKnowList;

        public void Start()
        {
        
        }
        public void UpdateCredits()
        {
            CivData.Credits += 50;
        }
    }
}
