using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IMonster
{
    private MonsterData monster;
    public string Id { get; set; }
    public string Name { get; set; }
    public CharacterStat Stat { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    public GameObject damageText;

    void Start()
    {
        monster = DataManager.Instance.GetMonsterByName("Slime");
        Id = monster.Id;
        Name = monster.Name;
        Stat = monster.Stat;
        MaxHealth = monster.Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        CurrentHealth = MaxHealth;
    }

    public void Die()
    {
        Debug.Log(gameObject.name + "is Die!");
        Destroy(gameObject);
    }
    public void TakeDamage(int amount)
    {
        GameObject tempText = Instantiate(damageText);
        tempText.transform.position = gameObject.transform.position;
        tempText.GetComponent<DamageText>().damage = amount;
        CurrentHealth -= amount;
        Debug.Log("CurrentHealth : " + CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void PerformAttack()
    {

    }
}
