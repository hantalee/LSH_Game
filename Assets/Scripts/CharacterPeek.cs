using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPeek : MonoBehaviour
{
    [SerializeField] private BaseCharacter usedCharacter;
    public BaseCharacter UsedCharacter { get => usedCharacter; }

    private static CharacterPeek instance;
    public static CharacterPeek Instance
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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RandomPeek()
    {
        if (usedCharacter != null)
            usedCharacter = null;

        List<Queue<BaseCharacter>> pool = CharacterManager.Instance.CharacterPool;
        int random = Random.Range(0, pool.Count);
        string randomName = pool[random].Peek().Data.Name;

        usedCharacter = CharacterManager.Instance.GetCharacterByName(randomName);
    }
}
