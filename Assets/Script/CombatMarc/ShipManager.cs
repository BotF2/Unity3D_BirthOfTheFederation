using Assets.Core;
using NUnit.Framework.Internal.Execution;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;
    [SerializeField]
    private GameObject shipControllerPrefab;
    [SerializeField]
    private GameObject shipDataPrefab;
    public List<ShipController> ShipControllerGameList;
    public List<ShipSO> ShipSOListTech0;
    public List<ShipSO> ShipSOListTech1;
    public List<ShipSO> ShipSOListTech2;
    public List<ShipSO> ShipSOListTech3;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public List<GameObject> ShipGameObjectWithDataFromSO(List<ShipSO> shipSOList)
    {
        List<GameObject> listShipGO = new List<GameObject>();
        foreach (var shipSO in shipSOList)
        {
            if (shipSO != null)
            {
                GameObject shipDataGO = (GameObject)Instantiate(shipDataPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
                var shipData = shipDataGO.GetComponent<ShipData>();
                shipData.ShipName = shipSO.ShipName;
                shipData.CivEnum = shipSO.CivEnum;
                shipData.TechLevel = shipSO.TechLevel;
                shipData.ShipType = shipSO.ShipType;
                if (shipSO.shipSprite != null)
                    shipData.ShipSprite = shipSO.shipSprite;
                shipData.maxWarpFactor = shipSO.maxWarpFactor;
                //shipData.currentWarpFactor = 0f;
                shipData.ShieldMaxHealth = shipSO.ShieldMaxHealth;
                shipData.HullMaxHealth = shipSO.HullMaxHealth;
                shipData.TorpedoDamage = shipSO.TorpedoDamage;
                shipData.BeamDamage = shipSO.BeamDamage;
                shipData.BuildDuration = shipSO.BuildDuration;
                GameObject shipNewGameOb = (GameObject)Instantiate(shipControllerPrefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                shipDataGO.transform.SetParent(shipNewGameOb.transform);

                shipNewGameOb.name = shipData.ShipName;
                ShipController shipController = shipNewGameOb.GetComponent<ShipController>();
                shipController.ShipData = shipData;
                ShipControllerGameList.Add(shipController);
                listShipGO.Add(shipNewGameOb);
            }
        }
        return listShipGO;
    }

    public int GetShipBuildDuration(ShipType shipType, TechLevel techLevel, CivEnum civEnum)
    {
        ShipSO aShipSO = new ShipSO();
        int duration = 1;
        aShipSO = GetShipSO(shipType, techLevel, civEnum);
        duration = aShipSO.BuildDuration;
        return duration;
    }
    private ShipSO GetShipSO(ShipType shipType, TechLevel techLevel, CivEnum civEnum)
    {
        ShipSO ourShipSO = new ShipSO();
        switch (techLevel)
        {
            case TechLevel.EARLY:
                var shipSOIEnumEarly = ShipSOListTech0.Where(x => x.ShipType == shipType && x.CivEnum == civEnum);
                var shipSOe = shipSOIEnumEarly.ToList().FirstOrDefault();
                ourShipSO = shipSOe;
                break;
            case TechLevel.DEVELOPED:
                var shipSOIEnumDeveloped = ShipSOListTech1.Where(x => x.ShipType == shipType && x.CivEnum == civEnum);
                var shipSOd = shipSOIEnumDeveloped.ToList().FirstOrDefault();
                ourShipSO = shipSOd;
                break;
            case TechLevel.ADVANCED:
                var shipSOIEnumAdvanced = ShipSOListTech1.Where(x => x.ShipType == shipType && x.CivEnum == civEnum);
                var shipSOa = shipSOIEnumAdvanced.ToList().FirstOrDefault();
                ourShipSO = shipSOa;
                break;
            case TechLevel.SUPREME:
                var shipSOIEnumSup = ShipSOListTech1.Where(x => x.ShipType == shipType && x.CivEnum == civEnum);
                var shipSOs = shipSOIEnumSup.ToList().FirstOrDefault();
                ourShipSO = shipSOs;
                break;
            default:
                break;
        }
        return ourShipSO;
    }
    public void BuildShipInOurFleet(ShipType shipType, GameObject fleetGO, StarSysController sysCon)
    {
        ShipSO ourShipSO = GetShipSO(shipType, sysCon.StarSysData.CurrentCivController.CivData.TechLevel, sysCon.StarSysData.CurrentOwner);
        List<ShipSO> shipSOAsList = new List<ShipSO> { ourShipSO };
        var ourShipGOList = ShipGameObjectWithDataFromSO(shipSOAsList); // takes a list of ShipSO
        for (int i = 0; i < ourShipGOList.Count; i++)
        {
            ourShipGOList[i].transform.SetParent(fleetGO.transform);
            fleetGO.GetComponent<FleetController>().FleetData.AddToShipList(ourShipGOList[i].GetComponent<ShipController>());
        }
    }
    public void BuildShipsOfFirstFleet(GameObject fleetGO)
    {
        var fleetCon = fleetGO.GetComponent<FleetController>();
        CivEnum civEnum = fleetCon.FleetData.CivEnum;
        List<ShipSO> ships = new List<ShipSO>();
        ships = FirstShipDateByTechlevel((int)CivManager.Instance.GetCivDataByCivEnum(civEnum).TechLevel, civEnum);
        //if (ships != null)
        List<GameObject> shipGOs = new List<GameObject>();
        if (ships != null)
        {
            shipGOs = ShipGameObjectWithDataFromSO(ships);
            foreach (GameObject shipGO in shipGOs)
            {
                if (shipGO != null)
                {
                    shipGO.transform.SetParent(fleetGO.transform);
                    fleetCon.FleetData.ShipsList.Add(shipGO.GetComponent<ShipController>());
                }
            }
        }

        fleetCon.UpdateMaxWarp();
        //fleetCon.FleetData.CurrentWarpFactor = 0f;
    }
    public List<ShipSO> FirstShipDateByTechlevel(int techLevel, CivEnum civ)
    {
        List<ShipSO> listOfShipSOs = new List<ShipSO>();
        switch (techLevel)
        {
            case 100:// early
                foreach (var shipSO in ShipSOListTech0)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            case 300: // developed
                foreach (var shipSO in ShipSOListTech1)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            case 600: // advanced
                foreach (var shipSO in ShipSOListTech2)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            case 900: // supreme
                foreach (var shipSO in ShipSOListTech3)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            default:
                foreach (var shipSO in ShipSOListTech0)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                };
                break;
        }
        return listOfShipSOs;
    }
}
