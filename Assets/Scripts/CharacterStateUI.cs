using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateUI : MonoBehaviour
{
    private Player player;
    public Slider HealthSlider;
    public Slider ManaSlider;

    private void Awake()
    {

    }

    private void Start()
    {
        UIEventHandler.OnPlayerRecovery += CheckPlayerHealthChange;
        UIEventHandler.OnPlayerTakeDamage += CheckPlayerHealthChange;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        HealthSlider.value = 100;
    }

    void CheckPlayerHealthChange()
    {
        int max = player.MaxHealth;
        int curr = player.CurrentHealth;

        HealthSlider.value = (((float)curr / (float)max) * 100.0f);
    }
}
