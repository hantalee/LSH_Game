using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseConsumeable
{
    public int Amount = 10;
    Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); ;
    }
    void Start()
    {
        data = DataManager.Instance.GetItemByName("HealthPotion");
    }

    public override void Consume()
    {
        base.Consume();

        int max = player.MaxHealth;
        int curr = player.CurrentHealth;

        if (curr + Amount > max)
            player.CurrentHealth = max;
        else
            player.CurrentHealth += Amount;

        UIEventHandler.PlayerRecovery();
    }
}
