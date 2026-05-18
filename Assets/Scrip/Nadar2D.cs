using UnityEngine;
using UnityEngine.InputSystem;

public class Nadar2D : MonoBehaviour
{
    public float velocidad = 2.5f;
    public float impulsoNado = 4f;
    public float gravedadAgua = 1.5f;
    public float limiteCaida = -3f;

    private Rigidbody2D rb;
    private Vector2 movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // gravedad suave como agua
        rb.gravityScale = gravedadAgua;

        // hace que se sienta más flotante
        rb.linearDamping = 1.5f;
    }

    void Update()
    {
        movimiento = Vector2.zero;

        // Movimiento horizontal
        if (Keyboard.current.aKey.isPressed)
            movimiento.x = -1;

        if (Keyboard.current.dKey.isPressed)
            movimiento.x = 1;

        // Movimiento vertical leve
        if (Keyboard.current.wKey.isPressed)
            movimiento.y = 1;

        if (Keyboard.current.sKey.isPressed)
            movimiento.y = -1;

        // Impulso de nado tipo Mario
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                impulsoNado
            );
        }
    }

    void FixedUpdate()
    {
        // Movimiento horizontal suave
        rb.linearVelocity = new Vector2(
            movimiento.x * velocidad,
            rb.linearVelocity.y
        );

        // Limita la velocidad de caída
        if (rb.linearVelocity.y < limiteCaida)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                limiteCaida
            );
        }
    }
}