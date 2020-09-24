using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;
    public Text itemText;
    public Image itemIcon;

    public void SetItem(ItemData itemData)
    {
        this.itemData = itemData;
        InitItemValues();
    }

    void InitItemValues()
    {
        itemText.text = itemData.Name;
        itemIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + itemData.Class);
    }
}
