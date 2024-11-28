using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private DifficultyManager difficultyManager;

    [SerializeField] private List<WaveConfig> allWaveTypes;

    [SerializeField] private TextMeshProUGUI waveTXT;

    [HideInInspector]
    public bool isSpawning = true;

    private WaveConfig waveConfigs;

    private int wavesSpawned = 0;
    private float timeBetweenWaves;


    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }
    private void Start()
    {
        StartSpawning();
    }

    private void Update()
    {
        waveTXT.text = wavesSpawned.ToString();
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
       int randomWave;
       int lastWaveIndex = -1; 

       while (isSpawning)
        {
            timeBetweenWaves = difficultyManager.GetTimeBetweenSpawns();
            yield return new WaitForSeconds(timeBetweenWaves);
            do
            {
                 randomWave = Random.Range(0, allWaveTypes.Count);
            }
            while (randomWave == lastWaveIndex);
            lastWaveIndex = randomWave;
            waveConfigs = allWaveTypes[randomWave];
            Debug.Log("Wave index: " + randomWave);
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {

        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies() + difficultyManager.bounsEnemyCount; enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.Euler(0, 0, 0));
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            var waitTime = waveConfig.GetTimeBetweenSpawns() - difficultyManager.boundsSpawnRate;
            if (waitTime < 0.01) waitTime = 0.01f;

            yield return new WaitForSeconds(waitTime);
        }
        wavesSpawned++;
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
