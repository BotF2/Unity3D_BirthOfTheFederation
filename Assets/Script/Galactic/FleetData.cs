
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
        public List<ShipController> ShipsList;
        private float maxWarpFactor;
        public float CurrentWarpFactor = 0f;
        public GameObject Destination;
        public string CivLongName;
        public string CivShortName;
        public string Name;
        private string description;
        public float yAboveGalaxyImage; // used in FleetManager

        public FleetData(FleetSO fleetSO)
        {
            Insignia = fleetSO.Insignia;
            ShipsList = fleetSO.ShipsList;
            maxWarpFactor = fleetSO.MaxWarpFactor;
            description = fleetSO.Description;
        }
       public FleetData(string name)
        {
            Name = name;
        }
        public FleetData()
        {

        }
        public List<ShipController> GetShipList()
        {
            return ShipsList;
        }
        public void SetShipList(List<ShipController> newShipList)
        {
            ShipsList = newShipList;
        }
        public void AddToShipList(ShipController shipController)
        {
            ShipsList.Add(shipController);
        }
        public void RemoveFromShipList(ShipController shipController)
        {
            ShipsList.Remove(shipController);
        }
        public float GetMaxWarpFactor()
        {
            return maxWarpFactor;
        }
        public string GetDescription()
        {
            return description;
        }
    }
}


