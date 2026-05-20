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
        rb.gravityScale = gravedadAgua;
        rb.linearDamping = 1.5f;
    }

    void Update()
    {
        movimiento = Vector2.zero;
        if (Keyboard.current.aKey.isPressed)
            movimiento.x = -1;
        if (Keyboard.current.dKey.isPressed)
            movimiento.x = 1;
        if (Keyboard.current.wKey.isPressed)
            movimiento.y = 1;
        if (Keyboard.current.sKey.isPressed)
            movimiento.y = -1;
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
        rb.linearVelocity = new Vector2(
            movimiento.x * velocidad,
            rb.linearVelocity.y
        );

        if (rb.linearVelocity.y < limiteCaida)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                limiteCaida
            );
        }
    }
}