using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core
{
    [RequireComponent(typeof(Toggle))]
    public class CombatOrderSelection : MonoBehaviour //, IPointerDownHandler
    {
        //public static Toggle Engage, Rush, Retreat, Formation, ProtectTransports, TargetTransports;
        //public List<Toggle> toggleOrderList = new List<Toggle>() { Engage, Rush, Retreat, Formation, ProtectTransports, TargetTransports};
        //private Toggle activeLocalPlayerToggle;
        //private Toggle previousToggle;
        //public static Orders order;

        //private void Start()
        //{
        //    previousToggle = toggleOrderList[0]; ;
        //}
        //private void Update()
        //{

        //    foreach (var toggle in toggleOrderList)
        //    {
        //        if (toggle.isOn)
        //        {
        //            activeLocalPlayerToggle = toggle;
        //            if (previousToggle != toggle)
        //                ActivePlayerToggle(activeLocalPlayerToggle);
        //            break;
        //        }
        //    }
        //    previousToggle = activeLocalPlayerToggle;
        //}
        //public void ActivePlayerToggle(Toggle activeToggleOrder)
        //{

        //    switch (activeToggleOrder.name.ToUpper())
        //    {
        //        case "TOGGLE_ENGAGE":
        //            GameManager.Instance._combatOrder = Orders.Engage;
        //            order = Orders.Engage;
        //            Debug.Log("Active Engage.");
        //            break;
        //        case "TOGGLE_RUSH":
        //            Debug.Log("Active Rush.");
        //            GameManager.Instance._combatOrder = Orders.Rush;
        //            order = Orders.Rush;
        //            break;
        //        case "TOGGLE_RETREAT":
        //            Debug.Log("Active Retreat.");
        //            GameManager.Instance._combatOrder = Orders.Retreat;
        //            order = Orders.Retreat;
        //            break;
        //        case "TOGGLE_FORMATION":
        //            Debug.Log("Active Formation.");
        //            GameManager.Instance._combatOrder = Orders.Formation;
        //            order = Orders.Formation;
        //            break;
        //        case "TOGGLE_PROTECT_TRANSPORTS":
        //            Debug.Log("Active Protect Transports.");
        //            GameManager.Instance._combatOrder = Orders.ProtectTransports;
        //            order = Orders.ProtectTransports;
        //            break;
        //        case "TOGGLE_TARGET_TRANSPORTS":
        //            Debug.Log("Active Target Transports.");
        //            GameManager.Instance._combatOrder = Orders.TargetTransports;
        //            order= Orders.TargetTransports;
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //public void OnPointerDown(PointerEventData eventData)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

