using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float velocityMultiplier = 1.01f;
    [SerializeField] private Transform player;
    public float Seconds = 2f;
    private Vector2 initialVelocity;

    private Rigidbody2D ballRb;
    private bool isBallMoving;


    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ResetBall();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBallMoving)                           // Comprobar si la bola ya esta en movimiento
        {
            LaunchBall();
        }
    }

    private void LaunchBall()                                                           // Quitar a la bola del parent del player
    {
        transform.parent = null;

        initialVelocity = new Vector2(Random.Range(-4, 4), 6);
        Debug.Log("Initial Velocity: " + initialVelocity);

        ballRb.velocity = initialVelocity;
        isBallMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)                              // Si choca con un bloque lo destruye
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            if(brick != null)
            {
                brick.TakeDamage();
            }

            ballRb.velocity *= velocityMultiplier;
        }

        AudioManager.Instance.PlaySFX(0);                                               // Reproducir efecto de sonido para la colisión de la pelota
        VelocityFix();
    }

    private void VelocityFix()                                                          // Fix para arreglar que la bola se quede rebotando infinitamente
    {
        float velocityDelta = 0.5f;
        float minVelocity = 0.2f;

        if(Mathf.Abs(ballRb.velocity.x) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            ballRb.velocity += new Vector2(velocityDelta, 0f);
        }

        if(Mathf.Abs(ballRb.velocity.y) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            ballRb.velocity += new Vector2(0f, velocityDelta);
        }
    }

    public void ResetBall()
    {
        ballRb.velocity = Vector2.zero;
        isBallMoving = false;

        transform.position = player.position + new Vector3(0f, 0.5f, 0f);

        transform.parent = player;

        StartCoroutine(LaunchBallAfterDelay(Seconds));
    }

    private IEnumerator LaunchBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        LaunchBall();
    }
}