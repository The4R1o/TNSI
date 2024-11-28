using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health health;

    [Header("Score")]
    [SerializeField] private float scoreValue;
    [Header("RageValue")]
    [SerializeField] private float  rageValue;
    [Header("VFX")]
    [SerializeField] private GameObject deathVFX;
    void Start()
    {
        health = GetComponent<Health>();
        health.OnDead += EnemyDied;
    }
    private void EnemyDied()
    {
        BossSpawner.instance.IncreasRage(rageValue);
        AudioManager.instance.PlaySound("Death");
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        ScoreManager.instance.IncrementScore(scoreValue);
        ScoreManager.instance.IncrementEnemyKillCount();
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        health.OnDead -= EnemyDied;
    }
}


