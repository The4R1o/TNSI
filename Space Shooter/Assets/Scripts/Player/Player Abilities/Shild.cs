using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Type type;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.collider.GetComponent<Bullet>();
        if(bullet != null)
            collision.gameObject.SetActive(false);
    }
}
public enum Type
{
    PlayerBullet,
    EnemyBullet,
}

