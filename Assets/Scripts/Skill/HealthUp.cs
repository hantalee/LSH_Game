﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : BaseSkill
{
    private Player player;
    private BaseStat Stat;

    public override void Activation()
    {
        gameObject.SetActive(true);
    }

    public override void DeActivation()
    {
        gameObject.SetActive(false);
        player.Stat.RemoveBonusStat(Stat);
    }

    public override void Init()
    {
        Data = DataManager.Instance.GetSkillDataByName("Health Up");
        gameObject.SetActive(false);

        Stat = new BaseStat(BaseStat.BaseStatType.Hp, 50, "HealthUpBuff");
    }

    public override void Use()
    {
        if (!gameObject.activeSelf)
            return;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.Stat.AddBonusStat(Stat);
        player.CurrentHealth += Stat.BaseValue;
        player.MaxHealth = player.Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        UIEventHandler.PlayerRecovery();
    }
}
