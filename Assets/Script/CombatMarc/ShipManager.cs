using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;
    public ShipData shipData;
    public GameObject ShipPrefab;
    public List<ShipSO> ShipSOList;// all possible ShipSO(s), one list for each civ
    public List<ShipController> ShipControllerList;
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
        shipData = gameObject.AddComponent<ShipData>();
        shipData.shipName = "999";
        List <ShipData> list = new List<ShipData>() { shipData };
        //ShipDictionary = new Dictionary<CivEnum, List<ShipData>>() { { CivEnum.ZZUNINHABITED9, list } };
    }
    public void ShipDataFromSO(CivSO civSO) 
    {
        //ShipSO ShipSO = GetShipSObyInt();//(civSO.CivInt);
        //if (ShipSO != null)
        //{
        //    ShipData ShipData = new ShipData();
        //    ShipData.CivIndex = ShipSO.CivIndex;
        //    ShipData.Insignia = ShipSO.Insignia;
        //    ShipData.CivOwnerEnum = ShipSO.CivOwnerEnum;
        //    ShipData.Position = position;

        //    ShipData.ShipsList = ShipSO.ShipsList;
        //    ShipData.MaxWarpFactor = ShipSO.WarpFactor;
        //    ShipData.DefaultWarpFactor = ShipSO.DefaultWarpFactor;
        //    ShipData.CivLongName = civSO.CivLongName;
        //    ShipData.CivShortName = civSO.CivShortName;
        //    ShipData.Description = ShipSO.Description;
        //    ShipData.Name = "998";
        //    // List not Dictionary
        //    //if (!FleetDictionary.ContainsKey(civSO.CivEnum))
        //    //{
        //    //    List<FleetData> listA = new List<FleetData>() { ShipData };
        //    //    FleetDictionary.Add(civSO.CivEnum, listA);
        //    //}
        //    //else FleetDictionary[civSO.CivEnum].Add(ShipData);
        //    //if (ShipData.Name != "999")
        //    //{
        //    //    GetFleetName(civSO.CivEnum);
        //    //    InstantiateShip(ShipData, position);
        //    //}
    }
    public void InstantiateShip(ShipData fleetData)
    {

        //GameObject shipNewGameOb = (GameObject)Instantiate(shipPrefab, new Vector3(0, 0, 0),
        //        Quaternion.identity);
        //shipNewGameOb.transform.Translate(new Vector3(fleetData.Position.x + 40f, fleetData.Position.y, fleetData.Position.z + 10f));
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

        //controller.fleetData.deltaYofGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

        //shipNewGameOb.SetActive(true);
        //ShipControllerList.Add(controller);

    }

    public void GetShipSObyInt()//civSO.CivInt)
    {
     
    }
}
