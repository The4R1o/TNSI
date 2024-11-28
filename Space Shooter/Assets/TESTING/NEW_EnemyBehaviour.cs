using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [Header("Move Area")]
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Transform[] retreatPositions;
    [SerializeField] float sizeX;
    [SerializeField] float sizeY;
    [SerializeField] float timeToRetreat;
    private bool isPosReached;
    private bool canMove = true;
    private Vector3 targetPosition;
    private float timer;
    private Vector2 target;
    private NEW_EnemySpawner enemySpawner;
    private void Start()
    {
        targetPosition = GetRandomPosition();
        timer = timeToRetreat;
        target = retreatPositions[Random.Range(0, retreatPositions.Length)].position;
        enemySpawner = FindObjectOfType<NEW_EnemySpawner>();
    }

    private void Retreat()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.001f)
            Destroy(gameObject);
    }

    void Update()
    {
        if(canMove)
            Move();
        if (timer <= 0)
        {
            canMove = false;
            Retreat();
        }
        timer -= Time.deltaTime;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPos, new Vector3(sizeX, sizeY, 2f));
    }
    private void OnDestroy()
    {
        enemySpawner.RemoveEnemy(this.gameObject);
    }
}
