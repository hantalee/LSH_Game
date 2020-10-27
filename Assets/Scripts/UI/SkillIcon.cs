using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    Image img;

    private void Awake()
    {
        Transform icon = transform.GetChild(0);
        img = icon.GetComponent<Image>();
    }
    public void ChangeIcon(string skillName)
    {
        img.sprite = Resources.Load<Sprite>("UI/Icons/Skills/" + skillName);
    }
}

