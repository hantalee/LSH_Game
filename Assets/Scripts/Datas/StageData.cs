using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum RoundType
{
    Nomal, Store, Boss
}

[System.Serializable]
public class SpawnData
{
    public string monsterName;
    public int monsterNum;

    public SpawnData(string _monsterName, int _monsterNum)
    {
        this.monsterName = _monsterName;
        this.monsterNum = _monsterNum;
    }
}

[System.Serializable]
public class StageData
{
    public int Round { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public RoundType Type { get; set; }
    public SpawnData[] SpawnData { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public StageData(int _round, RoundType _type, SpawnData[] _spawnData)
    {
        this.Round = _round;
        this.Type = _type;
        this.SpawnData = _spawnData;
    }
}
