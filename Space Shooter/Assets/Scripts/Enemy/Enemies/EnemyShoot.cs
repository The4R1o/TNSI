using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Sprite bulletSprite;

    [Header("Shooting")]
    [SerializeField] protected float minTimeBetweenShoots = 0.2f;
    [SerializeField] protected float maxTimeBetweenShoots = 3f;
    [SerializeField] private int damage;
    [SerializeField] private int speed;

    [Header("Shooting Audio")]
    [SerializeField] string audioName;

    private float shotTimer;

    private DifficultyManager difficultyManager;
    private void Start()
    {
        difficultyManager = FindObjectOfType<DifficultyManager>();
        StartCoroutine(StartShooting());
    }
    private IEnumerator StartShooting()
    {
        while(true)
        {
            yield return StartCoroutine(Shoot());
        }          
    }
    private IEnumerator Shoot()
    {
        AudioManager.instance.PlaySound(audioName);
        GameObject bullet = ObjectPooler.instance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.eulerAngles = new Vector3(0, 0, 180f);
            bullet.GetComponent<EnemyBullet>().SetEnemyBullet(CaluclateDamage(), speed, bulletSprite);
            bullet.SetActive(true);
        }
        shotTimer = Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
        yield return new WaitForSeconds(shotTimer);
    }

    private int CaluclateDamage()
    {
        float totalDamage = damage;
        for (int i = 0; i < difficultyManager.difficultyLevel; i++)
        {
            ///<summary> 
            /// 1. totalDamage = 50; --> 50 + (50 * 5 / 100) = 52.5
            /// 2. totalDamage = 52.5; --> 52.5 + (52.5 * 5/100) = 55.125
            /// 10. totalDamage = 77.5; --> 77.5 + (77.5 * 5/100) = 77.5
            /// </summary>
            totalDamage += (damage * difficultyManager.bounsEnemyDamagePercent / 100f); 
        }
        return Mathf.RoundToInt(totalDamage);
    }
}
