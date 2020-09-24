using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    void Start()
    {
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
    }

    public void ItemAdded(ItemData itemData)
    {
        Debug.Log("InventoryUI::ItemdAdded");
    }
}
