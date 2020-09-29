using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPeek : MonoBehaviour
{
    public int peekupSkillCount = 3;
    public GameObject characterPosition;
    public Image[] imgs = new Image[3];

    private void Awake()
    {
        CharacterManager.Instance.RandomPeek();
        SkillManager.Instance.RandomPeek(peekupSkillCount);

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        StartRandomPeek();
    }

    public void StartRandomPeek()
    {
        BaseCharacter character = CharacterManager.Instance.UsedCharacter;
        List<BaseSkill> skills = SkillManager.Instance.UsedSkills;

        character.transform.position = characterPosition.transform.position;
        for(int i = 0; i < skills.Count; ++i)
        {
            imgs[i].sprite = Resources.Load<Sprite>("UI/Icons/Skills/" + skills[i].Data.Name);
            imgs[i].enabled = true;
        }
    }
}
