using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryController), typeof(PlayerSkillController))]
[RequireComponent(typeof(PlayerWeaponController), typeof(PlayerConsumableController))]
public class Player : MonoBehaviour
{
    public BaseCharacter character;
    public CharacterStat Stat;
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentMana { get; set; }
    public int MaxMana { get; set; }

    public GameObject damageText;

    private void Awake()
    {
        character = GetComponent<BaseCharacter>();
        Stat = character.Data.Stat;
        MaxHealth = Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        MaxMana = Stat.GetStat(BaseStat.BaseStatType.Mp).GetFinalValue();
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;

        damageText = Resources.Load<GameObject>("Prefabs/DamageText");
    }
    void Start()
    {
        UIEventHandler.PlayerRecovery();
    }

    public void TakeDamage(int amount)
    {
        GameObject tempText = Instantiate(damageText);
        tempText.transform.position = gameObject.transform.position;
        tempText.GetComponent<DamageText>().damage = amount;
        CurrentHealth -= amount;
        UIEventHandler.PlayerTakeDamage();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("플레이어 사망");
    }
}
