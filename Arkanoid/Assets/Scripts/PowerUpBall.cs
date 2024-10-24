using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBall : MonoBehaviour
{
    public float fallSpeed = 1f; // Velocidad de caída
    public float lifeTime = 8f; // Tiempo de vida de la pelota

    private void Start()
    {
        // Destruir la pelota después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Hacer que la pelota caiga
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Power-up touched Player");
            // Aquí llamas al método en el Player que crea una segunda pelota
            Player.Instance.CreateExtraBall(); // Asegúrate de que este método exista en tu Player

            Destroy(gameObject); // Destruir la pelota después de que el jugador la toca
        }
    }
}
