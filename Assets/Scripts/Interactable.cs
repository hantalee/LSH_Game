using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRange = 0.5f;
    public LayerMask interactLayers;
    public Collider2D[] Targets;

    public bool MakeSureIntaractable(string tag)
    {
        foreach (Collider2D target in Targets)
        {
            if (target.gameObject.tag == tag)
            {
                return true;
            }
        }
        return false;
    }

    public virtual void Interaction()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
