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
    private Transform originalParent;
    [SerializeField]
    StarSysController starSysController;
    [SerializeField]
    private List<GameObject> factorySlots;
    [SerializeField]
    private GameObject[] gameObjects;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        // GameObject[] goArray = GameObject.FindGameObjectsWithTag("FactoryBuildSlot") + GameObject.FindGameObjectsWithTag("");
        //factoryBuildSlots = goArray[0];
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false; // allow drag
        transform.SetParent(transform.root); // top layer to be seen
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
        }
        else if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot0Factory"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
            starSysController.FactoryBuildTimer(StarSysFacilities.Factory);
        }
        else if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot1Factory"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot2Factory"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot3Factory"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot4Factory"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        //else if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot5Factory"))
        //{
        //    transform.SetParent(eventData.pointerEnter.transform);
        //}

        else
        {
            transform.SetParent(originalParent);
        }
       // canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = Vector2.zero;
        Debug.Log("onEndDrag");
    }

}
