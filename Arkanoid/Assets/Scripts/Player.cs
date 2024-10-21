using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float bounds = 6.5f;
    private bool isAutoPlaying = false;
    private Ball ball;
    private float moveInput;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isAutoPlaying = !isAutoPlaying;                                                                                 // Cambiar el estado de isAutoPlaying
        }

        PlayerMove();
    }

    private void PlayerMove()
    {
        if (isAutoPlaying && ball != null)
        {
            // Calcular la dirección hacia la bola
            moveInput = ball.transform.position.x > transform.position.x ? 1 : -1;
        }
        else
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x + (moveInput * moveSpeed * Time.deltaTime), -bounds, bounds);
        transform.position = playerPosition;
    }
}
