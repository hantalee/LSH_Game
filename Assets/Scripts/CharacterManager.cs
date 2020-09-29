using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private BaseCharacter usedCharacter;
    public BaseCharacter UsedCharacter { get => usedCharacter; }

    private static CharacterManager instance;
    public static CharacterManager Instance
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
        DontDestroyOnLoad(gameObject);
    }

    public void RandomPeek()
    {
        List<Queue<BaseCharacter>> pool = ObjectPooling.Instance.CharacterPool;
        int random = Random.Range(0, pool.Count);
        string randomName = pool[random].Peek().Data.Name;

        usedCharacter =  ObjectPooling.Instance.GetCharacterByName(randomName);
    }
}
