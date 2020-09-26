using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<ItemData> playerItems = new List<ItemData>();
    private static InventoryController instance;
    public static InventoryController Instance
    {
        get 
        {
            if (instance != null)
                return instance;
            return null;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }
    
    public void AddItemToInventory(string itemName)
    {
        ItemData itemData = DataManager.Instance.GetItemByName(itemName);
        playerItems.Add(itemData);
        Debug.Log(playerItems.Count + "itams in inventory, Added: " + itemName);
        UIEventHandler.ItemAddedToInventory(itemData);
    }

    public void AddItemToInventory(ItemData itemData)
    {
        playerItems.Add(itemData);
        Debug.Log(playerItems.Count + "itams in inventory, Added: " + itemData.Name);
        UIEventHandler.ItemAddedToInventory(itemData);
    }
}
