using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using System.Diagnostics.CodeAnalysis;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;
    public GameObject ShipControllerPrefab;
    public GameObject ShipDataPrefab;
    public List<ShipController> ShipControllerList;
    public List<ShipController> allFirstFleetShipControllerList;
    public List<ShipSO> ShipSOListTech0;
    public List<ShipSO> ShipSOListTech1;
    public List<ShipSO> ShipSOListTech2;
    public List<ShipSO> ShipSOListTech3;

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
    }
    public void FirstShipDateByTechlevel(int techLevel)
    {
        if (techLevel == 0)
        {
            ShipDataFromSO(ShipSOListTech0);
        }
        if (techLevel == 1)
        {
            ShipDataFromSO(ShipSOListTech1);
        }
        if (techLevel == 2)
        {
            ShipDataFromSO(ShipSOListTech2);
        }
        if (techLevel == 3)  
        {
            ShipDataFromSO(ShipSOListTech3);
        }
    }
    public void ShipDataFromSO(List<ShipSO> shipSOList)
    {
        List<CivEnum> listOfCivEnum = CivManager.instance.CivSOInGame;

        foreach (var shipSO in shipSOList)
        {
            if (listOfCivEnum != null && listOfCivEnum.Contains(shipSO.CivEnum))
            {
                GameObject shipDataGO = (GameObject)Instantiate(ShipDataPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
                var shipData = shipDataGO.GetComponent<ShipData>();
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
                GameObject shipNewGameOb = (GameObject)Instantiate(ShipControllerPrefab, new Vector3(0, 0, 0),
                                Quaternion.identity);
                shipDataGO.transform.SetParent(shipNewGameOb.transform);

                shipNewGameOb.name = shipData.ShipName;
                //shipNewGameOb.name = "Feddy";
                ShipController shipController = shipNewGameOb.GetComponent<ShipController>();
                shipController.ShipData = shipData;
                ShipControllerList.Add(shipController);
                FleetManager.instance.UpdateFleetShipControllers(shipController);
            }
        }
    }
    public void SendEarlyCivSOListForFistShips(List<CivSO> listCivSO)// current is small list, ToDo random and large list
    { //This is before Menu selection of size, tech...
        for (int i = 0; i < listCivSO.Count; i++)
        {
            GameObject shipNewGameOb = (GameObject)Instantiate(ShipControllerPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
            shipNewGameOb.name = "First Ship" + i.ToString();
            shipNewGameOb.gameObject.tag = "ShipPlaceHolder";
            ShipController shipController = shipNewGameOb.GetComponent<ShipController>(); 
            allFirstFleetShipControllerList.Add(shipController);
        }
    }
    public List<ShipController> GetShipControllersOfFirstFleet()
    {
        return allFirstFleetShipControllerList;// not to early
    }
    public void InstantiateShip(ShipData fleetData)
    {


        //shipNewGameOb.transform.Translate(new Vector3(fleetData.position.x + 40f, fleetData.position.y, fleetData.position.z + 10f));
        //shipNewGameOb.transform.SetParent(galaxyCenter.transform, true);
        //shipNewGameOb.transform.localScale = new Vector3(1, 1, 1);

        //shipNewGameOb.name = fleetData.CivOwnerEnum.ToString() + " Fleet " + fleetData.Name; // game object CivName
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

        //var clickedController = shipNewGameOb.GetComponentInChildren<FleetController>();

        //clickedController.fleetData = fleetData;

        //clickedController.fleetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

        //shipNewGameOb.SetActive(true);
        //ShipControllerList.Add(clickedController);

    }
}
