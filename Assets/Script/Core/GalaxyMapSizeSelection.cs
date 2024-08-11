using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Core
{

    public class GalaxyMapSizeSelection : MonoBehaviour, IPointerDownHandler
    {
        Toggle _activeGalaxyMapSizeToggle;
        public Toggle Small, Medium, Large;
        ToggleGroup GalaxyMapSizeGroup;
        public GameObject Canvas;

        private void Awake()
        {
            Canvas = GameObject.Find("canvas"); 
            var _mainMenu = Canvas.transform.Find("PanelMain_Menu").gameObject;
            var _technologyGroup = _mainMenu.transform.Find("GALAXYMAPSIZE").gameObject; // DID WE CONNECT THIS???
            GalaxyMapSizeGroup = _technologyGroup.GetComponent<ToggleGroup>();
            GalaxyMapSizeGroup.RegisterToggle(Small);
            GalaxyMapSizeGroup.RegisterToggle(Medium);
            GalaxyMapSizeGroup.RegisterToggle(Large);
      
            Small.isOn = true;
            Medium.isOn = false;
            Large.isOn = false;
        }
        private void Start()
        {
            GalaxyMapSizeGroup = GetComponent<ToggleGroup>();
            GalaxyMapSizeGroup.enabled = true;
            Small.isOn = true;
            Small.Select();
            Small.OnSelect(null); // turns background selected color on, go figure.
            Medium.isOn = false;
            Large.isOn = false;
        }
        private void Update()
        {
            if (GameManager.Instance._statePassedLobbyInit)
            {
                _activeGalaxyMapSizeToggle = GalaxyMapSizeGroup.ActiveToggles().ToArray().FirstOrDefault();
                ActiveToggle();
            }
        }
        public void ActiveToggle()
        {
            switch (_activeGalaxyMapSizeToggle.name.ToUpper())
            {
                case "TOGGLE_LARGE":
                    Debug.Log("Galaxy Map Large.");
                    Large = _activeGalaxyMapSizeToggle;
                    GameManager.Instance.GalaxySize = GalaxySize.LARGE;
                   // GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt");// "AdvancedGameObjectData.txt");
                    break;
                case "TOGGLE_MEDIUM":
                    Debug.Log("Galaxy Map Medium");
                    Medium = _activeGalaxyMapSizeToggle;
                    GameManager.Instance.GalaxySize = GalaxySize.MEDIUM;
                  //  GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"DevelopedGameObjectsData.txt");
                    break;
                case "TOGGLE_SMALL":
                    Debug.Log("Galaxy Map Small");
                    Small = _activeGalaxyMapSizeToggle;
                    GameManager.Instance.GalaxySize = GalaxySize.SMALL;
                   // GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"EarlyGameObjectData.txt");
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

