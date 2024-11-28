using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Wave")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] int numberOfEnemies;
    [SerializeField] float moveSpeed;

    public GameObject GetEnemyPrefab() {
        GameObject enemyPF = GetRandomEnemy();
        return enemyPF; 
    }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
       foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }


    #region RandomEnemy
    private GameObject GetRandomEnemy()
    {
        var enemypf = UnityEngine.Random.Range(0, enemies.Length);
        return enemies[enemypf];
    }
    #endregion
}
