using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class GameData
    {
        public CivEnum LocalPlayer;
        public string NoDestination;
        private readonly List<string> destinationNames = new List<string>() { "No Destination Selected" };
        public List<string> DestinationNames { get { return destinationNames; } }
        public Dictionary<string, GameObject> DestinationDictionary = new Dictionary<string, GameObject>();
        void Start()
        {
            NoDestination = DestinationNames[0];
        }
        public void LoadGalacticDestinations(List<StarSysData> starSysDataList)
        {
            foreach (var sysData in starSysDataList)
            {
                destinationNames.Add(sysData.SysName);
                if (!DestinationDictionary.ContainsKey(sysData.SysName))
                    DestinationDictionary.Add(sysData.SysName, sysData.SysGameObject);
            }
        }
        public void LoadGalacticDestinations(FleetData fleetData, GameObject fleetGO)
        {
            destinationNames.Add(fleetData.Name);
            if (!DestinationDictionary.ContainsKey(fleetData.Name))
                DestinationDictionary.Add(fleetData.Name, fleetGO);
        }
        public void RemoveFleetFromGalaxyDestiations(FleetData fleetData, GameObject fleetGO)
        {
            destinationNames.Remove(fleetData.Name);
            DestinationDictionary.Remove(fleetData.Name);
        }
        public void LoadPlayerGalacticDestinations(PlayerDefinedTargetData playerTargetData, GameObject playerTargetGO)
        {
            destinationNames.Add(playerTargetData.Name);
            DestinationDictionary.Add(playerTargetData.Name, playerTargetGO);
        }
        public void RemovePlayerTargetFromGalaxyDestiations(PlayerDefinedTargetData playerTargetData)
        {
            destinationNames.Remove(playerTargetData.Name);
            DestinationDictionary.Remove(playerTargetData.Name);
        }
    }
}
