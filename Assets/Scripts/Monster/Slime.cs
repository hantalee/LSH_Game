using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseMonster
{
    public override void Init()
    {
        data = DataManager.Instance.GetMonsterByName("Slime");
        Id = data.Id;
        Name = data.Name;
        Stat = data.Stat;
        MaxHealth = data.Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        CurrentHealth = MaxHealth;

        SetDropTable();
    }

    public override void SetDropTable()
    {
        DropTable = new DropTable();
        DropTable.loots = new List<Loot>()
        {
            new Loot("IronSword", 50),
            new Loot("HealthPotion", 50)
        };
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
