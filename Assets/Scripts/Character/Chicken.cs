using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : BaseCharacter
{
    public override void Init()
    {
        data = DataManager.Instance.GetCharacterByName("닭");
        Id = data.Id;
        Name = data.Name;
        Type = data.ClassType;
        Stat = data.Stat;
        Price = data.Price;
        DropChance = data.DropChance;
        MaxHealth = data.Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        CurrentHealth = MaxHealth;
    }

    public override void Die()
    {
        base.Die();
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }
    public override void PerformAttack()
    {
        base.PerformAttack();
    }
}
