using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBullet : EnemyBullet
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] shootPositions;
    [SerializeField] private float minLifeTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private Rigidbody2D rigidbody2D;

    private float timeBetweenShoot;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timeBetweenShoot = UnityEngine.Random.Range(minLifeTime, maxLifeTime);
    }
    private void Update()
    {
        Rotate();
        if(timeBetweenShoot <= 0)
        {
            Shoot();
            timeBetweenShoot = UnityEngine.Random.Range(minLifeTime, maxLifeTime);
        }
        else
            timeBetweenShoot -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = Vector2.down * bulletSpeed;
    }
    private void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
   
    private void Shoot()
    {
        float angle = -90;
        foreach(Transform t in shootPositions)
        {
            AudioManager.instance.PlaySound("Red");
            angle += 90;
            var go = ObjectPooler.instance.GetPooledObject("BossBulletProjectile");
            if (go != null)
            {
                go.transform.position = t.position;
                go.transform.rotation = t.transform.rotation;
                go.SetActive(true);
                go.GetComponent<EnemyBullet>().direction = t.right;
            }
        }
        gameObject.SetActive(false);
    }
}
