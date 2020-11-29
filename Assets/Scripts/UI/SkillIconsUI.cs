using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIconsUI : MonoBehaviour
{
    public SkillIcon usedSkillIcon;
    void Start()
    {
        UIEventHandler.OnUseSkill += CreateIcon;
    }

    void CreateIcon(string skillName)
    {
        if(this)
        {
            SkillIcon icon = Instantiate(usedSkillIcon, transform, false);
            icon.ChangeIcon(skillName);
        }
    }

}
