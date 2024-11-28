using System.Collections;
using UnityEngine;

public class PlayerShooting : PlayerStats
{
    [Header("Shooting")]
    [SerializeField] private Transform shotPosition;
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private Sprite critSprite;

    bool isShooting;

    private void Update()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
     {
         isShooting = true;
         AudioManager.instance.PlaySound("Player Shoot");
         float substractOffset = (numOfGuns - 1) * 0.5f;
         for (var i = 0; i < numOfGuns; i++)
         {
             var offsetAngle = new Vector3(0f, 0f, (i - substractOffset) * spreadAngle);
             GameObject bullet = ObjectPooler.instance.GetPooledObject("PlayerBullet");
             if (bullet != null)
             {
                 bullet.transform.position = shotPosition.transform.position;
                 bullet.transform.rotation = transform.rotation;
                 bullet.SetActive(true);
                 bullet.transform.Rotate(offsetAngle);
                 bullet.GetComponent<PlayerBullet>().
                 SetUpPlayerBullet(
                     GetDamage(),
                     bulletSpeed,
                     isCrit? critSprite : bulletSprite,
                     isCrit?Color.red : Color.cyan
                 );
             }
         }
         yield return new WaitForSeconds(1 / fireRate);
         isShooting = false;
     }
}
