
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class FleetData // : MonoBehaviour
    {
        public int civIndex;
        public string fleetName;
        public string description;
        public Sprite insign;
        public CivEnum civOwnerEnum;
        public Vector3 location;
        public List<Ship> ships;
        public float warpFactor;
        public GameObject destination;
        public GameObject origin;
        public float defaultWarp = 0;
    }
}


