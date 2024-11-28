using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBoss : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private float scoreValue;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 0.75f;
    [SerializeField] private int collisionDamage;

    private float timeBetweenShoots;

    [Header("Reference")]
    [SerializeField] private Transform[] shootPositions;
    [SerializeField] private GameObject enemyShild;
    private MinionSpawner minionSpawner;
    Health health;
    private Vector3 targetPosition;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 startPos;
    [SerializeField] float sizeX;
    [SerializeField] float sizeY;

    bool isPosReached = true;
    bool canShoot = true;
    bool isStageCleared = false;
    bool wasInShild = false;

    private void Awake()
    {
        minionSpawner = GetComponentInChildren<MinionSpawner>();
        health = GetComponent<Health>();
        minionSpawner.OnEnemiesCleared += StageCleared;
        health.OnDead += BossDead;
        minionSpawner.gameObject.SetActive(false);
    }
    private void Start()
    {
        ChangePhase(Phase.Shooting);
        timeBetweenShoots = 1 / fireRate; 
    }
    private void Update()
    {
        CheckHealth();
        CountDownAndShoot();
    }

    private void BossDead()
    {
        ScoreManager.instance.IncrementScore(scoreValue);
        AudioManager.instance.PlaySound("Death");
        BossSpawner.instance.ResetRageValue();
        BossSpawner.instance.isSpawned = false;
        GameManager.instance.UpdateGameState(GameState.Gameplay);
        Destroy(gameObject);
        health.OnDead -= BossDead;

    }
    private Vector3 GetPosition()
    {
        var x = UnityEngine.Random.Range(-sizeX / 2, sizeX / 2);
        var y = UnityEngine.Random.Range(-sizeY / 2, sizeY / 2);
        Vector3 position = new Vector3(startPos.x + x, startPos.y + y, 1f);
        return position;
    }
    private void CountDownAndShoot()
    {
        if (timeBetweenShoots <= 0 && canShoot)
        {
            Shoot();
            timeBetweenShoots = 1 / fireRate;
        }
        else
        {
            timeBetweenShoots -= Time.deltaTime;
        }
    }
    private void Shoot()
    {
        foreach(Transform t in shootPositions)
        {
            AudioManager.instance.PlaySound("Purple");
            var randomRotation = UnityEngine.Random.Range(0, 360);
            GameObject projectile = ObjectPooler.instance.GetPooledObject("BossBullet");
            if(projectile != null)
            {
                projectile.transform.position = t.position;
                projectile.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
                projectile.gameObject.SetActive(true);
            }         
        }


    }

    private void CheckHealth()
    {
        if ((health.currentHealth < health.GetMaxHealth() * 0.7f) && !wasInShild)
        {
            ChangePhase(Phase.Shild);
        }
        else if (health.currentHealth < health.GetMaxHealth() * 0.2)
        {
            ChangePhase(Phase.Rage);
        }
        else
            ChangePhase(Phase.Shooting);
    }
    private void Move()
    {
        if (isPosReached)
            targetPosition = GetPosition();
        else
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
 

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            isPosReached = true;
        else
            isPosReached = false;
    }
    private void ChangePhase(Phase currenPhase)
    {
       switch(currenPhase)
        {
            case Phase.Shooting:
                HandleShootingtPhase();
                break; 
            case Phase.Shild:
                HandleShildPhase();
                break;
            case Phase.Rage:
                HandleRagePhase();
                break;
        }
    }

    private void HandleShootingtPhase()
    {
        Debug.Log("Shooting Phase");
        Move();
        canShoot = true;  
    }

    private void HandleShildPhase()
    {
        Debug.Log("Shild Phase");
        canShoot = false;
        targetPosition = new Vector3(0, 7, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * 3f * Time.deltaTime);
        enemyShild.SetActive(true);
        minionSpawner.gameObject.SetActive(true);
        if (isStageCleared)
        {
            wasInShild = true;
            enemyShild.SetActive(false);
            ChangePhase(Phase.Shooting);
        }   
    }
    private void StageCleared()
    {
        isStageCleared = true;

    }
    private void HandleRagePhase()
    {
        fireRate =1.5f;
        GetComponent<SpriteRenderer>().color = Color.red;
        Move();
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(collisionDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPos, new Vector3(sizeX, sizeY, 2f));
    }
}

public enum Phase
{
    Shooting,
    Shild,
    Rage
}
