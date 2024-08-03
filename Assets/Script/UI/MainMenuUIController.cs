using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Core
{
    public class MainMenuUIController : MonoBehaviour
    {
        public static MainMenuUIController instance;
        public GameObject mainMenuCanvas;
        public GameObject TipCanvas;
        //public GameObject mainMenuPanelSinglePlayer;
        //public GameObject mainMenuPanelTip;
        public GameObject mainMenuButton;
        public GameObject uiCameraGO;
        public GameObject galaxyCenter;
        public bool PastMainMenu = false;
        public GalaxyType selectedGalaxyType;
        public GalaxySize selectedGalaxySize;
        public TechLevel selectedTechLevel;
        public CivEnum selectedLocalCivEnum;
        public bool isSinglePlayer;

        //private AsyncOperation _SceneAsync;
        //private bool _bGalaxyShow = false;
        //private Scene PrevScene;
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
        }
        public void SetSingleVsMultiplayer(bool singleMultiSelection)
        {
            isSinglePlayer = singleMultiSelection;
        }
        public void SetGalaxySize(int index)
        {
            selectedGalaxySize = (GalaxySize)index;
            //GalaxyContro
        }

        public void SetGalaxyType(int index)
        {
            selectedGalaxyType = (GalaxyType)index;
        }

        public void SetTechLevel(int index)
        {
            selectedTechLevel = (TechLevel)index;
        }

        public void SetLocalCivilization(int index)
        {
            selectedLocalCivEnum = (CivEnum)index;
        }
        public void LoadGalaxyScene()
        {
            mainMenuCanvas.SetActive(false);
            uiCameraGO.SetActive(false);
            galaxyCenter.SetActive(true);
            PastMainMenu = true;
            TimeManager.instance.ResumeTime();
            SceneManager.LoadScene("GalaxyScene", LoadSceneMode.Additive);
            CivManager.instance.OnNewGameButtonClicked((int)selectedGalaxySize, (int)selectedTechLevel, (int)selectedGalaxyType,
                (int)selectedLocalCivEnum, isSinglePlayer);

        }
    }
}

