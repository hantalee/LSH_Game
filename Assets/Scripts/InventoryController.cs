using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<ItemData> playerItems = new List<ItemData>();
    public PlayerWeaponController weaponController;
    public PlayerConsumableController consumableController;

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

    private void Start()
    {
        weaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<PlayerConsumableController>();
        AddItemToInventory("IronSword");
        AddItemToInventory("HealthPotion");
    }

    public void AddItemToInventory(string itemName)
    {
        ItemData itemData = DataManager.Instance.GetItemByName(itemName);
        playerItems.Add(itemData);
        UIEventHandler.ItemAddedToInventory(itemData);
    }

    public void AddItemToInventory(ItemData itemData)
    {
        playerItems.Add(itemData);
        UIEventHandler.ItemAddedToInventory(itemData);
    }

    public void EquipItem(ItemData itemData)
    {
        weaponController.EquipWeapon(itemData);
    }

    public void ConsumeItem(ItemData itemData)
    {
        consumableController.ConsumeItem(itemData);
    }
}
