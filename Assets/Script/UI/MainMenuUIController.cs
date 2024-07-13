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
        public GameObject eventSystemGO;
        public GameObject galaxyCenter;

        public GalaxyType selectedGalaxyType;
        public GalaxySize selectedGalaxySize;
        public TechLevel selectedTechLevel;
        public CivEnum selectedCivEnum;

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

        public void SetCivilization(int index)
        {
            selectedCivEnum = (CivEnum)index;
        }


        public void LoadGalaxyScene()
        {
            mainMenuCanvas.SetActive(false);
            //mainMenuPanelSinglePlayer.SetActive(false);
            //mainMenuPanelTip.SetActive(true);
            uiCameraGO.SetActive(false);
            eventSystemGO.SetActive(false);
            galaxyCenter.SetActive(true);

            SceneManager.LoadScene("GalaxyScene", LoadSceneMode.Additive);
            CivManager.instance.OnNewGameButtonClicked((int)selectedGalaxySize, (int)selectedTechLevel, (int)selectedGalaxyType);
           
        }

        //IEnumerator loadScene(string SceneName)
        //{
        //    AsyncOperation nScene = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        //    nScene.allowSceneActivation = false;
        //    _SceneAsync = nScene;
        //    while (nScene.progress < 0.9f)
        //    {
        //        Debug.Log("Loading scene " + " [][] Progress: " + nScene.progress);
        //        yield return null;
        //    }

        //    //Activate the Scene
        //    _SceneAsync.allowSceneActivation = true;

        //    while (!nScene.isDone)
        //    {
        //        // wait until it is really finished
        //        yield return null;
        //    }
        //    //Debug.Log("Setting active scene..");
        //    //SceneManager.SetActiveScene(nScene);
        //}
    }
}

