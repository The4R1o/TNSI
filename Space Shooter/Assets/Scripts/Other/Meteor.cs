using UnityEngine;


public class Meteor : MonoBehaviour
{
    private int collisionDamage;
    [SerializeField] private float minSpeed, maxSpeed;
    [SerializeField] private Rigidbody2D meteorRB;
    [SerializeField] private GameObject meteorBurstVFX;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    private MeteorHealth meteorHealth;

    private float speed;
    private int health;

    private void OnEnable()
    {
        meteorRB = GetComponent<Rigidbody2D>();
        meteorHealth = GetComponent<MeteorHealth>();
        meteorHealth.OnDead += Destroyed;
        RandomizeStats();
    }
    private void OnDisable()
    {
        meteorHealth.OnDead -= Destroyed;
    }
    private void RandomizeStats()
    {
        float randomScale = Random.Range(minSize, maxSize);
        float randomRotation = Random.Range(0, 360);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        transform.eulerAngles = new Vector3(0, 0, randomRotation);
        health = Mathf.RoundToInt(randomScale * 200f);
        meteorHealth.SetMeteorMaxHealth(health);
        collisionDamage = Mathf.RoundToInt(randomScale * 100);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        meteorRB.velocity = Vector2.down * speed;
    }

    void Destroyed()
    {
        AudioManager.instance.PlaySound("Death");
        Instantiate(meteorBurstVFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") )
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(collisionDamage);
            Destroyed();
        }
    }
}
