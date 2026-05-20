using UnityEngine;

public class SpikeRock : MonoBehaviour
{
    [Header("Salto")]
    public float jumpForce = 8f;

    public float detectionDistance = 3f;

    [Header("Sprites")]
    public Sprite rescuedSprite;

    private Rigidbody2D rb;

    private Transform player;

    private SpriteRenderer spriteRenderer;

    private bool jumped = false;

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

    void Update()
    {
        if (rescued)
            return;

        if (jumped)
            return;

        if (player != null)
        {
            float distance =
                Vector2.Distance(transform.position, player.position);

            if (distance < detectionDistance)
            {
                jumped = true;

                rb.linearVelocity = new Vector2(3f, jumpForce);
            }
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

            Debug.Log("Piedra espina rescatada");
        }
    }
}