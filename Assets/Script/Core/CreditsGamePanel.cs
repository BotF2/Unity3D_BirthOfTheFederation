using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class CreditsGamePanel : MonoBehaviour
    {
        public GameObject Panel;

        public void Start()
        {
            Panel.SetActive(false);
        }
        public void OpenPanel()
        {
            if(Panel != null)
            {
                Panel.SetActive(true);
            }
        }
        public void ClosePanel()
        {
            if (Panel != null)
                Panel.SetActive(false);
        }
    }
}
