using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [HideInInspector]
    public Vector2 direction = Vector2.down;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        rigidbody2D.velocity = direction * bulletSpeed;
    }

    public void SetEnemyBullet(int _damage, int _bulletSpeed, Sprite _bulletSprite)
    {
        damage = _damage;
        bulletSpeed = _bulletSpeed;
        GetComponent<SpriteRenderer>().sprite = _bulletSprite;
    }
}
