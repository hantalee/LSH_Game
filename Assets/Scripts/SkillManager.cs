using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoadScene : Lobby
/// </summary>
public class SkillManager : MonoBehaviour
{
    private List<BaseSkill> skills = new List<BaseSkill>();
    private List<BaseSkill> usedSkills = new List<BaseSkill>();

    public List<BaseSkill> Skills { get => skills; }
    public List<BaseSkill> UsedSkills { get => usedSkills; }

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
        DontDestroyOnLoad(gameObject);

        LoadPrefabs();
    }

    void LoadPrefabs()
    {
        BaseSkill[] prefs = Resources.LoadAll<BaseSkill>("Skills/");
        for (int i = 0; i < prefs.Length; ++i)
        {
            Skills.Add(Instantiate(prefs[i]));
            Skills[i].transform.SetParent(gameObject.transform);
            Skills[i].Init();
        }
    }

    public BaseSkill GetRandomSkill()
    {
        int random = Random.Range(0, Skills.Count);
        BaseSkill randomSkill = Skills[random];

        return randomSkill;
    }

    public void RandomPeek(int num)
    {
        for(int i = 0; i < num; ++i)
        {
            if (Skills.Count < i + 1)
                break;

            int random = Random.Range(0, Skills.Count);
            string randomName = Skills[random].Data.Name;
            ActivateSkillByName(randomName);
        }
    }

    public void ActivateSkillByName(string skillName)
    {
        BaseSkill skill = Skills.Find(x => (x.Data.Name == skillName));
        if (skill != null)
        {
            skill.Activation();
            UsedSkills.Add(skill);
        }
        else
            Debug.LogWarning("Couldn't find skill : " + skillName);
    }

    public void UseSkillByName(string skillName)
    {
        BaseSkill skill = UsedSkills.Find(x => (x.Data.Name == skillName));
        if (skill != null)
        {
            skill.Use();
            UIEventHandler.UseSkill(skill.Data.Name);
        }
    }

    public void DeActivatSkillByName(string skillName)
    {
        BaseSkill skill = UsedSkills.Find(x => (x.Data.Name == skillName));
        if (skill != null)
            skill.DeActivation();
    }
}