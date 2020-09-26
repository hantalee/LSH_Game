using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class BaseMonster : MonoBehaviour
{
    protected MonsterData data;
    public string Id { get; set; }
    public string Name { get; set; }
    public CharacterStat Stat { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    public GameObject damageText;

    public PickupItem prefPickupItem;
    public DropTable DropTable { get; set; }

    public abstract void Init();
    public abstract void SetDropTable();

    public virtual void Die()
    {
        DropLoot();
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

    protected virtual void DropLoot()
    {
        ItemData itemData = DropTable.GetDrop();
        if (itemData != null)
        {
            PickupItem item = Instantiate(prefPickupItem, transform.position, Quaternion.identity);
            item.ItemData = itemData;
        }
    }
}
