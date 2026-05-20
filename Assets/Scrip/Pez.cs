using UnityEngine;

public class Pez : MonoBehaviour
{
    [Header("Movimiento")]
    public float swimSpeed = 2f;
    public float fleeSpeed = 5f;
    public float detectionDistance = 4f;
    [Header("Sprites")]
    public Sprite rescuedSprite;
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private bool rescued = false;

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
        Vector2 movement = Vector2.left * swimSpeed;

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < detectionDistance)
            {
                if (player.position.x < transform.position.x)
                {
                    movement = Vector2.right * fleeSpeed;
                }
                else
                {
                    movement = Vector2.left * fleeSpeed;
                }
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
        }
    }
}