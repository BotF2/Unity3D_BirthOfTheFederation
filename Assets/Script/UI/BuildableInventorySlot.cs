using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableInventorySlot : MonoBehaviour
{
    private void Awake()
    {
        gameObject.tag = "InventorySlot";
    }
}
