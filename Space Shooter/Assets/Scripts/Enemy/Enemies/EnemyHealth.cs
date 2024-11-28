using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    // Start is called before the first frame update

    private DifficultyManager difficultyManager;
    protected override void Start()
    {
        difficultyManager = FindObjectOfType<DifficultyManager>();
        maxHealth = CalculateMaxHealth();
        currentHealth = maxHealth;
    }

    private int CalculateMaxHealth()
    {
        float totalHealth = maxHealth;
        for (int i = 0; i < difficultyManager.difficultyLevel; i++)
        {
            totalHealth += (totalHealth * difficultyManager.bounsEnemyHealthPercent / 100);
        }
        return Mathf.RoundToInt(totalHealth);
    }
}
