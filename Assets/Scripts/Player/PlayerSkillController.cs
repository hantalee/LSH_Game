using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private SkillManager skillMgr;

    void Start()
    {
        skillMgr = SkillManager.Instance;
        List<BaseSkill> skills = skillMgr.UsedSkills;
        foreach (BaseSkill skill in skills)
        {
            skill.Use();
            UIEventHandler.UseSkill(skill.Data.Name);
        }
    }
}
