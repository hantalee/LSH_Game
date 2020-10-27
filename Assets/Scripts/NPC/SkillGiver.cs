using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGiver : BaseNPC
{
    public BaseSkill skill;
    public PickupSkill prefPickupSkill;
    public SkillGiverFSM FSM;

    private void Awake()
    {
        FSM = GetComponent<SkillGiverFSM>();
    }

    public void ActivateSkillGiver()
    {
        if (skill != null)
            skill = null;

        skill = SkillManager.Instance.GetRandomSkill();
        FSM.isGiveSkill = false;
    }

    public void GiveSkill()
    {
        PickupSkill prefSkill = Instantiate(prefPickupSkill, transform.position, Quaternion.identity);
        prefSkill.Skill = skill;
        Rigidbody2D rigid = prefSkill.gameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 2);

        FSM.isGiveSkill = true;
    }
}
