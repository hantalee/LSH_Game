using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : BaseSkill
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
        Data = DataManager.Instance.GetSkillDataByName("Power Up");
        gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Stat = new BaseStat(BaseStat.BaseStatType.AttackPower, 10, "PowerUpBuff");
    }

    public override void Use()
    {
        if (!gameObject.activeSelf)
            return;

        player.Stat.AddBonusStat(Stat);
    }
}