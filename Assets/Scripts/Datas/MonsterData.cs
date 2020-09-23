using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class MonsterData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public CharacterStat Stat { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public MonsterData(string _id, string _name, CharacterStat _stat)
    {
        this.Id = _id;
        this.Name = _name;
        this.Stat = _stat;
    }
}
