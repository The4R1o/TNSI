using UnityEngine;
using System;
using TMPro;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] protected int maxHealth;
    public float currentHealth { get; protected set; }

    public Action OnDead;
    public Action OnMaxHealthChange;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            OnDead?.Invoke();
    }
}
