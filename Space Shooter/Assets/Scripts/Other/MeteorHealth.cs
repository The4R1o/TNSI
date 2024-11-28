using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : Health
{
    public void SetMeteorMaxHealth(float health)
    {
        maxHealth = Mathf.RoundToInt(health);
    }
}
