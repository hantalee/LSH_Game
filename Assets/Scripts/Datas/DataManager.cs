using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    /// <데이터 리스트>
    private List<MonsterData> monsterDatas;
    private List<ItemData> itemDatas;
    private List<BuffData> buffDatas;
    private List<SkillData> skillDatas;

    public List<MonsterData> MonsterDatas { get; }
    public List<ItemData> ItemDatas { get; }
    public List<BuffData> BuffData { get; }
    public List<SkillData> SkillDatas { get; }
    /// </데이터 리스트>

    private static DataManager instance;
    public static DataManager Instance
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
            Destroy(this);

        instance = this;

        BuildDataBase();
    }

    void BuildDataBase()
    {
        monsterDatas = LoadJsonFile<MonsterData>("Data_Monster");
        itemDatas = LoadJsonFile<ItemData>("Data_Item");
        //buffDatas = LoadJsonFile<BuffData>("Data_Buff");
        skillDatas = LoadJsonFile<SkillData>("Data_Skill");
    }

    public MonsterData GetMonsterByName(string name)
    {
        foreach (MonsterData monster in monsterDatas)
        {
            if (monster.Name == name)
            {
                return monster;
            }
        }
        Debug.LogWarning("Couldn't find item : " + name);
        return null;
    }

    public ItemData GetItemByName(string name)
    {
        foreach (ItemData item in itemDatas)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
        Debug.LogWarning("Couldn't find item : " + name);
        return null;
    }

    public BuffData GetBuffDataByName(string name)
    {
        foreach (BuffData buff in buffDatas)
        {
            if (buff.Name == name)
            {
                return buff;
            }
        }
        Debug.LogWarning("Couldn't find item : " + name);
        return null;
    }

    public SkillData GetSkillDataByName(string name)
    {
        foreach (SkillData skill in skillDatas)
        {
            if (skill.Name == name)
            {
                return skill;
            }
        }
        Debug.LogWarning("Couldn't find item : " + name);
        return null;
    }

    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fs = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    List<T> LoadJsonFile<T>(string fileName)
    {
        return JsonConvert.DeserializeObject<List<T>>(Resources.Load<TextAsset>("Data/" + fileName).ToString());
    }
}
