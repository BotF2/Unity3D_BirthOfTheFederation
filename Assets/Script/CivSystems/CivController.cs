using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class CivController : MonoBehaviour
    {
        //Fields
        public CivData civData;
        
        public List<CivController> civsWeKnowList;

        public void UpdateCredits()
        {
            civData.Credits += 50;
        }
    }
}
