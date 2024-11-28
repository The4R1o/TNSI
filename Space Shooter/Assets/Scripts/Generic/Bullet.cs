using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float bulletSpeed;

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        IDamagable damagable = other.collider.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

}
