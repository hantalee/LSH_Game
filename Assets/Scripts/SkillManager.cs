using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<BaseSkill> skills = new List<BaseSkill>();
    public List<BaseSkill> usedSkills = new List<BaseSkill>();

    private static SkillManager instance;
    public static SkillManager Instance 
    { 
        get
        {
            if (instance != null)
                return instance;
            return null;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

        LoadPrefabs();
    }

    void LoadPrefabs()
    {
        BaseSkill[] prefs = Resources.LoadAll<BaseSkill>("Skills/");
        for (int i = 0; i < prefs.Length; ++i)
        {
            skills.Add(Instantiate(prefs[i]));
            skills[i].transform.SetParent(gameObject.transform);
            skills[i].Init();
        }
    }

    public void ActivateSkillByName(string skillName)
    {
        BaseSkill skill = skills.Find(x => (x.Data.Name == skillName));
        if (skill != null)
        {
            skill.Activation();
            usedSkills.Add(skill);
        }
        else
            Debug.LogWarning("Couldn't find skill : " + skillName);
    }

    public void UseSkillByName(string skillName)
    {
        BaseSkill skill = usedSkills.Find(x => (x.Data.Name == skillName));
        if (skill != null)
        {
            skill.Use();
            UIEventHandler.UseSkill(skill.Data.Name);
        }
    }

    public void DeleteSkillByName(string skillName)
    {
        BaseSkill skill = usedSkills.Find(x => (x.Data.Name == skillName));
        if (skill != null)
            skill.DeActivation();
    }
}