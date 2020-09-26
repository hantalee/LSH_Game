using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGiver : BaseNPC
{
    public override void Interaction()
    {
        base.Interaction();
        //DialogueSystem.Instance.AddNewDialogue(dialogue, name);
    }
}
