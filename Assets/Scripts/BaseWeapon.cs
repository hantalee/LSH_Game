using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    protected Animator animator;
    protected ItemData data;
    public List<BaseStat> Stats { get; set; }
    public int CurrentDamage { get; set; }

    public bool isAttack = false;

    public virtual void PerformAttack(int damage)
    {
        CurrentDamage = damage;
        animator.SetTrigger("IsAttack");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<BaseMonster>().TakeDamage(CurrentDamage);
        }
    }

    public virtual void OnAttack()
    {
        isAttack = !isAttack;
    }
}
