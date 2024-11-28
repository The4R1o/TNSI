using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerHealth : Health
{
    ShipConfig shipConfig;

    protected override void Start()
    {
        shipConfig = GameManager.instance.GetShipConfig();
        maxHealth = (int)shipConfig.health;
        currentHealth = maxHealth;
    }
    public void Heal(int healAmount)
    {
        currentHealth += (maxHealth *  ((float)healAmount/100)); // 10%
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
    public void SetMaxHealth(int _bonusHealth)
    {
        OnMaxHealthChange?.Invoke();
        maxHealth += _bonusHealth;
        currentHealth = maxHealth;
    }    
}
