using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonusStat
{
    public int BonusValue { get; set; }

    public BonusStat(int bonusValue)
    {
        this.BonusValue = bonusValue;
    }
}
