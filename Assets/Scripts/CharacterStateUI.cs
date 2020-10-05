using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateUI : MonoBehaviour
{
    private Player player;
    public Slider HealthSlider;
    public Slider ManaSlider;

    private void Start()
    {
        UIEventHandler.OnPlayerRecovery += CheckPlayerHealthChange;
        UIEventHandler.OnPlayerTakeDamage += CheckPlayerHealthChange;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        HealthSlider.value = 100;
    }

    void CheckPlayerHealthChange()
    {
        int maxHp = player.MaxHealth;
        int currHp = player.CurrentHealth;
        int maxMp = player.MaxMana;
        int currMp = player.CurrentMana;

        HealthSlider.value = (((float)currHp / (float)maxHp) * 100.0f);
        ManaSlider.value = (((float)currMp / (float)maxMp) * 100.0f);
    }
}
