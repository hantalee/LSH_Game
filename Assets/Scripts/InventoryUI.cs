using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private ItemSlot[] slots;
    public Transform slotHolder;
    public int slotNum;
    public int ItemCount { get; set; }

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
                Debug.Log("ItemAdded");
                slots[i].SetItem(itemData);
                break;
            }
        }
    }
}
