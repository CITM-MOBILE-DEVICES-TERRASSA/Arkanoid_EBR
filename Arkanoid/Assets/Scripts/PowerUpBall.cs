using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBall : MonoBehaviour
{
    public float fallSpeed = 5f; // Velocidad de ca�da
    public float lifeTime = 5f; // Tiempo de vida de la pelota

    private void Start()
    {
        // Destruir la pelota despu�s de un tiempo
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
            // Aqu� llamas al m�todo en el Player que crea una segunda pelota
            Player.Instance.CreateSecondBall(); // Aseg�rate de que este m�todo exista en tu Player

            Destroy(gameObject); // Destruir la pelota despu�s de que el jugador la toca
        }
    }
}