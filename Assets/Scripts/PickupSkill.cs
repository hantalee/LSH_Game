using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSkill : MonoBehaviour
{
    public BaseSkill Skill { get; set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>("UI/Icons/Skills/" + Skill.Data.Name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Skill != null)
            {
                string skillName = Skill.Data.Name;
                SkillManager.Instance.ActivateSkillByName(skillName);
                SkillManager.Instance.UseSkillByName(skillName);
                Destroy(gameObject);
            }
        }
    }
}
