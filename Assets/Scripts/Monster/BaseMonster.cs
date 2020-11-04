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

    public bool isDead = false;

    public GameObject damageText;

    public Transform attackPoint;
    public float attackRadius = 1.0f;
    public LayerMask layerMask;

    public IFSM FSM;
    public PickupItem prefPickupItem;
    public DropTable DropTable { get; set; }
    public Spawner spawner;

    public abstract void Init();
    public abstract void SetDropTable();

    private void Awake()
    {
        FSM = GetComponent<IFSM>();
    }

    public virtual void Die()
    {
        Debug.Log("is dead!");
        DropLoot();
        isDead = true;
        spawner.monsterCount -= 1;
        spawner = null;
        FSM.ChangeState(State.Die);
    }

    public virtual void TakeDamage(int amount)
    {
        GameObject tempText = Instantiate(damageText);
        tempText.transform.position = gameObject.transform.position;
        tempText.GetComponent<DamageText>().damage = amount;
        FSM.ChangeState(State.Hit);

        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void PerformAttack()
    {
       Collider2D[] targets = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, layerMask);
        foreach (Collider2D target in targets)
        {
            if(target.CompareTag("Player"))
            {
                Player player = target.gameObject.GetComponent<Player>();

                int damage = data.Stat.GetStat(BaseStat.BaseStatType.AttackPower).GetFinalValue();
                player.TakeDamage(damage);
            }
        }
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
