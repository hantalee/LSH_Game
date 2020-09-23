using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class JSONManager : MonoBehaviour
{
    string filePath = "/Resources/Data";

    private void Start()
    {
        //List<BaseStat> stat = new List<BaseStat>()
        //{
        //    new BaseStat(BaseStat.BaseStatType.AttackPower, 10, "Damage"),
        //    new BaseStat(BaseStat.BaseStatType.AttackSpeed, 1, "AttackSpeed")
        //};
        //Item m = new Item("ITEM-001", "Sword", "IronSword", Item.ItemType.Weapon, "Equip", 100, "~", stat);
        //string jsonData = ObjectToJson(m);
        //CreateJsonFile(Application.dataPath + filePath, "Data_Item", jsonData);

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

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fs = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
