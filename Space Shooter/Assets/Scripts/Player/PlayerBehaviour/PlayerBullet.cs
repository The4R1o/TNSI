using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2D.velocity = transform.up * bulletSpeed;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        IDamagable damagable = other.collider.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
    public void SetUpPlayerBullet(int _damage, int _bulletSpeed,  Sprite _bulletSprite, Color _bulletSpriteColor)
    {
        damage = _damage;
        bulletSpeed = _bulletSpeed;
        spriteRenderer.sprite = _bulletSprite;
        spriteRenderer.color = _bulletSpriteColor;
    }
  
}
