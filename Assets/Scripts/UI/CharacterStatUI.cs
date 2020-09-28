using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatUI : MonoBehaviour
{
    private Player player;

    //Stats
    private List<Text> txt_PlayerStat = new List<Text>();
    [SerializeField] private Text txt_PlayerStatPref;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        //InitializeStats();
    }

    void InitializeStats()
    {
        for (int i=0; i < player.Stat.stats.Count; ++i)
        {
            txt_PlayerStat.Add(Instantiate(txt_PlayerStatPref));
            txt_PlayerStat[i].transform.SetParent(transform);
        }
        UpdateStats();
    }

    void UpdateStats()
    {
        for (int i = 0; i < player.Stat.stats.Count; ++i)
        {
            txt_PlayerStat[i].text = player.Stat.stats[i].StatName + " : " + player.Stat.stats[i].GetFinalValue().ToString();
        }
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {

    }

}
