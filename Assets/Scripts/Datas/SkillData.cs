using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[System.Serializable]
public class SkillData
{
    public enum SkillType
    {
        Passive, Active
    }

    public string Id { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public SkillType Type { get; set; }
    public float CoolDown { get; set; }
    public string Description { get; set; }
    public string IconName { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public SkillData(string _id, string _name, SkillType _type, float _cool, string _desc, string _iconName)
    {
        this.Id = _id;
        this.Name = _name;
        this.Type = _type;
        this.CoolDown = _cool;
        this.Description = _desc;
        this.IconName = _iconName;
    }
}
