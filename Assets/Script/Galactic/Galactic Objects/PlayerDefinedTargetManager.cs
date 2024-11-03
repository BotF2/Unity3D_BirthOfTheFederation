using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Assets.Core;
using TMPro;

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
        public KeyCode heldKeyForMouseDown = KeyCode.Space;
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
            PlayerDefinedTargetData playerTargetData = new PlayerDefinedTargetData();
            playerTargetData.Insignia = playerDefinedTargetSO.Insignia;
            playerTargetData.Description = playerDefinedTargetSO.Description;
            this.InstantiatePlayerTarget(playerTargetData, fleetGO.transform.position);
        }
        public void InstantiatePlayerTarget(PlayerDefinedTargetData playerTargetData, Vector3 position)
        {

            GameObject playerDefinedTargetGO = (GameObject)Instantiate(playerTargetPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
            var playerController = playerDefinedTargetGO.GetComponentInChildren<PlayerDefinedTargetController>();
            playerController.galaxyEventCamera = galaxyEventCamera;
            playerController.galaxyBackgroundImage = galaxyImageGO;
            playerController.playerTargetData = playerTargetData;
            //playerController.Name ToDo: number the destinations
            // Get position x and y defined by player
            playerDefinedTargetGO.transform.Translate(new Vector3(position.x + 5f, position.y +20f, position.z));
            playerDefinedTargetGO.transform.SetParent(galaxyCenter.transform, true);

            playerDefinedTargetGO.transform.localScale = new Vector3(1, 1, 1);

            playerDefinedTargetGO.SetActive(true);
            PlayerTargetGOList.Add(playerDefinedTargetGO);
            AddPlayerControllerToAllControllers(playerController);

            MapLineMovable ourLineToGalaxyImageScript = playerDefinedTargetGO.GetComponentInChildren<MapLineMovable>();

            ourLineToGalaxyImageScript.GetLineRenderer();
            ourLineToGalaxyImageScript.transform.SetParent(playerDefinedTargetGO.transform, false);
            Vector3 galaxyPlanePoint = new Vector3(playerDefinedTargetGO.transform.position.x,
                galaxyImageGO.transform.position.y, playerDefinedTargetGO.transform.position.z);
            Vector3[] points = { playerDefinedTargetGO.transform.position, galaxyPlanePoint };
            ourLineToGalaxyImageScript.SetUpLine(points);
     
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
