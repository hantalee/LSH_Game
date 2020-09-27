using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum ClassType
{
    Attacker, Defender, Supporter
}

[System.Serializable]
public class CharacterData
{
    public string Id { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ClassType ClassType { get; set; }
    public CharacterStat Stat { get; set; }
    public int Price { get; set; }
    public float DropChance { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public CharacterData(string _id, string _name, ClassType _classType, CharacterStat _stat, int _price, float _dropChance)
    {
        this.Id = _id;
        this.Name = _name;
        this.ClassType = _classType;
        this.Stat = _stat;
        this.Price = _price;
        this.DropChance = _dropChance;
    }
}
