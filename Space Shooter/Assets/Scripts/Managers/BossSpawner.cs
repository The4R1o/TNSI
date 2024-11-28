using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner instance;
    [SerializeField] private Transform bossSpawnPosition;
    [SerializeField] private GameObject boss;

    [SerializeField] Slider rageSlider;

    [HideInInspector]
    public  bool isSpawned = false;
    
    [SerializeField] private int maxRageValue = 0;

    private float currentRageValue = 0;

    private void Awake()
    {
        instance = this;
        rageSlider.maxValue = maxRageValue;
    }

    private void Update()
    {
        rageSlider.value = currentRageValue;

        if (currentRageValue >= maxRageValue)
        {
            SpawnBoss();
        }

    }
    public void SpawnBoss()
    {
        if (isSpawned)
            return;
        else
        {
            GameManager.instance.UpdateGameState(GameState.BossFight);
            isSpawned = true;
            var go =  Instantiate(boss, bossSpawnPosition.position, Quaternion.identity);
        }
    }
    
    public void IncreasRage(float value)
    {
        currentRageValue += value;
        rageSlider.GetComponentInChildren<Animator>().SetTrigger("ValueChanged");
    }

    public void ResetRageValue()
    {
        currentRageValue = 0;
        maxRageValue += 750;
        rageSlider.maxValue = maxRageValue;
    }
}

