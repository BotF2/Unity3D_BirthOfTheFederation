
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class FleetData
    {
        public int CivIndex;
        public Sprite Insignia;
        public CivEnum CivOwnerEnum;
        public Vector3 Position;
        public List<Ship> ShipsList;
        public float MaxWarpFactor;
        public float DefaultWarpFactor;
        public string CivLongName;
        public string CivShortName;
        public string Name;
        public string Description;
       // public GameObject Destination;
        public List<Ship> ShipList;
        public float deltaYofGalaxyImage;
        //public FleetController fleetController;
       // public GameObject origin;
       public FleetData(string name)
        {
            Name = name;
        }
        public FleetData()
        {

        }
    }
}


