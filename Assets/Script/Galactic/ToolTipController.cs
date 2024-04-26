using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI Name;
    //public TextMeshProUGUI Description;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        HoverManager.instance.ShowTip(Name.text); //, Description.text);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        HoverManager.instance.HidTip();
    }
}
