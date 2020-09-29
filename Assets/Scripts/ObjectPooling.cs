using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// LoadScene : Lobby
/// </summary>
public class ObjectPooling : MonoBehaviour
{
    public List<Queue<BaseCharacter>> CharacterPool = new List<Queue<BaseCharacter>>();
    public List<Queue<BaseMonster>> MonsterPool = new List<Queue<BaseMonster>>();
    public BaseCharacter[] CharacterPrefabs;
    public BaseMonster[] MonsterPrefabs;

    private static ObjectPooling instance;
    public static ObjectPooling Instance
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

        InitCharacter(1);
        InitMonster(5);
    }


    /// <summary>
    /// Init()
    /// </summary>
    private void InitCharacter(int num)
    {
        CharacterPrefabs = Resources.LoadAll<BaseCharacter>("Prefabs/Characters");
        for (int i = 0; i < CharacterPrefabs.Length; ++i)
        {
            BaseCharacter characterPrefab = CharacterPrefabs[i];
            characterPrefab.Init();
            Queue<BaseCharacter> objectPool = new Queue<BaseCharacter>();
            for (int j = 0; j < num; ++j)
            {
                objectPool.Enqueue(CreateNewCharacter(characterPrefab));
            }
            CharacterPool.Add(objectPool);
        }
    }

    private void InitMonster(int num)
    {
        MonsterPrefabs = Resources.LoadAll<BaseMonster>("Prefabs/Monsters");
        for (int i = 0; i < MonsterPrefabs.Length; ++i)
        {
            BaseMonster monsterPrefab = MonsterPrefabs[i];
            monsterPrefab.Init();
            Queue<BaseMonster> objectPool = new Queue<BaseMonster>();
            for (int j = 0; j< num; ++j)
            {
                objectPool.Enqueue(CreateNewMonster(monsterPrefab));
            }
            MonsterPool.Add(objectPool);
        }
    }


    /// <summary>
    /// Create()
    /// </summary>
    private BaseCharacter CreateNewCharacter(BaseCharacter prefab)
    {
        BaseCharacter newCharacter = Instantiate(prefab);
        newCharacter.Init();
        newCharacter.gameObject.SetActive(false);
        newCharacter.transform.SetParent(transform);
        return newCharacter;
    }

    private BaseMonster CreateNewMonster(BaseMonster prefab)
    {
        BaseMonster newMonster = Instantiate(prefab);
        newMonster.Init();
        newMonster.gameObject.SetActive(false);
        newMonster.transform.SetParent(transform);
        return newMonster;
    }


    /// <summary>
    /// 오브젝트 풀에서 오브젝트를 찾아 씬에 나타나게 합니다.
    /// 풀에 들어있던 오브젝트 보다 더 많은 오브젝트가 필요하게 되면 추가로 생성합니다.
    /// </summary>
    public BaseCharacter GetCharacterByName(string name)
    {
        Queue<BaseCharacter> pool = CharacterPool.Find(x => (x.Peek().Data.Name == name));
        if (pool.Count > 1)
        {
            BaseCharacter character = pool.Dequeue();
            character.transform.SetParent(null);
            character.gameObject.SetActive(true);
            return character;
        }
        else
        {
            for (int i = 0; i < CharacterPrefabs.Length; ++i)
            {
                if (CharacterPrefabs[i].Data.Name == name)
                {
                    BaseCharacter character = CreateNewCharacter(CharacterPrefabs[i]);
                    character.transform.SetParent(null);
                    character.gameObject.SetActive(true);
                    return character;
                }
            }
        }
        return null;
    }

    public BaseMonster GetMonsterByName(string name)
    {
        Queue<BaseMonster> pool = MonsterPool.Find(x => (x.Peek().Name == name));
        if(pool.Count > 1)
        {
            BaseMonster monster = pool.Dequeue();
            monster.transform.SetParent(null);
            monster.gameObject.SetActive(true);
            return monster;
        }
        else
        {
            for (int i = 0; i < MonsterPrefabs.Length; ++i)
            {
                if (MonsterPrefabs[i].Name == name)
                {
                    BaseMonster monster = CreateNewMonster(MonsterPrefabs[i]);
                    monster.transform.SetParent(null);
                    monster.gameObject.SetActive(true);
                    return monster;
                }
            }
        }
        return null;
    }


    /// <summary>
    /// Return()
    /// </summary>
    public void ReturnCharacter(BaseCharacter character)
    {
        character.gameObject.SetActive(false);
        character.transform.SetParent(transform);
        Queue<BaseCharacter> pool = CharacterPool.Find(x => (x.Peek().Data.Name == character.Data.Name));
        pool.Enqueue(character);
    }

    public void ReturnMonster(BaseMonster monster)
    {
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        Queue<BaseMonster> pool = MonsterPool.Find(x => (x.Peek().Name == monster.Name));
        pool.Enqueue(monster);
    }
}

