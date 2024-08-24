using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Core;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
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
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (HoverManager.Instance != null)
        HoverManager.Instance.HidTip();
    }
}
