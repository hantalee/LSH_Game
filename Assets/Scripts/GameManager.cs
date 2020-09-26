using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            BaseMonster slime = MonsterPooling.Instance.GetMonsterByName("Slime");
            slime.transform.position = Vector3.zero;
        }
    }
}
