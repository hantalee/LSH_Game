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

            BaseCharacter chickenMan = ObjectPooling.Instance.GetCharacterByName("닭");
            chickenMan.transform.position = Vector3.zero;
        }
    }
}
