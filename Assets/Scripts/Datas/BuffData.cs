using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class BuffData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public float Duration { get; set; }
    public string Description { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public BuffData(string _id, string _name, float _duration, string _desc)
    {
        this.Id = _id;
        this.Name = _name;
        this.Duration = _duration;
        this.Description = _desc;
    }
}
