using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    public List<BaseStat> stats;

    public CharacterStat(int atkPower, int DfPower, int moveSpeed, int atkSpeed, int hp)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStat.BaseStatType.AttackPower, atkPower, "AttackPower"),
            new BaseStat(BaseStat.BaseStatType.DefensePower, DfPower, "DefensePower"),
            new BaseStat(BaseStat.BaseStatType.MoveSpeed, moveSpeed, "MoveSpeed"),
            new BaseStat(BaseStat.BaseStatType.AttackSpeed, atkSpeed, "AttackSpeed"),
            new BaseStat(BaseStat.BaseStatType.Hp, hp, "Hp"),
        };
    }

    public BaseStat GetStat(BaseStat.BaseStatType statType)
    {
        return this.stats.Find(x => (x.StatType == statType));
    }

    public void AddBonusStat(List<BaseStat> baseStats)
    {
        foreach(BaseStat bonusStat in baseStats)
        {
            GetStat(bonusStat.StatType).AddBonusStat(new BonusStat(bonusStat.BaseValue));
        }
    }

    public void AddBonusStat(BaseStat baseStat)
    {
        GetStat(baseStat.StatType).AddBonusStat(new BonusStat(baseStat.BaseValue));
    }

    public void AddBonusStat(BaseStat.BaseStatType type, int value)
    {
        GetStat(type).AddBonusStat(new BonusStat(value));
    }

    public void RemoveBonusStat(List<BaseStat> baseStats)
    {
        foreach (BaseStat bonusStat in baseStats)
        {
            GetStat(bonusStat.StatType).RemoveBonusStat(new BonusStat(bonusStat.BaseValue));
        }
    }

    public void RemoveBonusStat(BaseStat baseStat)
    {
        GetStat(baseStat.StatType).RemoveBonusStat(new BonusStat(baseStat.BaseValue));
    }

    public void RemoveBonusStat(BaseStat.BaseStatType type, int value)
    {
        GetStat(type).RemoveBonusStat(new BonusStat(value));
    }
}
