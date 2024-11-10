using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using UnityEngine.UI;

public class TheirStarSysTabManager : MonoBehaviour
{
    public GameObject[] TabUIs;
    public Image[] TabButtonMasks;
    //public Sprite InactiveTabBackground, ActiveTabBackbround;
    public Vector3 InactiveTabButtonSize, ActiveTabButtonSize;

    public void SwitchToTab(int TabID)
    {
        foreach (GameObject tabGO in TabUIs)
        {
            tabGO.SetActive(false); 
        }
        TabUIs[TabID].SetActive(true);
        
        foreach (Image image in TabButtonMasks)
        {
            image.gameObject.SetActive(true);
        }
        TabButtonMasks[TabID].gameObject.SetActive(false);

    }
}
