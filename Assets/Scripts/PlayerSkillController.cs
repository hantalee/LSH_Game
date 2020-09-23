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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            skillMgr.ActivateSkillByName("Power Up");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            skillMgr.UseSkillByName("Power Up");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            skillMgr.DeleteSkillByName("Power Up");
        }
    }
}
