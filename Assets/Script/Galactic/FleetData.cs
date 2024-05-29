
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
        private List<ShipController> shipsList;
        private float maxWarpFactor;
        public float CurrentWarpFactor;
        public string CivLongName;
        public string CivShortName;
        public string Name;
        private string description;
        public float yAboveGalaxyImage;

        public FleetData(FleetSO fleetSO)
        {
            Insignia = fleetSO.Insignia;
            shipsList = fleetSO.ShipsList;
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
            return shipsList;
        }
        public void SetShipList(List<ShipController> newShipList)
        {
            shipsList = newShipList;
        }
        public void AddToShipList(ShipController shipController)
        {
            shipsList.Add(shipController);
        }
        public void RemoveFromShipList(ShipController shipController)
        {
            shipsList.Remove(shipController);
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


