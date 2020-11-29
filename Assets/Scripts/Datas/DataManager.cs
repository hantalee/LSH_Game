using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

/// <summary>
/// LoadScene : Lobby
/// </summary>

[System.Serializable]
public class Serialization<T>
{
    public List<T> target;
    public Serialization(List<T> _target)
    {
        target = _target;
    }
}
    
public class DataManager : MonoBehaviour
{
    /// <데이터 리스트>
    private List<CharacterData> characterDatas;
    private List<MonsterData> monsterDatas;
    private List<ItemData> itemDatas;
    private List<SkillData> skillDatas;
    private List<StageData> stageDatas;

    public List<CharacterData> CharacterDatas { get => characterDatas; }
    public List<MonsterData> MonsterDatas { get => monsterDatas; }
    public List<ItemData> ItemDatas { get => itemDatas; }
    public List<SkillData> SkillDatas { get => skillDatas; }
    public List<StageData> StageDatas { get => stageDatas; }

    /// </데이터 리스트>

    private string savePath;

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath;
            BuildDataBase();
        }
    }

    void BuildDataBase()
    {
        //SaveFiles();
        LoadFiles();
    }

    void LoadFiles()
    {
        characterDatas = LoadJsonFile<CharacterData>("Data_Character");
        monsterDatas = LoadJsonFile<MonsterData>("Data_Monster");
        itemDatas = LoadJsonFile<ItemData>("Data_Item");
        skillDatas = LoadJsonFile<SkillData>("Data_Skill");
        stageDatas = LoadJsonFile<StageData>("Data_Stage");
    }

    void SaveFiles()
    {
        Save<CharacterData>(characterDatas, "Data_Character");
        Save<MonsterData>(monsterDatas, "Data_Monster");
        Save<ItemData>(itemDatas, "Data_Item");
        Save<SkillData>(skillDatas, "Data_Skill");
        Save<StageData>(stageDatas, "Data_Stage");
    }

    void Save<T>(List<T> dataList, string fileName)
    {
        string jsonData = ObjectToJson(new Serialization<T>(dataList));
        File.WriteAllText(savePath + "/" + fileName + ".txt", jsonData);
    }

    List<T> Load<T>(string fileName)
    {
        if(!File.Exists(savePath + "/" + fileName + ".txt"))
        {
            Debug.LogError(savePath + "에 있는 " + fileName + "을 찾지 못하였습니다.");
            return null;
        }

        string jsonData = File.ReadAllText(savePath + "/" + fileName + ".txt");
        return JsonToObject<Serialization<T>>(jsonData).target;
    }

    //List<T> LoadJsonFile<T>(string fileName)
    //{
    //    return JsonConvert.DeserializeObject<List<T>>(Resources.Load<TextAsset>("Data/" + fileName).ToString());
    //}

    List<T> LoadJsonFile<T>(string fileName)
    {
        FileStream fs = new FileStream(string.Format("{0}/{1}.json", savePath, fileName), FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<List<T>>(jsonData);
    }

    public CharacterData GetCharacterByName(string name)
    {
        foreach (CharacterData character in characterDatas)
        {
            if (character.Name == name)
            {
                return character;
            }
        }
        Debug.LogWarning("Couldn't find character : " + name);
        return null;
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
        Debug.LogWarning("Couldn't find monster : " + name);
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

    public SkillData GetSkillDataByName(string name)
    {
        foreach (SkillData skill in skillDatas)
        {
            if (skill.Name == name)
            {
                return skill;
            }
        }
        Debug.LogWarning("Couldn't find skill : " + name);
        return null;
    }

    public StageData GetStageData(int round)
    {
        foreach (StageData stage in stageDatas)
        {
            if (stage.Round == round)
            {
                return stage;
            }
        }
        Debug.LogWarning("Couldn't find stage : " + round);
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
}
