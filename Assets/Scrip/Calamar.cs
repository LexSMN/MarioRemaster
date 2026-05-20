using UnityEngine;

public class Calamar : MonoBehaviour
{
    [Header("Movimiento")]
    public float swimSpeed = 1.5f;
    public float fleeSpeed = 4f;
    public float detectionDistance = 4f;
    [Header("Sprites")]
    public Sprite rescuedSprite;
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private bool rescued = false;
    private int moveDirection = -1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (rescued)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 movement;

        movement = new Vector2(
            moveDirection * swimSpeed,
            Mathf.Sin(Time.time * 3f)
        );

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < detectionDistance)
            {
                Vector2 fleeDirection =
                    (transform.position - player.position).normalized;

                movement = fleeDirection * fleeSpeed;
            }
        }
        rb.linearVelocity = movement;
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (rescued)
            return;

        if (other.CompareTag("Player"))
        {
            rescued = true;

            rb.linearVelocity = Vector2.zero;
            if (rescuedSprite != null)
            {
                spriteRenderer.sprite = rescuedSprite;
            }
            RescueManager.instance.AddRescue();
            Debug.Log("Pulpo rescatado");
        }
    }
}