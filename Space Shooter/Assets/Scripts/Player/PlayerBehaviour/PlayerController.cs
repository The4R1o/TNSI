using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ShipConfig shipConfig;

    [Header("Movement")]
    private Vector3 moveDir;
    public float moveSpeed = 10;

    [Header("Components")]
    private Health health;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    // Screen Bounds //
    [SerializeField] float offsetX = 6f;
    [SerializeField] float offsetY = 1f;
    float minX, maxX;
    float minY, maxY;
    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        shipConfig = GameManager.instance.GetShipConfig();
        moveSpeed = shipConfig.speed;
        GetCameraBounds();
        health.OnDead += PlayerDied;
        spriteRenderer.sprite = GameManager.instance.GetShipSprite();
    }
    private void Update() 
    {
        UserInput();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rigidbody2D.velocity = moveDir * moveSpeed;
        rigidbody2D.position = new Vector2(
                Mathf.Clamp(rigidbody2D.position.x, minX, maxX),
                Mathf.Clamp(rigidbody2D.position.y, minY, maxY)
                );
    }
    private void UserInput()
    {
        moveDir = new Vector2(
                    Input.GetAxisRaw("Horizontal"), 
                    Input.GetAxisRaw("Vertical")
                    ).normalized;
    }
    private void PlayerDied()
    {
        AudioManager.instance.PlaySound("Death");
        GameManager.instance.DPS = GetComponent<PlayerStats>().DPS();
        GameManager.instance.UpdateGameState(GameState.GameOver);     
        Destroy(gameObject);
        //Death VFX
        health.OnDead -= PlayerDied;
    }
    private void GetCameraBounds()
        {
            var camera = Camera.main;
            minX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + offsetX;
            maxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - offsetX;
            minY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + offsetY;
            maxY = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - offsetY;
        }

}


