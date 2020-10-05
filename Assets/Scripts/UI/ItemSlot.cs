using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;
    public Text txt_ItemCount;
    public Image img_ItemIcon;
    private Button btn;

    private void Awake()
    {
        img_ItemIcon.gameObject.SetActive(false);
        txt_ItemCount.text = 0.ToString();
        txt_ItemCount.gameObject.SetActive(false);

        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnItemInteract);
    }

    public void SetItem(ItemData itemData)
    {
        this.itemData = itemData;
        InitItemValues();

        img_ItemIcon.gameObject.SetActive(true);
        if(int.Parse(txt_ItemCount.text) > 1)
            txt_ItemCount.gameObject.SetActive(true);
    }

    void InitItemValues()
    {
        img_ItemIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + itemData.Class);
    }

    public void OnItemInteract()
    {
        if (itemData == null)
            return;

        if(itemData.Type == ItemType.Consumable)
        {
            InventoryController.Instance.ConsumeItem(itemData);
        }
        else if(itemData.Type == ItemType.Weapon)
        {
            InventoryController.Instance.EquipItem(itemData);
        }
        img_ItemIcon.gameObject.SetActive(false);
        txt_ItemCount.gameObject.SetActive(false);
        itemData = null;
    }
}
