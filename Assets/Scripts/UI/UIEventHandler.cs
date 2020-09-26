using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void ItemEventHandler(ItemData item);
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void StatsEventHandler();
    public static event StatsEventHandler OnStatsChanged;

    public delegate void UseSkillEventHandler(string skillName);
    public static event UseSkillEventHandler OnUseSkill;

    public static void ItemAddedToInventory(ItemData item)
    {
        OnItemAddedToInventory(item);
    }

    public static void ItemEquipped(ItemData item)
    {
        OnItemEquipped(item);
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged(currentHealth, maxHealth);
    }

    public static void StatsChanged()
    {
        OnStatsChanged();
    }

    public static void UseSkill(string skillName)
    {
        OnUseSkill(skillName);
    }
}
