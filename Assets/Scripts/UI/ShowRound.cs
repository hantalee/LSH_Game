using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRound : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        UIEventHandler.OnChangeRound += ChangeText;
    }
    
    void ChangeText(int round)
    {
        text.text = round.ToString();
    }
}
