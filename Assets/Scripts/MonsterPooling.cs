using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
    public List<Queue<BaseMonster>> poolList = new List<Queue<BaseMonster>>();
    public BaseMonster[] prefabs;

    private static MonsterPooling instance;
    public static MonsterPooling Instance
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

        Init(20);
    }
    
    private void Init(int monsterNum)
    {
        prefabs = Resources.LoadAll<BaseMonster>("Prefabs/Monsters");
        for (int i = 0; i < prefabs.Length; ++i)
        {
            BaseMonster monsterPrefab = prefabs[i];
            Queue<BaseMonster> objectPool = new Queue<BaseMonster>();
            for (int j = 0; j< monsterNum; ++j)
            {
                objectPool.Enqueue(CreateNewMonster(monsterPrefab));
            }
            poolList.Add(objectPool);
        }
    }

    private BaseMonster CreateNewMonster(BaseMonster monsterPrefab)
    {
        BaseMonster newMonster = Instantiate(monsterPrefab);
        newMonster.Init();
        newMonster.gameObject.SetActive(false);
        newMonster.transform.SetParent(transform);
        return newMonster;
    }

    public BaseMonster GetMonsterByName(string name)
    {
        Queue<BaseMonster> pool = poolList.Find(x => (x.Peek().Name == name));
        if(pool.Count > 0)
        {
            BaseMonster monster = pool.Dequeue();
            monster.transform.SetParent(null);
            monster.gameObject.SetActive(true);
            return monster;
        }
        else
        {
            for (int i = 0; i < prefabs.Length; ++i)
            {
                if (prefabs[i].Name == name)
                {
                    BaseMonster monster = CreateNewMonster(prefabs[i]);
                    monster.transform.SetParent(null);
                    monster.gameObject.SetActive(true);
                    return monster;
                }
            }
        }
        return null;
    }

    public void ReturnMonster(BaseMonster monster)
    {
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        Queue<BaseMonster> pool = poolList.Find(x => (x.Peek().Name == monster.Name));
        pool.Enqueue(monster);
    }
}

