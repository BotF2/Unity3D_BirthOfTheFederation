using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI FleetName;
    public TextMeshProUGUI FleetDetail;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        HoverManager.instance.ShowTip(FleetName.text, FleetDetail.text);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        HoverManager.instance.HidTip();
    }
}
