using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    ItemSlot ItemSlot { get; set; }
    void Start()
    {
        ItemSlot = Resources.Load<ItemSlot>("UI/Prefabs/ItemSlot");
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
    }

    public void ItemAdded(ItemData itemData)
    {
        ItemSlot emptySlot = Instantiate(ItemSlot);
        emptySlot.SetItem(itemData);
        emptySlot.transform.SetParent(gameObject.transform);
    }
}
