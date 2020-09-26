using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private SkillManager skillMgr;

    void Start()
    {
        skillMgr = SkillManager.Instance;
    }
}
