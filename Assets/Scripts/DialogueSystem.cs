using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject prefDialoguePanel;
    public List<string> scriptLines = new List<string>();
    public string npcName;

    private int scriptIndex;
    private Button Btn_Continue;
    private Text Txt_Script;
    private Text Txt_Name;

    private static DialogueSystem instance;
    public static DialogueSystem Instance
    {
        get
        {
            if (instance != null)
                return instance;
            return null;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;

        Btn_Continue = prefDialoguePanel.transform.Find("Btn_Continue").GetComponent<Button>();
        Txt_Script = prefDialoguePanel.transform.Find("Text_Script").GetComponent<Text>();
        Txt_Name = prefDialoguePanel.transform.Find("Text_Name").GetComponent<Text>();

        Btn_Continue.onClick.AddListener(ContinueDialogue);
        prefDialoguePanel.SetActive(false);
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        scriptIndex = 0;
        scriptLines = new List<string>(lines.Length);
        scriptLines.AddRange(lines);
        this.npcName = npcName;

        CreateDialogue();
    }

    public void CreateDialogue()
    {
        Txt_Script.text = scriptLines[scriptIndex];
        Txt_Name.text = npcName;
        prefDialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if(scriptIndex < scriptLines.Count - 1)
        {
            scriptIndex++;
            Txt_Script.text = scriptLines[scriptIndex];
        }
        else
        {
            prefDialoguePanel.SetActive(false);
        }
    }
}
