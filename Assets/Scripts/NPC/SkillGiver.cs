using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGiver : BaseNPC
{
    SkillManager skillMgr;
    public string[] skillsName;
    public int skillCount = 1;

    void Start()
    {
        skillMgr = SkillManager.Instance;
        skillsName = new string[skillMgr.Skills.Count];
        for (int i = 0; i < skillMgr.Skills.Count; ++i)
        {
            skillsName[i] = skillMgr.Skills[i].Data.Name;
        }
    }

    public override void Interaction()
    {
        base.Interaction();

        for(int i = 0; i < skillCount; ++i)
        {
            int randomSkill = Random.Range(0, skillsName.Length);
            skillMgr.ActivateSkillByName(skillsName[randomSkill]);
            skillMgr.UseSkillByName(skillsName[randomSkill]);
        }
    }
}
