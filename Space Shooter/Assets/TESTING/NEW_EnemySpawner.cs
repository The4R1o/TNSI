using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawmPositions;
    [SerializeField] private int maxEnemiesToSpawm;
    [SerializeField] private float timeBetweenSpawns;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool canSpawn = true;
    private bool isSpawning;

    private DifficultyManager difficultyManager;
    private void Start()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        difficultyManager = FindObjectOfType<DifficultyManager>();
        StartSpawning();   
        Debug.Log(difficultyManager);
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        if (state == GameState.Gameplay) StartSpawning();
        if (state == GameState.BossFight) StopSpawning();
    }
    private IEnumerator BeginSpawning()
    {
        while(isSpawning)
        {
           yield return StartCoroutine(SpawnEnemies());         
        }
    }

    private IEnumerator SpawnEnemies()
    {
        if (canSpawn)
        {
            var go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawmPositions[Random.Range(0, spawmPositions.Length)].position, Quaternion.identity);
            spawnedEnemies.Add(go);

        }
        if (spawnedEnemies.Count > maxEnemiesToSpawm + difficultyManager.bounsEnemyCount)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
    }

    public void  RemoveEnemy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(BeginSpawning());
    }
    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
