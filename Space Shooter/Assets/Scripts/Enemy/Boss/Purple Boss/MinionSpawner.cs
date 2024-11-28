using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minnionPF;
    [SerializeField] private int numOfEnemies;
    [HideInInspector]
    public List<GameObject> enemiesAlive = new List<GameObject>();

    public bool canSpawn = true;

    public Action OnEnemiesCleared;
    public void Start()
    {
        if(canSpawn)
        {
            for (int i = 0; i < numOfEnemies; i++)
            {
                StartCoroutine(SpawnMinion());
            }
        }
        canSpawn = false;
    }

    private IEnumerator SpawnMinion()
    {
        var go = Instantiate(minnionPF, transform.position + 
                             new Vector3(UnityEngine.Random.Range(0,2), UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2)), 
                             Quaternion.identity);
        enemiesAlive.Add(go);
        yield return new WaitForSeconds(0.2f);
    }

    private void Update()
    {
        if (enemiesAlive.Count == 0)
        {
            OnEnemiesCleared?.Invoke();
        }
    }
}

