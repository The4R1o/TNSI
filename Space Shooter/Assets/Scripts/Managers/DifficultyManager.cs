using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    [SerializeField] private float damagePercentPerLevel;
    [SerializeField] private float healthPercentPerLevel;
    [SerializeField] private int maxEnemiesInWave;
    [SerializeField] private float timeToDiffInc = 20f;

    [SerializeField] TextMeshProUGUI diffLevelText;
    private float gameTimer;
    public float boundsSpawnRate { private set; get; }
    public int bounsEnemyCount { private set; get; }
    public float bounsEnemySpeed { private set; get; }
    public float bounsEnemyDamagePercent { private set; get; }
    public float bounsEnemyHealthPercent { private set; get; }
    public int difficultyLevel { private set; get; }
    private float timeBetweenWaveSpawning;
    private float timeReduction;

    
    int index = 0;
    private DifficultyManager()
    {
        difficultyLevel = 0;
        timeBetweenWaveSpawning = 5f;
        timeReduction = 0.05f;
        boundsSpawnRate = 0;
        bounsEnemyCount = 0;
        bounsEnemySpeed = 0;
        ;

    }
    private void Start()
    {
        gameTimer = timeToDiffInc;
        bounsEnemyDamagePercent = damagePercentPerLevel;
        bounsEnemyHealthPercent = healthPercentPerLevel;
    }
    private void Update()
    {
        diffLevelText.text = difficultyLevel.ToString();
        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
        {
            gameTimer = timeToDiffInc;
            IncDifficulty();
        }
        //CheckDifficulty();
    }
    private void IncDifficulty()
    {
        difficultyLevel++;

        boundsSpawnRate += 0.01f;
        bounsEnemySpeed += 0.15f;
        bounsEnemyCount++;
        if (bounsEnemyCount > maxEnemiesInWave)
            bounsEnemyCount = maxEnemiesInWave;
        
    }
    public float GetTimeBetweenSpawns()
    {
        float time;

        time = timeBetweenWaveSpawning - (timeReduction * index);
        index++;
        return time;
    }    
    private void CheckDifficulty()
    {
        if (difficultyLevel == 10)
        {
            timeToDiffInc = 15f;
        }
        if (difficultyLevel == 20)
        {
            timeToDiffInc = 10f;

        }
        if (difficultyLevel >= 30)
        {
            timeToDiffInc = 5f;
        }
    }
}
