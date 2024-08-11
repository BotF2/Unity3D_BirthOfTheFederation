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
            //FedLocalPalyerToggle.isOn = true;
            //FedLocalPalyerToggle.Select();
            //FedLocalPalyerToggle.OnSelect(null); // turns background selected color on, go figure.
            //KlingLocalPlayerToggle.isOn = false;
            //RomLocalPlayerToggle.isOn = false;
            //CardLocalPlayerToggle.isOn = false;
            //DomLocalPlayerToggle.isOn = false;
            //BorgLocalPlayerToggle.isOn = false;
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
            //activeLocalPlayerToggle = SinglePlayerCivilizationGroup.ActiveToggles().ToArray().FirstOrDefault();
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
        //    //GameManager.Instance.LocalPlayer = _
        //    Toggle toggle = activeLocalPlayerToggle;
        //    Debug.Log(toggle.CivName + " _ ");
        //}
        //public void ActiveToggle()
        //{
        //    //switch (activeLocalPlayerToggle.CivName.ToUpper())
        //    //{
        //    //    case "TOGGLE_FED":
        //    //        FedLocalPalyerToggle = activeLocalPlayerToggle;
        //    //        GameManager.Instance.LocalPlayer = Civilization.FED;
        //    //        Debug.Log("Active FedLocalPalyerToggle.");
        //    //        break;
        //    //    case "TOGGLE_KLING":
        //    //        Debug.Log("Active KlingLocalPlayerToggle.");
        //    //        GameManager.Instance.LocalPlayer = Civilization.KLING;
        //    //        KlingLocalPlayerToggle = activeLocalPlayerToggle;
        //    //        break;
        //    //    case "TOGGLE_ROM":
        //    //        Debug.Log("Active RomLocalPlayerToggle.");
        //    //        RomLocalPlayerToggle = activeLocalPlayerToggle;
        //    //        GameManager.Instance.LocalPlayer = Civilization.ROM;
        //    //        break;
        //    //    case "TOGGLE_CARD":
        //    //        Debug.Log("Active CardLocalPlayerToggle.");
        //    //        CardLocalPlayerToggle = activeLocalPlayerToggle;
        //    //        GameManager.Instance.LocalPlayer = Civilization.CARD;
        //    //        break;
        //    //    case "TOGGLE_DOM":
        //    //        Debug.Log("Active DomLocalPlayerToggle.");
        //    //        DomLocalPlayerToggle = activeLocalPlayerToggle;
        //    //        GameManager.Instance.LocalPlayer = Civilization.DOM;
        //    //        break;
        //    //    case "TOGGLE_BORG":
        //    //        Debug.Log("Active BorgLocalPlayerToggle.");
        //    //        BorgLocalPlayerToggle = activeLocalPlayerToggle;
        //    //        GameManager.Instance.LocalPlayer = Civilization.BORG;
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
