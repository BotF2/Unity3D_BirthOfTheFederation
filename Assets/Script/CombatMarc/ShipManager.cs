using Assets.Core;
using System.Collections.Generic;
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

    public List<GameObject> ShipDataFromSO(List<ShipSO> shipSOList)
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
                shipData.Price = shipSO.Price;
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
            shipGOs = ShipDataFromSO(ships);
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
            case 0:
                foreach (var shipSO in ShipSOListTech0)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            case 1:
                foreach (var shipSO in ShipSOListTech1)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            case 2:
                foreach (var shipSO in ShipSOListTech2)
                {
                    if (shipSO.CivEnum == civ)
                    {
                        listOfShipSOs.Add(shipSO);
                    }
                }
                break;
            case 3:
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
