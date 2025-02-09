using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Core;
using System.Linq;

public class FactoryBuildableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform originalParent;
    
    public StarSysController StarSysController;
    public StarSysFacilities FacilityType;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false; // allow drag
        transform.SetParent(transform.root); // down list to top layer to be seen
        transform.SetAsLastSibling();
        Debug.Log("onBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // follow the mouse cursor
        rectTransform.anchoredPosition += eventData.delta / rectTransform.lossyScale;
        Debug.Log("onDraging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("FactoryBuildSlot"))
        {
            transform.SetParent(eventData.pointerEnter.transform);

            switch (this.FacilityType)
            {
                case StarSysFacilities.PowerPlanet:
                    StarSysManager.Instance.NewImageInEmptyBuildableInventory(StarSysManager.Instance.PowerPlantPrefab, this.StarSysController);
                    break;
                case StarSysFacilities.Factory:
                    StarSysManager.Instance.NewImageInEmptyBuildableInventory(StarSysManager.Instance.FactoryPrefab, this.StarSysController);
                    break;
                case StarSysFacilities.Shipyard:
                    StarSysManager.Instance.NewImageInEmptyBuildableInventory(StarSysManager.Instance.ShipyardPrefab, this.StarSysController);
                    break;
                case StarSysFacilities.ShieldGenerator:
                    StarSysManager.Instance.NewImageInEmptyBuildableInventory(StarSysManager.Instance.ShieldGeneratorPrefab, this.StarSysController);
                    break;
                case StarSysFacilities.OrbitalBattery:
                    StarSysManager.Instance.NewImageInEmptyBuildableInventory(StarSysManager.Instance.OrbitalBatteryPrefab, this.StarSysController);
                    break;
                case StarSysFacilities.ResearchCenter:
                    StarSysManager.Instance.NewImageInEmptyBuildableInventory(StarSysManager.Instance.ResearchCenterPrefab, this.StarSysController);
                    break;
                default:
                    break;
            }
            //StarSysManager.Instance.NewImageInEmptyBuildableInventory
        }
        else
        {
            transform.SetParent(originalParent);
        }
        rectTransform.anchoredPosition = Vector2.zero;
        Debug.Log("onEndDrag");
    }

}
