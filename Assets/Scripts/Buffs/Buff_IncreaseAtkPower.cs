using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff_IncreaseAtkPower : MonoBehaviour, IBuff
{
    private BuffData data;
    private float percentage;
    private float duration;
    private float currentTime;
    public Image icon;

    void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void Init(string buffName)
    {
        data = DataManager.Instance.GetBuffDataByName(buffName);
        duration = data.Duration;
        currentTime = duration;
    }
    public void Execute()
    {
        StartCoroutine(Activation());
    }
    public IEnumerator Activation()
    {
        while(currentTime > 0)
        {
            currentTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        currentTime = 0;
        DeActivation();
    }

    public void DeActivation()
    {
        Destroy(gameObject);
    }
}
