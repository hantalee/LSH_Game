using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class BaseCharacter : MonoBehaviour
{
    private CharacterData data;
    public CharacterData Data { get => data; set => data = value; }

    public abstract void Init();
}
