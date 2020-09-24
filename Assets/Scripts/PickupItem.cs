using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData ItemData { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ItemData != null)
            {
                InventoryController.Instance.AddItemToInventory(ItemData);
                Destroy(gameObject);
            }
            else
                Debug.LogWarning("PickupItem::ItemData is null");
        }
    }
}
