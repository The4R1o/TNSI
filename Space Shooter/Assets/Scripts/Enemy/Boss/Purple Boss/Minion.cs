using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Move Area")]
    [SerializeField] private Vector2 startPos;
    [SerializeField] float sizeX;
    [SerializeField] float sizeY;

    [Header("VFX")]
    [SerializeField] private GameObject deathVFX;



    private Health health;
    private MinionSpawner minionSpawner;

    private bool isPosReached;
    private Vector3 targetPosition;

    private void Awake()
    {
        health = GetComponent<Health>();
        minionSpawner = FindObjectOfType<MinionSpawner>();
    }
    private void Start()
    {
        health.OnDead += EnemyDied;
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (isPosReached)
            targetPosition = GetRandomPosition();
        else
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            isPosReached = true;
        else
            isPosReached = false;
    }
   
    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(-sizeX / 2, sizeX / 2);
        var y = Random.Range(-sizeY / 2, sizeY / 2);
        Vector3 position = new Vector3(startPos.x + x, startPos.y + y, 1f);
        return position;
    }
    private void EnemyDied()
    {
        AudioManager.instance.PlaySound("Death");
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        minionSpawner.enemiesAlive.RemoveAt(0);
        Destroy(gameObject);
        health.OnDead -= EnemyDied;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPos, new Vector3(sizeX, sizeY, 2f));
    }

}
