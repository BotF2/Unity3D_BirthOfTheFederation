using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;

namespace Assets.Core
{
    public class PlayerDefinedTargetManager : MonoBehaviour
    {
        public static PlayerDefinedTargetManager instance;
        [SerializeField]
        private GameObject playerTargetPrefab;
        public GameObject galaxyImage;
        public GameObject galaxyCenter;
        public string nameOfLocalFleet;
        [SerializeField]
        private Camera galaxyEventCamera;
        public List<PlayerDefinedTargetController> ManagersPlayerTargetControllerList;
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
            //PlayerTargetDictionary = new Dictionary<CivEnum, List<PlayerDefinedTargetData>>() { { CivEnum.ZZUNINHABITED9, list } };
        }
        void Start()
        {
            //GalaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
        }
        private void OnMouseDown()
        {
            
            galaxyEventCamera = GameObject.FindGameObjectWithTag("Galactic Camera").GetComponent<Camera>();
            Ray ray = galaxyEventCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                this.InstantiatePlayerTarget(nameOfLocalFleet, hitObject.transform.position);
            }
        }
        public void InstantiatePlayerTarget(string fleetName, Vector3 position)
        {

            GameObject playerDefinedTargetGO = (GameObject)Instantiate(playerTargetPrefab, new Vector3(0, 0, 0),
                    Quaternion.identity);
            // ***** ToDo: Get position defined by player

            playerDefinedTargetGO.transform.SetParent(galaxyCenter.transform, true);
            playerDefinedTargetGO.transform.Translate(new Vector3(position.x, position.y, position.z));
            playerDefinedTargetGO.transform.localScale = new Vector3(1, 1, 1);

            playerDefinedTargetGO.name = fleetName + " PlayerTarget ";

            TextMeshProUGUI TheText = playerDefinedTargetGO.GetComponentInChildren<TextMeshProUGUI>();

            TheText.text = playerDefinedTargetGO.name;
            //var Renderers = playerDefinedTargetGO.GetComponentsInChildren<SpriteRenderer>();
            //foreach (var oneRenderer in Renderers)
            //{
            //    if (oneRenderer != null)
            //    {
            //        if (oneRenderer.name == "InsigniaSprite")
            //        {
            //            oneRenderer.sprite = playerDefinedTargetData.InsigniaSprite;
            //        }
            //    }
            //}
            MapLineFixed ourDropLine = playerDefinedTargetGO.GetComponentInChildren<MapLineFixed>();

            ourDropLine.GetLineRenderer();

            Vector3 galaxyPlanePoint = new Vector3(playerDefinedTargetGO.transform.position.x,
                galaxyImage.transform.position.y, playerDefinedTargetGO.transform.position.z);
            Vector3[] points = { playerDefinedTargetGO.transform.position, galaxyPlanePoint };
            ourDropLine.SetUpLine(points);

            var playerTargetController = playerDefinedTargetGO.GetComponentInChildren<PlayerDefinedTargetController>();



            // playerTargetController.playerTargetData.yAboveGalaxyImage = galaxyCenter.transform.position.y - galaxyPlanePoint.y;

            playerDefinedTargetGO.SetActive(true);
            PlayerTargetGOList.Add(playerDefinedTargetGO);
            AddPlayerConrollerToAllControllers(playerTargetController);
            //GameManager.Instance.GameData.LoadPlayerGalacticDestinations(playerDefinedTargetData, playerDefinedTargetGO);
        }
        void AddPlayerConrollerToAllControllers(PlayerDefinedTargetController playerTargetController)
        {
            ManagersPlayerTargetControllerList.Add(playerTargetController);
        }
        void RemovePlayerConrollerToAllControllers(PlayerDefinedTargetController playerTargetController)
        {
            ManagersPlayerTargetControllerList.Remove(playerTargetController);
        }
    }
}
