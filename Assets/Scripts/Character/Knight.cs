using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseCharacter
{
    public override void Init()
    {
        Data = DataManager.Instance.GetCharacterByName("Knight");
    }
}
