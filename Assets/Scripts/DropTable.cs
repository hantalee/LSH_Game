using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    public List<Loot> loots;

    public ItemData GetDrop()
    {
        int range = Random.Range(0, 101);
        int rateSum = 0;
        foreach (Loot loot in loots)
        {
            rateSum += loot.DropRate;
            if (range < rateSum)
            {
                return DataManager.Instance.GetItemByName(loot.ItemName);
            }
        }
        return null;
    }
}

public class Loot
{
    public string ItemName { get; set; }
    public int DropRate { get; set; }

    public Loot(string _itemName, int _dropRate)
    {
        this.ItemName = _itemName;
        this.DropRate = _dropRate;
    }
}
