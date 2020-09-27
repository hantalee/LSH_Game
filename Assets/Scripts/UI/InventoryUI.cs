using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private ItemSlot[] slots;
    public Transform slotHolder;

    void Start()
    {
        slots = slotHolder.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].itemData = null;
        }
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
    }

    public void ItemAdded(ItemData itemData)
    {
        for(int i = 0; i < slots.Length; ++i)
        {
            if(slots[i].itemData == null)
            {
                slots[i].SetItem(itemData);
                break;
            }
        }
    }
}
