using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class PlayerDefinedTargetManager : MonoBehaviour
    {
        public static PlayerDefinedTargetManager instance;
        [SerializeField]
        private GameObject playerTargetPrefab;
        [SerializeField]
        private GameObject galaxyImageGO;
        public GameObject galaxyCenter;
        public string nameDestination;
        //public KeyCode heldKeyForMouseDown = KeyCode.Space;
        [SerializeField]
        private PlayerDefinedTargetSO playerDefinedTargetSO;
        [SerializeField]
        private Camera galaxyEventCamera;
        //  public List<PlayerDefinedTargetController> ManagersPlayerTargetControllerList;
        public List<GameObject> PlayerTargetGOList = new List<GameObject>(); // all player Defined GOs made

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
        }
        void Start()
        {

        }

        public void PlayerTargetFromData(GameObject fleetGO)
        {
            if (fleetGO.GetComponent<FleetController>().FleetData.CivEnum == GameController.Instance.GameData.LocalPlayerCivEnum)
            {
                PlayerDefinedTargetData playerTargetData = new PlayerDefinedTargetData();
                playerTargetData.Insignia = playerDefinedTargetSO.Insignia;
                playerTargetData.Description = playerDefinedTargetSO.Description;
                playerTargetData.CivOwnerEnum = GameController.Instance.GameData.LocalPlayerCivEnum;
                this.InstantiatePlayerTarget(playerTargetData, fleetGO);
            }
        }
        public void InstantiatePlayerTarget(PlayerDefinedTargetData playerTargetData, GameObject fleetGO)
        {
            Vector3 position = fleetGO.transform.position;
            GameObject playerDefinedTargetGO = (GameObject)Instantiate(playerTargetPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
            var playerController = playerDefinedTargetGO.GetComponentInChildren<PlayerDefinedTargetController>();
            playerController.galaxyEventCamera = galaxyEventCamera;
            playerController.galaxyBackgroundImage = galaxyImageGO;
            playerController.PlayerTargetData = playerTargetData;
            //playerController.TextComponent ToDo: number the destinations
            // Get position x and y defined by player
            playerDefinedTargetGO.transform.Translate(new Vector3(position.x + 20f, position.y, position.z));
            playerDefinedTargetGO.transform.SetParent(galaxyCenter.transform, true);

            playerDefinedTargetGO.transform.localScale = new Vector3(1, 1, 1);

            playerDefinedTargetGO.SetActive(true);
            PlayerTargetGOList.Add(playerDefinedTargetGO);
            AddPlayerControllerToAllControllers(playerController);

            //MapLineMovable ourLineToGalaxyImageScript = playerDefinedTargetGO.GetComponentInChildren<MapLineMovable>();

            //ourLineToGalaxyImageScript.GetLineRenderer();
            //ourLineToGalaxyImageScript.transform.SetParent(playerDefinedTargetGO.transform, false);
            // The line from Fleet to underlying galaxy image and to destination
            MapLineMovable itemMapLineScript = playerDefinedTargetGO.GetComponentInChildren<MapLineMovable>();

            itemMapLineScript.GetLineRenderer();
            itemMapLineScript.transform.SetParent(playerDefinedTargetGO.transform, false);
            Vector3 galaxyPlanePoint = new Vector3(playerDefinedTargetGO.transform.position.x,
                galaxyImageGO.transform.position.y, playerDefinedTargetGO.transform.position.z);
            Vector3[] points = { playerDefinedTargetGO.transform.position, galaxyPlanePoint };
            itemMapLineScript.SetUpLine(points);
            //fleetController.FleetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;
            playerController.DropLine = itemMapLineScript;

            fleetGO.GetComponent<FleetController>().TargetController = playerController;

        }
        void AddPlayerControllerToAllControllers(PlayerDefinedTargetController playerTargetController)
        {
            // ManagersPlayerTargetControllerList.Add(playerTargetController);
        }
        void RemovePlayerControllerToAllControllers(PlayerDefinedTargetController playerTargetController)
        {
            // ManagersPlayerTargetControllerList.Remove(playerTargetController);
        }
    }
}
