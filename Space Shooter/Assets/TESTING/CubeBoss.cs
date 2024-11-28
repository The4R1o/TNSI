using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBoss : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] int damage;
    [SerializeField] int bulletSpeed;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Transform[] shootPoints;

    [Header("Shooting Audio")]
    [SerializeField] string audioName;

    bool canShoot = false;
    bool isShooting = false;

    #region ANIMATION EVENTS
    public void AnimationEvent_StartShooting()
    {
        Debug.Log("I was called, true");
        canShoot = true;
    }
    public void AnimationEvent_StopShooting()
    {
        Debug.Log("I was called, false");
        canShoot = false;
    }
    #endregion

    private void Update()
    {
        Debug.Log(canShoot);
        if(canShoot && !isShooting)
            StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        isShooting = true;
       foreach(Transform t in shootPoints)
        {
            AudioManager.instance.PlaySound(audioName);
            var go = ObjectPooler.instance.GetPooledObject("Bullet");
            if(go != null)
            {            
                go.transform.position = t.transform.position;
                go.GetComponent<EnemyBullet>().SetEnemyBullet(damage, bulletSpeed, bulletSprite);
                go.GetComponent<EnemyBullet>().direction = t.right;
                go.SetActive(true);
            }
        }
        yield return new WaitForSeconds(1 / fireRate);
        isShooting = false;
    }
}
