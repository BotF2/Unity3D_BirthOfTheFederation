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
        public GameObject GalaxyCenter;
        public string nameDestination;
        
        //public KeyCode heldKeyForMouseDown = KeyCode.Space;
        [SerializeField]
        private PlayerDefinedTargetSO playerDefinedTargetSO;
        [SerializeField]
        private Camera galaxyEventCamera;
       //public List<PlayerDefinedTargetController> ListPlayerTargetControllerList;
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
            PlayerTargetGOList.Add(playerDefinedTargetGO);
            playerDefinedTargetGO.layer = 6;
            var playerController = playerDefinedTargetGO.GetComponentInChildren<PlayerDefinedTargetController>();
            playerController.galaxyEventCamera = galaxyEventCamera;
            playerController.galaxyBackgroundImage = galaxyImageGO;
            playerController.PlayerTargetData = playerTargetData;
            
            playerController.PlayerTargetData.FleetController = fleetGO.GetComponentInChildren<FleetController>();
            playerController.PlayerTargetData.CivOwnerEnum = playerController.PlayerTargetData.FleetController.FleetData.CivEnum;

            playerDefinedTargetGO.transform.SetParent(GalaxyCenter.transform, true);
            playerDefinedTargetGO.transform.Translate(new Vector3(position.x + 20f, position.y, position.z));
            
            playerDefinedTargetGO.transform.localScale = new Vector3(1f, 1f, 1f);

            playerDefinedTargetGO.SetActive(true);
            PlayerTargetGOList.Add(playerDefinedTargetGO);
            //AddPlayerControllerToAllControllers(playerController);
            Canvas[] canvasArray = playerDefinedTargetGO.GetComponentsInChildren<Canvas>();
            for (int j = 0; j < canvasArray.Length; j++)
            {
                if (canvasArray[j].name == "CanvasToolTip")
                {
                    playerController.CanvasToolTip = canvasArray[j];
                }
            }
            var transGalaxyCenter = GalaxyCenter.gameObject.transform;
            var trans = fleetGO.gameObject.transform;
            MapLineMovable itemMapLineScript = playerDefinedTargetGO.GetComponentInChildren<MapLineMovable>();

            itemMapLineScript.GetLineRenderer();
            itemMapLineScript.lineRenderer.startColor = Color.yellow;
            itemMapLineScript.lineRenderer.endColor = Color.yellow;
            itemMapLineScript.transform.SetParent(playerDefinedTargetGO.transform, false);
            Vector3 galaxyPlanePoint = new Vector3(playerDefinedTargetGO.transform.position.x,
                galaxyImageGO.transform.position.y, playerDefinedTargetGO.transform.position.z);
            Vector3[] points = { playerDefinedTargetGO.transform.position, galaxyPlanePoint };
            itemMapLineScript.SetUpLine(points);
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
