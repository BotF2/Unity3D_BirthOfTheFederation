using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;

public class PlayerDefinedTargetManager : MonoBehaviour
{
    public static PlayerDefinedTargetManager instance;

    //public List<FleetSO> fleetSOList;// all possible fleetSO(s), one list for each civ
    public GameObject playerDefinedTargetPrefab;
    public GameObject galaxyImage;
    public GameObject galaxyCenter;
    public List<PlayerDefinedTargetController> ManagersPlayerTargetControllerList;
    public List<GameObject> PlayerTargetGOList = new List<GameObject>(); // all fleetGO GOs made
                                                                                //public Dictionary<CivEnum, List<playerDefinedTargetData>> FleetDictionary; //all the fleetGO datas of that civ
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
        var data = new PlayerDefinedTargetData("999");
        List<PlayerDefinedTargetData> list = new List<PlayerDefinedTargetData>() { data };
        //PlayerTargetDictionary = new Dictionary<CivEnum, List<PlayerDefinedTargetData>>() { { CivEnum.ZZUNINHABITED9, list } };
    }
    public void InstantiatePlayerTarget(PlayerDefinedTargetData playerDefinedTargetData, Vector3 position)
    {

        GameObject playerDefinedTargetGO = (GameObject)Instantiate(playerDefinedTargetPrefab, new Vector3(0, 0, 0),
                Quaternion.identity);
        // ***** ToDo: Get position defined by player
        //playerDefinedTargetGO.transform.Translate(new Vector3(playerDefinedTargetData.position.x + 40f, playerDefinedTargetData.position.y, playerDefinedTargetData.position.z + 10f));
        playerDefinedTargetGO.transform.SetParent(galaxyCenter.transform, true);
        playerDefinedTargetGO.transform.localScale = new Vector3(1, 1, 1);

        playerDefinedTargetGO.name = playerDefinedTargetData.CivOwnerEnum.ToString() + " PlayerTarget " + playerDefinedTargetData.Name; // game object CivName
                                                                     
        TextMeshProUGUI TheText = playerDefinedTargetGO.GetComponentInChildren<TextMeshProUGUI>();

        TheText.text = playerDefinedTargetData.CivShortName + " - Player Target " + playerDefinedTargetData.Name;
        playerDefinedTargetData.Name = TheText.text;
        var Renderers = playerDefinedTargetGO.GetComponentsInChildren<SpriteRenderer>();
        foreach (var oneRenderer in Renderers)
        {
            if (oneRenderer != null)
            {
                if (oneRenderer.name == "Insignia")
                {
                    oneRenderer.sprite = playerDefinedTargetData.Insignia;
                }
            }
        }
        DropLineMovable ourLineScript = playerDefinedTargetGO.GetComponentInChildren<DropLineMovable>();

        ourLineScript.GetLineRenderer();
        ourLineScript.transform.SetParent(playerDefinedTargetGO.transform, false);
        Vector3 galaxyPlanePoint = new Vector3(playerDefinedTargetGO.transform.position.x,
            galaxyImage.transform.position.y, playerDefinedTargetGO.transform.position.z);
        Vector3[] points = { playerDefinedTargetGO.transform.position, galaxyPlanePoint };
        ourLineScript.SetUpLine(points);

        var playerTargetController = playerDefinedTargetGO.GetComponentInChildren<PlayerDefinedTargetController>();

        playerTargetController.playerTargetData = playerDefinedTargetData;

       // playerTargetController.playerTargetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

        playerDefinedTargetGO.SetActive(true);
        PlayerTargetGOList.Add(playerDefinedTargetGO);
        AddPlayerConrollerToAllControllers(playerTargetController);
        //GameManager.Instance.GameData.LoadPlayerGalacticDestinations(playerDefinedTargetData, playerDefinedTargetGO);
    }
    void AddPlayerConrollerToAllControllers(PlayerDefinedTargetController playerTargetController)
    {
        ManagersPlayerTargetControllerList.Add(playerTargetController);
        foreach (PlayerDefinedTargetController playerController in ManagersPlayerTargetControllerList)
        {
            playerController.AddPlayerTargetController(playerTargetController);
        }
    }
    void RemovePlayerConrollerToAllControllers(PlayerDefinedTargetController playerTargetController)
    {
        ManagersPlayerTargetControllerList.Remove(playerTargetController);
        foreach (PlayerDefinedTargetController playerController in ManagersPlayerTargetControllerList)
        {
            playerController.RemovePlayerTargetController(playerTargetController);
        }
    }
}
