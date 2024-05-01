using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Core
{
    public class LobbyOptions : MonoBehaviour
    {
        // Single Player and MultiPalyer buttons wired in Unity editor, not a button click method here like load, save & exit
        // ToDo: menu for Multiplayer, Saved games with Name enter and or Load games and something for the settings and credit button

        public void OnSaveClicked()
        {
            SaveLoadManager.Save();
        }
        public void OnLoadClicked()
        {
            SaveLoadManager.Load();
        }
        public void OnExitClicked()
        {
            //******* UnityEditor - EditorApplication not accessible during build ****
            //if (UnityEditor.EditorApplication.isPlaying) 
            //{
            //    UnityEditor.EditorApplication.isPlaying = false;
            //}
            //else
            //Application.Quit(); // does not work while in Unity editor, must build and run the .exe
        }
        //public void OnSettingsClicked()
        //{
        //    
        //}
        //public void OnCreditsClicked()
        //{
        //    
        //}
        
    }

}
