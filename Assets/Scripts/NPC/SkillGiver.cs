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

        Debug.Log("ActivateSkillGiver");
        skill = SkillManager.Instance.GetRandomSkill();
        FSM.isGiveSkill = false;
    }

    public void GiveSkill()
    {
        PickupSkill prefSkill = Instantiate(prefPickupSkill, transform.position, Quaternion.identity);
        prefSkill.Skill = skill;
        FSM.isGiveSkill = true;
    }
}
