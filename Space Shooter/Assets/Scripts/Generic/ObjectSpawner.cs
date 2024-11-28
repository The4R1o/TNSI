using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [HideInInspector]
    public bool isSpawned = false;

    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float timeBetweenSpawns;

    [Header("Spawn Area")]
    [SerializeField] float sizeX;
    [SerializeField] float sizeY;

    private float timer;

    private void Start()
    {
        timer = timeBetweenSpawns;
    }
    private void Update()
    {
        if (!isSpawned)
        {
            if (timer <= 0)
            {
                Spawn();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            timer = timeBetweenSpawns;
        }
    }

    private Vector3 GetRandomPosition()
    {
        var x = UnityEngine.Random.Range(-sizeX/2, sizeX/2);
        var y = UnityEngine.Random.Range(-sizeY/2, sizeY/2);
        Vector3 position = new Vector3(transform.position.x + x, transform.position.y + y, 1f);
        return position;
    }
    private void Spawn()
    {
        isSpawned = true;
        GameObject go = ObjectPooler.instance.GetPooledObject("Heal");
        if (go != null)
        {
            go.transform.position = GetRandomPosition();
            go.transform.rotation = Quaternion.identity;
            go.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, sizeY, 2f));
    }
}
