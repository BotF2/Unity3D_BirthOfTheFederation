
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class FleetData // : MonoBehaviour
    {
        public int CivIndex;
        public Sprite Insignia;
        public CivEnum CivOwnerEnum;
        public Vector3 Location;
        public List<Ship> ShipsList;
        public float WarpFactor;
        public float DefaultWarpFactor;
        public string Name;
        public string Description;
        public GameObject Destination;
       // public GameObject origin;
    }
}


