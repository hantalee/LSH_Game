using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonster
{
    string Id { get; set; }
    string Name { get; set; }
    CharacterStat Stat { get; set; }
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }

    void Die();
    void TakeDamage(int amount);
    void PerformAttack();
}
