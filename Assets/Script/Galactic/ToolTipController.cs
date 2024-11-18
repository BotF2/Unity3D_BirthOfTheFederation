using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
            else
            {
                foreach (CivController civCon in localPlayerCivCon.CivData.CivControllersWeKnow)
                {
                    HoverManager.Instance.ShowTip(Name.text);
                }
            }
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (HoverManager.Instance != null)
            HoverManager.Instance.HidTip();
    }
}
