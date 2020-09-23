using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    private SkillData data;
    public SkillData Data { get; set; }

    public abstract void Init();
    public abstract void Activation();
    public abstract void DeActivation();
    public abstract void Use();
}