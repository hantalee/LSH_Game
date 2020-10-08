using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    IEnumerator chasPlayer;
    IEnumerator readyAttack;
    Vector2 dir;

    Player player;
    BaseMonster monster;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        monster = GetComponent<BaseMonster>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        chasPlayer = ChasePlayer();
        readyAttack = ReadyAttack();
        StartCoroutine(chasPlayer);
    }

    void FindPlayerDirection()
    {
        dir = (transform.position.x < player.transform.position.x) ? new Vector2(1, 0) : new Vector2(-1, 0);
        if (dir.x == 1)
            spriteRenderer.flipX = false;
        else //if(dir.x == -1)
            spriteRenderer.flipX = true;
    }
    IEnumerator ChasePlayer()
    {
        while (!monster.isDead && !player.IsDead)
        {
            FindPlayerDirection();
            if (Vector2.Distance(transform.position, player.transform.position) > 2.0f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    monster.Stat.GetStat(BaseStat.BaseStatType.MoveSpeed).GetFinalValue() * Time.deltaTime);
                animator.SetBool("IsFollow", true);
            }
            else
            {
                StopCoroutine(chasPlayer);
                StartCoroutine(readyAttack);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ReadyAttack()
    {
        animator.SetBool("Ready", true);

        yield return new WaitForSeconds(1.5f);

        Attack();
        StopCoroutine(readyAttack);
    }

    void Attack()
    {

        animator.SetBool("Ready", false);
        animator.SetTrigger("Attack");

        rigid.AddForce(Vector2.right * dir.x * 3, ForceMode2D.Impulse);
        StartCoroutine(chasPlayer);
    }
}
