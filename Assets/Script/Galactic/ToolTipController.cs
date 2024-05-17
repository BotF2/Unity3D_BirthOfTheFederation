using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI Name;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        HoverManager.instance.ShowTip(Name.text); 
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        HoverManager.instance.HidTip();
    }
}
