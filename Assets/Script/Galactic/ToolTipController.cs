using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Core;
using Unity.VisualScripting;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Only tool tips on our systems and fleet for now. ToDo: do we want to add known systems and fleets? 
    /// It gets complicated.
    /// </summary>

    public TextMeshProUGUI Name;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (Name != null && HoverManager.Instance != null)
        {
            var localPlayerCivCon = CivManager.Instance.GetLocalPlayerCivController();
            foreach (StarSysController starSysCon in localPlayerCivCon.CivData.StarSysOwned)
            {
                if (starSysCon.StarSysData.GetSysName() == Name.text)
                {
                    HoverManager.Instance.ShowTip(Name.text);
                    break;
                }
            }
            
            if (Name.text.Contains(localPlayerCivCon.CivShortName))
            {
                HoverManager.Instance.ShowTip(Name.text);               
            }
            ///***** ToDo maybe - also see civs we know?
            //else
            //{
            //    foreach (CivController civCon in localPlayerCivCon.CivData.CivControllersWeKnow)
            //    {
            //        //if (Name.text.Contains(civCon.CivData.))
            //    }
            //}
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (HoverManager.Instance != null)
        HoverManager.Instance.HidTip();
    }
}
