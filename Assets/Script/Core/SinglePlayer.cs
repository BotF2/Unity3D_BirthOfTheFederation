using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Core
{

    public class SinglePlayer : MonoBehaviour, IPointerDownHandler
    {
        Button _singlePlayerButton;
        private void Awake()
        {
            _singlePlayerButton = GetComponent<Button>();      
        }
        private void Start()
        {
            //_isSinglePlayer.enabled = true;
            //_isMultipPlayer.enabled = true;
            //Fed.isOn = true;
            //Fed.Select();
            //Fed.OnSelect(null); // turns background selected color on, go figure.
            //Kling.isOn = false;
            //Rom.isOn = false;
            //Card.isOn = false;
            //Dom.isOn = false;
            //Borg.isOn = false;
        }
        private void Update()
        {
            //var what = _isSinglePlayer.onClick;
            //{
            //    _isSinglePlayer.enabled = true;
            //    //_isMultipPlayer.enabled = false;
            //    GameManager.Instance.SinglePlayerLobbyClicked();
            //}
            //if (_isMultipPlayer.onClick)
            //{
            //    _isSinglePlayer.enabled = false;
            //    _isMultipPlayer.enabled = true;
            //    GameManager.Instance.MultiPlayerLobbyClicked();
            //}
            //_activeHostToggle = SinglePlayerCivilizationGroup.ActiveToggles().ToArray().FirstOrDefault();
            //ActiveToggle();
        }
        //public void SinglePlay()
        //{

        //}
        //public void MultiPlay()
        //{

        //}
        //public void OnClickPlayCiv() // ToDo: call this on play button in Main Menu
        //{
        //    //GameManager.Instance.localPlayer = _
        //    Toggle toggle = _activeHostToggle;
        //    Debug.Log(toggle.CivName + " _ ");
        //}
        //public void ActiveToggle()
        //{
        //    //switch (_activeHostToggle.CivName.ToUpper())
        //    //{
        //    //    case "TOGGLE_FED":
        //    //        Fed = _activeHostToggle;
        //    //        GameManager.Instance.localPlayer = Civilization.FED;
        //    //        Debug.Log("Active Fed.");
        //    //        break;
        //    //    case "TOGGLE_KLING":
        //    //        Debug.Log("Active Kling.");
        //    //        GameManager.Instance.localPlayer = Civilization.KLING;
        //    //        Kling = _activeHostToggle;
        //    //        break;
        //    //    case "TOGGLE_ROM":
        //    //        Debug.Log("Active Rom.");
        //    //        Rom = _activeHostToggle;
        //    //        GameManager.Instance.localPlayer = Civilization.ROM;
        //    //        break;
        //    //    case "TOGGLE_CARD":
        //    //        Debug.Log("Active Card.");
        //    //        Card = _activeHostToggle;
        //    //        GameManager.Instance.localPlayer = Civilization.CARD;
        //    //        break;
        //    //    case "TOGGLE_DOM":
        //    //        Debug.Log("Active Dom.");
        //    //        Dom = _activeHostToggle;
        //    //        GameManager.Instance.localPlayer = Civilization.DOM;
        //    //        break;
        //    //    case "TOGGLE_BORG":
        //    //        Debug.Log("Active Borg.");
        //    //        Borg = _activeHostToggle;
        //    //        GameManager.Instance.localPlayer = Civilization.BORG;
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}
        //}

        public void OnPointerDown(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
