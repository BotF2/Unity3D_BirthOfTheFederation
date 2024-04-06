using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class CivController : MonoBehaviour
    {
        //Fields
        public CivData civData;
        public List<CivData> civsWeKnowList;

        public void UpdateCredits()
        {
            civData.Credits += 50;
        }
    }
}
