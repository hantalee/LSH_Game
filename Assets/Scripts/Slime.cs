using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IMonster
{
    private MonsterData monster;
    public string Id { get; set; }
    public string Name { get; set; }
    public CharacterStat Stat { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    public GameObject damageText;
    public PickupItem prefPickupItem;
    public DropTable DropTable { get; set; }

    void Start()
    {
        monster = DataManager.Instance.GetMonsterByName("Slime");
        Id = monster.Id;
        Name = monster.Name;
        Stat = monster.Stat;
        MaxHealth = monster.Stat.GetStat(BaseStat.BaseStatType.Hp).GetFinalValue();
        CurrentHealth = MaxHealth;

        DropTable = new DropTable();
        DropTable.loots = new List<Loot>()
        {
            new Loot("IronSword", 50),
            new Loot("HealthPotion", 50)
        };
    }

    public void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }
    public void TakeDamage(int amount)
    {
        GameObject tempText = Instantiate(damageText);
        tempText.transform.position = gameObject.transform.position;
        tempText.GetComponent<DamageText>().damage = amount;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void PerformAttack()
    {

    }

    void DropLoot()
    {
        ItemData itemData = DropTable.GetDrop();
        if(itemData != null)
        {
            PickupItem item = Instantiate(prefPickupItem, transform.position, Quaternion.identity);
            item.ItemData = itemData;
        }
    }
}
