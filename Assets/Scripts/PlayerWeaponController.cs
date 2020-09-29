using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject prefEquippedWeapon { get; set; }

    private ItemData currWeaponData;
    private BaseWeapon equippedWeapon;  //현재 장착중인 아이템
    private CharacterStat characterStats;

    private void Awake()
    {   
        playerHand = transform.Find("Hand").gameObject;
    }

    private void Start()
    {
        characterStats = GetComponent<Player>().Stat;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            PerformWeaponAttack();
        }
    }

    public void EquipWeapon(ItemData itemData)
    {
        if (prefEquippedWeapon != null)
            UnEquipWeapon();

        GameObject item = Resources.Load<GameObject>("Weapons/" + itemData.Name);
        prefEquippedWeapon = Instantiate(item);
        prefEquippedWeapon.transform.SetParent(playerHand.transform);

        equippedWeapon = prefEquippedWeapon.GetComponent<BaseWeapon>();
        equippedWeapon.Stats = itemData.Stats;
        currWeaponData = itemData;
        characterStats.AddBonusStat(itemData.Stats);
        //UIEventHandler.ItemEquipped(itemData);
        //UIEventHandler.StatsChanged();

    }

    public void UnEquipWeapon()
    {
        InventoryController.Instance.AddItemToInventory(currWeaponData);
        characterStats.RemoveBonusStat(equippedWeapon.Stats);
        Destroy(prefEquippedWeapon.transform.gameObject);
        UIEventHandler.StatsChanged();
    }

    public void PerformWeaponAttack()
    {
        if(equippedWeapon != null)
            equippedWeapon.PerformAttack(CalculateDamage());
    }

    int CalculateDamage()
    {
        int damage = characterStats.GetStat(BaseStat.BaseStatType.AttackPower).GetFinalValue();
        damage += CalculateCrit(damage);
        return damage;
    }

    int CalculateCrit(int damage)
    {
        if(Random.value <= 0.10f)
        {
            int critDamage = (int)(damage * Random.Range(0.5f, 0.75f));
            return critDamage;
        }
        return 0;
    }
}
