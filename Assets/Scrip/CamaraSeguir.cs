using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 5f;

    void LateUpdate()
    {
        Vector3 objetivo = new Vector3(
            jugador.position.x,
            transform.position.y, 
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            objetivo,
            velocidad * Time.deltaTime
        );
    }
}