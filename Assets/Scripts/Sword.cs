using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    private BoxCollider2D boxCollider;

    void Start()
    {
        data = DataManager.Instance.GetItemByName("IronSword");
        Stats = data.Stats;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    public override void PerformAttack(int damage)
    {
        AudioManager.Instance.Play("SwordAttack");
        base.PerformAttack(damage);
    }

    public override void OnAttack()
    {
        base.OnAttack();
        boxCollider.enabled = isAttack;
    }
}
