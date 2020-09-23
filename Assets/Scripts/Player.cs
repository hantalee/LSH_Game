using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStat Stat;
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }

    void Start()
    {
        Stat = new CharacterStat(1, 0, 3, 1, 10);
    }

    public void PerformAttack()
    {
    }

    public void TakeDamage(int amount)
    {
    }

    public void Die()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            int CurrentDamage = Stat.GetStat(BaseStat.BaseStatType.AttackPower).GetFinalValue();
            collision.gameObject.GetComponent<IMonster>().TakeDamage(CurrentDamage);
        }
    }
}
