using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStat Stat;
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }

    public GameObject damageText;

    private void Awake()
    {
        Stat = new CharacterStat(1, 0, 3, 1, 500);
        MaxHealth = Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        CurrentHealth = MaxHealth;
    }
    void Start()
    {
        UIEventHandler.PlayerRecovery();
    }

    public void PerformAttack()
    {
    }

    public void TakeDamage(int amount)
    {
        GameObject tempText = Instantiate(damageText);
        tempText.transform.position = gameObject.transform.position;
        tempText.GetComponent<DamageText>().damage = amount;
        CurrentHealth -= amount;
        UIEventHandler.PlayerTakeDamage();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("플레이어 사망");
    }
}
