using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class BaseNPC : Interactable
{
    public string[] dialogue;
    public string name;

    private void Update()
    {
        Targets = Physics2D.OverlapCircleAll(transform.position, interactRange, interactLayers);

        if (Input.GetKeyDown(KeyCode.F) && Targets.Length > 0)
        {
            if (MakeSureIntaractable("Player"))
                Interaction();
        }

    }
}
