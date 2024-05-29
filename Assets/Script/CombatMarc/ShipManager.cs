using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System.Diagnostics.CodeAnalysis;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;
    public ShipData shipData;
    public GameObject ShipPrefab;
    public List<ShipController> ShipControllerList;
    public List<ShipSO> ShipSOListTech0;
    private List<ShipData> shipsOfFirstFleet;
    public List<ShipSO> ShipSOListTech1;
    public List<ShipSO> ShipSOListTech2;
    public List<ShipSO> ShipSOListTech3;
    private bool firstFleet= false;

    //public Dictionary<CivEnum, List<ShipData>> ShipDictionary; //all the fleets of that civ

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //shipData = gameObject.AddComponent<ShipData>();
        //shipData.ShipName = "999";
        //List <ShipData> list = new List<ShipData>() { shipData };
        //ShipDictionary = new Dictionary<CivEnum, List<ShipData>>() { { CivEnum.ZZUNINHABITED9, list } };
    }
    public void FirstShipDateByTechlevel(int techLevel)
    {
        if (techLevel == 0)
        {
            ShipDataFromSO(ShipSOListTech0);
            firstFleet = true;
        }
        if (techLevel == 1)
        {
            ShipDataFromSO(ShipSOListTech1);
            firstFleet = true;
        }
        if (techLevel == 2)
        {
            ShipDataFromSO(ShipSOListTech2);
            firstFleet = true;
        }
        if (techLevel == 3)
        {
            ShipDataFromSO(ShipSOListTech3);
            firstFleet = true;
        }
    }
    public void ShipDataFromSO(List<ShipSO> shipSOList)
    {
        List<CivEnum> listOfCivEnum = CivManager.instance.CivSOInGame;

        foreach (var shipSO in shipSOList)
        {
            if (listOfCivEnum != null && listOfCivEnum.Contains(shipSO.CivEnum))
            {
                ShipData shipData = new ShipData();
                shipData.ShipName = shipSO.ShipName;
                shipData.CivEnum = shipSO.CivEnum;
                shipData.TechLevel = shipSO.TechLevel;
                shipData.ShipType = shipSO.ShipType;
                shipData.maxWarpFactor = shipSO.maxWarpFactor;
                shipData.currentWarpFactor = 0f;
                shipData.ShieldMaxHealth = shipSO.ShieldMaxHealth;
                shipData.HullMaxHealth = shipSO.HullMaxHealth;
                shipData.TorpedoDamage = shipSO.TorpedoDamage;
                shipData.BeamDamage = shipSO.BeamDamage;
                shipData.Cost = shipSO.Cost;
                if (firstFleet)
                {
                    shipsOfFirstFleet.Add(shipData);
                }
            }
            if (firstFleet)
                FleetManager.instance.SendFirstFleetShipData(shipsOfFirstFleet);
            firstFleet = false;
        }
    }

    public void GetFirstFleetShips(List<ShipSO> shipSOList)
    {
        for (int i = 0; i < shipSOList.Count; i++)
        {

        }
    }
    public void InstantiateShip(ShipData fleetData)
    {

        //GameObject shipNewGameOb = (GameObject)Instantiate(shipPrefab, new Vector3(0, 0, 0),
        //        Quaternion.identity);
        //shipNewGameOb.transform.Translate(new Vector3(fleetData.position.x + 40f, fleetData.position.y, fleetData.position.z + 10f));
        //shipNewGameOb.transform.SetParent(galaxyCenter.transform, true);
        //shipNewGameOb.transform.localScale = new Vector3(1, 1, 1);

        //shipNewGameOb.name = fleetData.CivOwnerEnum.ToString() + " Fleet " + fleetData.Name; // game object FleetName
        //                                                                                      //var canvas = shipNewGameOb.GetComponent<Canvas>();
        //                                                                                      //var fleet = FindGameObjectInChildrenWithTag(shipNewGameOb,"Fleet");
        //                                                                                      //fleet.transform.SetParent(canvas.transform, true);
        //TextMeshProUGUI TheText = shipNewGameOb.GetComponentInChildren<TextMeshProUGUI>();

        //TheText.text = fleetData.CivShortName + " - Fleet " + fleetData.Name;
        //var Renderers = shipNewGameOb.GetComponentsInChildren<SpriteRenderer>();
        //foreach (var oneRenderer in Renderers)
        //{
        //    if (oneRenderer != null)
        //    {
        //        if (oneRenderer.name == "Insignia")
        //        {
        //            oneRenderer.sprite = fleetData.Insignia;
        //        }
        //    }
        //}
        //DropLineMovable ourLineScript = shipNewGameOb.GetComponentInChildren<DropLineMovable>();

        //ourLineScript.GetLineRenderer();
        //ourLineScript.transform.SetParent(shipNewGameOb.transform, false);
        //Vector3 galaxyPlanePoint = new Vector3(shipNewGameOb.transform.position.x,
        //    galaxyImage.transform.position.y, shipNewGameOb.transform.position.z);
        //Vector3[] points = { shipNewGameOb.transform.position, galaxyPlanePoint };
        //ourLineScript.SetUpLine(points);

        //var controller = shipNewGameOb.GetComponentInChildren<FleetController>();

        //controller.fleetData = fleetData;

        //controller.fleetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

        //shipNewGameOb.SetActive(true);
        //ShipControllerList.Add(controller);

    }

    public void GetShipSObyInt()//civSO.CivInt)
    {
     
    }
}
