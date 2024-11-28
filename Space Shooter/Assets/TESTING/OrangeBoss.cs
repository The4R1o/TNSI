using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBoss : MonoBehaviour
{
    [Header("Move Area")]
    [SerializeField] private Vector2 startPos;
    [SerializeField] float sizeX;
    [SerializeField] float sizeY;

    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [Header("Shooting")]
    [SerializeField] float rateOfFire;
    [SerializeField] int numShots;
    [SerializeField] float  spreadAngle;
    [SerializeField] int laserSpeed;
    [SerializeField] Transform radialStrikePos;
    [SerializeField] Sprite laserSprite;
    [SerializeField] int laserDamage;

    private Vector3 targetPosition;

    private bool isShooting;
    private bool isPosReached = true;

    private Health health;
    private void Awake()
    {
        
    }
    void Start()
    {
        health = GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(!isShooting)
        {
            StartCoroutine(RadialStrike());
        }
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(health.currentHealth < health.GetMaxHealth() * 0.5)
        {
            float newRateOfFire = 2f;
            rateOfFire = newRateOfFire;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void Move()
    {
        if (isPosReached)
            targetPosition = GetRandomPosition();
        else
            transform.position = Vector3.LerpUnclamped(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
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

    private IEnumerator RadialStrike()
    {
        isShooting = true;

        float substractOffset = (numShots - 1) * 0.5f;
        for (var i = 0; i < numShots; i++)
        {
            var offsetAngle = new Vector3(0f, 0f, (i - substractOffset) * spreadAngle);
            //AudioManager.instance.PlaySound("Player Shoot");
            GameObject bullet = ObjectPooler.instance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = radialStrikePos.transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
                bullet.transform.Rotate(offsetAngle);
                bullet.GetComponent<EnemyBullet>().SetEnemyBullet(laserDamage, laserSpeed, laserSprite);
                bullet.transform.localScale = new Vector3(1,-1, 1);
                bullet.GetComponent<EnemyBullet>().direction = -bullet.transform.up;
            }
        }
        yield return new WaitForSeconds(1 / rateOfFire);
        isShooting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPos, new Vector3(sizeX, sizeY, 2f));
    }
}
