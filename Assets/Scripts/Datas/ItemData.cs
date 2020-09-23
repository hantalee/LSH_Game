using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[System.Serializable]
public class ItemData
{
    public enum ItemType
    {
        Weapon, Consumable, Quest
    }

    public string Id { get; set; }
    public string Class { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ItemType Type { get; set; }
    public string ActionName { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public List<BaseStat> Stats { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public ItemData(string _id, string _class, string _name, ItemType _type, string _actionName, int _price, string _description, List<BaseStat> _stats)
    {
        this.Id = _id;
        this.Class = _class;
        this.Name = _name;
        this.Type = _type;
        this.ActionName = _actionName;
        this.Price = _price;
        this.Description = _description;
        this.Stats = _stats;
    }

}
