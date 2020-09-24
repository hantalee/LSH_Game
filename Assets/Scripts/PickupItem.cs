using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData ItemData { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //인벤토리에 아이템 추가
            Debug.Log("Get " + ItemData.Name);
            Destroy(gameObject);
        }
    }
}
