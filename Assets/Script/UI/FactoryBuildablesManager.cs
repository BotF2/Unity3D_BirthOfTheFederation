using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class FactoryBuildablesManager : MonoBehaviour
{
    public Transform buildableInvetoryGrid;
    public GameObject buildableItemPrefab;

    public void AddItem(ItemData itemData)
    {
        GameObject newItemGO = Instantiate(buildableItemPrefab, buildableInvetoryGrid);
        var newItemData = newItemGO.GetComponent<ItemData>();
        newItemData.itemName = itemData.itemName;
        newItemData.itemIcon = itemData.itemIcon;
    }
}
