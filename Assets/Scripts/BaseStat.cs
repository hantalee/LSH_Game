using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class BaseStat
{
    public enum BaseStatType
    {
        AttackPower,
        DefensePower,
        MoveSpeed,
        AttackSpeed,
        Hp
    }

    [JsonIgnore]
    public List<BonusStat> BonusStat { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public BaseStatType StatType { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    [JsonIgnore]
    public int FinalValue { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public BaseStat(BaseStatType statType ,int baseValue, string statName)
    {
        this.BonusStat = new List<BonusStat>();
        this.StatType = statType;
        this.BaseValue = baseValue;
        this.StatName = statName;
    }

    public void AddBonusStat(BonusStat bonusStat)
    {
        this.BonusStat.Add(bonusStat);
    }

    public void RemoveBonusStat(BonusStat bonusStat)
    {
        this.BonusStat.Remove(BonusStat.Find(x =>(x.BonusValue == bonusStat.BonusValue)));
    }

    public int GetFinalValue()
    {
        this.FinalValue = 0;
        this.BonusStat.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }
}
