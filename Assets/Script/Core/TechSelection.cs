using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Core
{

    public class TechSelection : MonoBehaviour, IPointerDownHandler
    {
        Toggle _activeTechToggle;
        public Toggle Early, Developed, Advanced, Supreme;
        ToggleGroup TechLevelGroup;
        public GameObject Canvas;

        private void Awake()
        {
            Canvas = GameObject.Find("canvas"); // What changed? Now we have to code that unity use to assign in the Inspector.
            var _mainMenu = Canvas.transform.Find("PanelMain_Menu").gameObject;
            var _technologyGroup = _mainMenu.transform.Find("TECHNOLOGY").gameObject;
            TechLevelGroup = _technologyGroup.GetComponent<ToggleGroup>();
            TechLevelGroup.RegisterToggle(Early);
            TechLevelGroup.RegisterToggle(Developed);
            TechLevelGroup.RegisterToggle(Advanced);
            TechLevelGroup.RegisterToggle(Supreme);

            Early.isOn = true;
            Developed.isOn = false;
            Advanced.isOn = false;
            Supreme.isOn = false;
        }
        private void Start()
        {
            TechLevelGroup = GetComponent<ToggleGroup>();
            TechLevelGroup.enabled = true;
            Early.isOn = true;
            Early.Select();
            Early.OnSelect(null); // turns background selected color on, go figure.
            Developed.isOn = false;
            Advanced.isOn = false;
            Supreme.isOn = false;
        }
        private void Update()
        {
            if (GameManager.Instance._statePassedLobbyInit)
            {
                _activeTechToggle = TechLevelGroup.ActiveToggles().ToArray().FirstOrDefault();
                ActiveToggle();
            }
        }
        public void ActiveToggle()
        {
            switch (_activeTechToggle.name.ToUpper())
            {
                case "TOGGLE_SUPREME":
                    Supreme = _activeTechToggle;
                    GameManager.Instance.TechLevelOnLoadGame = TechLevel.SUPREME;
                    //GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"SupremeGameObjectsData.txt");
                    Debug.Log("Active FedLocalPalyerToggle.");
                    break;
                case "TOGGLE_ADVANCED":
                    Debug.Log("Active KlingLocalPlayerToggle.");
                    Advanced = _activeTechToggle;
                    GameManager.Instance.TechLevelOnLoadGame = TechLevel.ADVANCED;
                    //GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt");// "AdvancedGameObjectData.txt");
                    break;
                case "TOGGLE_DEVELOPED":
                    Debug.Log("Active RomLocalPlayerToggle.");
                    Developed = _activeTechToggle;
                    GameManager.Instance.TechLevelOnLoadGame = TechLevel.DEVELOPED;
                    //GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"DevelopedGameObjectsData.txt");
                    break;
                case "TOGGLE_EARLY":
                    Debug.Log("Active CardLocalPlayerToggle.");
                    Early = _activeTechToggle;
                    GameManager.Instance.TechLevelOnLoadGame = TechLevel.EARLY;
                    //GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"EarlyGameObjectData.txt");
                    break;
                default:
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}

