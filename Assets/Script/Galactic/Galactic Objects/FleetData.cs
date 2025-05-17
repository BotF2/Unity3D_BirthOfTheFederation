using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System.Linq;
using UnityEngine.UI;



public class FleetData
{
    public int CivIndex;
    public Sprite Insignia;
    public CivController CivController;
    public CivEnum CivEnum;
    public Vector3 Position;
    public List<ShipController> ShipsList;
    public List<FleetController> FleetGroupControllers; // used to hold fleets exchanging ships
    public float MaxWarpFactor = 3f;
    public float CurrentWarpFactor = 0f;
    public GameObject Destination;
    public GameObject LastDestination;
    public string CivLongName;
    public string CivShortName;
    public string Name;
    private string description;
    public int FleetInt;
    public List<int> EncounterIDs;
    public Button FleetButtonUp;
    public Button FleetButtonDown;
    public Button FleetButtonUIClose;
    public bool WarpButtonPressed = false;

    public FleetData(FleetSO fleetSO)
    {
        Insignia = fleetSO.Insignia;
        ShipsList = fleetSO.ShipsList;
        MaxWarpFactor = fleetSO.MaxWarpFactor;
        description = fleetSO.Description;
        CivIndex = fleetSO.CivIndex;
        CivEnum = fleetSO.CivOwnerEnum;
        CivLongName = CivManager.Instance.GetCivDataByCivEnum(CivEnum).CivLongName;
        CivShortName = CivManager.Instance.GetCivDataByCivEnum(CivEnum).CivShortName;
        IEnumerable<CivController> ourCivManagers =
                from x in CivManager.Instance.CivControllersInGame
                where (x.CivData.CivInt == (int)CivEnum)
                select x;
        CivController = ourCivManagers.ToList().FirstOrDefault();
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
        return MaxWarpFactor;
    }
    public string GetDescription()
    {
        return description;
    }

    public string GetFleetName() { return this.Name; }
}



