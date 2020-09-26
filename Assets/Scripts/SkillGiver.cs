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
        skillsName = new string[skillMgr.skills.Count];
        for (int i = 0; i < skillMgr.skills.Count; ++i)
        {
            skillsName[i] = skillMgr.skills[i].Data.Name;
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
