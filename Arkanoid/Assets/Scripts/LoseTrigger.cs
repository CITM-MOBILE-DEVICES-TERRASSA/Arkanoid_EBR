using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public Ball ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si es la pelota principal (Ball)
        if (collision.CompareTag("Ball"))
        {
            // Verificar si hay corazones restantes
            if (HeartManager.Instance.heartsLeft > 1)
            {
                HeartManager.Instance.LoseHeart();
                ball.ResetBall();  // Resetea la pelota principal
            }
            else if (HeartManager.Instance.heartsLeft <= 1)
            {
                GameManager.Instance.GameOver();  // Llamar al game over si no quedan corazones
            }
        }
        // Si es un PowerUpBall
        else if (collision.CompareTag("PowerUpBall"))
        {
            Destroy(collision.gameObject);  // Destruir el PowerUpBall
        }
        // Si es un ExtraBall
        else if (collision.CompareTag("ExtraBall"))
        {
            Destroy(collision.gameObject);  // Destruir el ExtraBall
        }
    }
}
