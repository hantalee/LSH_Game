using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;
    TextMeshPro text;
    public int damage;
    Color textColor;
    Color defaultColor;
    Color criticalColor;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        textColor = text.color;
        defaultColor = new Color(244,255,42);
        criticalColor = new Color(255,42,42);
        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        textColor.a = Mathf.Lerp(textColor.a, 0, Time.deltaTime * alphaSpeed);
        text.color = textColor;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
