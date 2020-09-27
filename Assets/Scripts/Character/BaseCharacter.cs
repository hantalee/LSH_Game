using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class BaseCharacter : MonoBehaviour
{
    protected CharacterData data;
    public string Id { get; set; }
    public string Name { get; set; }
    public ClassType Type { get; set; }
    public CharacterStat Stat { get; set; }
    public int Price { get; set; }
    public float DropChance { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public GameObject damageText;
    public abstract void Init();

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount)
    {
        GameObject tempText = Instantiate(damageText);
        tempText.transform.position = gameObject.transform.position;
        tempText.GetComponent<DamageText>().damage = amount;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void PerformAttack()
    {

    }
}
