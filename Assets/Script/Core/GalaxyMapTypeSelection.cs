using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Core
{

    public class GalaxyMapTypeSelection : MonoBehaviour, IPointerDownHandler
    {
        Toggle _activeGalaxyMapTypeToggle;
        public Toggle Canon, Random;
        ToggleGroup GalaxyMapTypeGroup;
        public GameObject Canvas;

        private void Awake()
        {
            Canvas = GameObject.Find("canvas"); 
            var _mainMenu = Canvas.transform.Find("PanelMain_Menu").gameObject;
            var _technologyGroup = _mainMenu.transform.Find("GALAXYMAPTYPE").gameObject; // DID WE CONNECT THIS???
            GalaxyMapTypeGroup = _technologyGroup.GetComponent<ToggleGroup>();
            GalaxyMapTypeGroup.RegisterToggle(Canon);
            GalaxyMapTypeGroup.RegisterToggle(Random);
     
            Canon.isOn = true;
            Random.isOn = false;
        }
        private void Start()
        {
            GalaxyMapTypeGroup = GetComponent<ToggleGroup>();
            GalaxyMapTypeGroup.enabled = true;
            Canon.isOn = true;
            Random.Select();
            Canon.OnSelect(null); // turns background selected color on, go figure.
            Random.isOn = false;
        }
        private void Update()
        {
            if (GameManager.Instance._statePassedLobbyInit)
            {
                _activeGalaxyMapTypeToggle = GalaxyMapTypeGroup.ActiveToggles().ToArray().FirstOrDefault();
                ActiveToggle();
            }
        }
        public void ActiveToggle()
        {
            switch (_activeGalaxyMapTypeToggle.name.ToUpper())
            {
                case "TOGGLE_CANON":
                    Debug.Log("CANON MAP");
                    Canon = _activeGalaxyMapTypeToggle;
                    GameManager.Instance.GalaxyType = GalaxyMapType.CANON;
                   // GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt");// "AdvancedGameObjectData.txt");
                    break;
                case "TOGGLE_RANOM":
                    Debug.Log("RANDOM MAP");
                    Random = _activeGalaxyMapTypeToggle;
                    GameManager.Instance.GalaxyType = GalaxyMapType.RANDOM;
                  //  GameManager.Instance.LoadStartGameObjectNames(Environment.CurrentDirectory + "\\Assets\\" + "Temp_GameObjectData.txt"); //"DevelopedGameObjectsData.txt");
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

