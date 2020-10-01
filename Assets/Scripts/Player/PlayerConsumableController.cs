using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumableController : MonoBehaviour
{
    CharacterStat stats;
    void Start()
    {
        stats = GetComponent<Player>().Stat;
    }

    public void ConsumeItem(ItemData itemData)
    {
        GameObject item = Instantiate(Resources.Load<GameObject>("Consumables/" + itemData.Name));
        if(item != null)
        {
            item.GetComponent<BaseConsumeable>().Consume();
        }
    }
}
