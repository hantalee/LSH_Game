using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;
    public Text txt_ItemCount;
    public Image img_ItemIcon;

    public void SetItem(ItemData itemData)
    {
        Debug.Log("SetItem");
        this.itemData = itemData;
        InitItemValues();
    }

    void InitItemValues()
    {
        Debug.Log("InitItemValues");
        txt_ItemCount.text = itemData.Name;
        img_ItemIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + itemData.Class);
    }
}
